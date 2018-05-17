using System;
using System.Deployment.Application;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Zajišťuje věci, které jsou potřeba provést při startu (jako čtení konfiguračního souboru (případně vytvoření), splnění starých událostí, zobrazení nesplněných úkolů a písemek, načtení nadpisů
    /// </summary>
    public partial class Start : Form
    {
        /// <summary>
        /// Načte potřebné věci jako třeba progress bar
        /// </summary>
        public Start()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Provádí jednotlivé akce a zobrazuje postup
        /// </summary>
        private void Start_Load(object sender, EventArgs e)
        {
            Nastaveni.NastavPodrobnostiVyjimek(true); // V tuto chvíli nejsou načteny data z konfiguráku, takže je zapnuto vypisování vyjímek
            if (System.IO.Directory.Exists(Nastaveni.SlozkaNastaveni) == false) // Existuje složka, kde má být uložen konfigurák? Pokud ne, tak se pokusí vytvořit
            {
                try
                {
                    System.IO.Directory.CreateDirectory(Nastaveni.SlozkaNastaveni);
                }
                catch (Exception exc)
                {
                    Vyjimky.VypisVyjimek("Nepodařilo se vytvořit tuto cestu: " + Nastaveni.SlozkaNastaveni, exc);
                    Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici); // Vynucené okamžité ukončení aplikace
                    this.Close();
                    return;
                }
            }
            if (System.IO.File.Exists(Nastaveni.SlozkaNastaveni + Nastaveni.SouborNastaveni) == false) // Existuje konfigurák? Pokud ne, tak ho vytvořit (v té době je ochrana proti zobrazování chyb), pokud ano, tak ho přečíst
            {
                try
                {
                    Nastaveni.PraveVytvorenKonfigurak = true;
                    Nastaveni.VytvorNastaveni();
                }
                catch (Exception exc)
                {
                    Vyjimky.VypisVyjimek("Nepodařilo se vytvořit konfigurační soubor v této složce: \n" + Nastaveni.SlozkaNastaveni, exc);
                    Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici); // Vynucené okamžité ukončení aplikace
                    this.Close();
                    return;
                }
            }
            else
            {
                if (Nastaveni.CtiNastaveni() == false)
                {
                    if (DialogResult.Abort == new Aktualizace().ShowDialog()) // Zda nedošlo k chybě při aktualizaci
                    {
                        Obecne.ZobrazZpravu("Nezdařila se konfigurace aplikace po provedení aktualizace, aplikace nemůže dál pokračovat a bude tudíž ukončena. Opětovné spuštění by mělo problém vyřešit. Pokud ne, tak jsou na vině internetové problémy, které snad brzo budou vyřešeny.", "Problém s konfigurací", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici);
                        this.Close();
                        return;
                    }
                }
            }
            progressBarStart.Value += 1;
            if (Nastaveni.StavAplikace != (int)Nastaveni.StavyAplikace.bezici) // Pokud nastala chyba při čtení konfiguráku
            {
                this.Close();
                return;
            }
            new System.Threading.Thread(Nastaveni.OdesliChybovySoubor).Start(); // Pokusí se odeslat soubor s chybama - má vlastní vlákno, aby nezdržovalo start aplikace 
            progressBarStart.Value += 1;
            Databaze db = new Databaze();
            db.Dotaz("UPDATE udalosti SET Splneno=1 WHERE Splneno=0 AND Typ=" + ((int)Obecne.UdalostiTypy.udalost).ToString() + " AND Kdy <= NOW();"); // Staré události automaticky splnění (když se mělo něco udát, tak se to událo)
            progressBarStart.Value += 1;
            alarm.AlarmKontrola(); // Aktivuje alarm, aby se upozornilo na případné blížící se události
            progressBarStart.Value += 1;
            db.Dotaz("SELECT COUNT(*) as Pocet FROM udalosti WHERE Splneno=0 AND Typ=" + ((int)Obecne.UdalostiTypy.ukol).ToString() + " AND Kdy <= NOW();"); // Získá nesplněné úkoly, který už měly být splněny
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") > 0) // Pokud jsou nesplněné úkoly, které měly být splněny, tak se zobrazé dialog s jejich výpisem
                {
                    StareUdalosti okno = new StareUdalosti((int)Obecne.UdalostiTypy.ukol);
                    okno.ShowDialog();
                }
            }
            progressBarStart.Value += 1;
            db.Dotaz("SELECT COUNT(*) as Pocet FROM udalosti WHERE Splneno=0 AND Typ=" + ((int)Obecne.UdalostiTypy.pisemka).ToString() + " AND Kdy <= NOW();"); // Získá nesplněné písemky, který už měly být splněny
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") > 0) // Pokud jsou nesplněné písemky, které měly být splněny, tak se zobrazé dialog s jejich výpisem
                {
                    StareUdalosti okno = new StareUdalosti((int)Obecne.UdalostiTypy.pisemka);
                    okno.ShowDialog();
                }
            }
            progressBarStart.Value += 1;
            Obecne.PametNadpis = new AutoCompleteStringCollection(); // Vytvoříme novou nápovědní kolekci nadpisů
            db.Dotaz("SELECT DISTINCT Nadpis FROM udalosti;"); // Získáme unikátní nadpisy a ty postupně přidáváme do kolekce
            while (db.DalsiVysledek())
            {
                Obecne.PametNadpis.Add(db.DejVysledekString("Nadpis"));
            }
            progressBarStart.Value += 1;
            db.Close(); // Zavře databázové spojení, uvolní zdroje
            Obecne.PripravNarozeninyPrehled(); // Připravíme narozeniny pro zobrazení (aby seděly přechody roků)
            alarm.Vypnout(); // Vypne upozorňování na blížíce se události
            progressBarStart.Value += 1;
            this.Close(); // Zavře okno
        }

        /// <summary>
        /// 
        /// </summary>
        private void NainstalujAktualizaci()
        {
            UpdateCheckInfo info = null;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();

                }
                catch (DeploymentDownloadException exc)
                {
                    Vyjimky.VypisVyjimek("Nepodařilo se stáhnout novou verzi. Zkontroluj připojení k internetu", exc);
                    return;
                }
                catch (InvalidDeploymentException exc)
                {
                    Vyjimky.VypisVyjimek("Nepodařilo se stáhnout novou verzi.", exc);
                    Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + ": Chybně sestavený soubor (deployment). Detaily: " + exc.Message);
                    return;
                }
                catch (InvalidOperationException exc)
                {
                    Vyjimky.VypisVyjimek("Nepodařilo se aktualizovat novou verzi.", exc);
                    Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + ": Nezdařená aktualizace. Detaily: " + exc.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    MessageBox.Show("Byla nalezena aktualizace a nyní bude provedena její instalace.", "Aktualizace k dispozici", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        ad.Update();
                        MessageBox.Show("Aplikace byla aktualizována a nyní bude restartována.");
                        Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici); // Vynucené okamžité ukončení aplikace
                        Application.Restart();
                        this.Close();
                        return;
                    }
                    catch (DeploymentDownloadException exc)
                    {
                        Vyjimky.VypisVyjimek("Nepodařilo se nainstalovat aktualizaci. Zkontorluj si připojení k internetu nebo zkus později", exc);
                        return;
                    }
                }
            }
        }
    }
}

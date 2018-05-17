using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Formulář, ve kterém se provádí změna nastavení Úkolníku
    /// </summary>
    public partial class NastavovaciOkno : Form
    {
        /// <summary>
        /// Indikuje, zda bylo nastavení spuštěno z důvodu chyby (tedy zda lze uzavřít nastavení bez změny nastavení)
        /// </summary>
        private bool Chyba
        {
            get;
            set;
        }

        /// <summary>
        /// Kolik je problémů v nastavení (upozornění dopředu), aby nebylo povoleno tlačítko uloženo, dkyž ještě všechny problémy nejsou vyřešeny
        /// </summary>
        private int Problemu
        {
            get;
            set;
        }
        
        /// <summary>
        /// Načte komponenty a data z nastavení, taky pokud je nastavení otevřeno z důvody chyby, tak přepíše tlačítko o neuložení na ukončení
        /// </summary>
        /// <param name="chyba">Zda bylo nastavení otevřeno z důvodu chyby</param>
        public NastavovaciOkno(bool chyba = false)
        {
            InitializeComponent();
            textBoxServer.Text = Nastaveni.Server;
            textBoxUzivatel.Text = Nastaveni.Uzivatel;
            textBoxHeslo.Text = Nastaveni.Heslo;
            textBoxDatabaze.Text = Nastaveni.Databaze;
            textBoxMinuty.Text = (Nastaveni.UpozorneniDopredu % 60).ToString(); // Získáme počet minut
            textBoxHodiny.Text = ((Nastaveni.UpozorneniDopredu % 1440) / 60).ToString(); // Získáme počet hodin
            textBoxDny.Text = (Nastaveni.UpozorneniDopredu / 1440).ToString(); // Získáme počet dnů
            if (Nastaveni.Spousteni == true)
                comboBoxSpousteni.SelectedIndex = 0;
            else
                comboBoxSpousteni.SelectedIndex = 1;
            if (Nastaveni.PodrobnostiVyjimek == true)
                comboBoxChyby.SelectedIndex = 0;
            else
                comboBoxChyby.SelectedIndex = 1;
            Chyba = chyba;
            if (Chyba == true)
            {
                buttonCancel.Text = "Ukončit aplikaci";
                labelVarovani.Visible = true;
            }
            else
                buttonCancel.Text = "Neukládat";
            Problemu = 0;
            Nastaveni.PraveVytvorenKonfigurak = false; // Chyba už jedna byla, takže se to může vypnout, aby byly vidět další
        }

        /// <summary>
        /// Kontrola obsahu textboxů s upozorněním dopředu
        /// </summary>
        private void textBox_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox) sender; // Přetypovat pro přístup k Textu
            if (textBox.BackColor == SystemColors.ControlDark) // Pokud bylo zašedlé, tak ho vrátíme do normálu a odečteme problém
            {
                textBox.BackColor = SystemColors.Window;
                Problemu -= 1;
                if (Problemu == 0)
                    buttonUlozit.Enabled = true;
            }
            try // Zkusíme získat číslo z textboxu, pokud bylo záporné nebo se to nezdařilo, tak textbox změní barvu, zvýšíme problémy a zablokujeme tlačítko na uložení
            {
                if (Convert.ToInt32(textBox.Text) < 0)
                {
                    textBox.BackColor = SystemColors.ControlDark;
                    Problemu += 1;
                    buttonUlozit.Enabled = false;
                }
            }
            catch
            {
                textBox.BackColor = SystemColors.ControlDark;
                Problemu += 1;
                buttonUlozit.Enabled = false;
            }
        }

        /// <summary>
        /// Při stisku tlačítka uložit provedeme uložení nastavení
        /// </summary>
        private void buttonUlozit_Click(object sender, EventArgs e)
        {
            if (Problemu == 0) // Pokud je vše OK, tak pokračujeme
            {
                int upozornit = Convert.ToInt32(textBoxMinuty.Text) + Convert.ToInt32(textBoxHodiny.Text) * 60 + Convert.ToInt32(textBoxDny.Text) * 1440; // Spočítáme kolik minut to je to automatické upozornění dopředu
                bool spousteni = true; 
                if (comboBoxSpousteni.SelectedIndex == 1) // Pokud nebylo vybráno automatické spouštění při startu počítače, tak se vypne spouštění, jinak se ponechá zaplé
                    spousteni = false;
                bool vypisy = true;
                if (comboBoxChyby.SelectedIndex == 1) // Pokud nebyly vybrány podrobné popisy vyjimek, tak se nebudou zobrazovat, jinak se ponechá jejich zobrazování
                    vypisy = false;
                Databaze db = new Databaze(textBoxServer.Text, textBoxUzivatel.Text, textBoxHeslo.Text, textBoxDatabaze.Text); // Testovací připojení k databázi s nově nastavenými hodnotami
                db.Dotaz("SHOW TABLES"); // Slouží jako testovací příkaz zda bylo vše dobře zadáno (pokud totiž by bylo prázdné uživatelské jméno a heslo, tak nás to nechá připojit, ale nefungují dotazy)
                if (db.DejVysledku() == -1) // Připojení se nezdařilo
                    return;
                db.Dotaz(Ukolnik.Properties.Resources.create); // Vytvoříme tabulky (pouze pokud už nebyly vytvořeny)
                db.Dotaz("SELECT COUNT(*) AS Pocet FROM svatky;"); // Zkontrolujeme, zda tabulka se svátky není prázdná, pokud je, tak ji naplníme
                while (db.DalsiVysledek())
                {
                    if (db.DejVysledekInt("Pocet") == 0)
                        db.Dotaz(Ukolnik.Properties.Resources.svatky);
                }
                db.Dotaz("SELECT COUNT(*) AS Pocet FROM vyznamne_dny;"); // Zkontrolujeme, zda tabulka s významnými dny není prázdná, pokud je, tak ji naplníme
                while (db.DalsiVysledek())
                {
                    if (db.DejVysledekInt("Pocet") == 0)
                        db.Dotaz(Ukolnik.Properties.Resources.vyznamne_dny);
                }
                Nastaveni.UpravNastaveni(textBoxServer.Text, textBoxUzivatel.Text, textBoxHeslo.Text, textBoxDatabaze.Text, spousteni, upozornit, vypisy); // Když vše dobře proběhlo, tak si nové připojovací údaje uložíme interně
                Nastaveni.UlozNastaveni(); // A taky do konfiguráku se to uloží
                Nastaveni.OnZmenaPripojeni(); // Vyvoláme nucenou změnu připojení u všech již otevřených spojeních s databází
                Chyba = false; // Chyba byla zažehnána
                DialogResult = System.Windows.Forms.DialogResult.Yes; // Signalizace úspěchu
                db.Close(); // Zvařeme spojení s databází
            }
        }

        /// <summary>
        /// Uzavře okno
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Jakmile se nastavovací okno načte, tak se indikuje, že je zorbazeno a tak nemůže být zobrazeno žádné další
        /// </summary>
        private void NastavovaciOkno_Load(object sender, EventArgs e)
        {
            Nastaveni.NastaveniOtevreno = true;
        }

        /// <summary>
        /// Jakmile se nastavovací okno zavírá, tak se indikuje, že už není zobrazeno a tak se může zobrazit další nastavovací okno v případě potřeby, taky se kontroluje, zda nebylo okno zobrazeno z důvodu chyb, pokud ano, tak dojde k ukončení aplikace
        /// </summary>
        private void NastavovaciOkno_FormClosing(object sender, FormClosingEventArgs e)
        {
            Nastaveni.NastaveniOtevreno = false;
            if (this.DialogResult != System.Windows.Forms.DialogResult.Yes && Chyba == true)
                Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici);
        }
    }
}

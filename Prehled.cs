using System;
using System.Threading;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Zobrazuje přehled o posledních splněných událostech, nesplněných událostech, události, které mají být brzo splněny, nejbližší narozeniny, co je za den a kdo má svátek
    /// </summary>
    public partial class Prehled : Form
    {   
        /// <summary>
        /// Provede základní inicializaci všech přehledů a načte data, sledování událostí
        /// </summary>
        /// <param name="start">True pokud je Přehled spouštěn při startu programu, jinak false, případně ignorovat</param>
        public Prehled(bool start = false)
        {
            InitializeComponent();
            Thread splnene = new Thread(NactiSplnene); // Načte splněné události
            splnene.Start();
            Thread nesplnene = new Thread(NactiNesplnene); // Načte nesplněné události
            nesplnene.Start();
            Thread blizke = new Thread(NactiBlizke); // Načte blízké události
            blizke.Start();
            Thread narozeniny = new Thread(NactiNarozeniny); // Načte narozeniny
            narozeniny.Start();
            Thread aktualizujSvatky = new Thread(AktualizujSvatky); // Načte co je za den, kdo má svátek a narozeniny
            aktualizujSvatky.Start();
            Obecne.ZmenaVeSvatcich += AktualizujSvatky; // Sledování zda dojde ke změně ve svátcích (upozornění na určité svátky) nebo narozeniny a případné aktualizaci
            if (start == true) // Pokud byl přehled spuštěn při startu programu, tak zapneme Alarm a zobrazíme tlačítko pokračovat
            {
                buttonPokracovat.Visible = true;
                alarm.Zapnout();
            }
            splnene.Join(); // Počkáme, až se doprovedou jednotlivá vlákna
            nesplnene.Join();
            blizke.Join();
            narozeniny.Join();
            aktualizujSvatky.Join();
        }

        /// <summary>
        /// Provede dotazy pro přehled posledních splněných událostí a následné načtení dat
        /// </summary>
        private void NactiSplnene()
        {
            string dotazNaPolozkySplnene = "SELECT * FROM udalosti WHERE Splneno=1 AND Typ <>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " ORDER BY Kdy DESC";
            string dotazNaPocetSplnene = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE Splneno=1 AND Typ <>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ";";
            strankaSplnene.NactiData(dotazNaPocetSplnene, dotazNaPolozkySplnene, DateTime.Today.Year);
        }

        /// <summary>
        /// Provede dotazy pro přehled nesplněných a proběhlých událostí a následné načtení dat
        /// </summary>
        private void NactiNesplnene()
        {
            string dotazNaPolozkyNesplnene = "SELECT * FROM udalosti WHERE Splneno=0 AND Typ <>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND Kdy <= NOW() ORDER BY Kdy ASC";
            string dotazNaPocetNesplnene = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE Splneno=0 AND Typ <>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND Kdy <= NOW();";
            strankaNesplnene.NactiData(dotazNaPocetNesplnene, dotazNaPolozkyNesplnene, DateTime.Today.Year);
        }

        /// <summary>
        /// Provede dotazy pro přehled blížících se nesplněných událostí a následné načtení dat
        /// </summary>
        private void NactiBlizke()
        {
            string dotazNaPolozkyBlizke = "SELECT * FROM udalosti WHERE Splneno=0 AND Kdy >= NOW() ORDER BY Kdy ASC";
            string dotazNaPocetBlizke = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE Splneno=0 AND Kdy >= NOW();";
            strankaBlizke.NactiData(dotazNaPocetBlizke, dotazNaPolozkyBlizke, DateTime.Today.Year);
        }

        /// <summary>
        /// Provede dotazy na narozeniny a následné načtení dat
        /// </summary>
        private void NactiNarozeniny()
        {
            string dotazNaPolozkyNarozeniny = "SELECT * FROM udalosti WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " ORDER BY Splneno ASC, Kdy ASC";
            string dotazNaPocetNarozeniny = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ";";
            strankaNarozeniny.NactiData(dotazNaPocetNarozeniny, dotazNaPolozkyNarozeniny, DateTime.Today.Year);
        }

        /// <summary>
        /// Načte svátky a narozeniny na ten den a zobrazí je, nastaví další aktualizaci (třeba začátek dalšího den)
        /// </summary>
        private void AktualizujSvatky()
        {
            timer.Stop();
            Obecne.Svatky(DateTime.Today.ToString("d-M"), labelSvatky, true, true); // Pomocná funkce, která získá požadovaný věci a uloží je do labelu v textovém provedení
            int dalsi = (23 - DateTime.Now.Hour) * 3600000 + (59 - DateTime.Now.Minute) * 60000 + (59 - DateTime.Now.Second) * 1000 + 30000;
            if (dalsi <= 0) // Předcházení několikanásobnému aktualizování během jedné sekundy
                dalsi = 30000;
            timer.Interval = dalsi;
            timer.Start();
        }

        /// <summary>
        /// Aktualizuje svátky a samotný timer
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            AktualizujSvatky();
            Obecne.PripravNarozeninyPrehled();
            strankaNarozeniny.NactiData();
        }

        /// <summary>
        /// Deaktivuje timery a uklidí přehledy
        /// </summary>
        private void Prehled_FormClosed(object sender, FormClosedEventArgs e)
        {
            alarm.Vypnout();
            Obecne.ZmenaVeSvatcich -= AktualizujSvatky; // Už není důvod pro sledování změn
            timer.Enabled = false;
            strankaSplnene.Uklid();
            strankaNesplnene.Uklid();
            strankaBlizke.Uklid();
            strankaNarozeniny.Uklid();
        }

        /// <summary>
        /// Způsobí zavření okna
        /// </summary>
        private void buttonPokracovat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

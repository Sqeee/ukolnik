using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Zobrazování starých událostí, které už měly být splněny a nejsou
    /// </summary>
    public partial class StareUdalosti : Form
    {
        /// <summary>
        /// Načte komponenty, nastaví texty, načte staré události a aktivuje alarm pro upozornění na události
        /// </summary>
        /// <param name="typ">Co za typ starých událostí se má zobrazit -> Podporovány jsou úkoly a písemky</param>
        public StareUdalosti(int typ)
        {
            InitializeComponent();
            if (typ == (int)Obecne.UdalostiTypy.ukol) // Když to jsou úkoly
                labelInfo.Text = "Tyto úkoly jsou nesplněné i když už mají být splněné.";
            else if (typ == (int)Obecne.UdalostiTypy.pisemka) // Když to jsou písemky
                labelInfo.Text = "Tyto písemky už měly být napsané, pokud jsou, tak je splň, jinak je uprav.";
            string dotazNaPolozky = "SELECT * FROM udalosti WHERE Splneno=0 AND Typ=" + typ.ToString() + " AND Kdy <= NOW() ORDER BY Kdy"; // Dotaz, kterým se zobrazej dané události
            string dotazNaPocet = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE Splneno=0 AND Typ=" + typ.ToString() + " AND Kdy <= NOW();"; // Dotaz, který várít počet daných událostí
            stranka.NactiData(dotazNaPocet, dotazNaPolozky, DateTime.Today.Year); // Načte data a zobrazí je
            alarm.Zapnout(); // Zapne se alarm pro upozornění na blížící se události
        }

        /// <summary>
        /// Pokud je kliknuto na Dík za informaci, tak se okno uzavře
        /// </summary>
        private void button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Při uzavírání okna se vypne alarm a také se ukliděj stránky na kterých se zobrazovaly staré události
        /// </summary>
        private void StareUdalosti_FormClosing(object sender, FormClosingEventArgs e)
        {
            alarm.Vypnout();
            stranka.Uklid();
        }
    }
}

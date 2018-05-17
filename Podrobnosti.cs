using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Okno, které zobrazuje informace k událostem
    /// </summary>
    public partial class Podrobnosti : Form
    {
        /// <summary>
        /// ID události, na kterou se upozorňuje
        /// </summary>
        private string Udalost;
        
        /// <summary>
        /// Načte komponenty a nastaví základní věci
        /// </summary>
        /// <param name="udalost">ID události, která se má zobrazit</param>
        public Podrobnosti(string udalost)
        {
            InitializeComponent();
            BackColor = SystemColors.Control; // Nastaví se barva pozadí okna stejná jako barva pro jednotlivé prvky
            richTextBox.SelectAll(); // Vybere se všechen text v richTextBoxu
            richTextBox.SelectionColor = SystemColors.WindowText; // A nastaví se mu základní barva textu
            Udalost = udalost;
            this.Text = "O události ID: " + udalost;
            NactiData(); // Načteme data
            Obecne.ZmenaVUdalostech += NactiData; // Při změně v událostech dojde k aktualizaci dat
        }

        /// <summary>
        /// Načte data a zobrazí je
        /// </summary>
        private void NactiData()
        {
            Databaze db = new Databaze();
            db.Dotaz("SELECT * FROM udalosti WHERE ID=" + Udalost + ";"); // Získáme data k události
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Upozorneno") == (int)Obecne.UpozorneniTypy.neupozornit) // Zjistíme, zda se na událost upozorňuje nebo ne a výsledek zobrazíme
                    labelUpozorneni.Text = "Ne";
                else
                    labelUpozorneni.Text = db.DejVysledekDatumCas("Upozorneni").ToString("d.M.yyyy H:mm");
                labelNazev.Text = db.DejVysledekString("Nadpis"); // Získáme název události
                labelTyp.Text = Obecne.DejNazevTypuUdalosti(db.DejVysledekInt("Typ")); // Získáme název typu události
                if (db.DejVysledekInt("Typ") != (int)Obecne.UdalostiTypy.narozeniny) // Zda to nejsou narozeniny
                {
                    labelKdy.Text = db.DejVysledekDatumCas("Kdy").ToString("d.M.yyyy H:mm"); // Získáme kdy se koná událost
                    richTextBox.Text = db.DejVysledekString("Zprava"); // Získáme popis k události
                    if (db.DejVysledekInt("Splneno") == 1) // Získáme, zda je událost splněná
                        labelSplneno.Text = "Ano";
                    else
                        labelSplneno.Text = "Ne";
                }
                else // Pokud to jsou narozeniny
                {
                    labelPopisInfo.Visible = false; // Popisek nepotřebujeme
                    richTextBox.Visible = false;
                    DateTime datum = db.DejVysledekDatumCas("Kdy"); // Nastavíme kdy (den, měsíc a rok) se koná událost
                    if (datum.Month > DateTime.Now.Month || (datum.Month == DateTime.Now.Month && datum.Day >= DateTime.Now.Day)) // Pokud nás ty narozeniny čekají ještě letos, tak rok nastavíme na letošek
                        datum = datum.AddYears(DateTime.Now.Year - datum.Year);
                    else // Pokud nás narozeniny čekají příští rok, tak se nastaví příští rok
                        datum = datum.AddYears(1 + DateTime.Now.Year - datum.Year);
                    labelKdy.Text = datum.ToString("d.M.yyyy") + " (" + (datum.Year-db.DejVysledekInt("Zprava")).ToString() + ". narozeniny)"; // Vypíšeme datum nejbližších budoucích narozenin a nový věk
                    labelNazevInfo.Text = "Jméno:"; // Nadpis události bude jménem
                    labelSplnenoInfo.Text = "Datum narození:"; // Splněnost událostí bude datem narození
                    labelSplneno.Text = db.DejVysledekDatumCas("Kdy").ToString("d.M.") + db.DejVysledekString("Zprava"); // Získáme datum narození
                }
            }
            if (richTextBox.Text == "") // Pokud nebylo nic napsáno do zprávy, tak bude písmo kurzívou a bude tam info o tom, že tam nic napsáno nebylo
            {
                richTextBox.Text = "Komentář k události nebyl připsán";
                richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
            db.Close();
            int vyska = (richTextBox.GetLineFromCharIndex(richTextBox.Text.Length) + 1) * richTextBox.Font.Height + 1 + richTextBox.Margin.Vertical; // Spočítá se výška, kterou zabírá text
            if (vyska < richTextBox.MaximumSize.Height) // Pokud je ta výška menší než maximální velikost okna, tak se richTextBox zmenší
                richTextBox.Size = new Size(this.Width, vyska);
        }

        /// <summary>
        /// Při zavírání okna přestaneme sledovat změny v událostech
        /// </summary>
        private void Podrobnosti_FormClosing(object sender, FormClosingEventArgs e)
        {
            Obecne.ZmenaVUdalostech -= NactiData;
        }

        /// <summary>
        /// Pokud dojde ke změně velikosti richTextBoxu, tak se změní velikost celého okna
        /// </summary>
        private void richTextBox_Resize(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width, richTextBox.Location.Y + richTextBox.Size.Height + 40);
        }

        /// <summary>
        /// Při kliknutí na odkaz otevře prohlížeč
        /// </summary>
        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }
    }
}

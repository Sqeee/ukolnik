using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Okno, které upozorňuje na události
    /// </summary>
    public partial class Upozorneni : Form
    {
        /// <summary>
        /// ID události, na kterou se upozorňuje
        /// </summary>
        private int Udalost;
        /// <summary>
        /// Typ události, na kterou se upozorňuje
        /// </summary>
        private int Typ;
        /// <summary>
        /// Posunuje rok, v případě, že narozeniny budou až zas příští rok, takže to ukáže správný věk
        /// </summary>
        private int PosunNarozeniny;
        /// <summary>
        /// Přehrávač, který vydává zvuky alarmu
        /// </summary>
        private System.Media.SoundPlayer Prehravac;
        
        /// <summary>
        /// Načte komponenty a nastaví základní věci
        /// </summary>
        /// <param name="udalost">ID události, na kteoru má být upozorněno</param>
        public Upozorneni(int udalost)
        {
            InitializeComponent();
            BackColor = SystemColors.Control; // Nastaví se barva pozadí okna stejná jako barva pro jednotlivé prvky
            richTextBox.SelectAll(); // Vybere se všechen text v richTextBoxu
            richTextBox.SelectionColor = SystemColors.WindowText; // A nastaví se mu základní barva textu
            richTextBox.Select(0, 0); // Zruší se vybrání textu
            Udalost = udalost;
            Databaze db = new Databaze();
            db.Dotaz("SELECT * FROM udalosti WHERE ID=" + Udalost.ToString() + ";"); // Získáme data k události
            while (db.DalsiVysledek())
            {
                Typ = db.DejVysledekInt("Typ");
                switch (Typ) // Podle typu události nastavíme text, kromě narozenin je to základní text o tom, kdy bude jaká událost, u narozenin i kolikáté narozeniny to budou
                {
                    case (int)Obecne.UdalostiTypy.udalost:
                        this.Text = "Událost " + db.DejVysledekString("Nadpis") + " se hlásí o slovo, protože nastane už dne " + db.DejVysledekDatumCas("Kdy").ToString("d.M.yyyy") + " v " + db.DejVysledekDatumCas("Kdy").ToString("H:mm") + "!";
                        richTextBox.Text = db.DejVysledekString("Zprava");
                        break;
                    case (int)Obecne.UdalostiTypy.ukol:
                        this.Text = "Úkol " + db.DejVysledekString("Nadpis") + " se hlásí o slovo, protože má být splněn do " + db.DejVysledekDatumCas("Kdy").ToString("d.M.yyyy") + " " + db.DejVysledekDatumCas("Kdy").ToString("H:mm") + "!";
                        richTextBox.Text = db.DejVysledekString("Zprava");
                    break;
                    case (int)Obecne.UdalostiTypy.pisemka:
                        this.Text = "Písemka " + db.DejVysledekString("Nadpis") + " se hlásí o slovo, protože se píše dne " + db.DejVysledekDatumCas("Kdy").ToString("d.M.yyyy") + " v " + db.DejVysledekDatumCas("Kdy").ToString("H:mm") + "!";
                        richTextBox.Text = db.DejVysledekString("Zprava");
                    break;
                    case (int)Obecne.UdalostiTypy.narozeniny:
                        this.Text = "Nezapomněl jsi na narozeniny človíčka jménem " + db.DejVysledekString("Nadpis") + ", které slaví dne " + db.DejVysledekDatumCas("Kdy").ToString("d.M") + "?";
                        if (db.DejVysledekDatumCas("Kdy").Month < DateTime.Now.Month || (db.DejVysledekDatumCas("Kdy").Month == DateTime.Now.Month && db.DejVysledekDatumCas("Kdy").Day < DateTime.Now.Day)) // Pokud narozeniny už byly v tomto roce, tak přidáme jedničku, protože budou až další rok, jinak přidáme nulu pro letošek
                            PosunNarozeniny = 1;
                        else
                            PosunNarozeniny = 0;
                        richTextBox.Text = "Máš už koupený dárek? Pokud ne, tak ho rychle sežeň a nezapomeň včas popřát oslavenci jménem " + db.DejVysledekString("Nadpis") + ", který slaví své " + (PosunNarozeniny + DateTime.Now.Year - db.DejVysledekInt("Zprava")).ToString() + ". narozeniny."; // Zobrazíme kdo má narozeniny a kolik mu bude s přičtením toho přenosu přes rok
                    break;
                }
            }
            if (richTextBox.Text == "") // Pokud nebylo nic napsáno do zprávy, tak bude písmo kurzívou a bude tam info o tom, že tam nic napsáno nebylo
            {
                richTextBox.Text = "Komentář k události nebyl připsán";
                richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
            if (Typ == (int)Obecne.UdalostiTypy.narozeniny) // Pokud jsou narozeniny upozorňovány, tak nelze dát hotovo, protože narozeniny nemaj vlastnost splněno
                buttonHotovo.Enabled = false;
            db.Close();
            richTextBox.SelectAll(); // Vybere se všechen text v richTextBoxu
            richTextBox.SelectionAlignment = HorizontalAlignment.Center; // A zarovná se na střed
            richTextBox.Select(0, 0); // Zruší se vybraní textu
            int vyska = (richTextBox.GetLineFromCharIndex(richTextBox.Text.Length) + 1) * richTextBox.Font.Height + 1 + richTextBox.Margin.Vertical; // Spočítá se výška, kterou zabírá text
            if (vyska < richTextBox.MaximumSize.Height) // Pokud je ta výška menší než maximální velikost okna, tak se richTextBox zmenší
                richTextBox.Size = new Size(this.Width, vyska);
            Prehravac = new System.Media.SoundPlayer(Ukolnik.Properties.Resources.aoogah); // Nastaví se přehrávač, připraví si alarm na přehrávání
        }

        /// <summary>
        /// Jakmile je upozornění načteno, tak se zapne alarm a pro danou událost se nastaví, že na ni bylo upozorněno, v případě narozenin se upozornění posune o rok
        /// </summary>
        private void Upozorneni_Load(object sender, EventArgs e)
        {
            Prehravac.PlayLooping();
            Databaze db = new Databaze();
            if (Typ != (int)Obecne.UdalostiTypy.narozeniny)
            {
                db.Dotaz("UPDATE udalosti SET Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + " WHERE ID=" + Udalost.ToString() + " AND Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + ";");
                db.Dotaz("UPDATE udalosti SET Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoVlastni).ToString() + " WHERE ID=" + Udalost.ToString() + " AND Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitVlastni).ToString() + ";");
            }
            else
            {
                db.Dotaz("UPDATE udalosti SET Upozorneni=DATE_ADD(Upozorneni, INTERVAL 1 YEAR) WHERE ID=" + Udalost.ToString() + ";");
                comboBoxPosun.Items.Remove("Hodinu");
                comboBoxPosun.Items.Remove("Rok");
            }
            db.Close();
        }

        /// <summary>
        /// Při kliknutí na ticho a nečekaně se tím umlčí alarm
        /// </summary>
        private void buttonTicho_Click(object sender, EventArgs e)
        {
            Prehravac.Stop();
        }

        /// <summary>
        /// Při kliknutí na hotovo, kdy se té události nastaví hotovo, umlčí alarm a zavře upozornění
        /// </summary>
        private void buttonHotovo_Click(object sender, EventArgs e)
        {
            Prehravac.Stop();
            if (Typ != (int)Obecne.UdalostiTypy.narozeniny)
            {
                Databaze db = new Databaze();
                db.Dotaz("UPDATE udalosti SET Splneno=1 WHERE ID=" + Udalost.ToString() + ";");
                db.Close();
            }
            this.Close();
        }

        /// <summary>
        /// Při kliknutí na OK se umlčí alarm a zavře se upozornění
        /// </summary>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            Prehravac.Stop();
            this.Close();
        }

        /// <summary>
        /// Pokud se změní velikost richTextBoxu, tak se pak hejbne s tlačítka a změní velikost okna
        /// </summary>
        private void richTextBox_Resize(object sender, EventArgs e)
        {
            int posun = richTextBox.Location.Y + richTextBox.Size.Height + 6; // Spočítá se y souřadnice pro tlačítka
            buttonOK.Location = new Point(buttonOK.Location.X, posun);
            buttonHotovo.Location = new Point(buttonHotovo.Location.X, posun);
            buttonTicho.Location = new Point(buttonTicho.Location.X, posun);
            labelPosunout.Location = new Point(labelPosunout.Location.X, posun + 5);
            comboBoxPosun.Location = new Point(comboBoxPosun.Location.X, posun + 1);
            buttonPosunout.Location = new Point(buttonPosunout.Location.X, posun);
            this.Size = new Size(this.Size.Width, posun + 70); // Nastaví se velikost okna tak, aby se tam vše vešlo
        }

        /// <summary>
        /// Při zavírání vypne přehrávač
        /// </summary>
        private void Upozorneni_FormClosing(object sender, FormClosingEventArgs e)
        {
            Prehravac.Stop();
        }

        /// <summary>
        /// Při kliknutí na odkaz otevře prohlížeč
        /// </summary>
        private void richTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        /// <summary>
        /// Při kliknutí na posunout provede posunutí události
        /// </summary>
        private void buttonPosunout_Click(object sender, EventArgs e)
        {
            if (comboBoxPosun.SelectedItem == null)
            {
                return;
            }
            Prehravac.Stop();
            string interval = "";
            switch (comboBoxPosun.SelectedItem.ToString())
            {
                case "Hodinu":
                    interval = "HOUR";
                    break;
                case "Den":
                    interval = "DAY";
                    break;
                case "Týden":
                    interval = "WEEK";
                    break;
                case "Měsíc":
                    interval = "MONTH";
                    break;
                case "Rok":
                    interval = "YEAR";
                    break;
            }
            Obecne.PresunUdalost(Udalost.ToString(), Typ, interval);
            this.Close();
        }

        /// <summary>
        /// Při vybrání intervalu posunu zpřístupní tlačítko na posunutí události
        /// </summary>
        private void comboBoxPosun_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPosun.SelectedItem != null)
            {
                buttonPosunout.Enabled = true;
            }
            else
            {
                buttonPosunout.Enabled = false;
            }
        }
    }
}

using System;
using System.Collections;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Zobrazení svátků, významných dnů a přehled komu kolik je podle narozenin
    /// </summary>
    public partial class Svatky : Form
    {
        /// <summary>
        /// Obsahuje ID významných dnů, kde došlo ke změně
        /// </summary>
        private ArrayList zmenyDnu = new ArrayList();
        /// <summary>
        /// Obsahuje ID svátků, kde došlo ke změně
        /// </summary>
        private ArrayList zmenySvatku = new ArrayList();
        
        /// <summary>
        /// Načte komponenty a provede základní nastavení
        /// </summary>
        public Svatky()
        {
            InitializeComponent();
            Databaze db = new Databaze();
            db.Dotaz("SELECT Upozornit, Den, Datum FROM vyznamne_dny ORDER BY Den;"); // Získá významné dny, které postupně vloží do checkedListBoxu i s tím, zda si je přeje zvýrazňovat
            while (db.DalsiVysledek())
                checkedListBoxVyznamneDny.Items.Add(db.DejVysledekString("Den") + " (" + db.DejVysledekString("Datum").Replace('-', '.') + ")", Convert.ToBoolean(db.DejVysledekInt("Upozornit")));
            db.Dotaz("SELECT Upozornit, Jmeno, Datum FROM svatky ORDER BY Jmeno;"); // Získá svátky, které postupně vloží do checkedListBoxu i s tím, zda si je přeje zvýrazňovat
            while (db.DalsiVysledek())
                checkedListBoxSvatky.Items.Add(db.DejVysledekString("Jmeno") + " (" + db.DejVysledekString("Datum").Replace('-', '.') + ")", Convert.ToBoolean(db.DejVysledekInt("Upozornit")));
            db.Close();
            NactiNarozeniny(); // Načteme narozeniny
            checkedListBoxVyznamneDny.ItemCheck += checkedListBoxVyznamneDny_ItemCheck; // Už je třeba sledovat změny checků u významných dnů
            checkedListBoxSvatky.ItemCheck += checkedListBoxSvatky_ItemCheck; // Už je třeba sledovat změny checků u svátků
            Obecne.ZmenaVeSvatcich += NactiNarozeniny; // Je potřeba sledovat změny ve svátcích, abychom aktualizovali seznam narozenin
        }

        /// <summary>
        /// Provede načtení narozenin a zobrazení kolik je komu roků
        /// </summary>
        private void NactiNarozeniny()
        {
            Obecne.PripravNarozeninyPrehled(); // Připraví narozeniny, aby seděli roky
            richTextBoxNarozeniny.Text = ""; // Vymažeme dosavadní seznam narozenin
            Databaze db = new Databaze();
            db.Dotaz("SELECT Kdy, Nadpis, Zprava, Splneno FROM udalosti WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " ORDER BY Nadpis;"); // Získá seznam lidí, kteří maj zadané narozeniny a zobrazí je i kolik jim je
            while (db.DalsiVysledek())
                richTextBoxNarozeniny.Text += db.DejVysledekString("Nadpis") + " " + db.DejVysledekDatumCas("Kdy").ToString("d.M") + "." + db.DejVysledekString("Zprava") + " (" + (DateTime.Today.Year - db.DejVysledekInt("Zprava") + -1 + db.DejVysledekInt("Splneno") + Convert.ToInt32(DateTime.Today.Day == db.DejVysledekDatumCas("Kdy").Day && DateTime.Today.Month == db.DejVysledekDatumCas("Kdy").Month)).ToString() + ")\n";
            db.Close();
            if (richTextBoxNarozeniny.Text != "") // Pokud je zobrazení narozeniny neprázdné, tak odstraníme koncový nový řádek, jinak napíšeme info o nulovém počtu narozenin
                richTextBoxNarozeniny.Text = richTextBoxNarozeniny.Text.Remove(richTextBoxNarozeniny.TextLength - 1);
            else
                richTextBoxNarozeniny.Text = "Zatím nebyly zadány žádné narozeniny";
        }

        /// <summary>
        /// Zjistí zda check, který byl pozměněn je nyní zaškrtlý nebo ne
        /// </summary>
        /// <param name="e">Argument k události změna zaškrnutí</param>
        /// <returns>Vrátí 1 pokud je nový stav zaškrnutý nebo 0 pokud je nový stav nezaškrtnutý</returns>
        private string JeZaskrtnuto(ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// Zjišťuje, zda jsou změny zaškrtlé nebo ne
        /// </summary>
        /// <param name="index">Index prvku, pro který chceme zjistit zaškrtnutí</param>
        /// <param name="pole">Pole ve kterém jsou jendotlivé prvky</param>
        /// <returns>Vrátí 1 pokud je prvek zaškrnutý nebo 0 pokud je prvek nezaškrtnutý</returns>
        private string JeZaskrtnuto(int index, CheckedListBox pole)
        {
            if (pole.CheckedItems.Contains(pole.Items[index]))
                return "1";
            else
                return "0";
        }

        /// <summary>
        /// Získá datum z textové popisku události nebo svátku
        /// </summary>
        /// <param name="datum">Popisek ve kterém je datum v textové podobě</param>
        /// <returns>Vrátí datum získané z popisku události nebo svátku</returns>
        private string ZiskejDatum(string datum)
        {
            datum = datum.Remove(0, datum.Length - 6); // Datum je na konci, takže odstraníme vše kromě posledních 6 písmen (4 datum, 1 tečka, 1 závorka)
            datum = datum.Remove(datum.Length - 1); // Odstraníme koncovou závorku
            if (datum[1] == '(') // Pokud je měsíc i den jednociferné, tak tam máme navíc mezeru se závorkou, tak je odstraníme
                datum = datum.Remove(0, 2);
            else if (datum[0] == '(') // Pokud je měsíc nebo den jednociferné a too druhé dvojciferné, tak musíme odstranit závorku, která tam je navíc
                datum = datum.Remove(0, 1);
            datum = datum.Replace('.', '-'); // Z tečky uděláme pomlčku a máme, co bylo třeba
            return datum;
        }

        /// <summary>
        /// Pokud byla provedena změna upozornění významného dne, tak položka která to způsobila je vložena do změn (případně vyjmuta, pokud je to změna změny) a po ukončení proběhne uložení
        /// </summary>
        private void checkedListBoxVyznamneDny_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (zmenyDnu.Contains(e.Index))
                zmenyDnu.Remove(e.Index);
            else
                zmenyDnu.Add(e.Index);
        }

        /// <summary>
        /// Pokud byla provedena změna upozornění na svátek, tak položka která to způsobila je vložena do změn (případně vyjmuta, pokud je to změna změny) a po ukončení proběhne uložení
        /// </summary>
        private void checkedListBoxSvatky_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (zmenySvatku.Contains(e.Index))
                zmenySvatku.Remove(e.Index);
            else
                zmenySvatku.Add(e.Index);
        }

        /// <summary>
        /// Pokud se nastavení svátků zavírá, tak je třeba zrušit sledování událostí
        /// </summary>
        private void Svatky_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkedListBoxVyznamneDny.ItemCheck -= checkedListBoxVyznamneDny_ItemCheck;
            checkedListBoxSvatky.ItemCheck -= checkedListBoxSvatky_ItemCheck;
            Obecne.ZmenaVeSvatcich -= NactiNarozeniny;
        }

        /// <summary>
        /// Způsobí uložení změn a zavře okno
        /// </summary>
        private void buttonUložit_Click(object sender, EventArgs e)
        {
            Databaze db = new Databaze();
            if (zmenyDnu.Count > 0) // Jsou nějaké změny ve významných dnech, pokud ano, tak je prostupně projdeme a uložíme do databáze
            {
                foreach (int i in zmenyDnu)
                    db.Dotaz("UPDATE vyznamne_dny SET Upozornit=" + JeZaskrtnuto(i, checkedListBoxVyznamneDny) + " WHERE Datum='" + ZiskejDatum(checkedListBoxVyznamneDny.Items[i].ToString()) + "'");
            }
            if (zmenySvatku.Count > 0) // Jsou nějaké změny ve svátcích, pokud ano, tak je prostupně projdeme a uložíme do databáze
            {
                foreach (int i in zmenySvatku)
                    db.Dotaz("UPDATE svatky SET Upozornit=" + JeZaskrtnuto(i, checkedListBoxSvatky) + " WHERE Datum='" + ZiskejDatum(checkedListBoxSvatky.Items[i].ToString()) + "'");
            }
            db.Close();
            if (zmenyDnu.Count > 0 || zmenySvatku.Count > 0) // Pokud došlo k nějaké změně, tak je třeba aktualizovat svátky
                Obecne.OnZmenaVeSvatcich();
            this.Close();
        }

        /// <summary>
        /// Zavře okno bez uložení změn
        /// </summary>
        private void buttonPonechat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

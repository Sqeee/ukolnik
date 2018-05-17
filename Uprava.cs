using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Okno starající se o úpravu událostí
    /// </summary>
    public partial class Uprava : Form
    {
        /// <summary>
        /// ID události, která se upravuje
        /// </summary>
        private int Udalost;
        /// <summary>
        /// Typ události, která se upravuje, přístupné z venku, protože se kontroluje, zda původní nebo nová událost byly narozeniny
        /// </summary>
        public int Typ;
        /// <summary>
        /// Ukládá se původní nadpis, aby mohl být případně odstraněn z kolekce, pokud se změní nadpis
        /// </summary>
        private string Nadpis;
        /// <summary>
        /// Zde se ukládá původní datum události, je to pro změnu vlastního upozornění
        /// </summary>
        private DateTime puvodniDatumUdalosti;
        
        /// <summary>
        /// Načte komponenty, získá ID události, připraví text, načte data, vyplní kalendář tučnými daty a ty pak nechá zobrazit, zapně alarm na upozorňování
        /// </summary>
        /// <param name="udalost">ID události, která s ebude upravovat</param>
        public Uprava(int udalost)
        {
            InitializeComponent();
            Udalost = udalost;
            this.Text = "Úprava události ID: " + Udalost.ToString();
            NactiData();
            Obecne.VyplnKalendar(monthCalendar);
            monthCalendar.UpdateBoldedDates();
            alarm.Zapnout();
        }

        /// <summary>
        /// Načte data pro událost a vyplní jimi jednotlivé prvky
        /// </summary>
        private void NactiData()
        {
            Databaze db = new Databaze();
            db.Dotaz("SELECT * FROM udalosti WHERE ID=" + Udalost.ToString() + ";"); // Vytáhneme info z databáze
            while (db.DalsiVysledek())
            {
                comboBoxTyp.SelectedIndex = db.DejVysledekInt("Typ"); // ComboBox dostane typ události
                Typ = comboBoxTyp.SelectedIndex; // Také ten typ uložíme do sledování typu, abychom pak věděli, zda to byli narozeniny nebo ne
                textBoxNadpis.Text = db.DejVysledekString("Nadpis"); // Nastavíme nadpis
                Nadpis = textBoxNadpis.Text; // Nadpis si zapamatujeme pro případné odstranění z kolekce
                textBoxZprava.Text = db.DejVysledekString("Zprava"); // Popis události nastavíme
                DateTime datum = db.DejVysledekDatumCas("Kdy"); // Nastavíme kdy (den, měsíc a rok) se koná událost
                textBoxCasHodina.Text = datum.Hour.ToString(); // Nastavíme hodinu konání události
                textBoxCasMinuta.Text = datum.ToString("mm"); // Nastavíme minutu konání události
                if (Typ == (int)Obecne.UdalostiTypy.narozeniny) // Pokud to jsou narozeniny, tak musíme speciálně zobrazit datum konání, aby to zobrazilo nejbližší budoucí narozeniny
                {
                    if (datum.Month > DateTime.Now.Month || (datum.Month == DateTime.Now.Month && datum.Day >= DateTime.Now.Day)) // Pokud nás ty narozeniny čekají ještě letos, tak rok nastavíme na letošek
                        datum = datum.AddYears(DateTime.Now.Year - datum.Year);
                    else // Pokud nás narozeniny čekají příští rok, tak se nastaví příští rok
                        datum = datum.AddYears(1 + DateTime.Now.Year - datum.Year);
                }
                DateTime upozorneni; // Zde bude uloženo, kdy má proběhnout upozornění
                if (db.DejVysledekInt("Upozorneno") == (int)Obecne.UpozorneniTypy.neupozornit) // Pokud se neupozorňuje, tak se vybere možnost neupozorňovat a upozornění se nastaví na automatické upozornění (jen aby tam něco bylo)
                {
                    radioButtonNe.Checked = true;
                    upozorneni = datum;
                    upozorneni = upozorneni.AddMinutes(Nastaveni.UpozorneniDopredu * -1);
                }
                else if (db.DejVysledekInt("Upozorneno") == (int)Obecne.UpozorneniTypy.upozornitVlastni || db.DejVysledekInt("Upozorneno") == (int)Obecne.UpozorneniTypy.upozornenoVlastni) // Pokud je nastavené vlastní upozornění, tak se vybere možnost Vlastní a upozornění se nastaví na to, které bylo nastavené u události
                {
                    radioButtonVlastni.Checked = true;
                    upozorneni = db.DejVysledekDatumCas("Upozorneni");
                }
                else // Pokud je upozornění automatické (případně už upozorněné), tak se nastaví jako automatické upozornění, upozornění se odvodí automaticky z doby konání události
                {
                    radioButtonAno.Checked = true;
                    upozorneni = datum;
                    upozorneni = upozorneni.AddMinutes(Nastaveni.UpozorneniDopredu * -1);
                }
                datum = datum.AddHours(datum.Hour * -1); // Vynulujeme hodinu v datu, abychom začínali od nuly (by se to jinak přičítalo k původnímu času, takže když by byl původní 10 hodin, dáme novej 11, tak by výsledek byl 21)
                datum = datum.AddMinutes(datum.Minute * -1); // To samé jako s hodinama, jen to je s minutama
                monthCalendar.SetSelectionRange(datum, datum); // Nastavíme, aby byl vybrán den konání události
                puvodniDatumUdalosti = datum; // Nastavíme původní datum
                textBoxCasHodinaUpozorneni.Text = upozorneni.Hour.ToString(); // Získá se hodina upozornění
                textBoxCasMinutaUpozorneni.Text = upozorneni.ToString("mm"); // Získá se minuta upozornění
                upozorneni = upozorneni.AddHours(upozorneni.Hour * -1); // Upozornění opět vynulujeme, aby nám neovlivňovalo počet hodin, protože se změna budou přičítat, takže je potřeba mít nulu
                upozorneni = upozorneni.AddMinutes(upozorneni.Minute * -1); // Stejné jako pro hodiny, jen to je pro minuty
                dateTimePickerUpozorneni.Value = upozorneni; // Získané upozornění se nastaví do dateTimePickeru
                comboBoxSplneno.SelectedIndex = db.DejVysledekInt("Splneno"); // Nastaví se, zda byla událost splněna nebo ne
            }
            db.Close();
        }

        /// <summary>
        /// Hlídá změny typů upozornění a dle toho zapíná a vypíná nastavovací věci
        /// </summary>
        private void radioButtonUpozorneni_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVlastni.Checked == true) // Pokud je zapnuto vlasntí nastavení, tak se aktivujou prvky, které se využívají pro vlastní upozornění
            {
                labelKdy.Enabled = true;
                dateTimePickerUpozorneni.Enabled = true;
                labelCasUpozorneni.Enabled = true;
                textBoxCasHodinaUpozorneni.Enabled = true;
                labelCasDvojteckaUpozorneni.Enabled = true;
                textBoxCasMinutaUpozorneni.Enabled = true;
            }
            else // Pokud je vybráno automatické upozornění nebo dokonce žádné, tak se deaktivujou prvky, které se využívají pro vlastní upozornění
            {
                labelKdy.Enabled = false;
                dateTimePickerUpozorneni.Enabled = false;
                labelCasUpozorneni.Enabled = false;
                textBoxCasHodinaUpozorneni.Enabled = false;
                labelCasDvojteckaUpozorneni.Enabled = false;
                textBoxCasMinutaUpozorneni.Enabled = false;
            }
        }

        /// <summary>
        /// Pokud bylo kliknuto na tlačítko reset, tak se znovu načtou data
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            NactiData();
        }

        /// <summary>
        /// Pokud bylo kliknuto na tlačítko Smazat událost, tak se provede její smazání a zavření okna
        /// </summary>
        private void buttonSmazat_Click(object sender, EventArgs e)
        {
            alarm.Vypnout(); // Vypne se alarm na upozornění
            Databaze db = new Databaze();
            db.Dotaz("SELECT COUNT(*) AS Pocet FROM udalosti WHERE Nadpis='" + Nadpis + "';"); // Získá se z databáze počet událsotí s daným nadpisem
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") < 2) // Pokud byla jediná, tak se nadpis vymaže z kolekce
                    Obecne.PametNadpis.Remove(Nadpis);
            }
            db.Dotaz("DELETE FROM udalosti WHERE ID=" + Udalost.ToString() + ";"); // Smažeme událost
            db.Close();
            this.Close(); // Zavřeme okno, má nastavený dialog result na Yes, takže dojde k aktualizaci událostí (případně i svátků v případě narozenin)
        }

        /// <summary>
        /// Pokud bylo zvoleno neukládat, tak se jen vypne alarm a zavře okno
        /// </summary>
        private void buttonNeukladat_Click(object sender, EventArgs e)
        {
            alarm.Vypnout();
            this.Close();
        }

        /// <summary>
        /// Pokud bylo kliknuto na uložit, tka je třeba data uložit, uloží se vše, nesledují se změny
        /// </summary>
        private void buttonUlozit_Click(object sender, EventArgs e)
        {
            if (textBoxNadpis.Text == "") // Zda není prázdný nadpis (nebo jméno), protože to není dovolené
            {
                Obecne.ZobrazZpravu("Nadpis (nebo jméno) nemůže být prázdný!", "Pozor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Pokud jsou vybrány narozeniny
            {
                try
                {
                    int rokNarozeni = Convert.ToInt32(textBoxZprava.Text); // Pokusíme se získat rok narození z textu, přinejhorším vyhodíme vyjimku
                    rokNarozeni = Math.Abs(rokNarozeni); // Záporné roky nebereme
                    textBoxZprava.Text = rokNarozeni.ToString(); // Upravený rok narození vložíme zpátky do Zprávy
                }
                catch // Pokud se převod nezdařil, tak se zobrazí info o tom
                {
                    Obecne.ZobrazZpravu("Nebyl zadán platný rok narození!", "Pozor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            alarm.Vypnout(); // Vypne se alarm, protože nyní už to nemá co zastavit to ukládání
            string nadpis = textBoxNadpis.Text;
            nadpis = Obecne.OdstranProblemoveZnaky(nadpis); // Odstraníme znaky z nadpisu, jen ty které by mohly dělat problémy, případně je nahradíme vhodnějšími
            string zprava = textBoxZprava.Text;
            zprava = Obecne.OdstranProblemoveZnaky(zprava); // Odstraníme znaky z textu, jen ty které by mohly dělat problémy, případně je nahradíme vhodnějšími
            int hodiny;
            int minuty;
            if (comboBoxTyp.SelectedIndex != (int) Obecne.UdalostiTypy.narozeniny) // Pokud nejsou narozeniny, tak se pokusíme získat čas konání
            {
                try
                {
                    hodiny = Convert.ToInt32(textBoxCasHodina.Text); // Pokusíme se získat z textu hodinu konání
                    hodiny = hodiny % 24; // Převedeme ji na normální rozmezí
                }
                catch
                {
                    hodiny = 0; // Když se nám převod nezdařil, tak hodin bude nula
                }
                try
                {
                    minuty = Convert.ToInt32(textBoxCasMinuta.Text); // Pokusíme se získat z textu minutu konání
                    minuty = minuty % 60; // Převedeme ji na normální rozmezí
                }
                catch
                {
                    minuty = 0; // Když se nám převod nezdařil, tak minut bude nula
                }
            }
            else // Pro narozeniny bude nula minut a nula hodin
            {
                hodiny = 0;
                minuty = 0;
            }
            if (hodiny < 0) // Hodiny nemohou být záporné, takže budou nejméně nula
                hodiny = 0;
            if (minuty < 0) // Ani minuty nemohou být záporné, takže budou také nejméně nula
                minuty = 0;
            monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddHours(hodiny); // Získané hodiny zaznamenáme do kalendáře
            monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddMinutes(minuty); // Získané minuty zaznamenáme do kalendáře
            DateTime upozorneni;
            if (radioButtonVlastni.Checked == true) // Pokud si chce sám zvolit dobu upozornění
            {
                upozorneni = new DateTime(dateTimePickerUpozorneni.Value.Year, dateTimePickerUpozorneni.Value.Month, dateTimePickerUpozorneni.Value.Day, 0, 0, 0); // Získáme ji z dateTimePickeru
                try
                {
                    hodiny = Convert.ToInt32(textBoxCasHodinaUpozorneni.Text); // Pokusíme se získat z textu hodiny
                    if (hodiny < 0) // Hodiny nesmějí být záporné, nejméně nulové musejí být
                        hodiny = 0;
                    hodiny = hodiny % 24; // Také by mělý být ve správném rozmezí
                    minuty = Convert.ToInt32(textBoxCasMinutaUpozorneni.Text); // Pokusíme se získat z textu minuty
                    if (minuty < 0) // Minuty nesmějí být záporné, nejméně nulové musejí být
                        minuty = 0;
                    minuty = minuty % 60; // Také by měly být ve správném rozmezí
                }
                catch // Když se nějaký převod nepovedl, tak hodiny nastavíme na nula, minuty dle upozornění dopředu (ale záporné, aby se dobře odečítaly od doby konání), upozornění se bude čerpat z doby konání
                {
                    hodiny = 0;
                    minuty = Nastaveni.UpozorneniDopredu * -1;
                    upozorneni = new DateTime(monthCalendar.SelectionStart.Year, monthCalendar.SelectionStart.Month, monthCalendar.SelectionStart.Day, monthCalendar.SelectionStart.Hour, monthCalendar.SelectionStart.Minute, monthCalendar.SelectionStart.Second); // Získáme dobu konání
                }
            }
            else // Pokud chceme automatické upozornění
            {
                upozorneni = new DateTime(monthCalendar.SelectionStart.Year, monthCalendar.SelectionStart.Month, monthCalendar.SelectionStart.Day, monthCalendar.SelectionStart.Hour, monthCalendar.SelectionStart.Minute, monthCalendar.SelectionStart.Second); // Získáme dobu konání
                hodiny = 0; // S hodinami nebudeme hýbat
                minuty = Nastaveni.UpozorneniDopredu * -1; // Minuty budou mít automatickou hodnotu dle Upozornění dopředu a zápornou pro lepší odečítání
            }
            upozorneni = upozorneni.AddHours(hodiny); // K době upozornění přidáme hodiny
            upozorneni = upozorneni.AddMinutes(minuty); // K době upozornění přidáme minuty
            if (upozorneni > monthCalendar.SelectionStart) // Pokud by doba upozornění byla až po události, tak jí nastavíme automaticky před událost
            {
                upozorneni = new DateTime(monthCalendar.SelectionStart.Year, monthCalendar.SelectionStart.Month, monthCalendar.SelectionStart.Day, monthCalendar.SelectionStart.Hour, monthCalendar.SelectionStart.Minute, monthCalendar.SelectionStart.Second);
                upozorneni = upozorneni.AddMinutes(Nastaveni.UpozorneniDopredu * -1);
            }
            string upozornit;
            if (radioButtonVlastni.Checked == true) // Je zvoleno vlastní upozornění?
            {
                if (upozorneni <= DateTime.Now) // Pokud už to upozornění mělo proběhnout, tak se nastaví jako proběhlé, jinak jako vlastní neproběhlé
                    upozornit = ((int)Obecne.UpozorneniTypy.upozornenoVlastni).ToString();
                else
                    upozornit = ((int)Obecne.UpozorneniTypy.upozornitVlastni).ToString();
            }
            else if (radioButtonAno.Checked == true)
            {
                if (upozorneni <= DateTime.Now) // Pokud už to upozornění mělo proběhnout, tak se nastaví jako proběhlé, jinak jako automatické neproběhlé
                    upozornit = ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString();
                else
                    upozornit = ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString();
            }
            else // Je zvoleno neupozorňovat? Tak se nastaví neupozorňování
                upozornit = ((int)Obecne.UpozorneniTypy.neupozornit).ToString();
            if (upozorneni.Year < dateTimePickerUpozorneni.MinDate.Year) // Pokud by byla událost na hranici zobrazení dateTimePickeru, tak automatické upozornění by mohlo způsobit, že by při úpravě hlásil neplatnost data dateTimePicker, takže datum dostaneme do požadovaného rozmezí
                upozorneni = upozorneni.AddYears(dateTimePickerUpozorneni.MinDate.Year - upozorneni.Year);
            Databaze db = new Databaze();
            if (Nadpis != textBoxNadpis.Text) // Pokud se předchozí nadpis neshoduje s novým, tak zjišťujeme, jestli starý nadpis používala i jiný událost, pokud nepoužívala, tak starý nadpis je smazán z kolekce, pak také se zjišťuje, zda nový nadpis byl používán jinou událostí, pokud nebyl, tak se vloží do kolekce 
            {
                db.Dotaz("SELECT COUNT(*) AS Pocet FROM udalosti WHERE Nadpis='" + Nadpis + "';");
                while (db.DalsiVysledek())
                {
                    if (db.DejVysledekInt("Pocet") < 2)
                        Obecne.PametNadpis.Remove(Nadpis);
                }
                db.Dotaz("SELECT COUNT(*) AS Pocet FROM udalosti WHERE Nadpis='" + textBoxNadpis.Text + "';");
                while (db.DalsiVysledek())
                {
                    if (db.DejVysledekInt("Pocet") == 0)
                        Obecne.PametNadpis.Add(textBoxNadpis.Text);
                }
            }
            if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Pokud ukládáme narozeniny, tak se nastaví rok 4
                monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddYears(4 - monthCalendar.SelectionStart.Year);
            db.Dotaz("UPDATE udalosti SET Typ=" + comboBoxTyp.SelectedIndex.ToString() + ", Nadpis='" + nadpis + "', Zprava='" + zprava + "', Kdy='" + monthCalendar.SelectionStart.ToString("yyyy-MM-dd HH:mm:ss") + "', Splneno=" + comboBoxSplneno.SelectedIndex.ToString() + ", Upozorneno=" + upozornit + ", Upozorneni='" + upozorneni.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE ID=" + Udalost.ToString() + ";"); // Všechno to uložíme
            db.Close();
            if (comboBoxTyp.SelectedIndex == ((int) Obecne.UdalostiTypy.narozeniny)) // Pokud je nově upravená událost narozeninama, tak si to uložíme (ukládáme jen pro případ narozenin, protože ta stará mohla být narozeniny, ale byl jí změněn typ na něco jinýho, ale i tak je třeba udělat aktualizaci svátků)
                Typ = (int) Obecne.UdalostiTypy.narozeniny;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes; // Nastavíme Yes, protože došlo ke změně události
            this.Close();
        }

        /// <summary>
        /// Pokud se změnil typ události, tak je třeba změnit aktivované a neaktivované prvky
        /// </summary>
        private void comboBoxTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTyp.SelectedIndex != (int)Obecne.UdalostiTypy.narozeniny) // Zda to nebyli narozeniny
            {
                if (comboBoxTyp.SelectedIndex != (int)Obecne.UdalostiTypy.pisemka) // Pokud to nebyla ani písemka, tak se nastaví upozornění na automatické
                    radioButtonAno.Checked = true;
                else // Pokud to byla písemka, tak se vypne upozornění
                    radioButtonNe.Checked = true;
                labelCas.Enabled = true; // Zobrazíme časové prvky
                labelCasDvojtecka.Enabled = true;
                textBoxCasHodina.Enabled = true;
                textBoxCasMinuta.Enabled = true;
                labelNadpis.Text = "Nadpis:"; // Nadpis bude nadpisem
                labelZprava.Text = "Zpráva:"; // Zpráva bude zprávou
                labelSplneno.Enabled = true; // Bude možnost nastavit splněnost
                comboBoxSplneno.Enabled = true;
            }
            else
            {
                radioButtonAno.Checked = true; // Zapne se automatické upozornění
                labelCas.Enabled = false; // Časové prvky budou vypnuty
                labelCasDvojtecka.Enabled = false;
                textBoxCasHodina.Enabled = false;
                textBoxCasMinuta.Enabled = false;
                labelNadpis.Text = "Jméno:"; // Z nadpisu bude jméno
                labelZprava.Text = "Rok narození (například " + DateTime.Now.Year.ToString() + "):"; // Ze zprávy bude rok narození i s ukázkou
                labelSplneno.Enabled = false; // Splněnost se u narozenin neřeší a je nastavena na ne
                comboBoxSplneno.Enabled = false;
                comboBoxSplneno.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Zmáčknutí enteru při zadávání času funguje jako klinutí na uložit
        /// </summary>
        private void textBoxCas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Enter))
            {
                buttonUlozit.PerformClick();
                e.Handled = true; // Aby nebyl vydán zvuk při enteru
            }
        }

        /// <summary>
        /// Při vlastním upozornění a změně data konání události posune i datum upozornění
        /// </summary>
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (radioButtonVlastni.Checked == true)
            {
                dateTimePickerUpozorneni.Value = dateTimePickerUpozorneni.Value.Subtract(puvodniDatumUdalosti - e.Start);
            }
            puvodniDatumUdalosti = e.Start;
        }
    }
}

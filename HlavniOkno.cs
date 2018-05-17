using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Hlavní okno zobrazující události pro jednotlivé dny, lze se přes něj dostat kamkoliv
    /// </summary>
    public partial class HlavniOkno : Form
    {   
        /// <summary>
        /// Načte komponenty, vytvoří dotazy, načte data pro události a svátky, vyplní kalendář, přiřadí nápovědu novým událostem, nastaví ikonu, skryje nové události, zapne sledování změn v událsotech a svátcích a zapne upozorňování
        /// </summary>
        public HlavniOkno()
        {
            InitializeComponent();
            VytvorDotazy(); // Vytvoříme dotazy a načteme data
            AktualizujSvatky();
            AktualizujKalendarNasilne();
            textBoxNadpis.AutoCompleteCustomSource = Obecne.PametNadpis; // Nastaví se našeptávač
            Obecne.IkonaNastav(notifyIcon.ContextMenuStrip); // Nastaví se ikona, aby se mohla vypínat, když se zobrazej dialogy
            labelZobrazVic_Click(labelZobrazVic, null); // Schováme rozšířené okno
            Obecne.ZmenaVeSvatcich += AktualizujSvatky;
            Obecne.ZmenaVUdalostech += AktualizujKalendarNasilne;
            alarm.Zapnout(); // Zapneme upozorňování na události
        }

        /// <summary>
        /// Provede vytvoření dotazů, kterými se bude dotazovat na události vybraných dnů, po vytvoření dotazů načte události
        /// </summary>
        private void VytvorDotazy()
        {
            string dotazNaPolozky;
            string dotazNaPocet;
            if (checkBoxSplnenost.Checked == false) // Mohou se zobrazovat i nesplněné (není zaškrtnuo pouze splněné)
            {
                dotazNaPolozky = "SELECT * FROM udalosti WHERE (DATE(Kdy)='" + monthCalendar.SelectionStart.ToString("yyyy-MM-dd") + "' AND Typ<>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ") OR DATE(Kdy)='0004-" + monthCalendar.SelectionStart.ToString("MM-dd") + "' ORDER BY Kdy, Nadpis";
                dotazNaPocet = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE (DATE(Kdy)='" + monthCalendar.SelectionStart.ToString("yyyy-MM-dd") + "' AND Typ<>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ") OR DATE(Kdy)='0004-" + monthCalendar.SelectionStart.ToString("MM-dd") + "' ORDER BY Kdy, Nadpis";
            }
            else // Pouze splněné
            {
                dotazNaPolozky = "SELECT * FROM udalosti WHERE (DATE(Kdy)='" + monthCalendar.SelectionStart.ToString("yyyy-MM-dd") + "' AND Typ<>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND Splneno=0) OR DATE(Kdy)='0004-" + monthCalendar.SelectionStart.ToString("MM-dd") + "' ORDER BY Kdy, Nadpis";
                dotazNaPocet = "SELECT COUNT(*) AS Pocet FROM udalosti WHERE (DATE(Kdy)='" + monthCalendar.SelectionStart.ToString("yyyy-MM-dd") + "' AND Typ<>" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND Splneno=0) OR DATE(Kdy)='0004-" + monthCalendar.SelectionStart.ToString("MM-dd") + "' ORDER BY Kdy, Nadpis";
            }
            stranky.NactiData(dotazNaPocet, dotazNaPolozky, monthCalendar.SelectionStart.Year); // Načteme data
        }

        /// <summary>
        /// Načte svátky pro daný den
        /// </summary>
        private void AktualizujSvatky()
        {
            Obecne.Svatky(monthCalendar.SelectionStart.Day.ToString() + "-" + monthCalendar.SelectionStart.Month.ToString(), labelSvatek);
        }

        /// <summary>
        /// Načte data pro kalendář, aby se vedělo co je třeba ztučnit, nevinutí si to okamžitou aktualizaci kalendáře, data budou načtena pro budoucí potřebu (například přechod na jiný měsíc)
        /// </summary>
        private void AktualizujKalendar()
        {
            Obecne.VyplnKalendar(monthCalendar, checkBoxSplnenost.Checked);
        }

        /// <summary>
        /// Načte data pro kalendář, aby se vedělo co je třeba ztučnit, provede okamžité zobrazení načtených dat
        /// </summary>
        private void AktualizujKalendarNasilne()
        {
            Obecne.VyplnKalendar(monthCalendar, checkBoxSplnenost.Checked);
            monthCalendar.UpdateBoldedDates();
        }

        /// <summary>
        /// Obsluha události kliknutí na ikonku Úkolníku, tedy minimalizaci Úkolníku nebo přenos Úkolníku do popředí
        /// </summary>
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (Obecne.IkonaAktivni() == false) // Pokud je deaktivovaná nabídka, tak ikona nereaguje ani na kliknutí, aby se nedal obejít dialog
                return;
            if (e.Button == MouseButtons.Left) // Zda to bylo levé tlačítko, pravé tlačítko slouží pro zobrazení nabídky
            {
                if (this.WindowState == FormWindowState.Minimized) // Zda je Úkolník minimalizovaný, pokud ano, tak zobrazit ho na liště a dát do popředí, v opačném případě minimalizovat
                {
                    this.ShowInTaskbar = true;
                    this.WindowState = FormWindowState.Normal;
                }
                else
                    this.WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// Obsluha události změny velikosti okna, kdy pokud byl Úkolník minimalizován se skryje ikonka na liště
        /// </summary>
        private void HlavniOkno_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.ShowInTaskbar = false;
        }

        /// <summary>
        /// V menu byla vybrána volba Konec, takže Úkolník bude ukončen
        /// </summary>
        private void MoznostKonec_Click(object sender, EventArgs e)
        {
            Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici); // Pozastavit probíhající věci
            Application.Exit();
        }

        /// <summary>
        /// V menu byla vybrána možnost o Úkolníku, takže bude zobrazeno okno s informacemi o Úkolníku
        /// </summary>
        private void MenuItemOUkolniku_Click(object sender, EventArgs e)
        {
            OUkolniku okno = new OUkolniku();
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(this.Location, okno.Size);
            okno.Show();
        }

        /// <summary>
        /// V menu byla vybrána možnost nastavení svátků, takže to nastavení se zobrazí
        /// </summary>
        private void ToolStripMenuItemSvatkyNarozeniny_Click(object sender, EventArgs e)
        {
            Svatky okno = new Svatky();
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(this.Location, okno.Size);
            okno.Show();
        }

        /// <summary>
        /// V menu byla vybrána možnost Nastavení, vypne se nabídka ikonky a zobrazí se dialog s nastavením
        /// </summary>
        private void ToolStripMenuItemNastaveni_Click(object sender, EventArgs e)
        {
            Obecne.IkonaStav(false);
            NastavovaciOkno okno = new NastavovaciOkno();
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(this.Location, okno.Size);
            okno.ShowDialog();
            Obecne.IkonaStav(true);
        }

        /// <summary>
        /// V menu byla vybrána možnost Přehled, takže se zobrazí přehled událostí
        /// </summary>
        private void ToolStripMenuItemPrehled_Click(object sender, EventArgs e)
        {
            Prehled okno = new Prehled();
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(this.Location, okno.Size);
            okno.Show();
        }

        /// <summary>
        /// V menu byla vybrána možnost Návrhy a připomínky, takže se zobrazí formulář pro jejich zadání
        /// </summary>
        private void toolStripMenuItemNavrhyPripominky_Click(object sender, EventArgs e)
        {
            Formular okno =  new Formular();
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(this.Location, okno.Size);
            okno.Show();
        }

        /// <summary>
        /// Obsluha události pokusu o zavření Hlavního okna, kdy uživatel musí potvrdit zavření hlavního okna (tím pádem i Úkolníku)
        /// </summary>
        private void HlavniOkno_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Nastaveni.StavAplikace == (int) Nastaveni.StavyAplikace.bezici && e.CloseReason.ToString() == "UserClosing") // Zavírá uživatel? (Například když OS zavírá při vypínání, tak by to neuměl ukončit bez pomoci)
            {
                DialogResult odpoved = Obecne.ZobrazZpravu("Opravdu chceš ukončit Úkolník?", "Opravdu to chceš?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (odpoved == System.Windows.Forms.DialogResult.No) // Nechce zavřít okno, takže to zrušíme
                {
                    e.Cancel = true;
                    return;
                }
            }
            alarm.Vypnout(); // Vypneme alarm, už nebude třeba
            Obecne.ZmenaVeSvatcich -= AktualizujSvatky;
            Obecne.ZmenaVUdalostech -= AktualizujKalendarNasilne;
        }

        /// <summary>
        /// Obsluha události, kdy v kalendáři změníme datum výběru, aby se načetli nové události
        /// </summary>
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            VytvorDotazy(); // Je potřeba vytvořit nové dotazy pro nový den
            Obecne.Svatky(monthCalendar.SelectionStart.Day.ToString() + "-" + monthCalendar.SelectionStart.Month.ToString(), labelSvatek); // Získáme přehled svátků pro nový den
            AktualizujKalendar(); // Aktualizujeme data v kalendáři, ale dopředu, projeví se až později, protože všechna potřebná data pro nové datum už byla načtena předem
            if ((monthCalendar.SelectionEnd - monthCalendar.SelectionStart).Days > 0 && textBoxCasHodina.Enabled == true) // Vypneme možnost souvislé události pokud nelze nastavovat čas, nebo není vybráno více dnů
                checkBoxSouvisla.Enabled = true;
            else
                checkBoxSouvisla.Enabled = false;
        }

        /// <summary>
        /// Obsluha události změna Splněnosti pro zobrazení v checkboxu, takže se vytvoří nové dotazy a kalendář se musí násilně aktualizovat (předem načtená data jsou neplatný)
        /// </summary>
        private void checkBoxSplnenost_CheckedChanged(object sender, EventArgs e)
        {
            VytvorDotazy();
            AktualizujKalendarNasilne();
        }

        /// <summary>
        /// Obsluha události kliknutí na Uložit ve formuláři pro nové události
        /// </summary>
        private void buttonUlozit_Click(object sender, EventArgs e)
        {
            if (comboBoxTyp.SelectedIndex == -1) // Nic nebylo vybráno? Něco se vybrat musí
            {
                Obecne.ZobrazZpravu("Nevybral jsi typ události!", "Pozor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (textBoxNadpis.Text == "") // Má ta událost nějaký název? Nějak se to musí přece jmenovat
            {
                Obecne.ZobrazZpravu("Nadpis (nebo jméno) nemůže být prázdný!", "Pozor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Jsou to narozeniny?
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
            else if (radioButtonAno.Checked == true) // Je zvoleno automatické upozornění?
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
            int navratDoRoku = monthCalendar.SelectionStart.Year; // Abychom se vrátili zpátky do správného roku, protože třeba u narozenin skočíme na rok 4
            if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Pokud ukládáme narozeniny, tak se nastaví rok 4
                monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddYears(4 - monthCalendar.SelectionStart.Year);
            Databaze db = new Databaze();
            for (DateTime i = monthCalendar.SelectionStart; i <= monthCalendar.SelectionEnd; i = i.AddDays(1), upozorneni = upozorneni.AddDays(1)) // Postupně procházíme jednotlivé dny, které byly vybrány a vkládáme je do databáze
            {
                db.Dotaz("INSERT INTO udalosti VALUES ('NULL', '" + comboBoxTyp.SelectedIndex.ToString() + "', '" + nadpis + "', '" + zprava + "', '" + i.ToString("yyyy-MM-dd HH:mm:ss") + "', '0', '" + upozornit + "', '" + upozorneni.ToString("yyyy-MM-dd HH:mm:ss") + "');");
                if (i == monthCalendar.SelectionStart && checkBoxSouvisla.Checked == true) // Jakmile jsme uložili první den, tak se vypne upozorňování pokud to je souvislá událost
                {
                    i = i.AddHours(-i.Hour);
                    i = i.AddMinutes(-i.Minute);
                    i = i.AddSeconds(-i.Second);
                    upozornit = ((int)Obecne.UpozorneniTypy.neupozornit).ToString();
                }
                if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Pokud jsou narozeniny, tak tam není dovoleno ukládat víc dní, přece člověk se narodil jen jednou
                    i = monthCalendar.SelectionEnd;
            }
            db.Close();
            monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddYears(navratDoRoku - monthCalendar.SelectionStart.Year); // Vrátíme se zpátky do původního roku
            monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddMinutes(-monthCalendar.SelectionStart.Minute); // Vynulujeme minuty
            monthCalendar.SelectionStart = monthCalendar.SelectionStart.AddHours(-monthCalendar.SelectionStart.Hour); // Vynulujeme hodiny
            Obecne.OnZmenaVUdalostech(); // Vyvoláme událost změny v událostech, aby se načetli znovu data
            if (comboBoxTyp.SelectedIndex == (int)Obecne.UdalostiTypy.narozeniny) // Pokud jsme ukládali narozeniny, tak ty v přehledu spadají i do svátků, takže je potřeba vyvolat událost pro změnu svátků
                Obecne.OnZmenaVeSvatcich();
            Obecne.VyplnKalendar(monthCalendar, checkBoxSplnenost.Checked);
            if (Obecne.PametNadpis.Contains(textBoxNadpis.Text) == false) // Zjistíme zda se uložený nadpis nachází v kolekci nadpisů, pokud ne, tak ho tam přidáme
                Obecne.PametNadpis.Add(textBoxNadpis.Text);
            buttonReset.PerformClick(); // Vše vyresetujeme pomocí tlačítka reset
        }

        /// <summary>
        /// Obsluha události, která se stará o stisknutí enteru v časových políčkách, protože po jeho stisku je automaticky událost uložena
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
        /// Obsluha události kdy je změněn typ události
        /// </summary>
        private void comboBoxTyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTyp.SelectedIndex != (int)Obecne.UdalostiTypy.narozeniny) // Jestliže to nejsou narozeniny tak aktivujeme časová okna, nadpis bude nadpisem a zpráva bude zprávou
            {
                if (comboBoxTyp.SelectedIndex != (int)Obecne.UdalostiTypy.pisemka) // Jestliže to není písemka, tak bude nastaveno automatické upozornění
                    radioButtonAno.Checked = true;
                else // Jestliže to je písemka, tak bude nastaveno vypnuté upozorňování
                    radioButtonNe.Checked = true;
                labelCas.Enabled = true;
                labelCasDvojtecka.Enabled = true;
                textBoxCasHodina.Enabled = true;
                textBoxCasMinuta.Enabled = true;
                if ((monthCalendar.SelectionEnd - monthCalendar.SelectionStart).Days > 0)
                    checkBoxSouvisla.Enabled = true;
                labelNadpis.Text = "Nadpis:";
                labelZprava.Text = "Zpráva:";
            }
            else // Jestliže to jsou narozeniny tak nastavíme automatické upozornění, zneaktivníme časová okna, z nadpisu se stane jméno a ze zprávy rok narození
            {
                radioButtonAno.Checked = true;
                labelCas.Enabled = false;
                labelCasDvojtecka.Enabled = false;
                textBoxCasHodina.Enabled = false;
                textBoxCasMinuta.Enabled = false;
                checkBoxSouvisla.Enabled = false;
                labelNadpis.Text = "Jméno:";
                labelZprava.Text = "Rok narození (například " + DateTime.Now.Year.ToString() + "):";
            }
        }

        /// <summary>
        /// Obsluha události kliknutí na rozevírací položku, kdy zobrazujeme a skrýváme zadávání nové události
        /// </summary>
        private void labelZobrazVic_Click(object sender, EventArgs e)
        {
            if (labelZobrazVic.Text == "<<") // Jestliže bylo nastaveno skrytí zadávání, tak zmenšíme velikost okna, nastavíme zobrazování zvětšení (s příslušným popiskem) a zneviditelníme zadávání událostí
            {
                this.Size = this.MinimumSize;
                labelZobrazVic.Text = ">>";
                panelNovaUdalost.Visible = false;
                toolTip.SetToolTip(labelZobrazVic, "Zobrazí zadávání nové události");
            }
            else // Pokud se zvětšuje, tak zviditelníme zadávání nových událostí, zvětšíme okno na maximum, nastavíme zobrazování zmenšování (s příslušným popiskem)
            {
                panelNovaUdalost.Visible = true;
                this.Size = this.MaximumSize;
                labelZobrazVic.Text = "<<";
                toolTip.SetToolTip(labelZobrazVic, "Skryje zadávání nové události");
            }
            labelZobrazVic.Location = new Point(this.Size.Width - 38, labelZobrazVic.Location.Y); // Přesuneme položku na zmenšování/zvětšování na nový kraj obrazovky
        }

        /// <summary>
        /// Obsluha události, kdy se mění stavy radiobuttonů vyjadřujících typ upozornění
        /// </summary>
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonVlastni.Checked == true) // Pokud je vybráno vlastní upozornění, tak se aktivují položky pro zadání vlastního upozornění
            {
                labelKdy.Enabled = true;
                dateTimePickerUpozorneni.Enabled = true;
                labelCasDvojteckaUpozorneni.Enabled = true;
                labelCasUpozorneni.Enabled = true;
                textBoxCasMinutaUpozorneni.Enabled = true;
                textBoxCasHodinaUpozorneni.Enabled = true;
            }
            else // Pokud nechceme vlastní upozornění, tak se dané položky deaktivují
            {
                labelKdy.Enabled = false;
                dateTimePickerUpozorneni.Enabled = false;
                labelCasDvojteckaUpozorneni.Enabled = false;
                labelCasUpozorneni.Enabled = false;
                textBoxCasMinutaUpozorneni.Enabled = false;
                textBoxCasHodinaUpozorneni.Enabled = false;
            }
        }

        /// <summary>
        /// Obsluha události kliknutí na resetovací tlačítko, aby se vymazalo to co bylo zadáno do zadávání nových událostí (tedy uvedení do nic neobsahujícího stavu)
        /// </summary>
        private void buttonReset_Click(object sender, EventArgs e)
        {
            comboBoxTyp.SelectedIndex = -1;
            textBoxNadpis.Text = "";
            textBoxZprava.Text = "";
            textBoxCasHodina.Text = "";
            textBoxCasMinuta.Text = "";
            radioButtonAno.Checked = true;
            dateTimePickerUpozorneni.ResetText();
            textBoxCasMinutaUpozorneni.Text = "";
            textBoxCasHodinaUpozorneni.Text = "";
        }
    }
}

using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Umožňuje zobrazování událostí v tabulce, která se dá všelijak nastavit
    /// </summary>
    public partial class Stranka : UserControl
    {
        /// <summary>
        /// Možnosti zobrazení navigace
        /// </summary>
        public enum polohyNavigace { uprostred, vpravo };
        /// <summary>
        /// Jak vysoký jsou jednotlivý řádky
        /// </summary>
        const int vyskaRadku = 19;
        
        /// <summary>
        /// Základní hodnota pro počet řádků
        /// </summary>
        private int radku = 1;
        /// <summary>
        /// Základní hodnota pro umístění navigace
        /// </summary>
        private polohyNavigace alignOvladaciPrvky = polohyNavigace.uprostred;
        /// <summary>
        /// Základní hodnota pro vykreslování (dokud je -1, tak se nic nevykresluje, čímž se zrychlí inicializace, protože by během ní nastalo několikrát vykreslení)
        /// </summary>
        private short zrychleneVykreslovani = -1;
        /// <summary>
        /// MySQL dotaz, kterým zjistíme kolik je celkem událostí na zobrazení v dané tabulce
        /// </summary>
        private string dotazNaPocet = "";
        /// <summary>
        /// MySQL dotaz, kterým získáme jednotlivé události do tabulky
        /// </summary>
        private string dotazNaPolozky = "";
        /// <summary>
        /// Pro jaký rok je daná tabulka aktuální
        /// </summary>
        private int rok = DateTime.Now.Year;
        /// <summary>
        /// Zda se má v názvu událostí zobrazovat datum
        /// </summary>
        private bool zobrazDatum = true;
        /// <summary>
        /// Zda se řeší přechod narozenin do dalšího roku
        /// </summary>
        private short prechodNarozeninDoDalsihoRoku = 0;
        /// <summary>
        /// Zda se má zavřít celé okno, pokud buda stránka prázdná
        /// </summary>
        private bool zavritPrazdne = false;
        /// <summary>
        /// Co bude napsáno, pokud bude nula položek ve stránce
        /// </summary>
        private string textNulaPolozek = "";
        /// <summary>
        /// Co za typ události je ta, na kterou bylo kliknuto pravým tlačítkem
        /// </summary>
        private int typ;
        /// <summary>
        /// Co za ID události je ta, na kteoru bylo kliknuto pravým tlačítkem
        /// </summary>
        private string udalost;

        /// <summary>
        /// Aktuálně zobrazená stránka (počítá se od nuly)
        /// </summary>
        private int aktualniStranka = 0;
        
        /// <summary>
        /// Vlastnost pro počet řádků v tabulce, změna vyvolá vykreslení tabulky
        /// </summary>
        [System.ComponentModel.Description("Udává kolik bude řádků v tabulce, změna vyvolá okamžité vykreslení"), System.ComponentModel.Category("Appearance")]
        public int Radku
        {
            get { return radku; }
            set { if (value < 1) radku = 1; else radku = value; VytvorTabulku(); }
        }

        /// <summary>
        /// Vlastnost pro umístění navigace
        /// </summary>
        [System.ComponentModel.Description("Udává polohu navigačních tlačítek"), System.ComponentModel.Category("Appearance")]
        public polohyNavigace AlignOvladaciPrvky
        {
            get { return alignOvladaciPrvky; }
            set { alignOvladaciPrvky = value; ZarovnejOvladaciPrvky(); }
        }

        /// <summary>
        /// Vlasnost pro zrychlené vykreslování, aby se nevykreslovalo už během inicializace a tak neproběhlo vícenásobné vykreslení, změna vyvolá okamžité vykreslení tabulky
        /// </summary>
        [System.ComponentModel.Description("Udává, zda se stránka bude načítat až po inicializaci nebo i při ní (při ní znamená zpomalení), změna vyvolá okamžité vykreslení"), System.ComponentModel.Category("Behavior")]
        public bool ZrychleneVykreslovani
        {
            get { if (zrychleneVykreslovani == -1) zrychleneVykreslovani = 0; return zrychleneVykreslovani == 1; }
            set { if (value == true) zrychleneVykreslovani = 1; else zrychleneVykreslovani = 0 ; VytvorTabulku(); }
        }

        /// <summary>
        /// Vlasnost pro zobrazení data v názvu událostí
        /// </summary>
        [System.ComponentModel.Description("Udává, zda se v názvu události bude zobrazovat i datum"), System.ComponentModel.Category("Behavior")]
        public bool ZobrazDatum
        {
            get { return zobrazDatum; }
            set { zobrazDatum = value; }
        }

        /// <summary>
        /// Vlasnost pro to, zda bude se přecházet do dalšího roku, využití u narozenin, když se počítá i s narozeninami v příštím roce (to znamená pro přehledy narozeniny)
        /// </summary>
        [System.ComponentModel.Description("Udává, zda bude přechod do dalšího roku, používá se pouze pro narozeniny, konkrétně pro jejich souhrnné přehledy"), System.ComponentModel.Category("Behavior")]
        public bool PrechodNarozeninDoDalsihoRoku
        {
            get { return prechodNarozeninDoDalsihoRoku == 1; }
            set { if (value == false) prechodNarozeninDoDalsihoRoku = 0; else prechodNarozeninDoDalsihoRoku = 1; }
        }

        /// <summary>
        /// Vlastnost, která určuje, zda se zavře okno, když budou stránky prázdné
        /// </summary>
        [System.ComponentModel.Description("Udává, zda se zavře nadřazené okno, když stránka nebude obsahovat ani jednu položku"), System.ComponentModel.Category("Behavior")]
        public bool ZavritPrazdne
        {
            get { return zavritPrazdne; }
            set { zavritPrazdne = value; }
        }

        /// <summary>
        /// Vlasnost, co bude vypsáno pokud budou stránky prázdné
        /// </summary>
        [System.ComponentModel.Description("Udává, co se vypíše, když stránka nebude obsahovat ani jednu položku"), System.ComponentModel.Category("Behavior")] 
        public string TextNulaPolozek
        {
            get { return textNulaPolozek; }
            set { textNulaPolozek = value; }
        }
        
        /// <summary>
        /// Načtou se komponenty, přiřadí se obsluha události zvětšení a reakce na událost změna v událostech
        /// </summary>
        public Stranka()
        {
            InitializeComponent();
            this.Resize += new EventHandler(Stranka_Resize); // není v inicializaci, protože se tam nastavuje velikost a byla by tak vyvolána
            Obecne.ZmenaVUdalostech += NactiData;
        }

        /// <summary>
        /// Provede se úklid, tedy odstraněné oblushy událsoti a změně velikosti, reakci na změnu v událostech, zrušení reakce na kliknutí na popisek a tabulka bude rozpuštěna
        /// </summary>
        public void Uklid()
        {
            this.Resize -= Stranka_Resize;
            Obecne.ZmenaVUdalostech -= NactiData;
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).Click -= labelUdalost_Click;
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.Dispose();
        }

        /// <summary>
        /// Při změně velikosti se vytvoří tabulka
        /// </summary>
        private void Stranka_Resize(object sender, EventArgs e)
        {
            VytvorTabulku();
        }

        /// <summary>
        /// Vytváření tabulky dle zadaných hodnot
        /// </summary>
        private void VytvorTabulku()
        {
            if (zrychleneVykreslovani == -1) // Aby se nevykreslovalo už při inicializaci a to několikrát, protože toto je reakce třeba na změnu velikosti, počtu řádků, atd.
                return;
            tableLayoutPanel.RowCount = radku; // Řádků bude tolik, kolik bylo nastaveno
            tableLayoutPanel.RowStyles.Clear(); // Styl řádků smažeme, začneme na novo
            tableLayoutPanel.Controls.Clear(); // Předchozí odpad pošleme do paměťového odpadu, aby si GC smlsnul
            for (int i = 0; i < radku; i++) // Přidáme základní styl řádků, tedy automatická velikost
                tableLayoutPanel.RowStyles.Add(new RowStyle());
            for (int i = 0; i < radku; i++) // Postupně dáme do jednotlivých řádků to co chceme
            {
                tableLayoutPanel.Controls.Add(new PictureBoxUprava(), 0, i); // V prvním sloupci tabulky je speciální PictureBox na úpravu událostí
                tableLayoutPanel.Controls.Add(new PictureBoxSplneno(), 1, i); // Ve druhém sloupci tabulky je speciální PictureBox na změnu splněnosti událostí
                tableLayoutPanel.Controls.Add(new Label(), 2, i); // V posledním sloupci tabulky je label, která bude obshaovat název události
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).AutoSize = true; // Label bude mít proměnnou délku
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).MaximumSize = new System.Drawing.Size(0, (int)labelDozadu.Font.GetHeight() + 1); // Maximální velikost pro popisek, řešíme jen výšku a tu získáme z velikosti písma
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).ContextMenuStrip = contextMenuStrip; // Po kliknutí pravým na label se zobrazí nabídka
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).Margin = new Padding(3, 3, 0, 0); // Nastavíme okraje Labelu, aby to nebylo tak nalepený na kraj
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).Click += new EventHandler(labelUdalost_Click);
                toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(0, i), "Upravit událost"); // Nastavíme tooltip úpravě
                toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(1, i), "Označit jako nesplněné"); // Nastavíme tooltip splněnosti
            }
            tableLayoutPanel.Size = new System.Drawing.Size(this.Size.Width, vyskaRadku * radku); // Vypočítáme velikost tabulky a tu jí nastavíme
            ZarovnejOvladaciPrvky(); // Provedeme zarovnání navigačních prvků, protože naše tabulka pravděpodobně změnila velikost
        }

        /// <summary>
        /// Provede zarovnání navigačních prvků
        /// </summary>
        private void ZarovnejOvladaciPrvky()
        {
            if (zrychleneVykreslovani == -1) // Aby se nevykreslovalo už při inicializaci a to několikrát, protože toto je reakce třeba na změnu velikosti, počtu řádků, atd.
                return;
            // Konstanty, které mají hodnoty podle toho, jak velká díra má být mezi jednotlivými prvky
            const int rozdilZacatekDozadu = 23;
            const int rozdilDozaduStranka = 20;
            const int rozdilStrankaVpred = 28;
            const int rozdilVpredKonec = 20;
            int poziceY = tableLayoutPanel.RowCount * vyskaRadku + 4; // Vypočítáme Y souřadnici pro navigační prvky (pro prvek textBoxStranka)
            int poziceX;
            if (alignOvladaciPrvky == polohyNavigace.uprostred) // Pokud byl vybrán střed pro zarovnání navigačních prvků
                poziceX = this.Size.Width / 2 - textBoxStranka.Size.Width / 2 + rozdilStrankaVpred + rozdilVpredKonec; // Nejdřív získáme střed, od toho odečteme půlku šířky textBoxu se stránkou a pak k tomu přičteme rozdíl vzdálenosti mezi stránkou a vpřed a ještě rozdíl mezi vpřed a konec
            else // Zarovnání doprava
                poziceX = this.Size.Width - labelKonec.Size.Width; // Od konce odečteme šířku prvku konec
            labelKonec.Location = new System.Drawing.Point(poziceX, poziceY + 3); // Konec bude mít vypočtené souřadnice (jen Y bude o tři níž, protože není tak velký jako textBoxStranka)
            labelVpred.Location = new System.Drawing.Point(labelKonec.Location.X - rozdilVpredKonec, poziceY + 3); // Vpřed je více vlevo, tedy o rozdíl mezi vpřed a konec, Y stejné jako konec
            textBoxStranka.Location = new System.Drawing.Point(labelVpred.Location.X - rozdilStrankaVpred, poziceY); // Stránka je více vlevo, tedy o rozdíl mezi stránka a vpřed, Y se rovná tomu vypočtenému, protože pro něj jsme to počítali
            labelDozadu.Location = new System.Drawing.Point(textBoxStranka.Location.X - rozdilDozaduStranka, poziceY + 3); // Dozadu je více vlevo, tedy o rozdíl mezi dozadu a stránka, Y stejné jako konec
            labelZacatek.Location = new System.Drawing.Point(labelDozadu.Location.X - rozdilZacatekDozadu, poziceY + 3); // Začátek je více vlevo, tedy o rozdíl mezi začátek a dozadu, Y stejné jako konec
        }

        /// <summary>
        /// Mění aktuálně zobrazenou stránku na požadovanou stránku (pokud je příliš velká, tak jí srazí na nejvyšší možnou), vrací zda byla změna úspěšná nebo ne
        /// </summary>
        /// <param name="stranka">Na kterou stránku se to má otočit</param>
        /// <returns>Zda byla úspěšně nebo neúspěšně změněna stránka</returns>
        private bool ZmenStranku(int stranka)
        {
            int maxStranka = MaxStranka(); // Zjistíme maximální stránku, na kterou se lze dostat
            if (stranka > maxStranka) // Stránka musí být menší nebo rovna té maximální, pokud není, tak nastavíme tu maximální
                stranka = maxStranka;
            if (stranka <= -1) // Záporné stránky nemáme
                return false; // Změna stránky neproběhla
            MaxStranka();
            aktualniStranka = stranka;
            textBoxStranka.Text = (aktualniStranka + 1).ToString(); // Lidé číslují od jedničky, takže se tam nastaví o jedna větší stránka, změna té stránky navíc vyvolá akci kdy se načtou data pro tu stránku
            return true; // Změna stránky proběhla úspěšně
        }

        /// <summary>
        /// Zjistí maximální stránku, která může být a tu vrátí
        /// </summary>
        /// <returns>Vrátí maximální možnou stránku</returns>
        private int MaxStranka()
        {
            int stranka = -1; // Pro pířpad, že by bylo nula položek do tabulky
            Databaze db = new Databaze();
            db.Dotaz(dotazNaPocet); // Zjistíme kolik je položek
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") != 0) // Když je alespoň jedna, tak spočítáme na kolik stránek to bude
                    stranka = (db.DejVysledekInt("Pocet") - 1) / tableLayoutPanel.RowCount;
            }
            db.Close();
            return stranka; // Vrátíme počet stránek
        }

        /// <summary>
        /// Nastaví nové dotazy a nový rok a pak provede načtení dat
        /// </summary>
        /// <param name="poctovyDotaz">Dotaz na počet položek</param>
        /// <param name="polozkovyDotaz">Dotaz na položky (je třeba zde nemít středník na konci, protože se na konec budou připisovat limity pro omezené zobrazení stránek)</param>
        /// <param name="rokZobrazeni">Rok, ve kterém se stránky zobrazujou (důležité hlavně pro narozeniny)</param>
        public void NactiData(string poctovyDotaz, string polozkovyDotaz, int rokZobrazeni)
        {
            dotazNaPocet = poctovyDotaz;
            dotazNaPolozky = polozkovyDotaz;
            rok = rokZobrazeni;
            NactiData();
        }

        /// <summary>
        /// Provede načtení dat do stránky a jejich zobrazení
        /// </summary>
        public void NactiData()
        {
            if (dotazNaPocet == "" || dotazNaPolozky == "") // Pokud nejsou nastaveny dotazy, pak není co načítat
                return;
            int polozka = 0; // Kolik položek bylo načteno (abych věděl, kolik jich musím skrýt) a tím pádem zobrazeno na stránce
            string tooltip = ""; // Co bude zobrazeno v tooltipu pro danou událost
            string popisek = ""; // Co bude zobrazeno jako popisek u události
            Databaze db = new Databaze();
            db.Dotaz(dotazNaPolozky + " LIMIT " + (aktualniStranka * tableLayoutPanel.RowCount).ToString() + ", " + tableLayoutPanel.RowCount.ToString() + ";"); // Použijeme předem nastavený dotaz na položky, ale na konec přihodíme limit kvůli omezenému zobrazení stránek
            while (db.DalsiVysledek() == true)
            {
                tableLayoutPanel.GetControlFromPosition(0, polozka).Tag = db.DejVysledekString("ID"); // Uložíme ID události do PictureBoxu s úpravou události
                tableLayoutPanel.GetControlFromPosition(1, polozka).Tag = db.DejVysledekString("ID"); // Uložíme ID události do PictureBoxu splněnosti události
                tableLayoutPanel.GetControlFromPosition(2, polozka).Tag = db.DejVysledekString("ID"); // Uložíme ID události do popisku události
                if (db.DejVysledekInt("Typ") != ((int)Obecne.UdalostiTypy.narozeniny)) // Pokud událost nejsou narozeniny
                {
                    if (db.DejVysledekInt("Splneno") == 1) // Pokud je událost splněná, tak nastavíme zorbazení křížku pro nesplnění, upravíme taky tooltip křížku a popisek události přeškrtneme
                    {
                        ((PictureBox)tableLayoutPanel.GetControlFromPosition(1, polozka)).Image = Ukolnik.Properties.Resources.krizek;
                        ((PictureBox)tableLayoutPanel.GetControlFromPosition(1, polozka)).Image.Tag = "krizek";
                        toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(1, polozka), "Označit jako nesplněno");
                        tableLayoutPanel.GetControlFromPosition(2, polozka).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Strikeout);
                    }
                    else // V opačném případě se nastaví fajka pro splnění, upraví tooltip fajfky a popiske události nebude přeškrtlý
                    {
                        ((PictureBox)tableLayoutPanel.GetControlFromPosition(1, polozka)).Image = Ukolnik.Properties.Resources.fajfka;
                        ((PictureBox)tableLayoutPanel.GetControlFromPosition(1, polozka)).Image.Tag = "fajfka";
                        toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(1, polozka), "Označit jako splněno");
                        tableLayoutPanel.GetControlFromPosition(2, polozka).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
                    }
                    if (zobrazDatum == true) // Pokud se má zobrazit datum v popisku tak bude zobrazeno, jinak bude jen samotný název události
                        popisek = db.DejVysledekDatumCas("Kdy").ToString("d.M.yyyy") + " " + db.DejVysledekString("Nadpis");
                    else
                        popisek = db.DejVysledekString("Nadpis");
                    popisek += " | " + db.DejVysledekDatumCas("Kdy").ToString("H:mm"); // Přidáme do popisku čas konání
                    if (db.DejVysledekString("Zprava").Length > 0) // Pokud není zpráva prázdná, tak ji přiřadíme do popisku
                        popisek += " | " + db.DejVysledekString("Zprava");
                    tableLayoutPanel.GetControlFromPosition(2, polozka).Text = popisek;
                    tooltip = Obecne.DejNazevTypuUdalosti(db.DejVysledekInt("Typ")) + " " + db.DejVysledekDatumCas("Kdy").ToString("H:mm") + "\n" + db.DejVysledekString("Zprava"); // Tooltip bude obsahovat co to je za typ události, kdy nastane daná událost a také její podrobný popis
                    tableLayoutPanel.GetControlFromPosition(1, polozka).Visible = true; // Zobrazí se PictureBox se splněností (mohl být skryt narozeninama z jiného dne)
                }
                else // Pokud jsou to narozeniny
                {
                    if (zobrazDatum == false) // Pokud se nemá zobrazit datum v popisku tak nebude zobrazeno, bude jen samotný název události, v opačném případě bude před názvem události i datum, kdy se narozeniny odehrávají (využívá se přechod, který násobí splněnost, která se přičitá k roku -> znamená, že další narozeniny jsou až v příštím roce)
                        tableLayoutPanel.GetControlFromPosition(2, polozka).Text = "Narozeniny " + db.DejVysledekString("Nadpis") + " (" + (rok - db.DejVysledekInt("Zprava") + (db.DejVysledekInt("Splneno") * prechodNarozeninDoDalsihoRoku)).ToString() + ")"; // Pouze nápis narozeniny, kdo je má a kolik mu bude v závorce
                    else
                        tableLayoutPanel.GetControlFromPosition(2, polozka).Text = db.DejVysledekDatumCas("Kdy").ToString("d.M") + "." + (rok + db.DejVysledekInt("Splneno") * prechodNarozeninDoDalsihoRoku).ToString() + " " + db.DejVysledekString("Nadpis") + " (" + (rok - db.DejVysledekInt("Zprava") + (db.DejVysledekInt("Splneno") * prechodNarozeninDoDalsihoRoku)).ToString() + ")"; // Kdy má narozeniny, kdo má narozeniny a v závorce kolik mu bude (využívá se přechod, který násobí splněnost, která se přičitá k roku -> znamená, že další narozeniny jsou až v příštím roce)
                    tooltip = "Narodil se v roce " + db.DejVysledekString("Zprava"); // Jednoduchý tooltip, který zobrazuje, dky se dotyčný narodil
                    tableLayoutPanel.GetControlFromPosition(2, polozka).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F); // Text je obyčejný, nepřeškrtnutý
                    tableLayoutPanel.GetControlFromPosition(1, polozka).Visible = false; // PictureBox se splněností se schová (narozeniny nelze splnit, budou tu furt a furt)
                }
                toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(2, polozka), tooltip); // Nastavíme událostem tooltipy
                tableLayoutPanel.GetControlFromPosition(0, polozka).Visible = true; // Necháme zjevit PictureBox s úpravou události (mohl být schován z předchozího nedostatku událostí)
                tableLayoutPanel.GetControlFromPosition(2, polozka).Visible = true; // Necháme zjevit popisek události (mohl být schován z předchozího nedostatku událostí)
                polozka += 1; // Zvýšíme počet položek, které budou na stránce
            }
            if (polozka == 0) // Pokud bylo načtneo nula položek, tak se pokusí změnšit stránka
            {
                if (ZmenStranku(aktualniStranka - 1) == true) // Jestliže byla změna stránky úspěšná, tak se dále nepokračuje, protože se mohou znova načítat data a taky se znovu načítat budou, protože díky té změně stránky bylo vyvoláno další načítání
                    return;
                else // Jestliže změna stránky nebyla úspěšná, tak to znamená, že už více snížit nemůžeme a je tam tedy definitivně 0 položek, takže se provede akce, která se má provést při nula položkách a vrátí počet položek, které díky té akci vznikly
                    polozka += NulaPolozek();
                if (polozka == 0) // Pokud je i nadále nula položek, tak se ukončí načítání, protože se bude zavírat okno
                    return;
            }
            int velikost = (int)tableLayoutPanel.Height / polozka; // Spočítá kolik místa si mohou rozdělit popisky
            velikost = velikost - (velikost % ((int)labelDozadu.Font.GetHeight() + 1)); // Zaokrouhlí je na celé řádky
            for (int i = 0; i < polozka; i++) // Postupně zvětší místo pro popisky - není efektivní využití přidělení místa, za to ale mnohem rychlejší
                ((Label)tableLayoutPanel.GetControlFromPosition(2, i)).MaximumSize = new System.Drawing.Size(0, velikost);
            for (int i = polozka; i < tableLayoutPanel.RowCount; i++) // Řádky, kterým nebyla přiřazena událost budou zneviditelněny
            {
                tableLayoutPanel.GetControlFromPosition(0, i).Visible = false;
                tableLayoutPanel.GetControlFromPosition(1, i).Visible = false;
                tableLayoutPanel.GetControlFromPosition(2, i).Visible = false;
            }
            db.Dotaz(dotazNaPocet); // Zjistíme kolik máme událostí do stránek
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") > tableLayoutPanel.RowCount) // Pokud je událostí více než se vejde na jendu stránku, tak musíme zobrazit navigační prvky
                {
                    textBoxStranka.Visible = true; // Zobrazí se textBox obsahující aktuální stránku
                    if (aktualniStranka == 0) // Pokud jsme na první stránce, tak schováme ovládání na skok na začátek nebo dozadu
                    {
                        labelDozadu.Visible = false;
                        labelZacatek.Visible = false;
                    }
                    else // Pokud nejsem na začátku, tak zviditelníme ovládáníé na skok na začátek nebo dozadu, též nastavíme kam dozadu vede
                    {
                        labelDozadu.Visible = true;
                        labelZacatek.Visible = true;
                        labelDozadu.Tag = aktualniStranka - 1; // Vede na předchozí stránku
                    }
                    int stranek = (db.DejVysledekInt("Pocet") - 1) / tableLayoutPanel.RowCount; // Spočítáme počet stránek
                    if (stranek == 0 || aktualniStranka == stranek) // Pokud je stránek nula nebo už jsme na té poslední, tak schováme ovládání na skok vpřed a na konec
                    {
                        labelVpred.Visible = false;
                        labelKonec.Visible = false;
                    }
                    else // V opačném případě zviditelníme ovládání na skok vpřed a na konec, nastavíme kam vede vpřed a kde je konec
                    {
                        labelVpred.Visible = true;
                        labelVpred.Tag = aktualniStranka + 1; // Vede na následující stránku
                        labelKonec.Visible = true;
                        labelKonec.Tag = stranek; // Vede na psolední stránku
                    }
                }
                else // Pokud je počet událostí menší než kolik se vejde na jednu stránku, tak navigaci skryjeme
                {
                    labelZacatek.Visible = false;
                    labelDozadu.Visible = false;
                    labelVpred.Visible = false;
                    labelKonec.Visible = false;
                    textBoxStranka.Visible = false;
                }
            }
            db.Close();
        }

        /// <summary>
        /// Je provedena, když stránky neobsahují ani jednu položku
        /// </summary>
        /// <returns>Vrací 0 při zavírání okna, jinak 1, protože bude přidán popisek o prázdnosti</returns>
        private int NulaPolozek()
        {
            if (zavritPrazdne == true) // Zda se má zavírat okno při nula položkách
            {
                this.ParentForm.Close(); // Zjistíme okno, kterému stránky patří a zavřeme ho
                return 0; // Vrátíme nulu, protože zavíráme okno, nepřidáváme tam informační popisek o prázdnosti
            }
            tableLayoutPanel.GetControlFromPosition(2, 0).Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F); // Nastavíme obyčejné písmo
            tableLayoutPanel.GetControlFromPosition(2, 0).Text = textNulaPolozek; // Nastavíme popisek pro nula položek
            tableLayoutPanel.GetControlFromPosition(2, 0).Tag = "-1"; // Není to událost, což symbolizuje toto záporné ID
            tableLayoutPanel.GetControlFromPosition(2, 0).Visible = true; // Popisek je vidět
            tableLayoutPanel.GetControlFromPosition(1, 0).Visible = false; // PictureBox na splněnost je skryt
            tableLayoutPanel.GetControlFromPosition(0, 0).Visible = false; // PictureBox na úpravu je skryt
            toolTip.SetToolTip(tableLayoutPanel.GetControlFromPosition(2, 0), ""); // Tooltip je vymazán, protože tam není událost
            return 1; // Vrátíme jedničku, protože v tabulce je jedna položka a to ta s infem o nula položkách
        }

        /// <summary>
        /// Při kliku na navigační prvky zavolá změnu stránky
        /// </summary>
        private void PosunStranek_Click(object sender, EventArgs e)
        {
            ZmenStranku(Convert.ToInt32(((Label)sender).Tag)); // V tagu jednotlivých prvků je uvedeno číslo stránky na kterou odkazujou
        }

        /// <summary>
        /// Pokud je stisknut enter při psaní čísla stránky, tak se pokusí na to číslo změnit stránku
        /// </summary>
        private void PosunStranek_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int stranka = Convert.ToInt32(((TextBox)sender).Text); // Pokusí se získat číslo
                if (stranka > 0) // Pokud je to platná lidská strínka, tak se uloží počítačová stránka
                    aktualniStranka = stranka - 1;
            }
            catch // Když se nepodařil převod, tak se to ignoruje
            {
                return;
            }
            NactiData(); // Načtou se data pro aktuální stránku
        }

        /// <summary>
        /// Když je kliknuto pravým na popisek události, tak se otevře nabídka
        /// </summary>
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            udalost = ((ContextMenuStrip)sender).SourceControl.Tag.ToString(); // Získáme z nabídky na co bylo kliknuto a z toho ID události, které to přísluší
            if (udalost == "-1") // Pokud to je jen popisek pro nula položek, tak zrušíme zobrazení nabídky
            {
                e.Cancel = true;
                return;
            }
            Databaze db = new Databaze();
            db.Dotaz("SELECT Typ FROM udalosti WHERE ID=" + udalost + ";"); // Zjistíme typ události, na kterou bylo kliknuto
            while (db.DalsiVysledek())
            {
                typ = db.DejVysledekInt("Typ");
                if (typ != (int)Obecne.UdalostiTypy.narozeniny) // Pokud to nebyly narozeniny, tak je zobrazeno možnost posunout to o rok a o hodinu, jinak je neaktivní (protože narozeniny jsou uloženy, že se staly v roce 4 a takto by odešli do roku 5)  a s hodinama taky nechceme hýbat
                {
                    this.toolStripMenuItemPosunoutUdalostORok.Enabled = true;
                    this.toolStripMenuItemPosunoutUdalostOHodinu.Enabled = true;
                }
                else
                {
                    this.toolStripMenuItemPosunoutUdalostORok.Enabled = false;
                    this.toolStripMenuItemPosunoutUdalostOHodinu.Enabled = false;
                }
            }
            db.Close();
        }

        /// <summary>
        /// Pokud byla v nabídce vybrána možnost posunu události, tak se ten posun provede
        /// </summary>
        private void toolStripMenuItemPosunoutUdalost_Click(object sender, EventArgs e)
        {
            Obecne.PresunUdalost(udalost, typ, ((ToolStripMenuItem)sender).Tag.ToString());
        }

        /// <summary>
        /// Pokud byla v nabídce vybrána možnost smazat událost, tak se smaže a bude provedeno znovunačtení událostí, případně svátků
        /// </summary>
        private void toolStripMenuItemSmazatUdalost_Click(object sender, EventArgs e)
        {
            Databaze db = new Databaze();
            db.Dotaz("SELECT Nadpis FROM udalosti WHERE ID=" + udalost + ";"); // Vytáhneme název události
            string nadpis = "";
            while (db.DalsiVysledek())
                nadpis = db.DejVysledekString("Nadpis");
            db.Dotaz("SELECT COUNT(*) AS Pocet FROM udalosti WHERE Nadpis='" + nadpis + "';"); // Spočítáme kolik událsotí sdílí tento název
            while (db.DalsiVysledek())
            {
                if (db.DejVysledekInt("Pocet") < 2) // Pokud méně než dva, tak název odstraníme z kolekce našeptávače
                    Obecne.PametNadpis.Remove(nadpis);
            }
            db.Dotaz("DELETE FROM udalosti WHERE ID=" + udalost + ";"); // Smažeme událost z databáze
            db.Close();
            Obecne.OnZmenaVUdalostech(); // Informujeme o změně v událostech
            if (typ == (int)Obecne.UdalostiTypy.narozeniny) // V případě narozenin informujeme i o změně narozenin
                Obecne.OnZmenaVeSvatcich();
        }

        /// <summary>
        /// Zobrazí informace k události
        /// </summary>
        private void toolStripMenuItemZobrazitUdalost_Click(object sender, EventArgs e)
        {
            Podrobnosti okno = new Podrobnosti(udalost);
            okno.StartPosition = FormStartPosition.Manual;
            okno.Location = Obecne.UmisteniOkna(((ToolStripMenuItem)sender).Owner.Location, okno.Size, 15); // Získáme pozici nabídky a u ní zobrazíme podrobnosti události
            okno.Show();
        }

        /// <summary>
        /// Po stisknutí levého tlačítka na popisku se otevře okno se zobrazením události
        /// </summary>
        private void labelUdalost_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Left)
            {
                udalost = ((Label)sender).Tag.ToString();
                if (udalost != "-1") // Je to událost nebo jen informační popisek?
                {
                    Podrobnosti okno = new Podrobnosti(udalost);
                    okno.StartPosition = FormStartPosition.Manual;
                    int x = this.ParentForm.Location.X + this.Location.X + ((MouseEventArgs)e).Location.X + ((Label)sender).Location.X; // Spočítáme X souřadnici na obrazovce tak, že získáme souřadnice nadřazených objektů a sečteme je
                    int y = this.ParentForm.Location.Y + this.Location.Y + ((MouseEventArgs)e).Location.Y + ((Label)sender).Location.Y; // Spočítáme Y souřadnici na obrazovce tak, že získáme souřadnice nadřazených objektů a sečteme je
                    okno.Location = Obecne.UmisteniOkna(new System.Drawing.Point(x, y), okno.Size, 0);
                    okno.Show();
                }
            }
        }
    }
}

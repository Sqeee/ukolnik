namespace Ukolnik
{
    partial class HlavniOkno
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HlavniOkno));
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.checkBoxSplnenost = new System.Windows.Forms.CheckBox();
            this.buttonUlozit = new System.Windows.Forms.Button();
            this.comboBoxTyp = new System.Windows.Forms.ComboBox();
            this.textBoxNadpis = new System.Windows.Forms.TextBox();
            this.textBoxZprava = new System.Windows.Forms.TextBox();
            this.textBoxCasHodina = new System.Windows.Forms.TextBox();
            this.textBoxCasMinuta = new System.Windows.Forms.TextBox();
            this.labelNovaUdalost = new System.Windows.Forms.Label();
            this.labelTyp = new System.Windows.Forms.Label();
            this.labelNadpis = new System.Windows.Forms.Label();
            this.labelZprava = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemUkolnik = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPrehled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSvatkyNarozeniny = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNastaveni = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKonec = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNapoveda = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNavrhyPripominky = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOUkolniku = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemUkolnikIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCas = new System.Windows.Forms.Label();
            this.labelCasDvojtecka = new System.Windows.Forms.Label();
            this.labelSvatek = new System.Windows.Forms.Label();
            this.labelUdalosti = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelCasUpozorneni = new System.Windows.Forms.Label();
            this.textBoxCasMinutaUpozorneni = new System.Windows.Forms.TextBox();
            this.textBoxCasHodinaUpozorneni = new System.Windows.Forms.TextBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.dateTimePickerUpozorneni = new System.Windows.Forms.DateTimePicker();
            this.labelUpozornit = new System.Windows.Forms.Label();
            this.radioButtonAno = new System.Windows.Forms.RadioButton();
            this.radioButtonNe = new System.Windows.Forms.RadioButton();
            this.labelKdy = new System.Windows.Forms.Label();
            this.labelZobrazVic = new System.Windows.Forms.Label();
            this.radioButtonVlastni = new System.Windows.Forms.RadioButton();
            this.checkBoxSouvisla = new System.Windows.Forms.CheckBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPrehledIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSvatkyNarozeninyIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNastaveniIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOUkolnikuIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemKonecIkona = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCasDvojteckaUpozorneni = new System.Windows.Forms.Label();
            this.panelNovaUdalost = new System.Windows.Forms.Panel();
            this.pokusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stranky = new Ukolnik.Stranka();
            this.alarm = new Ukolnik.Alarm();
            this.menuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.panelNovaUdalost.SuspendLayout();
            this.SuspendLayout();
            // 
            // monthCalendar
            // 
            this.monthCalendar.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.monthCalendar.Location = new System.Drawing.Point(18, 33);
            this.monthCalendar.MaxSelectionCount = 366;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ScrollChange = 2;
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 3;
            this.toolTip.SetToolTip(this.monthCalendar, "Pro vybraný den se zobrazí události na ten den, při zadávání nové události bude t" +
        "ato událost dána na vybraný den");
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // checkBoxSplnenost
            // 
            this.checkBoxSplnenost.AutoSize = true;
            this.checkBoxSplnenost.Location = new System.Drawing.Point(202, 312);
            this.checkBoxSplnenost.Name = "checkBoxSplnenost";
            this.checkBoxSplnenost.Size = new System.Drawing.Size(148, 17);
            this.checkBoxSplnenost.TabIndex = 13;
            this.checkBoxSplnenost.Text = "Zobrazit pouze nesplněné";
            this.toolTip.SetToolTip(this.checkBoxSplnenost, "Při zaškrtnutí to nezobrazuje splněné události");
            this.checkBoxSplnenost.UseVisualStyleBackColor = true;
            this.checkBoxSplnenost.CheckedChanged += new System.EventHandler(this.checkBoxSplnenost_CheckedChanged);
            // 
            // buttonUlozit
            // 
            this.buttonUlozit.Location = new System.Drawing.Point(6, 297);
            this.buttonUlozit.Name = "buttonUlozit";
            this.buttonUlozit.Size = new System.Drawing.Size(104, 23);
            this.buttonUlozit.TabIndex = 11;
            this.buttonUlozit.Text = "Uložit";
            this.toolTip.SetToolTip(this.buttonUlozit, "Uloží  událost do databáze, pokud je vyplněn nadpis a typ");
            this.buttonUlozit.UseVisualStyleBackColor = true;
            this.buttonUlozit.Click += new System.EventHandler(this.buttonUlozit_Click);
            // 
            // comboBoxTyp
            // 
            this.comboBoxTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTyp.FormattingEnabled = true;
            this.comboBoxTyp.Items.AddRange(new object[] {
            "Úkol",
            "Událost",
            "Písemka",
            "Narozeniny"});
            this.comboBoxTyp.Location = new System.Drawing.Point(52, 22);
            this.comboBoxTyp.Name = "comboBoxTyp";
            this.comboBoxTyp.Size = new System.Drawing.Size(164, 21);
            this.comboBoxTyp.TabIndex = 0;
            this.toolTip.SetToolTip(this.comboBoxTyp, resources.GetString("comboBoxTyp.ToolTip"));
            this.comboBoxTyp.SelectedIndexChanged += new System.EventHandler(this.comboBoxTyp_SelectedIndexChanged);
            // 
            // textBoxNadpis
            // 
            this.textBoxNadpis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxNadpis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxNadpis.Location = new System.Drawing.Point(52, 49);
            this.textBoxNadpis.MaxLength = 45;
            this.textBoxNadpis.Name = "textBoxNadpis";
            this.textBoxNadpis.Size = new System.Drawing.Size(164, 20);
            this.textBoxNadpis.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxNadpis, "Toto bude zobrazeno v přehledu a seznamu událostí na určitý den, nadpis by měl bý" +
        "t krátký");
            // 
            // textBoxZprava
            // 
            this.textBoxZprava.Location = new System.Drawing.Point(6, 95);
            this.textBoxZprava.Multiline = true;
            this.textBoxZprava.Name = "textBoxZprava";
            this.textBoxZprava.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxZprava.Size = new System.Drawing.Size(210, 72);
            this.textBoxZprava.TabIndex = 2;
            this.toolTip.SetToolTip(this.textBoxZprava, "Zde může být i delší text, který bude zobrazen po najetí myší na popisek události" +
        " (nadpis)");
            // 
            // textBoxCasHodina
            // 
            this.textBoxCasHodina.Location = new System.Drawing.Point(37, 173);
            this.textBoxCasHodina.MaxLength = 2;
            this.textBoxCasHodina.Name = "textBoxCasHodina";
            this.textBoxCasHodina.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasHodina.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxCasHodina, "Zde je hodina konání");
            this.textBoxCasHodina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // textBoxCasMinuta
            // 
            this.textBoxCasMinuta.Location = new System.Drawing.Point(84, 173);
            this.textBoxCasMinuta.MaxLength = 2;
            this.textBoxCasMinuta.Name = "textBoxCasMinuta";
            this.textBoxCasMinuta.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasMinuta.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBoxCasMinuta, "Zde jsou minuty konání");
            this.textBoxCasMinuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // labelNovaUdalost
            // 
            this.labelNovaUdalost.AutoSize = true;
            this.labelNovaUdalost.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNovaUdalost.Location = new System.Drawing.Point(3, 6);
            this.labelNovaUdalost.Name = "labelNovaUdalost";
            this.labelNovaUdalost.Size = new System.Drawing.Size(132, 13);
            this.labelNovaUdalost.TabIndex = 10;
            this.labelNovaUdalost.Text = "Zadání nové události:";
            // 
            // labelTyp
            // 
            this.labelTyp.AutoSize = true;
            this.labelTyp.Location = new System.Drawing.Point(3, 25);
            this.labelTyp.Name = "labelTyp";
            this.labelTyp.Size = new System.Drawing.Size(28, 13);
            this.labelTyp.TabIndex = 11;
            this.labelTyp.Text = "Typ:";
            this.toolTip.SetToolTip(this.labelTyp, "Úkol: Automaticky se neoznačí za splněný, upozorňuje na nesplnění po uplynutí ter" +
        "mínu, upozorňuje na blízké vypršení termínu");
            // 
            // labelNadpis
            // 
            this.labelNadpis.AutoSize = true;
            this.labelNadpis.Location = new System.Drawing.Point(3, 52);
            this.labelNadpis.Name = "labelNadpis";
            this.labelNadpis.Size = new System.Drawing.Size(43, 13);
            this.labelNadpis.TabIndex = 12;
            this.labelNadpis.Text = "Nadpis:";
            this.toolTip.SetToolTip(this.labelNadpis, "Toto bude zobrazeno v přehledu a seznamu událostí na určitý den, nadpis by měl bý" +
        "t krátký");
            // 
            // labelZprava
            // 
            this.labelZprava.AutoSize = true;
            this.labelZprava.Location = new System.Drawing.Point(3, 79);
            this.labelZprava.Name = "labelZprava";
            this.labelZprava.Size = new System.Drawing.Size(44, 13);
            this.labelZprava.TabIndex = 13;
            this.labelZprava.Text = "Zpráva:";
            this.toolTip.SetToolTip(this.labelZprava, "Zde může být i delší text, který bude zobrazen po najetí myší na popisek události" +
        " (nadpis)");
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemUkolnik,
            this.toolStripMenuItemNapoveda});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(790, 24);
            this.menuStrip.TabIndex = 14;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemUkolnik
            // 
            this.toolStripMenuItemUkolnik.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPrehled,
            this.toolStripMenuItemSvatkyNarozeniny,
            this.toolStripMenuItemNastaveni,
            this.toolStripMenuItemKonec});
            this.toolStripMenuItemUkolnik.Name = "toolStripMenuItemUkolnik";
            this.toolStripMenuItemUkolnik.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItemUkolnik.Text = "&Úkolník";
            // 
            // toolStripMenuItemPrehled
            // 
            this.toolStripMenuItemPrehled.Name = "toolStripMenuItemPrehled";
            this.toolStripMenuItemPrehled.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemPrehled.Text = "&Přehled";
            this.toolStripMenuItemPrehled.Click += new System.EventHandler(this.ToolStripMenuItemPrehled_Click);
            // 
            // toolStripMenuItemSvatkyNarozeniny
            // 
            this.toolStripMenuItemSvatkyNarozeniny.Name = "toolStripMenuItemSvatkyNarozeniny";
            this.toolStripMenuItemSvatkyNarozeniny.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemSvatkyNarozeniny.Text = "&Svátky a narozeniny...";
            this.toolStripMenuItemSvatkyNarozeniny.Click += new System.EventHandler(this.ToolStripMenuItemSvatkyNarozeniny_Click);
            // 
            // toolStripMenuItemNastaveni
            // 
            this.toolStripMenuItemNastaveni.Name = "toolStripMenuItemNastaveni";
            this.toolStripMenuItemNastaveni.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemNastaveni.Text = "&Nastavení...";
            this.toolStripMenuItemNastaveni.Click += new System.EventHandler(this.ToolStripMenuItemNastaveni_Click);
            // 
            // toolStripMenuItemKonec
            // 
            this.toolStripMenuItemKonec.Name = "toolStripMenuItemKonec";
            this.toolStripMenuItemKonec.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemKonec.Text = "&Konec";
            this.toolStripMenuItemKonec.Click += new System.EventHandler(this.MoznostKonec_Click);
            // 
            // toolStripMenuItemNapoveda
            // 
            this.toolStripMenuItemNapoveda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNavrhyPripominky,
            this.toolStripMenuItemOUkolniku});
            this.toolStripMenuItemNapoveda.Name = "toolStripMenuItemNapoveda";
            this.toolStripMenuItemNapoveda.Size = new System.Drawing.Size(73, 20);
            this.toolStripMenuItemNapoveda.Text = "&Nápověda";
            // 
            // toolStripMenuItemNavrhyPripominky
            // 
            this.toolStripMenuItemNavrhyPripominky.Name = "toolStripMenuItemNavrhyPripominky";
            this.toolStripMenuItemNavrhyPripominky.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemNavrhyPripominky.Text = "&Návrhy, připomínky a chyby...";
            this.toolStripMenuItemNavrhyPripominky.Click += new System.EventHandler(this.toolStripMenuItemNavrhyPripominky_Click);
            // 
            // toolStripMenuItemOUkolniku
            // 
            this.toolStripMenuItemOUkolniku.Name = "toolStripMenuItemOUkolniku";
            this.toolStripMenuItemOUkolniku.Size = new System.Drawing.Size(232, 22);
            this.toolStripMenuItemOUkolniku.Text = "&O Úkolníku";
            this.toolStripMenuItemOUkolniku.Click += new System.EventHandler(this.MenuItemOUkolniku_Click);
            // 
            // toolStripMenuItemUkolnikIkona
            // 
            this.toolStripMenuItemUkolnikIkona.Name = "toolStripMenuItemUkolnikIkona";
            this.toolStripMenuItemUkolnikIkona.Size = new System.Drawing.Size(32, 19);
            // 
            // labelCas
            // 
            this.labelCas.AutoSize = true;
            this.labelCas.Location = new System.Drawing.Point(3, 176);
            this.labelCas.Name = "labelCas";
            this.labelCas.Size = new System.Drawing.Size(28, 13);
            this.labelCas.TabIndex = 15;
            this.labelCas.Text = "Čas:";
            this.toolTip.SetToolTip(this.labelCas, "V kolik hodin proběhne událost, nebo v kolik má být úkol hotov, písemka napsána");
            // 
            // labelCasDvojtecka
            // 
            this.labelCasDvojtecka.AutoSize = true;
            this.labelCasDvojtecka.Location = new System.Drawing.Point(68, 176);
            this.labelCasDvojtecka.Name = "labelCasDvojtecka";
            this.labelCasDvojtecka.Size = new System.Drawing.Size(10, 13);
            this.labelCasDvojtecka.TabIndex = 16;
            this.labelCasDvojtecka.Text = ":";
            // 
            // labelSvatek
            // 
            this.labelSvatek.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSvatek.ForeColor = System.Drawing.Color.Black;
            this.labelSvatek.Location = new System.Drawing.Point(205, 52);
            this.labelSvatek.Name = "labelSvatek";
            this.labelSvatek.Size = new System.Drawing.Size(324, 59);
            this.labelSvatek.TabIndex = 17;
            this.labelSvatek.Text = "Svátky";
            this.toolTip.SetToolTip(this.labelSvatek, "Co je za den a kdo má svátek");
            // 
            // labelUdalosti
            // 
            this.labelUdalosti.AutoSize = true;
            this.labelUdalosti.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUdalosti.Location = new System.Drawing.Point(202, 33);
            this.labelUdalosti.Name = "labelUdalosti";
            this.labelUdalosti.Size = new System.Drawing.Size(137, 13);
            this.labelUdalosti.TabIndex = 34;
            this.labelUdalosti.Text = "Události pro tento den:";
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // labelCasUpozorneni
            // 
            this.labelCasUpozorneni.AutoSize = true;
            this.labelCasUpozorneni.Enabled = false;
            this.labelCasUpozorneni.Location = new System.Drawing.Point(3, 274);
            this.labelCasUpozorneni.Name = "labelCasUpozorneni";
            this.labelCasUpozorneni.Size = new System.Drawing.Size(28, 13);
            this.labelCasUpozorneni.TabIndex = 45;
            this.labelCasUpozorneni.Text = "Čas:";
            this.toolTip.SetToolTip(this.labelCasUpozorneni, "V kolik hodin bude upozorněno na událost");
            // 
            // textBoxCasMinutaUpozorneni
            // 
            this.textBoxCasMinutaUpozorneni.Enabled = false;
            this.textBoxCasMinutaUpozorneni.Location = new System.Drawing.Point(84, 271);
            this.textBoxCasMinutaUpozorneni.MaxLength = 2;
            this.textBoxCasMinutaUpozorneni.Name = "textBoxCasMinutaUpozorneni";
            this.textBoxCasMinutaUpozorneni.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasMinutaUpozorneni.TabIndex = 10;
            this.toolTip.SetToolTip(this.textBoxCasMinutaUpozorneni, "Zde jsou minuty upozornění");
            this.textBoxCasMinutaUpozorneni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // textBoxCasHodinaUpozorneni
            // 
            this.textBoxCasHodinaUpozorneni.Enabled = false;
            this.textBoxCasHodinaUpozorneni.Location = new System.Drawing.Point(37, 271);
            this.textBoxCasHodinaUpozorneni.MaxLength = 2;
            this.textBoxCasHodinaUpozorneni.Name = "textBoxCasHodinaUpozorneni";
            this.textBoxCasHodinaUpozorneni.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasHodinaUpozorneni.TabIndex = 9;
            this.toolTip.SetToolTip(this.textBoxCasHodinaUpozorneni, "Zde je hodina upozornění");
            this.textBoxCasHodinaUpozorneni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(116, 297);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(104, 23);
            this.buttonReset.TabIndex = 12;
            this.buttonReset.Text = "Reset";
            this.toolTip.SetToolTip(this.buttonReset, "Smaže všechen text a uvede formuláře do původního stavu");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // dateTimePickerUpozorneni
            // 
            this.dateTimePickerUpozorneni.Enabled = false;
            this.dateTimePickerUpozorneni.Location = new System.Drawing.Point(37, 245);
            this.dateTimePickerUpozorneni.Name = "dateTimePickerUpozorneni";
            this.dateTimePickerUpozorneni.Size = new System.Drawing.Size(179, 20);
            this.dateTimePickerUpozorneni.TabIndex = 8;
            this.toolTip.SetToolTip(this.dateTimePickerUpozorneni, "Kdy má být upozorněno na událost");
            // 
            // labelUpozornit
            // 
            this.labelUpozornit.AutoSize = true;
            this.labelUpozornit.Location = new System.Drawing.Point(3, 201);
            this.labelUpozornit.Name = "labelUpozornit";
            this.labelUpozornit.Size = new System.Drawing.Size(94, 13);
            this.labelUpozornit.TabIndex = 39;
            this.labelUpozornit.Text = "Upozornit předem:";
            this.toolTip.SetToolTip(this.labelUpozornit, "Zda má být na událost upozorněno předem");
            // 
            // radioButtonAno
            // 
            this.radioButtonAno.AutoSize = true;
            this.radioButtonAno.Checked = true;
            this.radioButtonAno.Location = new System.Drawing.Point(103, 199);
            this.radioButtonAno.Name = "radioButtonAno";
            this.radioButtonAno.Size = new System.Drawing.Size(44, 17);
            this.radioButtonAno.TabIndex = 6;
            this.radioButtonAno.TabStop = true;
            this.radioButtonAno.Text = "Ano";
            this.toolTip.SetToolTip(this.radioButtonAno, "Má být upozorněno na událost předem (dle nastaveného upozorňování předem v nastav" +
        "ení)");
            this.radioButtonAno.UseVisualStyleBackColor = true;
            this.radioButtonAno.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonNe
            // 
            this.radioButtonNe.AutoSize = true;
            this.radioButtonNe.Location = new System.Drawing.Point(153, 199);
            this.radioButtonNe.Name = "radioButtonNe";
            this.radioButtonNe.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNe.TabIndex = 7;
            this.radioButtonNe.Text = "Ne";
            this.toolTip.SetToolTip(this.radioButtonNe, "Nemá být upozorněno na událost předem");
            this.radioButtonNe.UseVisualStyleBackColor = true;
            this.radioButtonNe.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // labelKdy
            // 
            this.labelKdy.AutoSize = true;
            this.labelKdy.Enabled = false;
            this.labelKdy.Location = new System.Drawing.Point(3, 248);
            this.labelKdy.Name = "labelKdy";
            this.labelKdy.Size = new System.Drawing.Size(28, 13);
            this.labelKdy.TabIndex = 42;
            this.labelKdy.Text = "Kdy:";
            this.toolTip.SetToolTip(this.labelKdy, "Kdy má být upozorněno na událost");
            // 
            // labelZobrazVic
            // 
            this.labelZobrazVic.AutoSize = true;
            this.labelZobrazVic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelZobrazVic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelZobrazVic.Location = new System.Drawing.Point(768, 177);
            this.labelZobrazVic.Name = "labelZobrazVic";
            this.labelZobrazVic.Size = new System.Drawing.Size(21, 13);
            this.labelZobrazVic.TabIndex = 47;
            this.labelZobrazVic.Text = "<<";
            this.toolTip.SetToolTip(this.labelZobrazVic, "Skryje zadávání nové události");
            this.labelZobrazVic.Click += new System.EventHandler(this.labelZobrazVic_Click);
            // 
            // radioButtonVlastni
            // 
            this.radioButtonVlastni.AutoSize = true;
            this.radioButtonVlastni.Location = new System.Drawing.Point(103, 222);
            this.radioButtonVlastni.Name = "radioButtonVlastni";
            this.radioButtonVlastni.Size = new System.Drawing.Size(61, 17);
            this.radioButtonVlastni.TabIndex = 47;
            this.radioButtonVlastni.Text = "Vlastní:";
            this.toolTip.SetToolTip(this.radioButtonVlastni, "Má být upozorněno na událost předem (lze si určit kdy přesně)");
            this.radioButtonVlastni.UseVisualStyleBackColor = true;
            this.radioButtonVlastni.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // checkBoxSouvisla
            // 
            this.checkBoxSouvisla.AutoSize = true;
            this.checkBoxSouvisla.Enabled = false;
            this.checkBoxSouvisla.Location = new System.Drawing.Point(115, 175);
            this.checkBoxSouvisla.Name = "checkBoxSouvisla";
            this.checkBoxSouvisla.Size = new System.Drawing.Size(103, 17);
            this.checkBoxSouvisla.TabIndex = 48;
            this.checkBoxSouvisla.Text = "Souvislá událost";
            this.toolTip.SetToolTip(this.checkBoxSouvisla, "Pokud trvá více dní, tak při zaškrtnutí bude automaticky událost začínat v čase 0" +
        ":00\r\nBez zaškrtnutí to funguje jako opakující se událost, například jít do práce" +
        " v tolik a tolik hodin.");
            this.checkBoxSouvisla.UseVisualStyleBackColor = true;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Úkolník";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPrehledIkona,
            this.toolStripMenuItemSvatkyNarozeninyIkona,
            this.toolStripMenuItemNastaveniIkona,
            this.toolStripMenuItemOUkolnikuIkona,
            this.toolStripMenuItemKonecIkona});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(188, 114);
            // 
            // toolStripMenuItemPrehledIkona
            // 
            this.toolStripMenuItemPrehledIkona.Name = "toolStripMenuItemPrehledIkona";
            this.toolStripMenuItemPrehledIkona.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemPrehledIkona.Text = "&Přehled";
            this.toolStripMenuItemPrehledIkona.Click += new System.EventHandler(this.ToolStripMenuItemPrehled_Click);
            // 
            // toolStripMenuItemSvatkyNarozeninyIkona
            // 
            this.toolStripMenuItemSvatkyNarozeninyIkona.Name = "toolStripMenuItemSvatkyNarozeninyIkona";
            this.toolStripMenuItemSvatkyNarozeninyIkona.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemSvatkyNarozeninyIkona.Text = "&Svátky a narozeniny...";
            this.toolStripMenuItemSvatkyNarozeninyIkona.Click += new System.EventHandler(this.ToolStripMenuItemSvatkyNarozeniny_Click);
            // 
            // toolStripMenuItemNastaveniIkona
            // 
            this.toolStripMenuItemNastaveniIkona.Name = "toolStripMenuItemNastaveniIkona";
            this.toolStripMenuItemNastaveniIkona.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemNastaveniIkona.Text = "&Nastavení...";
            this.toolStripMenuItemNastaveniIkona.Click += new System.EventHandler(this.ToolStripMenuItemNastaveni_Click);
            // 
            // toolStripMenuItemOUkolnikuIkona
            // 
            this.toolStripMenuItemOUkolnikuIkona.Name = "toolStripMenuItemOUkolnikuIkona";
            this.toolStripMenuItemOUkolnikuIkona.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemOUkolnikuIkona.Text = "&O Úkolníku";
            this.toolStripMenuItemOUkolnikuIkona.Click += new System.EventHandler(this.MenuItemOUkolniku_Click);
            // 
            // toolStripMenuItemKonecIkona
            // 
            this.toolStripMenuItemKonecIkona.Name = "toolStripMenuItemKonecIkona";
            this.toolStripMenuItemKonecIkona.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemKonecIkona.Text = "&Konec";
            this.toolStripMenuItemKonecIkona.Click += new System.EventHandler(this.MoznostKonec_Click);
            // 
            // labelCasDvojteckaUpozorneni
            // 
            this.labelCasDvojteckaUpozorneni.AutoSize = true;
            this.labelCasDvojteckaUpozorneni.Enabled = false;
            this.labelCasDvojteckaUpozorneni.Location = new System.Drawing.Point(68, 274);
            this.labelCasDvojteckaUpozorneni.Name = "labelCasDvojteckaUpozorneni";
            this.labelCasDvojteckaUpozorneni.Size = new System.Drawing.Size(10, 13);
            this.labelCasDvojteckaUpozorneni.TabIndex = 46;
            this.labelCasDvojteckaUpozorneni.Text = ":";
            // 
            // panelNovaUdalost
            // 
            this.panelNovaUdalost.Controls.Add(this.checkBoxSouvisla);
            this.panelNovaUdalost.Controls.Add(this.radioButtonVlastni);
            this.panelNovaUdalost.Controls.Add(this.buttonReset);
            this.panelNovaUdalost.Controls.Add(this.labelNovaUdalost);
            this.panelNovaUdalost.Controls.Add(this.textBoxNadpis);
            this.panelNovaUdalost.Controls.Add(this.labelCasDvojteckaUpozorneni);
            this.panelNovaUdalost.Controls.Add(this.textBoxZprava);
            this.panelNovaUdalost.Controls.Add(this.textBoxCasHodina);
            this.panelNovaUdalost.Controls.Add(this.buttonUlozit);
            this.panelNovaUdalost.Controls.Add(this.labelCasUpozorneni);
            this.panelNovaUdalost.Controls.Add(this.textBoxCasMinuta);
            this.panelNovaUdalost.Controls.Add(this.textBoxCasMinutaUpozorneni);
            this.panelNovaUdalost.Controls.Add(this.labelTyp);
            this.panelNovaUdalost.Controls.Add(this.textBoxCasHodinaUpozorneni);
            this.panelNovaUdalost.Controls.Add(this.comboBoxTyp);
            this.panelNovaUdalost.Controls.Add(this.labelNadpis);
            this.panelNovaUdalost.Controls.Add(this.labelKdy);
            this.panelNovaUdalost.Controls.Add(this.labelZprava);
            this.panelNovaUdalost.Controls.Add(this.labelCas);
            this.panelNovaUdalost.Controls.Add(this.labelCasDvojtecka);
            this.panelNovaUdalost.Controls.Add(this.radioButtonAno);
            this.panelNovaUdalost.Controls.Add(this.radioButtonNe);
            this.panelNovaUdalost.Controls.Add(this.labelUpozornit);
            this.panelNovaUdalost.Controls.Add(this.dateTimePickerUpozorneni);
            this.panelNovaUdalost.Location = new System.Drawing.Point(538, 27);
            this.panelNovaUdalost.Name = "panelNovaUdalost";
            this.panelNovaUdalost.Size = new System.Drawing.Size(224, 327);
            this.panelNovaUdalost.TabIndex = 47;
            // 
            // pokusToolStripMenuItem
            // 
            this.pokusToolStripMenuItem.Name = "pokusToolStripMenuItem";
            this.pokusToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.pokusToolStripMenuItem.Text = "pokus";
            // 
            // stranky
            // 
            this.stranky.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.vpravo;
            this.stranky.Location = new System.Drawing.Point(201, 114);
            this.stranky.Name = "stranky";
            this.stranky.PrechodNarozeninDoDalsihoRoku = false;
            this.stranky.Radku = 10;
            this.stranky.Size = new System.Drawing.Size(328, 215);
            this.stranky.TabIndex = 49;
            this.stranky.TextNulaPolozek = "Pro tento den nejsou žádné události";
            this.stranky.ZavritPrazdne = false;
            this.stranky.ZobrazDatum = false;
            this.stranky.ZrychleneVykreslovani = true;
            // 
            // HlavniOkno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 366);
            this.Controls.Add(this.checkBoxSplnenost);
            this.Controls.Add(this.labelUdalosti);
            this.Controls.Add(this.labelZobrazVic);
            this.Controls.Add(this.labelSvatek);
            this.Controls.Add(this.stranky);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panelNovaUdalost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(806, 394);
            this.MinimumSize = new System.Drawing.Size(573, 394);
            this.Name = "HlavniOkno";
            this.Text = "Úkolník";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HlavniOkno_FormClosing);
            this.Resize += new System.EventHandler(this.HlavniOkno_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.panelNovaUdalost.ResumeLayout(false);
            this.panelNovaUdalost.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.CheckBox checkBoxSplnenost;
        private System.Windows.Forms.Button buttonUlozit;
        private System.Windows.Forms.ComboBox comboBoxTyp;
        private System.Windows.Forms.TextBox textBoxNadpis;
        private System.Windows.Forms.TextBox textBoxZprava;
        private System.Windows.Forms.TextBox textBoxCasHodina;
        private System.Windows.Forms.TextBox textBoxCasMinuta;
        private System.Windows.Forms.Label labelNovaUdalost;
        private System.Windows.Forms.Label labelTyp;
        private System.Windows.Forms.Label labelNadpis;
        private System.Windows.Forms.Label labelZprava;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUkolnikIkona;
        private System.Windows.Forms.Label labelCas;
        private System.Windows.Forms.Label labelCasDvojtecka;
        internal System.Windows.Forms.Label labelSvatek;
        private System.Windows.Forms.Label labelUdalosti;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemUkolnik;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNapoveda;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPrehled;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSvatkyNarozeniny;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNastaveni;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKonec;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOUkolniku;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPrehledIkona;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSvatkyNarozeninyIkona;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNastaveniIkona;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOUkolnikuIkona;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemKonecIkona;
        private System.Windows.Forms.DateTimePicker dateTimePickerUpozorneni;
        private System.Windows.Forms.Label labelUpozornit;
        private System.Windows.Forms.RadioButton radioButtonAno;
        private System.Windows.Forms.RadioButton radioButtonNe;
        private System.Windows.Forms.Label labelKdy;
        private System.Windows.Forms.Label labelCasDvojteckaUpozorneni;
        private System.Windows.Forms.Label labelCasUpozorneni;
        private System.Windows.Forms.TextBox textBoxCasMinutaUpozorneni;
        private System.Windows.Forms.TextBox textBoxCasHodinaUpozorneni;
        private System.Windows.Forms.Panel panelNovaUdalost;
        private System.Windows.Forms.Label labelZobrazVic;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem pokusToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonVlastni;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNavrhyPripominky;
        private Alarm alarm;
        private Stranka stranky;
        private System.Windows.Forms.CheckBox checkBoxSouvisla;

    }
}


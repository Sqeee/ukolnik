namespace Ukolnik
{
    partial class Uprava
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Uprava));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonReset = new System.Windows.Forms.Button();
            this.textBoxNadpis = new System.Windows.Forms.TextBox();
            this.textBoxZprava = new System.Windows.Forms.TextBox();
            this.textBoxCasHodina = new System.Windows.Forms.TextBox();
            this.buttonUlozit = new System.Windows.Forms.Button();
            this.labelCasUpozorneni = new System.Windows.Forms.Label();
            this.textBoxCasMinuta = new System.Windows.Forms.TextBox();
            this.textBoxCasMinutaUpozorneni = new System.Windows.Forms.TextBox();
            this.labelTyp = new System.Windows.Forms.Label();
            this.textBoxCasHodinaUpozorneni = new System.Windows.Forms.TextBox();
            this.comboBoxTyp = new System.Windows.Forms.ComboBox();
            this.labelNadpis = new System.Windows.Forms.Label();
            this.labelKdy = new System.Windows.Forms.Label();
            this.labelZprava = new System.Windows.Forms.Label();
            this.labelCas = new System.Windows.Forms.Label();
            this.radioButtonAno = new System.Windows.Forms.RadioButton();
            this.radioButtonNe = new System.Windows.Forms.RadioButton();
            this.labelUpozornit = new System.Windows.Forms.Label();
            this.dateTimePickerUpozorneni = new System.Windows.Forms.DateTimePicker();
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.comboBoxSplneno = new System.Windows.Forms.ComboBox();
            this.labelSplneno = new System.Windows.Forms.Label();
            this.buttonSmazat = new System.Windows.Forms.Button();
            this.buttonNeukladat = new System.Windows.Forms.Button();
            this.radioButtonVlastni = new System.Windows.Forms.RadioButton();
            this.panelNovaUdalost = new System.Windows.Forms.Panel();
            this.labelCasDvojteckaUpozorneni = new System.Windows.Forms.Label();
            this.labelCasDvojtecka = new System.Windows.Forms.Label();
            this.alarm = new Ukolnik.Alarm();
            this.panelNovaUdalost.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(219, 328);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(91, 23);
            this.buttonReset.TabIndex = 14;
            this.buttonReset.Text = "Reset";
            this.toolTip.SetToolTip(this.buttonReset, "Smaže všechen text a uvede formuláře do původního stavu");
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // textBoxNadpis
            // 
            this.textBoxNadpis.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.textBoxNadpis.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxNadpis.Location = new System.Drawing.Point(52, 34);
            this.textBoxNadpis.MaxLength = 45;
            this.textBoxNadpis.Name = "textBoxNadpis";
            this.textBoxNadpis.Size = new System.Drawing.Size(164, 20);
            this.textBoxNadpis.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxNadpis, "Toto bude zobrazeno v přehledu a seznamu událostí na určitý den, nadpis by měl bý" +
        "t krátký");
            // 
            // textBoxZprava
            // 
            this.textBoxZprava.Location = new System.Drawing.Point(6, 80);
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
            this.textBoxCasHodina.Location = new System.Drawing.Point(37, 158);
            this.textBoxCasHodina.MaxLength = 2;
            this.textBoxCasHodina.Name = "textBoxCasHodina";
            this.textBoxCasHodina.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasHodina.TabIndex = 4;
            this.toolTip.SetToolTip(this.textBoxCasHodina, "Zde je hodina konání");
            this.textBoxCasHodina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // buttonUlozit
            // 
            this.buttonUlozit.Location = new System.Drawing.Point(25, 328);
            this.buttonUlozit.Name = "buttonUlozit";
            this.buttonUlozit.Size = new System.Drawing.Size(91, 23);
            this.buttonUlozit.TabIndex = 12;
            this.buttonUlozit.Text = "Uložit";
            this.toolTip.SetToolTip(this.buttonUlozit, "Uloží  událost do databáze, pokud je vyplněn nadpis a typ");
            this.buttonUlozit.UseVisualStyleBackColor = true;
            this.buttonUlozit.Click += new System.EventHandler(this.buttonUlozit_Click);
            // 
            // labelCasUpozorneni
            // 
            this.labelCasUpozorneni.AutoSize = true;
            this.labelCasUpozorneni.Location = new System.Drawing.Point(3, 259);
            this.labelCasUpozorneni.Name = "labelCasUpozorneni";
            this.labelCasUpozorneni.Size = new System.Drawing.Size(28, 13);
            this.labelCasUpozorneni.TabIndex = 45;
            this.labelCasUpozorneni.Text = "Čas:";
            this.toolTip.SetToolTip(this.labelCasUpozorneni, "V kolik hodin bude upozorněno na událost");
            // 
            // textBoxCasMinuta
            // 
            this.textBoxCasMinuta.Location = new System.Drawing.Point(84, 158);
            this.textBoxCasMinuta.MaxLength = 2;
            this.textBoxCasMinuta.Name = "textBoxCasMinuta";
            this.textBoxCasMinuta.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasMinuta.TabIndex = 5;
            this.toolTip.SetToolTip(this.textBoxCasMinuta, "Zde jsou minuty konání");
            this.textBoxCasMinuta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // textBoxCasMinutaUpozorneni
            // 
            this.textBoxCasMinutaUpozorneni.Location = new System.Drawing.Point(84, 256);
            this.textBoxCasMinutaUpozorneni.MaxLength = 2;
            this.textBoxCasMinutaUpozorneni.Name = "textBoxCasMinutaUpozorneni";
            this.textBoxCasMinutaUpozorneni.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasMinutaUpozorneni.TabIndex = 10;
            this.toolTip.SetToolTip(this.textBoxCasMinutaUpozorneni, "Zde jsou minuty upozornění");
            this.textBoxCasMinutaUpozorneni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
            // 
            // labelTyp
            // 
            this.labelTyp.AutoSize = true;
            this.labelTyp.Location = new System.Drawing.Point(3, 10);
            this.labelTyp.Name = "labelTyp";
            this.labelTyp.Size = new System.Drawing.Size(28, 13);
            this.labelTyp.TabIndex = 11;
            this.labelTyp.Text = "Typ:";
            this.toolTip.SetToolTip(this.labelTyp, "Úkol: Automaticky se neoznačí za splněný, upozorňuje na nesplnění po uplynutí ter" +
        "mínu, upozorňuje na blízké vypršení termínu");
            // 
            // textBoxCasHodinaUpozorneni
            // 
            this.textBoxCasHodinaUpozorneni.Location = new System.Drawing.Point(37, 256);
            this.textBoxCasHodinaUpozorneni.MaxLength = 2;
            this.textBoxCasHodinaUpozorneni.Name = "textBoxCasHodinaUpozorneni";
            this.textBoxCasHodinaUpozorneni.Size = new System.Drawing.Size(25, 20);
            this.textBoxCasHodinaUpozorneni.TabIndex = 9;
            this.toolTip.SetToolTip(this.textBoxCasHodinaUpozorneni, "Zde je hodina upozornění");
            this.textBoxCasHodinaUpozorneni.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCas_KeyPress);
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
            this.comboBoxTyp.Location = new System.Drawing.Point(52, 7);
            this.comboBoxTyp.Name = "comboBoxTyp";
            this.comboBoxTyp.Size = new System.Drawing.Size(164, 21);
            this.comboBoxTyp.TabIndex = 0;
            this.toolTip.SetToolTip(this.comboBoxTyp, resources.GetString("comboBoxTyp.ToolTip"));
            this.comboBoxTyp.SelectedIndexChanged += new System.EventHandler(this.comboBoxTyp_SelectedIndexChanged);
            // 
            // labelNadpis
            // 
            this.labelNadpis.AutoSize = true;
            this.labelNadpis.Location = new System.Drawing.Point(3, 37);
            this.labelNadpis.Name = "labelNadpis";
            this.labelNadpis.Size = new System.Drawing.Size(43, 13);
            this.labelNadpis.TabIndex = 12;
            this.labelNadpis.Text = "Nadpis:";
            this.toolTip.SetToolTip(this.labelNadpis, "Toto bude zobrazeno v přehledu a seznamu událostí na určitý den, nadpis by měl bý" +
        "t krátký");
            // 
            // labelKdy
            // 
            this.labelKdy.AutoSize = true;
            this.labelKdy.Location = new System.Drawing.Point(3, 233);
            this.labelKdy.Name = "labelKdy";
            this.labelKdy.Size = new System.Drawing.Size(28, 13);
            this.labelKdy.TabIndex = 42;
            this.labelKdy.Text = "Kdy:";
            this.toolTip.SetToolTip(this.labelKdy, "Kdy má být upozorněno na událost");
            // 
            // labelZprava
            // 
            this.labelZprava.AutoSize = true;
            this.labelZprava.Location = new System.Drawing.Point(3, 64);
            this.labelZprava.Name = "labelZprava";
            this.labelZprava.Size = new System.Drawing.Size(44, 13);
            this.labelZprava.TabIndex = 13;
            this.labelZprava.Text = "Zpráva:";
            this.toolTip.SetToolTip(this.labelZprava, "Zde může být i delší text, který bude zobrazen po najetí myší na popisek události" +
        " (nadpis)");
            // 
            // labelCas
            // 
            this.labelCas.AutoSize = true;
            this.labelCas.Location = new System.Drawing.Point(3, 161);
            this.labelCas.Name = "labelCas";
            this.labelCas.Size = new System.Drawing.Size(28, 13);
            this.labelCas.TabIndex = 15;
            this.labelCas.Text = "Čas:";
            this.toolTip.SetToolTip(this.labelCas, "V kolik hodin proběhne událost, nebo v kolik má být úkol hotov, písemka napsána");
            // 
            // radioButtonAno
            // 
            this.radioButtonAno.AutoSize = true;
            this.radioButtonAno.Location = new System.Drawing.Point(103, 184);
            this.radioButtonAno.Name = "radioButtonAno";
            this.radioButtonAno.Size = new System.Drawing.Size(44, 17);
            this.radioButtonAno.TabIndex = 6;
            this.radioButtonAno.Text = "Ano";
            this.toolTip.SetToolTip(this.radioButtonAno, "Má být upozorněno na událost předem (dle nastaveného upozorňování předem v nastav" +
        "ení)");
            this.radioButtonAno.UseVisualStyleBackColor = true;
            this.radioButtonAno.CheckedChanged += new System.EventHandler(this.radioButtonUpozorneni_CheckedChanged);
            // 
            // radioButtonNe
            // 
            this.radioButtonNe.AutoSize = true;
            this.radioButtonNe.Location = new System.Drawing.Point(153, 184);
            this.radioButtonNe.Name = "radioButtonNe";
            this.radioButtonNe.Size = new System.Drawing.Size(39, 17);
            this.radioButtonNe.TabIndex = 7;
            this.radioButtonNe.Text = "Ne";
            this.toolTip.SetToolTip(this.radioButtonNe, "Nemá být upozorněno na událost předem");
            this.radioButtonNe.UseVisualStyleBackColor = true;
            this.radioButtonNe.CheckedChanged += new System.EventHandler(this.radioButtonUpozorneni_CheckedChanged);
            // 
            // labelUpozornit
            // 
            this.labelUpozornit.AutoSize = true;
            this.labelUpozornit.Location = new System.Drawing.Point(3, 186);
            this.labelUpozornit.Name = "labelUpozornit";
            this.labelUpozornit.Size = new System.Drawing.Size(94, 13);
            this.labelUpozornit.TabIndex = 39;
            this.labelUpozornit.Text = "Upozornit předem:";
            this.toolTip.SetToolTip(this.labelUpozornit, "Zda má být na událost upozorněno předem");
            // 
            // dateTimePickerUpozorneni
            // 
            this.dateTimePickerUpozorneni.Location = new System.Drawing.Point(37, 230);
            this.dateTimePickerUpozorneni.Name = "dateTimePickerUpozorneni";
            this.dateTimePickerUpozorneni.Size = new System.Drawing.Size(179, 20);
            this.dateTimePickerUpozorneni.TabIndex = 8;
            this.toolTip.SetToolTip(this.dateTimePickerUpozorneni, "Kdy má být upozorněno na událost");
            // 
            // monthCalendar
            // 
            this.monthCalendar.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.monthCalendar.Location = new System.Drawing.Point(248, 10);
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.Name = "monthCalendar";
            this.monthCalendar.ScrollChange = 2;
            this.monthCalendar.ShowWeekNumbers = true;
            this.monthCalendar.TabIndex = 3;
            this.toolTip.SetToolTip(this.monthCalendar, "Kdy se daná událost koná");
            this.monthCalendar.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar_DateChanged);
            // 
            // comboBoxSplneno
            // 
            this.comboBoxSplneno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSplneno.FormattingEnabled = true;
            this.comboBoxSplneno.Items.AddRange(new object[] {
            "Ne",
            "Ano"});
            this.comboBoxSplneno.Location = new System.Drawing.Point(58, 282);
            this.comboBoxSplneno.Name = "comboBoxSplneno";
            this.comboBoxSplneno.Size = new System.Drawing.Size(158, 21);
            this.comboBoxSplneno.TabIndex = 11;
            this.toolTip.SetToolTip(this.comboBoxSplneno, "Zda je událost splněná");
            // 
            // labelSplneno
            // 
            this.labelSplneno.AutoSize = true;
            this.labelSplneno.Location = new System.Drawing.Point(3, 285);
            this.labelSplneno.Name = "labelSplneno";
            this.labelSplneno.Size = new System.Drawing.Size(49, 13);
            this.labelSplneno.TabIndex = 48;
            this.labelSplneno.Text = "Splněno:";
            this.toolTip.SetToolTip(this.labelSplneno, "Zda je událost splněná");
            // 
            // buttonSmazat
            // 
            this.buttonSmazat.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonSmazat.Location = new System.Drawing.Point(316, 328);
            this.buttonSmazat.Name = "buttonSmazat";
            this.buttonSmazat.Size = new System.Drawing.Size(91, 23);
            this.buttonSmazat.TabIndex = 15;
            this.buttonSmazat.Text = "Smazat událost";
            this.toolTip.SetToolTip(this.buttonSmazat, "Nenávratně smaže událost");
            this.buttonSmazat.UseVisualStyleBackColor = true;
            this.buttonSmazat.Click += new System.EventHandler(this.buttonSmazat_Click);
            // 
            // buttonNeukladat
            // 
            this.buttonNeukladat.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNeukladat.Location = new System.Drawing.Point(122, 328);
            this.buttonNeukladat.Name = "buttonNeukladat";
            this.buttonNeukladat.Size = new System.Drawing.Size(91, 23);
            this.buttonNeukladat.TabIndex = 13;
            this.buttonNeukladat.Text = "Neukládat";
            this.toolTip.SetToolTip(this.buttonNeukladat, "Změny nebudou uloženy");
            this.buttonNeukladat.UseVisualStyleBackColor = true;
            this.buttonNeukladat.Click += new System.EventHandler(this.buttonNeukladat_Click);
            // 
            // radioButtonVlastni
            // 
            this.radioButtonVlastni.AutoSize = true;
            this.radioButtonVlastni.Checked = true;
            this.radioButtonVlastni.Location = new System.Drawing.Point(103, 207);
            this.radioButtonVlastni.Name = "radioButtonVlastni";
            this.radioButtonVlastni.Size = new System.Drawing.Size(61, 17);
            this.radioButtonVlastni.TabIndex = 49;
            this.radioButtonVlastni.TabStop = true;
            this.radioButtonVlastni.Text = "Vlastní:";
            this.toolTip.SetToolTip(this.radioButtonVlastni, "Má být upozorněno na událost předem (lze si určit kdy přesně)");
            this.radioButtonVlastni.UseVisualStyleBackColor = true;
            this.radioButtonVlastni.CheckedChanged += new System.EventHandler(this.radioButtonUpozorneni_CheckedChanged);
            // 
            // panelNovaUdalost
            // 
            this.panelNovaUdalost.Controls.Add(this.radioButtonVlastni);
            this.panelNovaUdalost.Controls.Add(this.labelSplneno);
            this.panelNovaUdalost.Controls.Add(this.comboBoxSplneno);
            this.panelNovaUdalost.Controls.Add(this.textBoxNadpis);
            this.panelNovaUdalost.Controls.Add(this.labelCasDvojteckaUpozorneni);
            this.panelNovaUdalost.Controls.Add(this.textBoxZprava);
            this.panelNovaUdalost.Controls.Add(this.textBoxCasHodina);
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
            this.panelNovaUdalost.Location = new System.Drawing.Point(12, 15);
            this.panelNovaUdalost.Name = "panelNovaUdalost";
            this.panelNovaUdalost.Size = new System.Drawing.Size(224, 307);
            this.panelNovaUdalost.TabIndex = 48;
            // 
            // labelCasDvojteckaUpozorneni
            // 
            this.labelCasDvojteckaUpozorneni.AutoSize = true;
            this.labelCasDvojteckaUpozorneni.Location = new System.Drawing.Point(68, 259);
            this.labelCasDvojteckaUpozorneni.Name = "labelCasDvojteckaUpozorneni";
            this.labelCasDvojteckaUpozorneni.Size = new System.Drawing.Size(10, 13);
            this.labelCasDvojteckaUpozorneni.TabIndex = 46;
            this.labelCasDvojteckaUpozorneni.Text = ":";
            // 
            // labelCasDvojtecka
            // 
            this.labelCasDvojtecka.AutoSize = true;
            this.labelCasDvojtecka.Location = new System.Drawing.Point(68, 161);
            this.labelCasDvojtecka.Name = "labelCasDvojtecka";
            this.labelCasDvojtecka.Size = new System.Drawing.Size(10, 13);
            this.labelCasDvojtecka.TabIndex = 16;
            this.labelCasDvojtecka.Text = ":";
            // 
            // Uprava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 363);
            this.Controls.Add(this.buttonNeukladat);
            this.Controls.Add(this.buttonSmazat);
            this.Controls.Add(this.monthCalendar);
            this.Controls.Add(this.panelNovaUdalost);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonUlozit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Uprava";
            this.Text = "Úprava události ID: ";
            this.panelNovaUdalost.ResumeLayout(false);
            this.panelNovaUdalost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel panelNovaUdalost;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.TextBox textBoxNadpis;
        private System.Windows.Forms.Label labelCasDvojteckaUpozorneni;
        private System.Windows.Forms.TextBox textBoxZprava;
        private System.Windows.Forms.TextBox textBoxCasHodina;
        private System.Windows.Forms.Button buttonUlozit;
        private System.Windows.Forms.Label labelCasUpozorneni;
        private System.Windows.Forms.TextBox textBoxCasMinuta;
        private System.Windows.Forms.TextBox textBoxCasMinutaUpozorneni;
        private System.Windows.Forms.Label labelTyp;
        private System.Windows.Forms.TextBox textBoxCasHodinaUpozorneni;
        private System.Windows.Forms.ComboBox comboBoxTyp;
        private System.Windows.Forms.Label labelNadpis;
        private System.Windows.Forms.Label labelKdy;
        private System.Windows.Forms.Label labelZprava;
        private System.Windows.Forms.Label labelCas;
        private System.Windows.Forms.Label labelCasDvojtecka;
        private System.Windows.Forms.RadioButton radioButtonAno;
        private System.Windows.Forms.RadioButton radioButtonNe;
        private System.Windows.Forms.Label labelUpozornit;
        private System.Windows.Forms.DateTimePicker dateTimePickerUpozorneni;
        private System.Windows.Forms.MonthCalendar monthCalendar;
        private System.Windows.Forms.Label labelSplneno;
        private System.Windows.Forms.ComboBox comboBoxSplneno;
        private System.Windows.Forms.Button buttonSmazat;
        private System.Windows.Forms.Button buttonNeukladat;
        private System.Windows.Forms.RadioButton radioButtonVlastni;
        private Alarm alarm;
    }
}
namespace Ukolnik
{
    partial class NastavovaciOkno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NastavovaciOkno));
            this.buttonUlozit = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxServer = new System.Windows.Forms.TextBox();
            this.textBoxUzivatel = new System.Windows.Forms.TextBox();
            this.textBoxHeslo = new System.Windows.Forms.TextBox();
            this.textBoxDatabaze = new System.Windows.Forms.TextBox();
            this.textBoxHodiny = new System.Windows.Forms.TextBox();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelUzivatel = new System.Windows.Forms.Label();
            this.labelHeslo = new System.Windows.Forms.Label();
            this.labelDatabaze = new System.Windows.Forms.Label();
            this.comboBoxSpousteni = new System.Windows.Forms.ComboBox();
            this.comboBoxChyby = new System.Windows.Forms.ComboBox();
            this.labelSpousteni = new System.Windows.Forms.Label();
            this.labelChyby = new System.Windows.Forms.Label();
            this.textBoxDny = new System.Windows.Forms.TextBox();
            this.labelUpozorneni = new System.Windows.Forms.Label();
            this.labelDny = new System.Windows.Forms.Label();
            this.labelHodiny = new System.Windows.Forms.Label();
            this.textBoxMinuty = new System.Windows.Forms.TextBox();
            this.labelMinuty = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelVarovani = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonUlozit
            // 
            this.buttonUlozit.Location = new System.Drawing.Point(66, 248);
            this.buttonUlozit.Name = "buttonUlozit";
            this.buttonUlozit.Size = new System.Drawing.Size(104, 23);
            this.buttonUlozit.TabIndex = 9;
            this.buttonUlozit.Text = "Uložit";
            this.toolTip.SetToolTip(this.buttonUlozit, "Uloží nové nastavení");
            this.buttonUlozit.UseVisualStyleBackColor = true;
            this.buttonUlozit.Click += new System.EventHandler(this.buttonUlozit_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonCancel.Location = new System.Drawing.Point(176, 248);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(104, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Neukládat";
            this.toolTip.SetToolTip(this.buttonCancel, "Změny v nastavení nebudou uloženy");
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxServer
            // 
            this.textBoxServer.Location = new System.Drawing.Point(160, 33);
            this.textBoxServer.Name = "textBoxServer";
            this.textBoxServer.Size = new System.Drawing.Size(174, 20);
            this.textBoxServer.TabIndex = 0;
            this.toolTip.SetToolTip(this.textBoxServer, "Adresa serveru, kde běží databáze (zpravidla 127.0.0.1)");
            // 
            // textBoxUzivatel
            // 
            this.textBoxUzivatel.Location = new System.Drawing.Point(160, 59);
            this.textBoxUzivatel.Name = "textBoxUzivatel";
            this.textBoxUzivatel.Size = new System.Drawing.Size(174, 20);
            this.textBoxUzivatel.TabIndex = 1;
            this.toolTip.SetToolTip(this.textBoxUzivatel, "Přihlašovací jméno k databázi");
            // 
            // textBoxHeslo
            // 
            this.textBoxHeslo.Location = new System.Drawing.Point(160, 85);
            this.textBoxHeslo.Name = "textBoxHeslo";
            this.textBoxHeslo.PasswordChar = '*';
            this.textBoxHeslo.Size = new System.Drawing.Size(174, 20);
            this.textBoxHeslo.TabIndex = 2;
            this.toolTip.SetToolTip(this.textBoxHeslo, "Heslo pro přihlášení k databázi");
            // 
            // textBoxDatabaze
            // 
            this.textBoxDatabaze.Location = new System.Drawing.Point(160, 111);
            this.textBoxDatabaze.Name = "textBoxDatabaze";
            this.textBoxDatabaze.Size = new System.Drawing.Size(174, 20);
            this.textBoxDatabaze.TabIndex = 3;
            this.toolTip.SetToolTip(this.textBoxDatabaze, "Databáze obsahující tabulky Úkolníku, zpravidla je to databáze ukolnik");
            // 
            // textBoxHodiny
            // 
            this.textBoxHodiny.Location = new System.Drawing.Point(226, 191);
            this.textBoxHodiny.MaxLength = 2;
            this.textBoxHodiny.Name = "textBoxHodiny";
            this.textBoxHodiny.Size = new System.Drawing.Size(25, 20);
            this.textBoxHodiny.TabIndex = 7;
            this.toolTip.SetToolTip(this.textBoxHodiny, "Kolik hodin předem se má upozorňovat na události, které nemají vlastní upozornění" +
        "");
            this.textBoxHodiny.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // labelServer
            // 
            this.labelServer.AutoSize = true;
            this.labelServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelServer.Location = new System.Drawing.Point(13, 36);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(48, 13);
            this.labelServer.TabIndex = 11;
            this.labelServer.Text = "Server:";
            // 
            // labelUzivatel
            // 
            this.labelUzivatel.AutoSize = true;
            this.labelUzivatel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUzivatel.Location = new System.Drawing.Point(13, 62);
            this.labelUzivatel.Name = "labelUzivatel";
            this.labelUzivatel.Size = new System.Drawing.Size(57, 13);
            this.labelUzivatel.TabIndex = 12;
            this.labelUzivatel.Text = "Uživatel:";
            // 
            // labelHeslo
            // 
            this.labelHeslo.AutoSize = true;
            this.labelHeslo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelHeslo.Location = new System.Drawing.Point(13, 88);
            this.labelHeslo.Name = "labelHeslo";
            this.labelHeslo.Size = new System.Drawing.Size(43, 13);
            this.labelHeslo.TabIndex = 13;
            this.labelHeslo.Text = "Heslo:";
            // 
            // labelDatabaze
            // 
            this.labelDatabaze.AutoSize = true;
            this.labelDatabaze.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDatabaze.Location = new System.Drawing.Point(13, 114);
            this.labelDatabaze.Name = "labelDatabaze";
            this.labelDatabaze.Size = new System.Drawing.Size(65, 13);
            this.labelDatabaze.TabIndex = 14;
            this.labelDatabaze.Text = "Databáze:";
            // 
            // comboBoxSpousteni
            // 
            this.comboBoxSpousteni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSpousteni.FormattingEnabled = true;
            this.comboBoxSpousteni.Items.AddRange(new object[] {
            "Ano",
            "Ne"});
            this.comboBoxSpousteni.Location = new System.Drawing.Point(160, 137);
            this.comboBoxSpousteni.Name = "comboBoxSpousteni";
            this.comboBoxSpousteni.Size = new System.Drawing.Size(174, 21);
            this.comboBoxSpousteni.TabIndex = 4;
            this.toolTip.SetToolTip(this.comboBoxSpousteni, "Zda se má Úkolník spouštět sám při startu počítače");
            // 
            // comboBoxChyby
            // 
            this.comboBoxChyby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChyby.FormattingEnabled = true;
            this.comboBoxChyby.Items.AddRange(new object[] {
            "Ano",
            "Ne"});
            this.comboBoxChyby.Location = new System.Drawing.Point(160, 164);
            this.comboBoxChyby.Name = "comboBoxChyby";
            this.comboBoxChyby.Size = new System.Drawing.Size(174, 21);
            this.comboBoxChyby.TabIndex = 5;
            this.toolTip.SetToolTip(this.comboBoxChyby, "Zda se mají zobrazovat podrobné popisky chyb -> obyčejným uživatelům jsou podrobn" +
        "é popisky chyb k ničemu, slouží vývojářům");
            // 
            // labelSpousteni
            // 
            this.labelSpousteni.AutoSize = true;
            this.labelSpousteni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSpousteni.Location = new System.Drawing.Point(13, 140);
            this.labelSpousteni.Name = "labelSpousteni";
            this.labelSpousteni.Size = new System.Drawing.Size(141, 13);
            this.labelSpousteni.TabIndex = 15;
            this.labelSpousteni.Text = "Automatické spouštění:";
            // 
            // labelChyby
            // 
            this.labelChyby.AutoSize = true;
            this.labelChyby.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelChyby.Location = new System.Drawing.Point(13, 167);
            this.labelChyby.Name = "labelChyby";
            this.labelChyby.Size = new System.Drawing.Size(128, 13);
            this.labelChyby.TabIndex = 16;
            this.labelChyby.Text = "Podrobný výpis chyb:";
            // 
            // textBoxDny
            // 
            this.textBoxDny.Location = new System.Drawing.Point(160, 191);
            this.textBoxDny.MaxLength = 3;
            this.textBoxDny.Name = "textBoxDny";
            this.textBoxDny.Size = new System.Drawing.Size(25, 20);
            this.textBoxDny.TabIndex = 6;
            this.toolTip.SetToolTip(this.textBoxDny, "Kolik dní předem se má upozorňovat na události, které nemají vlastní upozornění");
            this.textBoxDny.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // labelUpozorneni
            // 
            this.labelUpozorneni.AutoSize = true;
            this.labelUpozorneni.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelUpozorneni.Location = new System.Drawing.Point(13, 193);
            this.labelUpozorneni.Name = "labelUpozorneni";
            this.labelUpozorneni.Size = new System.Drawing.Size(111, 13);
            this.labelUpozorneni.TabIndex = 17;
            this.labelUpozorneni.Text = "Upozornit předem:";
            // 
            // labelDny
            // 
            this.labelDny.AutoSize = true;
            this.labelDny.Location = new System.Drawing.Point(193, 194);
            this.labelDny.Name = "labelDny";
            this.labelDny.Size = new System.Drawing.Size(28, 13);
            this.labelDny.TabIndex = 18;
            this.labelDny.Text = "dnů,";
            // 
            // labelHodiny
            // 
            this.labelHodiny.AutoSize = true;
            this.labelHodiny.Location = new System.Drawing.Point(257, 194);
            this.labelHodiny.Name = "labelHodiny";
            this.labelHodiny.Size = new System.Drawing.Size(42, 13);
            this.labelHodiny.TabIndex = 19;
            this.labelHodiny.Text = "hodin a";
            // 
            // textBoxMinuty
            // 
            this.textBoxMinuty.Location = new System.Drawing.Point(160, 217);
            this.textBoxMinuty.MaxLength = 2;
            this.textBoxMinuty.Name = "textBoxMinuty";
            this.textBoxMinuty.Size = new System.Drawing.Size(25, 20);
            this.textBoxMinuty.TabIndex = 8;
            this.toolTip.SetToolTip(this.textBoxMinuty, "Kolik minut předem se má upozorňovat na události, které nemají vlastní upozornění" +
        "");
            this.textBoxMinuty.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // labelMinuty
            // 
            this.labelMinuty.AutoSize = true;
            this.labelMinuty.Location = new System.Drawing.Point(191, 220);
            this.labelMinuty.Name = "labelMinuty";
            this.labelMinuty.Size = new System.Drawing.Size(35, 13);
            this.labelMinuty.TabIndex = 20;
            this.labelMinuty.Text = "minut.";
            // 
            // labelVarovani
            // 
            this.labelVarovani.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.labelVarovani.Location = new System.Drawing.Point(12, 2);
            this.labelVarovani.Name = "labelVarovani";
            this.labelVarovani.Size = new System.Drawing.Size(323, 28);
            this.labelVarovani.TabIndex = 21;
            this.labelVarovani.Text = "Chybné nastavení aplikace, které neumožňuje pokračovat aplikaci v běhu. Pokud neb" +
    "ude změněno, tak se aplikace ukončí.";
            this.labelVarovani.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelVarovani.Visible = false;
            // 
            // NastavovaciOkno
            // 
            this.AcceptButton = this.buttonUlozit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(347, 298);
            this.Controls.Add(this.labelVarovani);
            this.Controls.Add(this.labelMinuty);
            this.Controls.Add(this.textBoxMinuty);
            this.Controls.Add(this.labelHodiny);
            this.Controls.Add(this.labelDny);
            this.Controls.Add(this.labelUpozorneni);
            this.Controls.Add(this.textBoxDny);
            this.Controls.Add(this.labelChyby);
            this.Controls.Add(this.labelSpousteni);
            this.Controls.Add(this.comboBoxChyby);
            this.Controls.Add(this.comboBoxSpousteni);
            this.Controls.Add(this.labelDatabaze);
            this.Controls.Add(this.labelHeslo);
            this.Controls.Add(this.labelUzivatel);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.textBoxHodiny);
            this.Controls.Add(this.textBoxDatabaze);
            this.Controls.Add(this.textBoxHeslo);
            this.Controls.Add(this.textBoxUzivatel);
            this.Controls.Add(this.textBoxServer);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonUlozit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NastavovaciOkno";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nastavení";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NastavovaciOkno_FormClosing);
            this.Load += new System.EventHandler(this.NastavovaciOkno_Load);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUlozit;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxServer;
        private System.Windows.Forms.TextBox textBoxUzivatel;
        private System.Windows.Forms.TextBox textBoxHeslo;
        private System.Windows.Forms.TextBox textBoxDatabaze;
        private System.Windows.Forms.TextBox textBoxHodiny;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelUzivatel;
        private System.Windows.Forms.Label labelHeslo;
        private System.Windows.Forms.Label labelDatabaze;
        private System.Windows.Forms.ComboBox comboBoxSpousteni;
        private System.Windows.Forms.ComboBox comboBoxChyby;
        private System.Windows.Forms.Label labelSpousteni;
        private System.Windows.Forms.Label labelChyby;
        private System.Windows.Forms.TextBox textBoxDny;
        private System.Windows.Forms.Label labelUpozorneni;
        private System.Windows.Forms.Label labelDny;
        private System.Windows.Forms.Label labelHodiny;
        private System.Windows.Forms.TextBox textBoxMinuty;
        private System.Windows.Forms.Label labelMinuty;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelVarovani;
    }
}
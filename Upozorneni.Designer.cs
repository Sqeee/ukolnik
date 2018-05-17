namespace Ukolnik
{
    partial class Upozorneni
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upozorneni));
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonHotovo = new System.Windows.Forms.Button();
            this.buttonTicho = new System.Windows.Forms.Button();
            this.labelPosunout = new System.Windows.Forms.Label();
            this.comboBoxPosun = new System.Windows.Forms.ComboBox();
            this.buttonPosunout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox
            // 
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Location = new System.Drawing.Point(12, 12);
            this.richTextBox.MaximumSize = new System.Drawing.Size(652, 309);
            this.richTextBox.MinimumSize = new System.Drawing.Size(652, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox.Size = new System.Drawing.Size(652, 290);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "Text";
            this.richTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox_LinkClicked);
            this.richTextBox.Resize += new System.EventHandler(this.richTextBox_Resize);
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(71, 308);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(119, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "Už na tom pracuju";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonHotovo
            // 
            this.buttonHotovo.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonHotovo.Location = new System.Drawing.Point(196, 308);
            this.buttonHotovo.Name = "buttonHotovo";
            this.buttonHotovo.Size = new System.Drawing.Size(75, 23);
            this.buttonHotovo.TabIndex = 2;
            this.buttonHotovo.Text = "Hotovo";
            this.buttonHotovo.UseVisualStyleBackColor = true;
            this.buttonHotovo.Click += new System.EventHandler(this.buttonHotovo_Click);
            // 
            // buttonTicho
            // 
            this.buttonTicho.Location = new System.Drawing.Point(277, 308);
            this.buttonTicho.Name = "buttonTicho";
            this.buttonTicho.Size = new System.Drawing.Size(75, 23);
            this.buttonTicho.TabIndex = 3;
            this.buttonTicho.Text = "Ticho!";
            this.buttonTicho.UseVisualStyleBackColor = true;
            this.buttonTicho.Click += new System.EventHandler(this.buttonTicho_Click);
            // 
            // labelPosunout
            // 
            this.labelPosunout.AutoSize = true;
            this.labelPosunout.Location = new System.Drawing.Point(358, 313);
            this.labelPosunout.Name = "labelPosunout";
            this.labelPosunout.Size = new System.Drawing.Size(64, 13);
            this.labelPosunout.TabIndex = 4;
            this.labelPosunout.Text = "Posunout o:";
            // 
            // comboBoxPosun
            // 
            this.comboBoxPosun.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxPosun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPosun.FormattingEnabled = true;
            this.comboBoxPosun.Items.AddRange(new object[] {
            "Hodinu",
            "Den",
            "Týden",
            "Měsíc",
            "Rok"});
            this.comboBoxPosun.Location = new System.Drawing.Point(428, 309);
            this.comboBoxPosun.Name = "comboBoxPosun";
            this.comboBoxPosun.Size = new System.Drawing.Size(96, 21);
            this.comboBoxPosun.TabIndex = 5;
            this.comboBoxPosun.SelectedIndexChanged += new System.EventHandler(this.comboBoxPosun_SelectedIndexChanged);
            // 
            // buttonPosunout
            // 
            this.buttonPosunout.Enabled = false;
            this.buttonPosunout.Location = new System.Drawing.Point(531, 308);
            this.buttonPosunout.Name = "buttonPosunout";
            this.buttonPosunout.Size = new System.Drawing.Size(75, 23);
            this.buttonPosunout.TabIndex = 6;
            this.buttonPosunout.Text = "Posunout";
            this.buttonPosunout.UseVisualStyleBackColor = true;
            this.buttonPosunout.Click += new System.EventHandler(this.buttonPosunout_Click);
            // 
            // Upozorneni
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 343);
            this.Controls.Add(this.buttonPosunout);
            this.Controls.Add(this.comboBoxPosun);
            this.Controls.Add(this.labelPosunout);
            this.Controls.Add(this.buttonTicho);
            this.Controls.Add(this.buttonHotovo);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.richTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Upozorneni";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Událost se hlásí o slovo!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Upozorneni_FormClosing);
            this.Load += new System.EventHandler(this.Upozorneni_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonHotovo;
        private System.Windows.Forms.Button buttonTicho;
        private System.Windows.Forms.Label labelPosunout;
        private System.Windows.Forms.ComboBox comboBoxPosun;
        private System.Windows.Forms.Button buttonPosunout;
    }
}
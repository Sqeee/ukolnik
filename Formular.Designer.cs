namespace Ukolnik
{
    partial class Formular
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
            this.comboBoxTyp = new System.Windows.Forms.ComboBox();
            this.labelPopisek = new System.Windows.Forms.Label();
            this.richTextBoxZprava = new System.Windows.Forms.RichTextBox();
            this.buttonOdeslat = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxTyp
            // 
            this.comboBoxTyp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxTyp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTyp.FormattingEnabled = true;
            this.comboBoxTyp.Items.AddRange(new object[] {
            "Chyba",
            "Návrh",
            "Připomínka"});
            this.comboBoxTyp.Location = new System.Drawing.Point(106, 12);
            this.comboBoxTyp.Name = "comboBoxTyp";
            this.comboBoxTyp.Size = new System.Drawing.Size(166, 21);
            this.comboBoxTyp.TabIndex = 0;
            // 
            // labelPopisek
            // 
            this.labelPopisek.AutoSize = true;
            this.labelPopisek.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelPopisek.Location = new System.Drawing.Point(12, 15);
            this.labelPopisek.Name = "labelPopisek";
            this.labelPopisek.Size = new System.Drawing.Size(88, 13);
            this.labelPopisek.TabIndex = 3;
            this.labelPopisek.Text = "Co chci sdělit:";
            // 
            // richTextBoxZprava
            // 
            this.richTextBoxZprava.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxZprava.Location = new System.Drawing.Point(12, 54);
            this.richTextBoxZprava.Name = "richTextBoxZprava";
            this.richTextBoxZprava.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxZprava.Size = new System.Drawing.Size(260, 196);
            this.richTextBoxZprava.TabIndex = 1;
            this.richTextBoxZprava.Text = "";
            // 
            // buttonOdeslat
            // 
            this.buttonOdeslat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonOdeslat.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOdeslat.Location = new System.Drawing.Point(105, 256);
            this.buttonOdeslat.Name = "buttonOdeslat";
            this.buttonOdeslat.Size = new System.Drawing.Size(75, 23);
            this.buttonOdeslat.TabIndex = 2;
            this.buttonOdeslat.Text = "Odeslat";
            this.buttonOdeslat.UseVisualStyleBackColor = true;
            this.buttonOdeslat.Click += new System.EventHandler(this.buttonOdeslat_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(6, 36);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(277, 13);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "Nebylo by špatné připsat kontakt pro případnou domluvu";
            // 
            // Formular
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 287);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonOdeslat);
            this.Controls.Add(this.richTextBoxZprava);
            this.Controls.Add(this.labelPopisek);
            this.Controls.Add(this.comboBoxTyp);
            this.MinimumSize = new System.Drawing.Size(300, 325);
            this.Name = "Formular";
            this.Text = "Návrhy, připomínky a chyby";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxTyp;
        private System.Windows.Forms.Label labelPopisek;
        private System.Windows.Forms.RichTextBox richTextBoxZprava;
        private System.Windows.Forms.Button buttonOdeslat;
        private System.Windows.Forms.Label labelInfo;
    }
}
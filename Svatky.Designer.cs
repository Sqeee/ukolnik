namespace Ukolnik
{
    partial class Svatky
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Svatky));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkedListBoxVyznamneDny = new System.Windows.Forms.CheckedListBox();
            this.checkedListBoxSvatky = new System.Windows.Forms.CheckedListBox();
            this.richTextBoxNarozeniny = new System.Windows.Forms.RichTextBox();
            this.labelVyznamneDny = new System.Windows.Forms.Label();
            this.labelSvatky = new System.Windows.Forms.Label();
            this.labelNarozeniny = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonUložit = new System.Windows.Forms.Button();
            this.buttonPonechat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBoxVyznamneDny
            // 
            this.checkedListBoxVyznamneDny.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxVyznamneDny.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxVyznamneDny.CheckOnClick = true;
            this.checkedListBoxVyznamneDny.FormattingEnabled = true;
            this.checkedListBoxVyznamneDny.Location = new System.Drawing.Point(17, 56);
            this.checkedListBoxVyznamneDny.Name = "checkedListBoxVyznamneDny";
            this.checkedListBoxVyznamneDny.Size = new System.Drawing.Size(238, 435);
            this.checkedListBoxVyznamneDny.TabIndex = 0;
            this.toolTip.SetToolTip(this.checkedListBoxVyznamneDny, "Zaškrtnutím svátku bude zvýrazněné info o tom svátku");
            // 
            // checkedListBoxSvatky
            // 
            this.checkedListBoxSvatky.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBoxSvatky.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBoxSvatky.CheckOnClick = true;
            this.checkedListBoxSvatky.FormattingEnabled = true;
            this.checkedListBoxSvatky.Location = new System.Drawing.Point(261, 56);
            this.checkedListBoxSvatky.MultiColumn = true;
            this.checkedListBoxSvatky.Name = "checkedListBoxSvatky";
            this.checkedListBoxSvatky.Size = new System.Drawing.Size(470, 435);
            this.checkedListBoxSvatky.Sorted = true;
            this.checkedListBoxSvatky.TabIndex = 1;
            this.toolTip.SetToolTip(this.checkedListBoxSvatky, "Zaškrtnutím jména bude zvýrazněné info o svátku toho jména");
            // 
            // richTextBoxNarozeniny
            // 
            this.richTextBoxNarozeniny.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxNarozeniny.Location = new System.Drawing.Point(737, 56);
            this.richTextBoxNarozeniny.Name = "richTextBoxNarozeniny";
            this.richTextBoxNarozeniny.ReadOnly = true;
            this.richTextBoxNarozeniny.Size = new System.Drawing.Size(240, 435);
            this.richTextBoxNarozeniny.TabIndex = 5;
            this.richTextBoxNarozeniny.Text = "";
            this.toolTip.SetToolTip(this.richTextBoxNarozeniny, "Kdo má kdy narozeniny a kolik mu je");
            // 
            // labelVyznamneDny
            // 
            this.labelVyznamneDny.AutoSize = true;
            this.labelVyznamneDny.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVyznamneDny.Location = new System.Drawing.Point(17, 40);
            this.labelVyznamneDny.Name = "labelVyznamneDny";
            this.labelVyznamneDny.Size = new System.Drawing.Size(92, 13);
            this.labelVyznamneDny.TabIndex = 2;
            this.labelVyznamneDny.Text = "Významné dny:";
            // 
            // labelSvatky
            // 
            this.labelSvatky.AutoSize = true;
            this.labelSvatky.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSvatky.Location = new System.Drawing.Point(258, 40);
            this.labelSvatky.Name = "labelSvatky";
            this.labelSvatky.Size = new System.Drawing.Size(50, 13);
            this.labelSvatky.TabIndex = 3;
            this.labelSvatky.Text = "Svátky:";
            // 
            // labelNarozeniny
            // 
            this.labelNarozeniny.AutoSize = true;
            this.labelNarozeniny.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNarozeniny.Location = new System.Drawing.Point(737, 40);
            this.labelNarozeniny.Name = "labelNarozeniny";
            this.labelNarozeniny.Size = new System.Drawing.Size(74, 13);
            this.labelNarozeniny.TabIndex = 4;
            this.labelNarozeniny.Text = "Narozeniny:";
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(970, 31);
            this.labelInfo.TabIndex = 6;
            this.labelInfo.Text = resources.GetString("labelInfo.Text");
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonUložit
            // 
            this.buttonUložit.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonUložit.Location = new System.Drawing.Point(371, 497);
            this.buttonUložit.Name = "buttonUložit";
            this.buttonUložit.Size = new System.Drawing.Size(123, 23);
            this.buttonUložit.TabIndex = 7;
            this.buttonUložit.Text = "Uložit výběr";
            this.buttonUložit.UseVisualStyleBackColor = true;
            this.buttonUložit.Click += new System.EventHandler(this.buttonUložit_Click);
            // 
            // buttonPonechat
            // 
            this.buttonPonechat.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonPonechat.Location = new System.Drawing.Point(500, 497);
            this.buttonPonechat.Name = "buttonPonechat";
            this.buttonPonechat.Size = new System.Drawing.Size(123, 23);
            this.buttonPonechat.TabIndex = 8;
            this.buttonPonechat.Text = "Ponechat beze změn";
            this.buttonPonechat.UseVisualStyleBackColor = true;
            this.buttonPonechat.Click += new System.EventHandler(this.buttonPonechat_Click);
            // 
            // Svatky
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 528);
            this.Controls.Add(this.buttonPonechat);
            this.Controls.Add(this.buttonUložit);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.richTextBoxNarozeniny);
            this.Controls.Add(this.labelNarozeniny);
            this.Controls.Add(this.labelSvatky);
            this.Controls.Add(this.labelVyznamneDny);
            this.Controls.Add(this.checkedListBoxSvatky);
            this.Controls.Add(this.checkedListBoxVyznamneDny);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Svatky";
            this.Text = "Upozornění na svátky s přehledem narozenin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Svatky_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckedListBox checkedListBoxVyznamneDny;
        private System.Windows.Forms.CheckedListBox checkedListBoxSvatky;
        private System.Windows.Forms.Label labelVyznamneDny;
        private System.Windows.Forms.Label labelSvatky;
        private System.Windows.Forms.Label labelNarozeniny;
        private System.Windows.Forms.RichTextBox richTextBoxNarozeniny;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonUložit;
        private System.Windows.Forms.Button buttonPonechat;
    }
}
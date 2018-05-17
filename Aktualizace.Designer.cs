namespace Ukolnik
{
    partial class Aktualizace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Aktualizace));
            this.progressBarKonfigurace = new System.Windows.Forms.ProgressBar();
            this.labelStav = new System.Windows.Forms.Label();
            this.webBrowserChangelog = new System.Windows.Forms.WebBrowser();
            this.buttonPokracovat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBarKonfigurace
            // 
            this.progressBarKonfigurace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarKonfigurace.Location = new System.Drawing.Point(12, 375);
            this.progressBarKonfigurace.Maximum = 3;
            this.progressBarKonfigurace.Name = "progressBarKonfigurace";
            this.progressBarKonfigurace.Size = new System.Drawing.Size(786, 23);
            this.progressBarKonfigurace.Step = 1;
            this.progressBarKonfigurace.TabIndex = 0;
            // 
            // labelStav
            // 
            this.labelStav.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelStav.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStav.Location = new System.Drawing.Point(12, 354);
            this.labelStav.Name = "labelStav";
            this.labelStav.Size = new System.Drawing.Size(786, 18);
            this.labelStav.TabIndex = 1;
            this.labelStav.Text = "Probíhá konfigurace...";
            this.labelStav.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // webBrowserChangelog
            // 
            this.webBrowserChangelog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowserChangelog.Location = new System.Drawing.Point(12, 12);
            this.webBrowserChangelog.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserChangelog.Name = "webBrowserChangelog";
            this.webBrowserChangelog.Size = new System.Drawing.Size(786, 339);
            this.webBrowserChangelog.TabIndex = 2;
            // 
            // buttonPokracovat
            // 
            this.buttonPokracovat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonPokracovat.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonPokracovat.Enabled = false;
            this.buttonPokracovat.Location = new System.Drawing.Point(368, 404);
            this.buttonPokracovat.Name = "buttonPokracovat";
            this.buttonPokracovat.Size = new System.Drawing.Size(75, 23);
            this.buttonPokracovat.TabIndex = 3;
            this.buttonPokracovat.Text = "Pokračovat";
            this.buttonPokracovat.UseVisualStyleBackColor = true;
            // 
            // Aktualizace
            // 
            this.AcceptButton = this.buttonPokracovat;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 436);
            this.ControlBox = false;
            this.Controls.Add(this.buttonPokracovat);
            this.Controls.Add(this.webBrowserChangelog);
            this.Controls.Add(this.labelStav);
            this.Controls.Add(this.progressBarKonfigurace);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "Aktualizace";
            this.Text = "Změny v nové verzi";
            this.Load += new System.EventHandler(this.Changelog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarKonfigurace;
        private System.Windows.Forms.Label labelStav;
        private System.Windows.Forms.WebBrowser webBrowserChangelog;
        private System.Windows.Forms.Button buttonPokracovat;
    }
}
namespace Ukolnik
{
    partial class StareUdalosti
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StareUdalosti));
            this.labelInfo = new System.Windows.Forms.Label();
            this.button = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.alarm = new Ukolnik.Alarm();
            this.stranka = new Ukolnik.Stranka();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(327, 38);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Tyto úkoly jsou nesplněné i když už mají být splněné.";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(127, 274);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(96, 23);
            this.button.TabIndex = 42;
            this.button.Text = "Dík za informaci";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // stranka
            // 
            this.stranka.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.uprostred;
            this.stranka.Location = new System.Drawing.Point(12, 50);
            this.stranka.Name = "stranka";
            this.stranka.PrechodNarozeninDoDalsihoRoku = false;
            this.stranka.Radku = 10;
            this.stranka.Size = new System.Drawing.Size(327, 218);
            this.stranka.TabIndex = 43;
            this.stranka.TextNulaPolozek = "";
            this.stranka.ZavritPrazdne = true;
            this.stranka.ZobrazDatum = true;
            this.stranka.ZrychleneVykreslovani = true;
            // 
            // StareUdalosti
            // 
            this.AcceptButton = this.button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 315);
            this.Controls.Add(this.button);
            this.Controls.Add(this.stranka);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(367, 343);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(367, 343);
            this.Name = "StareUdalosti";
            this.Text = "Nesplněné úkoly!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StareUdalosti_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.ToolTip toolTip;
        private Alarm alarm;
        private Stranka stranka;
    }
}
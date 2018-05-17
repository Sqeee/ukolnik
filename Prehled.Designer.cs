namespace Ukolnik
{
    partial class Prehled
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Prehled));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.labelSvatky = new System.Windows.Forms.Label();
            this.labelSplnene = new System.Windows.Forms.Label();
            this.labelNesplnene = new System.Windows.Forms.Label();
            this.labelBlizke = new System.Windows.Forms.Label();
            this.labelNarozeniny = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.alarm = new Ukolnik.Alarm();
            this.strankaSplnene = new Ukolnik.Stranka();
            this.strankaNesplnene = new Ukolnik.Stranka();
            this.strankaBlizke = new Ukolnik.Stranka();
            this.strankaNarozeniny = new Ukolnik.Stranka();
            this.buttonPokracovat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // labelSvatky
            // 
            this.labelSvatky.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSvatky.Location = new System.Drawing.Point(12, 9);
            this.labelSvatky.Name = "labelSvatky";
            this.labelSvatky.Size = new System.Drawing.Size(991, 40);
            this.labelSvatky.TabIndex = 0;
            this.labelSvatky.Text = "Svátky";
            this.labelSvatky.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.labelSvatky, "Co je za den, kdo má svátek a narozeniny");
            // 
            // labelSplnene
            // 
            this.labelSplnene.AutoSize = true;
            this.labelSplnene.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSplnene.Location = new System.Drawing.Point(12, 49);
            this.labelSplnene.Name = "labelSplnene";
            this.labelSplnene.Size = new System.Drawing.Size(157, 13);
            this.labelSplnene.TabIndex = 1;
            this.labelSplnene.Text = "Poslední splněné události:";
            this.toolTip.SetToolTip(this.labelSplnene, "Zobrazuje události, které už jsou splněné, jsou řazeny podle data do kdy měly být" +
        " splněny");
            // 
            // labelNesplnene
            // 
            this.labelNesplnene.AutoSize = true;
            this.labelNesplnene.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNesplnene.Location = new System.Drawing.Point(342, 49);
            this.labelNesplnene.Name = "labelNesplnene";
            this.labelNesplnene.Size = new System.Drawing.Size(270, 13);
            this.labelNesplnene.TabIndex = 2;
            this.labelNesplnene.Text = "Nesplněné události, které už mají být splněné:";
            this.toolTip.SetToolTip(this.labelNesplnene, "Zobrazuje nesplněné události, který už mají být touto dobou splněné, řazeny jsou " +
        "podle událostí, které už mají být splněny nejdelší dobu");
            // 
            // labelBlizke
            // 
            this.labelBlizke.AutoSize = true;
            this.labelBlizke.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelBlizke.Location = new System.Drawing.Point(672, 49);
            this.labelBlizke.Name = "labelBlizke";
            this.labelBlizke.Size = new System.Drawing.Size(108, 13);
            this.labelBlizke.TabIndex = 3;
            this.labelBlizke.Text = "Nejbližší události:";
            this.toolTip.SetToolTip(this.labelBlizke, "Zobrazuje události, která by v nejbližší době měly být splněny, řazeny jsou podle" +
        " toho, za jak dlouho mají být splněny, nahoře jsou ty nejdřív");
            // 
            // labelNarozeniny
            // 
            this.labelNarozeniny.AutoSize = true;
            this.labelNarozeniny.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNarozeniny.Location = new System.Drawing.Point(440, 286);
            this.labelNarozeniny.Name = "labelNarozeniny";
            this.labelNarozeniny.Size = new System.Drawing.Size(125, 13);
            this.labelNarozeniny.TabIndex = 4;
            this.labelNarozeniny.Text = "Nejbližší narozeniny:";
            this.toolTip.SetToolTip(this.labelNarozeniny, "Zobrazuje nejbližší narozeniny, aby jsi stihnul koupit dárky");
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // strankaSplnene
            // 
            this.strankaSplnene.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.uprostred;
            this.strankaSplnene.Location = new System.Drawing.Point(15, 65);
            this.strankaSplnene.Name = "strankaSplnene";
            this.strankaSplnene.PrechodNarozeninDoDalsihoRoku = false;
            this.strankaSplnene.Radku = 10;
            this.strankaSplnene.Size = new System.Drawing.Size(322, 218);
            this.strankaSplnene.TabIndex = 63;
            this.strankaSplnene.TextNulaPolozek = "Nejsou žádné splněné události";
            this.strankaSplnene.ZavritPrazdne = false;
            this.strankaSplnene.ZobrazDatum = true;
            this.strankaSplnene.ZrychleneVykreslovani = true;
            // 
            // strankaNesplnene
            // 
            this.strankaNesplnene.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.uprostred;
            this.strankaNesplnene.Location = new System.Drawing.Point(345, 65);
            this.strankaNesplnene.Name = "strankaNesplnene";
            this.strankaNesplnene.PrechodNarozeninDoDalsihoRoku = false;
            this.strankaNesplnene.Radku = 10;
            this.strankaNesplnene.Size = new System.Drawing.Size(322, 214);
            this.strankaNesplnene.TabIndex = 64;
            this.strankaNesplnene.TextNulaPolozek = "Nejsou nesplněné události, které měly být splněny";
            this.strankaNesplnene.ZavritPrazdne = false;
            this.strankaNesplnene.ZobrazDatum = true;
            this.strankaNesplnene.ZrychleneVykreslovani = true;
            // 
            // strankaBlizke
            // 
            this.strankaBlizke.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.uprostred;
            this.strankaBlizke.Location = new System.Drawing.Point(675, 65);
            this.strankaBlizke.Name = "strankaBlizke";
            this.strankaBlizke.PrechodNarozeninDoDalsihoRoku = false;
            this.strankaBlizke.Radku = 10;
            this.strankaBlizke.Size = new System.Drawing.Size(322, 214);
            this.strankaBlizke.TabIndex = 65;
            this.strankaBlizke.TextNulaPolozek = "Nejsou žádné budoucí události zadané";
            this.strankaBlizke.ZavritPrazdne = false;
            this.strankaBlizke.ZobrazDatum = true;
            this.strankaBlizke.ZrychleneVykreslovani = true;
            // 
            // strankaNarozeniny
            // 
            this.strankaNarozeniny.AlignOvladaciPrvky = Ukolnik.Stranka.polohyNavigace.uprostred;
            this.strankaNarozeniny.Location = new System.Drawing.Point(345, 302);
            this.strankaNarozeniny.Name = "strankaNarozeniny";
            this.strankaNarozeniny.PrechodNarozeninDoDalsihoRoku = true;
            this.strankaNarozeniny.Radku = 5;
            this.strankaNarozeniny.Size = new System.Drawing.Size(322, 121);
            this.strankaNarozeniny.TabIndex = 66;
            this.strankaNarozeniny.TextNulaPolozek = "Ještě nebyly žádné narozeniny zadány";
            this.strankaNarozeniny.ZavritPrazdne = false;
            this.strankaNarozeniny.ZobrazDatum = true;
            this.strankaNarozeniny.ZrychleneVykreslovani = true;
            // 
            // buttonPokracovat
            // 
            this.buttonPokracovat.Location = new System.Drawing.Point(470, 427);
            this.buttonPokracovat.Name = "buttonPokracovat";
            this.buttonPokracovat.Size = new System.Drawing.Size(75, 23);
            this.buttonPokracovat.TabIndex = 67;
            this.buttonPokracovat.Text = "Pokračovat";
            this.buttonPokracovat.UseVisualStyleBackColor = true;
            this.buttonPokracovat.Visible = false;
            this.buttonPokracovat.Click += new System.EventHandler(this.buttonPokracovat_Click);
            // 
            // Prehled
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 462);
            this.Controls.Add(this.buttonPokracovat);
            this.Controls.Add(this.strankaNarozeniny);
            this.Controls.Add(this.strankaBlizke);
            this.Controls.Add(this.strankaNesplnene);
            this.Controls.Add(this.strankaSplnene);
            this.Controls.Add(this.labelNarozeniny);
            this.Controls.Add(this.labelBlizke);
            this.Controls.Add(this.labelNesplnene);
            this.Controls.Add(this.labelSplnene);
            this.Controls.Add(this.labelSvatky);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Prehled";
            this.Text = "Přehled";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Prehled_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label labelSvatky;
        private System.Windows.Forms.Label labelSplnene;
        private System.Windows.Forms.Label labelNesplnene;
        private System.Windows.Forms.Label labelBlizke;
        private System.Windows.Forms.Label labelNarozeniny;
        private System.Windows.Forms.Timer timer;
        private Alarm alarm;
        private Stranka strankaSplnene;
        private Stranka strankaNesplnene;
        private Stranka strankaBlizke;
        private Stranka strankaNarozeniny;
        private System.Windows.Forms.Button buttonPokracovat;
    }
}
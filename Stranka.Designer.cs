namespace Ukolnik
{
    partial class Stranka
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxStranka = new System.Windows.Forms.TextBox();
            this.labelDozadu = new System.Windows.Forms.Label();
            this.labelKonec = new System.Windows.Forms.Label();
            this.labelVpred = new System.Windows.Forms.Label();
            this.labelZacatek = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPosunoutUdalostOHodinu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPosunoutUdalostODen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPosunoutUdalostOTyden = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPosunoutUdalostOMesic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPosunoutUdalostORok = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSmazatUdalost = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemZobrazitUdalost = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxStranka
            // 
            this.textBoxStranka.Location = new System.Drawing.Point(269, 136);
            this.textBoxStranka.MaxLength = 3;
            this.textBoxStranka.Name = "textBoxStranka";
            this.textBoxStranka.Size = new System.Drawing.Size(22, 20);
            this.textBoxStranka.TabIndex = 14;
            this.textBoxStranka.TabStop = false;
            this.textBoxStranka.Text = "1";
            this.textBoxStranka.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxStranka.TextChanged += new System.EventHandler(this.PosunStranek_TextChanged);
            // 
            // labelDozadu
            // 
            this.labelDozadu.AutoSize = true;
            this.labelDozadu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDozadu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDozadu.Location = new System.Drawing.Point(249, 139);
            this.labelDozadu.Name = "labelDozadu";
            this.labelDozadu.Size = new System.Drawing.Size(14, 13);
            this.labelDozadu.TabIndex = 19;
            this.labelDozadu.Text = "<";
            this.toolTip.SetToolTip(this.labelDozadu, "Dozadu");
            this.labelDozadu.Click += new System.EventHandler(this.PosunStranek_Click);
            // 
            // labelKonec
            // 
            this.labelKonec.AutoSize = true;
            this.labelKonec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelKonec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelKonec.Location = new System.Drawing.Point(317, 139);
            this.labelKonec.Name = "labelKonec";
            this.labelKonec.Size = new System.Drawing.Size(17, 13);
            this.labelKonec.TabIndex = 21;
            this.labelKonec.Text = ">|";
            this.toolTip.SetToolTip(this.labelKonec, "Na konec");
            this.labelKonec.Click += new System.EventHandler(this.PosunStranek_Click);
            // 
            // labelVpred
            // 
            this.labelVpred.AutoSize = true;
            this.labelVpred.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelVpred.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVpred.Location = new System.Drawing.Point(297, 139);
            this.labelVpred.Name = "labelVpred";
            this.labelVpred.Size = new System.Drawing.Size(14, 13);
            this.labelVpred.TabIndex = 20;
            this.labelVpred.Text = ">";
            this.toolTip.SetToolTip(this.labelVpred, "Dopředu");
            this.labelVpred.Click += new System.EventHandler(this.PosunStranek_Click);
            // 
            // labelZacatek
            // 
            this.labelZacatek.AutoSize = true;
            this.labelZacatek.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelZacatek.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelZacatek.Location = new System.Drawing.Point(226, 139);
            this.labelZacatek.Name = "labelZacatek";
            this.labelZacatek.Size = new System.Drawing.Size(17, 13);
            this.labelZacatek.TabIndex = 18;
            this.labelZacatek.Tag = "0";
            this.labelZacatek.Text = "|<";
            this.toolTip.SetToolTip(this.labelZacatek, "Na začátek");
            this.labelZacatek.Click += new System.EventHandler(this.PosunStranek_Click);
            // 
            // toolTip
            // 
            this.toolTip.AutoPopDelay = 10000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 1;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemZobrazitUdalost,
            this.toolStripMenuItemPosunoutUdalostOHodinu,
            this.toolStripMenuItemPosunoutUdalostODen,
            this.toolStripMenuItemPosunoutUdalostOTyden,
            this.toolStripMenuItemPosunoutUdalostOMesic,
            this.toolStripMenuItemPosunoutUdalostORok,
            this.toolStripMenuItemSmazatUdalost});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(219, 180);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // toolStripMenuItemPosunoutUdalostOHodinu
            // 
            this.toolStripMenuItemPosunoutUdalostOHodinu.Name = "toolStripMenuItemPosunoutUdalostOHodinu";
            this.toolStripMenuItemPosunoutUdalostOHodinu.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemPosunoutUdalostOHodinu.Tag = "HOUR";
            this.toolStripMenuItemPosunoutUdalostOHodinu.Text = "Posunout událost o hodinu";
            this.toolStripMenuItemPosunoutUdalostOHodinu.Click += new System.EventHandler(this.toolStripMenuItemPosunoutUdalost_Click);
            // 
            // toolStripMenuItemPosunoutUdalostODen
            // 
            this.toolStripMenuItemPosunoutUdalostODen.Name = "toolStripMenuItemPosunoutUdalostODen";
            this.toolStripMenuItemPosunoutUdalostODen.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemPosunoutUdalostODen.Tag = "DAY";
            this.toolStripMenuItemPosunoutUdalostODen.Text = "Posunout událost o den";
            this.toolStripMenuItemPosunoutUdalostODen.Click += new System.EventHandler(this.toolStripMenuItemPosunoutUdalost_Click);
            // 
            // toolStripMenuItemPosunoutUdalostOTyden
            // 
            this.toolStripMenuItemPosunoutUdalostOTyden.Name = "toolStripMenuItemPosunoutUdalostOTyden";
            this.toolStripMenuItemPosunoutUdalostOTyden.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemPosunoutUdalostOTyden.Tag = "WEEK";
            this.toolStripMenuItemPosunoutUdalostOTyden.Text = "Posunout událost o týden";
            this.toolStripMenuItemPosunoutUdalostOTyden.Click += new System.EventHandler(this.toolStripMenuItemPosunoutUdalost_Click);
            // 
            // toolStripMenuItemPosunoutUdalostOMesic
            // 
            this.toolStripMenuItemPosunoutUdalostOMesic.Name = "toolStripMenuItemPosunoutUdalostOMesic";
            this.toolStripMenuItemPosunoutUdalostOMesic.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemPosunoutUdalostOMesic.Tag = "MONTH";
            this.toolStripMenuItemPosunoutUdalostOMesic.Text = "Posunout událost o měsíc";
            this.toolStripMenuItemPosunoutUdalostOMesic.Click += new System.EventHandler(this.toolStripMenuItemPosunoutUdalost_Click);
            // 
            // toolStripMenuItemPosunoutUdalostORok
            // 
            this.toolStripMenuItemPosunoutUdalostORok.Name = "toolStripMenuItemPosunoutUdalostORok";
            this.toolStripMenuItemPosunoutUdalostORok.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemPosunoutUdalostORok.Tag = "YEAR";
            this.toolStripMenuItemPosunoutUdalostORok.Text = "Posunout událost o rok";
            this.toolStripMenuItemPosunoutUdalostORok.Click += new System.EventHandler(this.toolStripMenuItemPosunoutUdalost_Click);
            // 
            // toolStripMenuItemSmazatUdalost
            // 
            this.toolStripMenuItemSmazatUdalost.Name = "toolStripMenuItemSmazatUdalost";
            this.toolStripMenuItemSmazatUdalost.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemSmazatUdalost.Text = "Smazat událost";
            this.toolStripMenuItemSmazatUdalost.Click += new System.EventHandler(this.toolStripMenuItemSmazatUdalost_Click);
            // 
            // toolStripMenuItemZobrazitUdalost
            // 
            this.toolStripMenuItemZobrazitUdalost.Name = "toolStripMenuItemZobrazitUdalost";
            this.toolStripMenuItemZobrazitUdalost.Size = new System.Drawing.Size(218, 22);
            this.toolStripMenuItemZobrazitUdalost.Text = "Zobrazit událost...";
            this.toolStripMenuItemZobrazitUdalost.Click += new System.EventHandler(this.toolStripMenuItemZobrazitUdalost_Click);
            // 
            // Stranka
            // 
            this.Controls.Add(this.labelZacatek);
            this.Controls.Add(this.labelVpred);
            this.Controls.Add(this.textBoxStranka);
            this.Controls.Add(this.labelDozadu);
            this.Controls.Add(this.labelKonec);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Stranka";
            this.Size = new System.Drawing.Size(354, 180);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStranka;
        private System.Windows.Forms.Label labelDozadu;
        private System.Windows.Forms.Label labelKonec;
        private System.Windows.Forms.Label labelVpred;
        private System.Windows.Forms.Label labelZacatek;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPosunoutUdalostOHodinu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPosunoutUdalostODen;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPosunoutUdalostOTyden;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPosunoutUdalostOMesic;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPosunoutUdalostORok;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSmazatUdalost;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemZobrazitUdalost;
    }
}

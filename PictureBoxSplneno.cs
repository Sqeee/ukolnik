using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Picturebox, který je používán pro splnění či nesplnění nějaké události
    /// </summary>
    public partial class PictureBoxSplneno : PictureBox
    {
        /// <summary>
        /// Načte komponenty
        /// </summary>
        public PictureBoxSplneno()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Obsluha kliknutí, kdy bude splněno nebo nesplněna událost, ke které patří tento PictureBox
        /// </summary>
        private void PictureBoxSplneno_Click(object sender, EventArgs e)
        {
            Databaze db = new Databaze();
            string splneno;
            if ((string)((PictureBox)sender).Image.Tag == "fajfka") // Pokud tam byl obrázek fajfky, tak daná událost byla splněna kliknutím, v opačném případě byla nesplněna
                splneno = "1";
            else
                splneno = "0";
            db.Dotaz("UPDATE udalosti SET Splneno=" + splneno + " WHERE id=" + ((PictureBox)sender).Tag + ";"); // Provedeme změnu splněnosti pro danou událost
            db.Close();
            Obecne.OnZmenaVUdalostech(); // Je potřeba znovu načíst události
        }
    }
}

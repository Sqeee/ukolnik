using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Picturebox, který je používán pro úpravy události
    /// </summary>
    public partial class PictureBoxUprava : PictureBox
    {
        /// <summary>
        /// Načte komponenty
        /// </summary>
        public PictureBoxUprava()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Obsluha kliknutí, kdy bude deaktivována ikona a vyvolán dialog úpravy události
        /// </summary>
        private void PictureBoxUprava_Click(object sender, EventArgs e)
        {
            Obecne.IkonaStav(false); // Vypneme ikonu, aby nešlo obejít dialog
            Uprava okno = new Uprava(Convert.ToInt32(((PictureBox)sender).Tag)); // Vytvoříme úpravu události, ke které byl svázán kliknutý PictureBox
            okno.StartPosition = FormStartPosition.Manual;
            int x = this.Parent.Parent.Parent.Location.X + this.Parent.Parent.Location.X + this.Parent.Location.X + this.Location.X; // Vypočítá se X souřadnice pro zobrazení okna
            int y = this.Parent.Parent.Parent.Location.Y + this.Parent.Parent.Location.Y + this.Parent.Location.Y + this.Location.Y; // Vypočítá se Y souřadnice pro zobrazení okna
            okno.Location = Obecne.UmisteniOkna(new System.Drawing.Point(x, y), okno.Size, 0, 0);
            okno.ShowDialog();
            if (okno.DialogResult == DialogResult.Yes) // Pokud bylo něco upraveno, tak je třeba znovunačíst události, pokud to byly narozeniny tak i svátky
            {
                Obecne.OnZmenaVUdalostech();
                if (okno.Typ == ((int)Obecne.UdalostiTypy.narozeniny))
                    Obecne.OnZmenaVeSvatcich();
            }
            Obecne.IkonaStav(true); // Opět zapneme ikonu
        }
    }
}

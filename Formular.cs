using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Formulář, přes který lze odesílat návrhy, připomínky nebo nahlásit chyby
    /// </summary>
    public partial class Formular : Form
    {
        /// <summary>
        /// Inicializuje potřebný věci
        /// </summary>
        public Formular()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Zpracování kliknutí na odeslat, kdy se odešlou data, pokud je vše OK
        /// </summary>
        private void buttonOdeslat_Click(object sender, EventArgs e)
        {
            if (comboBoxTyp.SelectedIndex == -1) // Byl vybrán typ?
            {
                Obecne.ZobrazZpravu("Nevybral jsi zda se jedná o chybu, návrh nebo připomínku!", "Problém", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (richTextBoxZprava.Text == "") // Prázdné zprávy přece posílat nebudeme
            {
                Obecne.ZobrazZpravu("Prázdnou zprávu nelze poslat!", "Problém", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string odpoved = Obecne.PosliDataNaWeb("aplikace=ukolnik&typ=" + comboBoxTyp.SelectedIndex + "&zprava=" + Obecne.OdstranProblemoveHTTPPOSTZnaky(richTextBoxZprava.Text), "http://sqee.eu/programy/ukolnik/formular.php");
            if (odpoved == "DONE") // Pokud se odeslání zdařilo, tak zavřít formulář
                this.Close();
            else if (odpoved == "ERROR") // Pokud nastala chyba během odesílání, tak již info bylo zobrazeno a tudíž není co zobrazovat
                return;
            else // Jinak zobrazit info o nezdaru
                Obecne.ZobrazZpravu("Zprávu se nepodařilo odeslat, zkus to znovu", "Problém", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Zobrazení changelogu a zároveň provedení změn nové verze
    /// </summary>
    public partial class Aktualizace : Form
    {
        private WebBrowser webBrowserDatabaze;

        /// <summary>
        /// Načte komponenty
        /// </summary>
        public Aktualizace()
        {
            InitializeComponent();
            webBrowserDatabaze = new WebBrowser();
            webBrowserDatabaze.DocumentCompleted += webBrowserDatabaze_DocumentCompleted;
        }

        /// <summary>
        /// Po zobrazení okna se snaží získat z webu changelog a stáhnout případné aktualizační soubory
        /// </summary>
        private void Changelog_Load(object sender, EventArgs e)
        {
            webBrowserChangelog.Navigate("http://sqee.eu/programy/ukolnik/changelog.php?verze=" + Nastaveni.Verze);
            webBrowserDatabaze.Navigate("http://sqee.eu/programy/ukolnik/databaze.php?verze=" + Nastaveni.Verze);
            //webBrowserChangelog.Navigate("http://127.0.0.1/muj/programy/ukolnik/changelog.php?verze=" + Nastaveni.Verze);
            //webBrowserDatabaze.Navigate("http://127.0.0.1/muj/programy/ukolnik/databaze.php?verze=" + Nastaveni.Verze);
        }

        /// <summary>
        /// Přečte a vrátí zdroják webové stránky se správným kódováním
        /// </summary>
        /// <param name="browser">Prohlížeč, ze kterého má být zdroják přečten</param>
        /// <returns>Zdroják webové stránky</returns>
        private String PrectiZdrojak(WebBrowser browser)
        {
            Stream documentStream = browser.DocumentStream;
            if (documentStream == null)
            {
                return "";
            }
            StreamReader reader = new StreamReader(documentStream, Encoding.GetEncoding("utf-8"));
            documentStream.Position = 0;
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Zpracuje databázové změny při aktualizaci
        /// </summary>
        private void webBrowserDatabaze_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            String zdrojak = PrectiZdrojak(webBrowserDatabaze); // Získáme data, tedy dotazy pro aktualizaci databáze
            progressBarKonfigurace.PerformStep();
            const String KONTROLNI_ZACATEK = "DATABAZE-ZACATEK-OK";
            const String KONTROLNI_KONEC = "DATABAZE-KONEC-OK";
            if (zdrojak.Substring(0, KONTROLNI_ZACATEK.Length) != KONTROLNI_ZACATEK || zdrojak.Substring(zdrojak.Length - KONTROLNI_KONEC.Length) != KONTROLNI_KONEC) // Kontrola, zda se stáhly
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }
            zdrojak = zdrojak.Remove(zdrojak.Length - KONTROLNI_KONEC.Length); // Ořeže kontrolní řetězec na konci
            zdrojak = zdrojak.Substring(KONTROLNI_ZACATEK.Length); // Ořeže kontrolní řetězec na začátku
            if (zdrojak != "") // Pokud jsou nějaké dotazy, tak se provedou
            {
                Databaze db = new Databaze();
                db.Dotaz(zdrojak);
            }
            // Uklidíme po sobě
            webBrowserDatabaze.DocumentCompleted -= webBrowserDatabaze_DocumentCompleted;
            webBrowserDatabaze.Dispose();
            webBrowserDatabaze = null;
            progressBarKonfigurace.PerformStep();
            HotovoAkce(); // Zkontrolujeme, zda už je vše hotové
        }

        /// <summary>
        /// Zkontroluje, zda už vše bylo provedeno, pokud ano, tak se provede dokončení
        /// </summary>
        private void HotovoAkce()
        {
            if (progressBarKonfigurace.Value == progressBarKonfigurace.Maximum - 1) // Zda je vše provedeno (tedy kromě posledního kroku - uložení aktuální verze)
            {
                Nastaveni.NastavAktualniVerzi(); // Získáme aktuální verzi
                Nastaveni.UlozNastaveni(); // Uložíme aktuální verzi, aby pak nedocházelo k aktualizaci při dalším spuštění
                progressBarKonfigurace.PerformStep();
                labelStav.Text = "Hotovo"; // Označíme stav jako hotovo a povolíme tlačítka
                buttonPokracovat.Enabled = true;
                this.ControlBox = true;
            }
        }
    }
}

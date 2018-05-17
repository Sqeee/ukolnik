using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Úkolník
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Pro přenesení programu do popředí
        /// </summary>
        /// <param name="hWnd">Ukazatel na aktivní okno programu</param>
        /// <returns>Zda byl program přenesen do popředí</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool prvniOtevreni; // Zda je toto první otevření Úkolníku nebo už je otevřen
            System.Threading.Mutex m = new System.Threading.Mutex(true, "Ukolnik", out prvniOtevreni);
            if (prvniOtevreni == false) // Pokud to není první otevření tak zobrazíme info, získáme proces již běžícího Úkolníku, získáme aktvní okno a to se pokusíme přenést do popředí
            {
                Obecne.ZobrazZpravu("Úkolník je již spuštěn.", "Vícenásobné spuštění není dovoleno", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process[] procesy = System.Diagnostics.Process.GetProcessesByName("Ukolnik");
                SetForegroundWindow(procesy[0].MainWindowHandle);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.bezici); // Úkolník běží normálně
            Nastaveni.NastaveniOtevreno = false; // Nastavovací okno nebylo otevřeno
            Application.Run(new Start()); // Spustíme startovací okno, kde se provedou nezbytné věci
            if (Nastaveni.StavAplikace == (int) Nastaveni.StavyAplikace.bezici) // Pokud je vše v pořádku, tak se spustí okno s přehledem a po jeho zavření již hlavní okno
            {
                Application.Run(new Prehled(true));
                Application.Run(new HlavniOkno());
            }
        }
    }
}

using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Ukolnik
{
    /// <summary>
    /// Statická třída starjící se o nastavení Úkolníku
    /// </summary>
    static class Nastaveni
    {
        /// <summary>
        /// Kam se bude ukládat nastavení Úkolníku (bude v AppData...)
        /// </summary>
        public static readonly string SlozkaNastaveni = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SqeeHead\\Ukolnik\\";
        /// <summary>
        /// Jak se nazývá konfigurační soubor
        /// </summary>
        public const string SouborNastaveni = "nastaveni.uko";
        /// <summary>
        /// Jak se nazývá soubor s obsahující chyby, které se budou nahrávat na web
        /// </summary>
        private const string SouborChyb = "chyby.uko";
        private const string RegistryZaznam = "Ukolnik";

        /// <summary>
        /// Jakou jsou typy stavu aplikace
        /// </summary>
        public enum StavyAplikace { bezici, koncici };

        /// <summary>
        /// Aktuální verze Úkolníku
        /// </summary>
        public static string Verze
        {
            get;
            private set;
        }
        /// <summary>
        /// Databázovy server
        /// </summary>
        public static string Server
        {
            get;
            private set;
        }
        /// <summary>
        /// Uživatel pro připojení k databázi
        /// </summary>
        public static string Uzivatel
        {
            get;
            private set;
        }
        /// <summary>
        /// Heslo pro připojení k databázi
        /// </summary>
        public static string Heslo
        {
            get;
            private set;
        }
        /// <summary>
        /// Databáze, ve které jsou tabulky pro Úkolník
        /// </summary>
        public static string Databaze
        {
            get;
            private set;
        }
        /// <summary>
        /// Zda se má Úkolník sám spouštět po startu počítače
        /// </summary>
        public static bool Spousteni
        {
            get;
            private set;
        }
        /// <summary>
        /// O kolik minut dřív se má upozorňovat na události při automatickém upozornění
        /// </summary>
        public static int UpozorneniDopredu
        {
            get;
            private set;
        }
        /// <summary>
        /// Mají se vypisovat podorbnosti k vyjímkám nebo pouze jen všeobecné informace
        /// </summary>
        public static bool PodrobnostiVyjimek
        {
            get;
            private set;
        }
        /// <summary>
        /// V jakém je stavu aplikace (0 -> běžící, 1 -> ukončující se)
        /// </summary>
        public static int StavAplikace
        {
            get;
            private set;
        }
        /// <summary>
        /// Zda už je jedno okno s nastavením otevřeno nebo ne (například při chybě s připojením by se mohla furt otevírat nová a nová okna
        /// </summary>
        public static bool NastaveniOtevreno
        {
            get;
            set;
        }
        /// <summary>
        /// Zda byl právě vytvořen konfigurák -> aby se nezobrazila chyba s připojením
        /// </summary>
        public static bool PraveVytvorenKonfigurak = false;

        /// <summary>
        /// Dle zadané hodnoty nastaví automatické spouštění při startu
        /// </summary>
        /// <param name="hodnota">Hodnota dle které se nastavuje automatické spouštění při startu počítače</param>
        public static void NastavSpousteni(string hodnota)
        {
            if (hodnota == "True")
                Spousteni = true;
            else
                Spousteni = false;
        }

        /// <summary>
        /// Dle zadané hodnoty nastaví zobrazení podrobného popisku u vyjímek
        /// </summary>
        /// <param name="hodnota">Hodnota dle které se nastaví zobrazení podrobného popisku vyjímek</param>
        public static void NastavPodrobnostiVyjimek(bool hodnota)
        {
            PodrobnostiVyjimek = hodnota;
        }

        /// <summary>
        /// Dle zadané hodnoty nastaví zobrazení podrobného popisku u vyjímek
        /// </summary>
        /// <param name="hodnota">Hodnota dle které se nastaví zobrazení podrobného popisku vyjímek</param>
        public static void NastavPodrobnostiVyjimek(string hodnota)
        {
            if (hodnota == "True")
                PodrobnostiVyjimek = true;
            else
                PodrobnostiVyjimek = false;
        }

        /// <summary>
        /// Dle zadání hodnoty se nastaví stav aplikace
        /// </summary>
        /// <param name="hodnota">0 znamená běžící aplikace, 1 znamená aplikace bude ukončena a nebude při tom provádět další instrukce</param>
        public static void NastavStavAplikace(byte hodnota)
        {
            StavAplikace = hodnota;
        }

        /// <summary>
        /// Nastaví verzi na aktuální verzi aplikace
        /// </summary>
        public static void NastavAktualniVerzi()
        {
            Verze = Obecne.DejVerzi();
        }

        /// <summary>
        /// Vytvoří základní nastavení pro Úkolník, bude ale nutné ho doplnit o heslo do databáze, také vytvoří spoušťák pro Úkolník při startu počítače
        /// </summary>
        public static void VytvorNastaveni()
        {
            Verze = Obecne.DejVerzi();
            Server = "127.0.0.1";
            Uzivatel = "root";
            Heslo = "";
            Databaze = "ukolnik";
            Spousteni = true;
            UpozorneniDopredu = 7;
            UlozNastaveni(true);
            //VytvorSpoustak();
            VytvorRegistry();
        }

        /// <summary>
        /// Vytvoří spoušťák, který při startu počítače bude zapínat Úkolník, pokud by byl Úkolník smazán, tak dokáže sám sebe zničit
        /// </summary>
        public static void VytvorSpoustak()
        {
            string cestaSpoustaku = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\spoustak_ukolniku.bat";
            string cestaKUkolniku = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.Programs) + "\\SqeeHead\\Úkolník.appref-ms\"";
            System.IO.File.WriteAllText(cestaSpoustaku, cestaKUkolniku + "\nif not exist " + cestaKUkolniku + " DEL \"" + cestaSpoustaku + "\"", System.Text.Encoding.GetEncoding(852)); // Vytvoří dávku, která zkusí zapnout Úkolník z nabídky Start, pokud není nalezen ve Startu, tak sám sebe smaže spoušťák
        }

        /// <summary>
        /// Vytvoří záznam do registrů pro automatické spouštění
        /// </summary>
        public static void VytvorRegistry()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            string publisherName = Application.CompanyName;
            string productName = Application.ProductName;
            string allProgramsPath = Environment.GetFolderPath(Environment.SpecialFolder.Programs);
            string shortcutPath = Path.Combine(allProgramsPath, publisherName);
            shortcutPath = Path.Combine(shortcutPath, productName) + ".appref - ms";
            regkey.DeleteSubKey(RegistryZaznam, false);
            regkey.SetValue(RegistryZaznam, shortcutPath);
        }

        /// <summary>
        /// Uloží aktuální nastavení do souboru
        /// </summary>
        /// <param name="priVytvoreni">Nepovinný parametr, který značí, zda bylo uložení vyvoláno při vytvořen, takže pokud bude zde True, tak podrobné vypisování vyjímek nebude a taky se nepřepočítá upozornění na události</param>
        public static void UlozNastaveni(bool priVytvoreni = false)
        {
            string podrobnostiVyjimek;
            if (priVytvoreni == true) // Při prvním vytvoření (spíše uložení) se nastaví podrobnost vyjímek na false
            {
                podrobnostiVyjimek = "False";
                PodrobnostiVyjimek = false;
            }
            else
                podrobnostiVyjimek = PodrobnostiVyjimek.ToString();
            string obsah = string.Format("ver: {0}\nser: {1}\nuzi: {2}\nhes: {3}\ndab: {4}\nspo: {5}\nupo: {6}\npod: {7}\n", Verze, Server, Uzivatel, Heslo, Databaze, Spousteni.ToString(), UpozorneniDopredu.ToString(), podrobnostiVyjimek); // Vytvoření obsahu konfiguráku
            System.IO.File.WriteAllText(SlozkaNastaveni + SouborNastaveni, obsah);
            if (Spousteni == true) // Pokud je nastaveno automatické spouštění při startu, tak to vytvoří spoušťák, pokud ne, tak ho to smaže (pokud tedy ten spoušťák existuje)
            {
                //VytvorSpoustak();
                VytvorRegistry();
            }
            else
            {
                Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteSubKey(RegistryZaznam, false);
                /*if (System.IO.File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\spoustak_ukolniku.bat") == true)
                    System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\spoustak_ukolniku.bat");*/
            }
            Databaze db = new Databaze();
            db.Dotaz("UPDATE udalosti SET Upozorneni=Kdy WHERE Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + ";"); // Srovnáme upozornění s tím, kdy se bude událost konat (pouze automatické upozornění)
            db.Dotaz("UPDATE udalosti SET Upozorneni=DATE_ADD(Kdy, INTERVAL (1 + YEAR(NOW()) - YEAR(Kdy)) YEAR) WHERE (Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + ") AND Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND (MONTH(NOW()) > MONTH(KDY) OR (MONTH(NOW()) = MONTH(KDY) AND DAY(NOW()) > DAY(KDY)));"); // Nastavíme příští upozornění pro narozeniny v případě, že narozeniny již tento rok byly
            db.Dotaz("UPDATE udalosti SET Upozorneni=DATE_ADD(Kdy, INTERVAL (YEAR(NOW()) - YEAR(Kdy)) YEAR) WHERE (Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + ") AND Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND (MONTH(NOW()) < MONTH(KDY) OR (MONTH(NOW()) = MONTH(KDY) AND DAY(NOW()) <= DAY(KDY)));"); // Nastavíme přístí upozornění pro narozeniny, které nás ještě v tomto roce čekají
            db.Dotaz("UPDATE udalosti SET Upozorneni=DATE_ADD(Upozorneni, INTERVAL -" + UpozorneniDopredu.ToString() + " MINUTE) WHERE Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + ";"); // Dojde k přepočítání upozornění událostí, které mají automatické upozornění
            string nejmensiDatum = System.Windows.Forms.DateTimePicker.MinimumDateTime.ToString("yyyy-MM-dd HH:mm:ss"); // Zjistíme nejmenší možné datum pro sběrač data
            db.Dotaz("UPDATE udalosti SET Upozorneni='" + nejmensiDatum + "' WHERE Upozorneni<'" + nejmensiDatum + "';"); // Pokud je nějaké upozornění menší než to datum, tak ho nastavíme na to nejmenší datum, aby nám sběrač neházel chyby
            db.Dotaz("UPDATE udalosti SET Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " WHERE Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + " AND Upozorneni>NOW();"); // Pokud díky posunutí je upozornění v budoucnu, tak opět umožníme upozornění
            db.Close();
        }

        /// <summary>
        /// Provede úpravu nastavení Úkolníku, ale bez uložení do konfiguráku
        /// </summary>
        /// <param name="server">Databázový server</param>
        /// <param name="uzivatel">Uživatelské jméno pro přihlášení do databáze</param>
        /// <param name="heslo">Heslo pro přihlášení do databáze</param>
        /// <param name="databaze">Databáze obsahující tabulky Úkolníku</param>
        /// <param name="spousteni">Zda se má automaticky spouštět Úkolník při startu počítače</param>
        /// <param name="upozorneniDopredu">O kolik minut předem se má upozorňovat při automatickém upozorňování</param>
        /// <param name="podrobnostiVyjimek">Zda se mají zobrazovat podrobnosti u výpisu vyjímek</param>
        public static void UpravNastaveni(string server, string uzivatel, string heslo, string databaze, bool spousteni, int upozorneniDopredu, bool podrobnostiVyjimek)
        {
            Server = server;
            Uzivatel = uzivatel;
            Heslo = heslo;
            Databaze = databaze;
            Spousteni = spousteni;
            UpozorneniDopredu = upozorneniDopredu;
            PodrobnostiVyjimek = podrobnostiVyjimek;
        }

        /// <summary>
        /// Provádí čtení konfiguráku a kontroluje aktuálnost údajů
        /// </summary>
        /// <returns>True, pokud jsou data aktuální, false pokud nejsou data aktuální</returns>
        public static bool CtiNastaveni()
        {
            System.IO.StreamReader soubor = new System.IO.StreamReader(SlozkaNastaveni + SouborNastaveni); // Otevření konfiguráku
            while (!soubor.EndOfStream) // Dokud nejsme na samém konci konfiguráku
            {
                string radek = soubor.ReadLine(); // Postupně čteme jednotlivé řádky
                if (radek.Substring(0, 4) == "ver:") // Jestliže je na začátku řádku ver: tak je to info o poslední verzi Úkolníku (důležité pro aktualizace)
                {
                    Verze = radek.Remove(0, 5); // Odtrhneme začátek a získáme tak verzi
                }
                else if (radek.Substring(0, 4) == "ser:") // Jestliže je na začátku řádku ser: tak je to info o databázovém serveru
                {
                    Server = radek.Remove(0, 5); // Odtrhneme začátek a získáme tak databázový server
                }
                else if (radek.Substring(0, 4) == "uzi:") // Jestliže je na začátku řádku uzi: tak je to přihlašovací jméno do databáze
                {
                    Uzivatel = radek.Remove(0, 5); // Odtrhneme začátek a získáme tak uživatelské jméno pro přihlášení do databáze
                }
                else if (radek.Substring(0, 4) == "hes:") // Jestliže je na začátku řádku hes: tak je to heslo pro přihlášení do databáze
                {
                    Heslo = radek.Remove(0, 5); // Odtrhneme začátek a získáme tak heslo pro přihlášení do databáze
                }
                else if (radek.Substring(0, 4) == "dab:") // Jestliže je na začátku řádku dab: tak je to info o databázi obshaující tabulky pro Úkolník
                {
                    Databaze = radek.Remove(0, 5); // Odtrhneme začátek a získáme tak databázi obsahující tabulky pro Úkolník
                }
                else if (radek.Substring(0, 4) == "spo:") // Jestliže je na začátku řádku spo: tak je to info, zda se má Úkolník automaticky spouštět při startu počítače
                {
                    NastavSpousteni(radek.Remove(0, 5)); // Odtrhneme začátek a získáme tak, zda se má Úkolník automaticky spouštět při startu počítače
                }
                else if (radek.Substring(0, 4) == "upo:") // Jestliže je na začátku řádku upo: tak je to o kolik minut předem se má upozorňovat na události
                {
                    try
                    {
                        UpozorneniDopredu = Convert.ToInt32(radek.Remove(0, 5)); // Zkusíme odtrhnutím začátku a převodu na číslo získat o kolik minut dřív se má upozorňovat na události
                    }
                    catch (Exception exc) // Pokud nastal problém a zadaná hodnota nelze předělat na číslo, tak se nastaví záklandí hodnota a vypíše se info o tomto problému
                    {
                        UpozorneniDopredu = 7;
                        Vyjimky.VypisVyjimek("Chyba konfiguračním souboru, je zadána špatná hodnota pro upozornění, běž do nastavení a ulož tam novou.", exc);
                    }
                }
                else if (radek.Substring(0, 4) == "pod:") // Jestliže je na začátku řádku pod: tak je to info o tom, zda se mají zobrazovat podrobnosti u vyjimek
                {
                    NastavPodrobnostiVyjimek(radek.Remove(0, 5)); // Odtrhneme začátek a a předáme ho funkci na nastavení podrobností u výpisu vyjimek
                }
            }
            soubor.Close(); // Zavřeme konfigurák
            return (Verze == Obecne.DejVerzi());
        }

        /// <summary>
        /// Provádí zápis chyby do souboru s chybami
        /// </summary>
        /// <param name="chyba">Chyba, která bude do souboru připsána</param>
        public static void ZapisChybovySoubor(string chyba)
        {
            System.IO.StreamWriter soubor = new System.IO.StreamWriter(SlozkaNastaveni + SouborChyb, true);
            soubor.WriteLine(chyba);
            soubor.WriteLine();
            soubor.Close();
        }

        /// <summary>
        /// Provede odeslání souboru s chybama na web
        /// </summary>
        public static void OdesliChybovySoubor()
        {
            if (System.IO.File.Exists(Nastaveni.SlozkaNastaveni + SouborChyb) == false) // Pokud soubor s chybama neexistuje, tak pak není co odesílat
                return;
            System.IO.StreamReader soubor = new System.IO.StreamReader(SlozkaNastaveni + SouborChyb); // Otevře soubor s chybama
            System.Security.Cryptography.MD5 hash = System.Security.Cryptography.MD5.Create(); // Připravíme hash pro identifikaci, zda chyba vzniká na jendom a tom samém počítači nebo na různých počítačích
            byte[] zahashovano = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(System.Environment.UserName + System.Environment.MachineName)); // Hash bude z uživatelského jména a jména počítače
            if (Obecne.PosliDataNaWeb("aplikace=ukolnik&identifikace=" + Convert.ToBase64String(zahashovano) + "&system=" + System.Environment.OSVersion.ToString() + "&chyby=" + Obecne.OdstranProblemoveHTTPPOSTZnaky(soubor.ReadToEnd()), "http://sqee.eu/programy/ukolnik/formular.php", false) == "DONE") // Na web se odešlou data obsahující indentifikaci, OS a chyby (ze kterých byly odstraněny nevhodné znaky), pokud to proběhlo v pořádku, tak se soubor s chybama smaže, v opačném případě bude ponechán na příště
            {
                soubor.Close();
                System.IO.File.Delete(Nastaveni.SlozkaNastaveni + SouborChyb);
            }
            else
                soubor.Close();
        }

        /// <summary>
        /// Handler na Změnu připojení, událost která všem databázovým připojením nařídí změnu připojovacích údajů (například, když proběhne změna v nastavení)
        /// </summary>
        public delegate void ZmenaPripojeniHandler();
        /// <summary>
        /// Událost která všem databázovým připojením nařídí změnu připojovacích údajů (například, když proběhne změna v nastavení)
        /// </summary>
        public static event ZmenaPripojeniHandler ZmenaPripojeni;
        /// <summary>
        /// Vyvolá událost Změny připojení
        /// </summary>
        public static void OnZmenaPripojeni()
        {
            if (ZmenaPripojeni != null)
                ZmenaPripojeni();
        }
    }
}

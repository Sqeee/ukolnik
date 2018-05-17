using System;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Ukolnik
{
    /// <summary>
    /// Třída starající se o připojení k databázi
    /// </summary>
    public class Databaze
    {
        /// <summary>
        /// Obsahuje řetězec pro připojení k databázi
        /// </summary>
        private string PripojovaciUdaje;
        /// <summary>
        /// Kolik bylo výsledků na dotaz (je to ale vypočítáno z počtu proběhlých výsledků, takže zjištění výsledků znemožní získání dat, nejdřív tedy data, pak výsledky, obráceně to nelze)
        /// </summary>
        private int Vysledku;
        /// <summary>
        /// Signalizace, zda se vyskytla chyba při komunikaci s databází
        /// </summary>
        private bool Chyba;
        /// <summary>
        /// Obsahuje výsledky vrácené databází
        /// </summary>
        private MySqlDataReader Vysledek;
        /// <summary>
        /// Je to spojení, které se připojuje k databázi
        /// </summary>
        private MySqlConnection Pripojeni;
        /// <summary>
        /// Počítá, kolik stejných chyb v řadě je vykazováno (ochrana před zahlcením jednou a tou samou chybou)
        /// </summary>
        private int StejnychChybVRade;
        /// <summary>
        /// Jakou číselnou hodnotu měla poslední chyba
        /// </summary>
        private int KodChyby;
        /// <summary>
        /// Zda došlo ke změně údajů a je tedy potřeba uzavřít spojení a vytvořit nové
        /// </summary>
        private bool ZmenaUdaju;

        /// <summary>
        /// Provede připojení k databázi podle údajů z konfiguráku
        /// </summary>
        public Databaze() : this(Nastaveni.Server, Nastaveni.Uzivatel, Nastaveni.Heslo, Nastaveni.Databaze) { }

        /// <summary>
        /// Provede připojení k databázi dle zadaných údajů
        /// </summary>
        /// <param name="server">Server, kde je spuštěna databáze</param>
        /// <param name="uzivatel">Uživatelské jméno pro přihlášení k databázi</param>
        /// <param name="heslo">Heslo pro připojení k databázi</param>
        /// <param name="databaze">Databáze, ve které jsou tabulky pro Úkolník</param>
        public Databaze(string server, string uzivatel, string heslo, string databaze)
        {
            PripojovaciUdaje = string.Format("server={0};uid={1};pwd={2};database={3}", server, uzivatel, heslo, databaze); // Vytvoříme připojovací řetězec
            Nastaveni.ZmenaPripojeni += AktualizacePripojeni; // Začneme sledovat, zda není třeba změna připojení (například pomocí nastavení)
            Pripojeni = new MySqlConnection(PripojovaciUdaje); // Vytvoříme nové spojení z připojovacích údajů
            Vysledku = 0; // V tuto chvíli nejsou známé žádné výsledky
            StejnychChybVRade = 0; // Ani chyby zatím nebyly
            KodChyby = 0; // Tudíž ani kód chyby nemohl být
            ZmenaUdaju = false; // Ke změně údajů zatím také nedošlo
        }

        /// <summary>
        /// Stará se o uzavření spojení a úklidu
        /// </summary>
        public void Close()
        {
            Nastaveni.ZmenaPripojeni -= AktualizacePripojeni; // Už nechceme sledovat změnu připojení
            if (Pripojeni.State == System.Data.ConnectionState.Open) // Pokud je spojení otevřené, tak ho zavřeme
                Pripojeni.Close();
            if (Vysledek != null && Vysledek.IsClosed == false) // Pokud výsledkový stream existuje a není uzavřen, tak ho uzavřeme
                Vysledek.Close();
        }

        /// <summary>
        /// Vytvoří novou verzi připojovacího řetězce a nastaví změnu údajů, aby se mohlo následně zrušit stávající připojení a mohlo ho nahradit nové
        /// </summary>
        public void AktualizacePripojeni()
        {
            PripojovaciUdaje = string.Format("server={0};uid={1};pwd={2};database={3}", Nastaveni.Server, Nastaveni.Uzivatel, Nastaveni.Heslo, Nastaveni.Databaze);
            ZmenaUdaju = true;
        }

        /// <summary>
        /// V případě, že databáze neexistuje, tak je třeba ji vytvořit
        /// </summary>
        private void VytvorDatabazi()
        {
            int pozice = PripojovaciUdaje.LastIndexOf(';'); // Pro vytvoření databáze nemůžeme ale použít výběr databáze, takže najdeme kde začíná část řetězce o databázi
            string databaze = PripojovaciUdaje.Substring(pozice + 10); // Skočíme na samotné jméno databáze a to vytáhneme
            PripojovaciUdaje = PripojovaciUdaje.Remove(pozice); // Smažeme poslední středník i databázi za ním
            ZmenaUdaju = true; // Došlo ke změně údajů
            Dotaz("CREATE DATABASE IF NOT EXISTS " + databaze); // Necháme vytvořit databázi
            PripojovaciUdaje = PripojovaciUdaje + ";database=" + databaze; // A zase vrátíme databázi na konec připojovacího řetězce
            ZmenaUdaju = true; // Opět došlo ke změně údajů
        }

        /// <summary>
        /// Provede zadaný dotaz
        /// </summary>
        /// <param name="dotaz">Dotaz, který se má provést</param>
        public void Dotaz(string dotaz)
        {
            if (Nastaveni.StavAplikace == (int)Nastaveni.StavyAplikace.koncici) // Pokud se má Úkolník ukončit, tak není třeba provádět dotaz
                return;
            if (Vysledek != null && Vysledek.IsClosed == false) // Pokud existuje nebo je otevřeno čtení výsledku, tak ho uzavřeme
                Vysledek.Close();
            Vysledek = null; // A pošleme ho do lovišť GC
            Vysledku = 0; // Vynulujeme čítač výsledků
            Chyba = false; // Vynulujeme indikaci chyby
            if (ZmenaUdaju == true) // Pokud došlo ke změně připojovacích údajů a připojení je otevřené, tak ho uzavřeme, připravíme připojení s novými údaji a zrušíme zěmnu údajů, protože už jsme ji provedli
            {
                if (Pripojeni.State == System.Data.ConnectionState.Open)
                    Pripojeni.Close();
                Pripojeni = new MySqlConnection(PripojovaciUdaje);
                ZmenaUdaju = false;
            }
            try // Zkusíme otevřít připojení, tedy pokud již není otevřené
            {
                if (Pripojeni.State != System.Data.ConnectionState.Open)
                {
                    Pripojeni.Open();
                    StejnychChybVRade = 0; // Po otevření vynulujeme řadu chyb
                    KodChyby = 0; // A číslo poslední chyby, protože začínáme na novo
                }
            }
            catch (MySqlException exc) // Pokud se otevření připojení nezdařilo
            {
                if (exc.Number == 1045 || exc.Number == 1044) // Z důvodu špatných přihlašovacích údajů, v tom případě zobrazíme info (pouze pokud to není bezprostředně po vytvoření konfiguráku) a zavoláme ošetření chyb (tedy otevření nastavovacího okna) a dál se nepokračuje, protože nastavovací okno si případně zavolá znovu tento dotaz
                {
                    Chyba = true;
                    if (Nastaveni.PraveVytvorenKonfigurak == false)
                        Vyjimky.VypisVyjimek("Byl odepřen přístup, nejspíše špatný uživatel nebo heslo.", exc);
                    OsetreniChyby(dotaz);
                    return;
                }
                else if (exc.Number == 1049) // Z důvodu neexistující databáze, tak jí vytvoříme a znovu zavoláme dotaz (při něm vyskočí info o neexistující tabulce, otevře se nastavení a s jeho uložením se dovytvoří tabulky)
                {
                    VytvorDatabazi();
                    Dotaz(dotaz);
                    return;
                }
                else if (exc.Number == 1042) // Nebyl nalezen MySQL proces, takže se nastaví chyby, zjistí bitová verze OS a zobrazí se info o tom i s doporučením, kde sehnat MySQL databázi, pak se otevře okno s nastavením
                {
                    Chyba = true;
                    string verzeOS;
                    if (Environment.Is64BitOperatingSystem == true)
                        verzeOS = "64";
                    else
                        verzeOS = "32";
                    Vyjimky.VypisVyjimek("Na zadané adrese není spuštěna MySQL databáze. Je tam řádně nainstalována a spuštěna? MySQL databázi lze získat na této adrese: http://dev.mysql.com/downloads/mysql/ (doporučená verze MSI Installer " + verzeOS + "-bit)", exc);
                    OsetreniChyby(dotaz);
                    return;
                }
                else if (exc.ToString().Contains("using method 'mysql_native_password' failed with message: Reading from the stream has failed.") || exc.ToString().Contains("Fatal error encountered during command execution. ---> System.IO.IOException:")) //Není potřeba řešit
                {
                    return;
                }
                else // Neznámá chyba
                {
                    Chyba = true;
                    StejnychChybZaSebou(exc.Number); // Zkontrolujeme, zda není stejná jako minule
                    if (DlouhaRadaChyb()) // Pokud to je už šestá stejná chyba za sebou, tak se vyvolá ukončení
                    {
                        StejnaChybaFurt();
                        return;
                    }
                    Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + " Chyba cislo " + exc.Number.ToString() + " (chybovy kod cislo " + exc.ErrorCode.ToString() + "): " + exc.ToString()); // Zapíše se chyba do souboru (a pak někdy se odešle), aby se mohla případně opravit
                    Vyjimky.VypisVyjimek("Neznámá chyba, prosím o podrobný popis (co si zrovna dělal a tak) včetně následujícího výpisu (měl by se uložit do schránky jako když kopíruješ text), vložit do připomínek, abych se na to mohl podívat. Děkuji a omlouvám se.", exc, true); // Zobrazí se podrobný popisek
                    try // Uloží do schránky popis vyjimky, je to v try, protože to může hodit chybu
                    {
                        Clipboard.SetText(exc.ToString());
                    }
                    catch (Exception)
                    {
                    }
                    Dotaz(dotaz); // Znovu provede dotaz
                    return; // Není třeba pokračovat dál, protože se už znovu provádí ten dotaz a takto by se mohl udělat 2x
                }
            }
            catch (TimeoutException exc) // Po více než měsíci testování z ničeho nic během jednoho dne po zapnutí počítače (2 různý zapnutí) vyskočila časová chyba -> asi počítač nestíhal, takže toto to snad vyřeší
            {
                StejnychChybZaSebou(-1); // Identifikace časové chyby je -1, kontroluje se, zda toho není víc za sebou
                if (DlouhaRadaChyb()) // Pokud je, tak ukončení aplikace
                {
                    Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + " Chyba: " + exc.ToString()); // Zapíše se do souboru s chybama
                    StejnaChybaFurt();
                    return;
                }
                Vyjimky.VypisVyjimek("Vypršel čas pro požadovanou akci, bude snaha ji vykonat znovu.", exc); // Zobrazí info o vypršení času
                Dotaz(dotaz); // Znovu provede dotaz
                return; // Není třeba pokračovat dál, protože se už znovu provádí ten dotaz a takto by se mohl udělat 2x
            }
            catch (Exception exc) // Všeobecná chyba, takže není jasné, co se stalo, pokusí se zkusit znova stejnou akci
            {
                StejnychChybZaSebou(-2); // Identifikace neznámé chyby je -2, zkontroluje se, zda to není již několikátá chyba za sebou
                if (DlouhaRadaChyb()) // Pokud je těchto chyb více za sebou, tak se provede ukončení Úkolníku
                {
                    StejnaChybaFurt();
                    return;
                }
                Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + " Chyba: " + exc.ToString()); // Zapíše se do souboru s chybama
                Vyjimky.VypisVyjimek("Neznámá chyba, prosím o podrobný popis (co si zrovna dělal a tak) včetně následujícího výpisu (měl by se uložit do schránky jako když kopíruješ text), vložit do připomínek, abych se na to mohl podívat. Děkuji a omlouvám se.", exc, true); // Zobrazí se info s prosbou o nahlášení chyby, sice se chyba ukloží do souboru, ale je lepší vědět při jaké příležitosti nastala
                try // Uloží do schránky popis vyjimky, je to v try, protože to může hodit chybu
                {
                    Clipboard.SetText(exc.ToString());
                }
                catch (Exception)
                {
                }
                Dotaz(dotaz); // Pokusí se znovu provést dotaz
                return; // Není třeba pokračovat dál, protože se už znovu provádí ten dotaz a takto by se mohl udělat 2x
            }
            if (Pripojeni.State == System.Data.ConnectionState.Open) // Pokud je spojení otevřené
            {
                MySqlCommand prikaz = new MySqlCommand(dotaz, Pripojeni); // Vytvoříme dotaz
                try // Pokusíme se se dotaz provést a získat tak výsledek dotazu
                {
                    Vysledek = prikaz.ExecuteReader();
                    StejnychChybVRade = 0; // Když to bylo OK, tak se vynuluje řada chyb
                    KodChyby = 0; // Když to bylo OK, tak se odnastaví poslední kód chyby
                }
                catch (MySqlException exc) // Pokud došlo k vyjimce při vykonávání dotazu
                {
                    Chyba = true; // Nastala chyba
                    if (exc.Number == 1146) // Nebyla nalezena tabulka, zobrazí se info
                    {
                        Vyjimky.VypisVyjimek("V databázi chybí jedna tabulka. Měla by být doplněna po odsouhlasení nastavení aplikace.", exc); // Zobrazí se info co a jak
                        OsetreniChyby(dotaz); // Zobrazí se nastavení aplikace, po jejím uložení dojde k vytvoření tabulky
                        Dotaz(dotaz); // Snaha znovu vyvolat dotaz, když se tabulka dotvořila
                        return;
                    }
                    else if (exc.Number == 1142) // Problém s pravomocemi, takže se zobrazí info a nastavovaví tabulka
                    {
                        Chyba = true;
                        Vyjimky.VypisVyjimek("Nastal problém s pravomocemi, je potřeba mít v databázi " + Nastaveni.Databaze + " tyto pravomoce: Create, Delete, Insert, Select a Update.", exc);
                        OsetreniChyby(dotaz);
                        return;
                    }
                    else // Předpokládá se pouze chyba v dotazu, je možné, že mohou nastat i jiné, ale to by měl odchytit soubor s chybama
                    {
                        StejnychChybZaSebou(exc.Number); // Kontrola, zda se nevyskytuje jedna a ta samá chyba
                        if (DlouhaRadaChyb()) // Pokud vyskytuje a tvoří dlouhou řadu, tak dojde k ukončení Úkolníku
                        {
                            StejnaChybaFurt();
                            return;
                        }
                        Nastaveni.ZapisChybovySoubor(DateTime.Now.ToString("d.M.yyyy H:mm:ss") + " Chyba cislo " + exc.Number.ToString() + " (chybovy kod cislo " + exc.ErrorCode.ToString() + ") pri dotazu " + dotaz + ":" + exc.ToString()); // Chyba se zaloguje do souboru s chybama, aby se pak mohl odeslat
                        Vyjimky.VypisVyjimek("Vadný dotaz. Zde je ten dotaz: \n" + dotaz, exc); // Vypíše se info -> opět jen o dotazu ze stejných důvodů, dotaz se znovu neopakuje, protože pokud je to chyba v dotazu, tak ta tam bude furt
                    }
                }
            }
        }

        /// <summary>
        /// Zjistí počet výsledků, která databáze vrátila
        /// </summary>
        /// <returns>Pokud nastala chyba při připojení, tak vrátí -1, jinak vrací počet výsledků</returns>
        public int DejVysledku()
        {
            if (Chyba == true) // Pokud nastala chyba, tak je výsledků -1
                return -1;
            else if (Pripojeni.State == System.Data.ConnectionState.Open) // Pokud je spojení otevřeno, tak čteme výsledky a počítáme jejich počet
            {
                while (Vysledek.Read())
                    Vysledku += 1;
                Vysledek.Close();
            }
            return Vysledku; // Vrátíme počet výsledků
        }

        /// <summary>
        /// Zjistí počet výsledků, které jsme zatím zpracovali
        /// </summary>
        /// <returns>Vrátí počet zpracovaných výsledků (tedy kolik jich bylo zatím přečteno), při chybě během připojení se vrací -1</returns>
        public int DejAktualneVysledku()
        {
            if (Chyba == true) // Pokud nastala chyba, tak je výsledků -1
                return -1;
            else
                return Vysledku; // Vrátíme počet výsledků
        }

        /// <summary>
        /// Dá textový výsledek dle chtěné položky
        /// </summary>
        /// <param name="polozka">Jakou položku chceme</param>
        /// <returns>Vrací vyžádanou položku v podobě stringu</returns>
        public string DejVysledekString(string polozka)
        {
            if (Vysledek == null || Vysledek.IsClosed == true || Chyba == true)
                return "";
            try // Zda položka existuje
            {
                return Vysledek.GetString(polozka);
            }
            catch (Exception exc)
            {
                Vyjimky.VypisVyjimek("Daná položka nebyla nalezena. Není jisté, jaké bude chování Úkolníku po této chybě.", exc);
                return "";
            }
        }

        /// <summary>
        /// Dá číselný výsledek dle chtěné položky
        /// </summary>
        /// <param name="polozka">Jakou položku chceme</param>
        /// <returns>Vrací vyžádanou položku v podobě integeru</returns>
        public int DejVysledekInt(string polozka)
        {
            if (Vysledek == null || Vysledek.IsClosed == true || Chyba == true)
                return 0;
            try // Zda položka existuje
            {
                return Vysledek.GetInt32(polozka);
            }
            catch (Exception exc)
            {
                Vyjimky.VypisVyjimek("Daná položka nebyla nalezena. Není jisté, jaké bude chování Úkolníku po této chybě.", exc);
                return 0;
            }
        }

        /// <summary>
        /// Dá datumový výsledek dle chtěné položky
        /// </summary>
        /// <param name="polozka">Jakou položku chceme</param>
        /// <returns>Vrací vyžádanou položku v podobě DateTime</returns>
        public DateTime DejVysledekDatumCas(string polozka)
        {
            if (Vysledek == null || Vysledek.IsClosed == true || Chyba == true)
                return DateTime.Now;
            try // Zda položka existuje
            {
                return Vysledek.GetDateTime(polozka);
            }
            catch (Exception exc)
            {
                Vyjimky.VypisVyjimek("Daná položka nebyla nalezena. Není jisté, jaké bude chování Úkolníku po této chybě.", exc);
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Vrací zda jsou ještě další výsledky na zpracování
        /// </summary>
        /// <returns>Pokud ještě všechny výsledky nebyly zpracovány, tak vrátí True, jinak False</returns>
        public bool DalsiVysledek()
        {
            if (Nastaveni.StavAplikace == (int)Nastaveni.StavyAplikace.koncici) // Pokud se má ukončit aplikace, tak není co zpracovávat
                return false;
            else if (Chyba == true || Vysledek == null) // Pokud nastala chyba při připojení, tak není co zpracovávat
                return false;
            else if (Vysledek.Read() == true) // Pokud výsledek něco obsahuje, tak zvýšíme čítač výsledků a vrátíme True
            {
                Vysledku += 1;
                return true;
            }
            else // Pokud už není nic ve výsledkách, tak je zavřeme a vrátíme False
            {
                Vysledek.Close();
                return false;
            }
        }

        /// <summary>
        /// Funkce starající se o reakci na chybu
        /// </summary>
        /// <param name="dotaz">Dotaz při kterém vznikla chyba</param>
        private void OsetreniChyby(string dotaz)
        {
            if (Nastaveni.NastaveniOtevreno == false) // Pokud není nastavovací okno otevřeno
            {
                Obecne.IkonaStav(false); // Deaktivujeme ikonu, aby nešlo vyskočit z nastavení
                NastavovaciOkno nastaveni = new NastavovaciOkno(true); // Vytvoříme nové nastavovací okno s indikací, že bylo otevřeno z důvodu chyby
                if (nastaveni.ShowDialog() == DialogResult.Yes) // Pokud bylo kliknuto na uložit nové údaje (a bylo to úspěšné, protože jinak by nedošlo k ukončení dialogu) tak se znova zavolá dotaz, aby se provedl
                    Dotaz(dotaz);
                else if (Nastaveni.StavAplikace == (int)Nastaveni.StavyAplikace.koncici) // Pokud nebylo v nastavení opraveno připojení k databázi, tak je vyžádáno ukončení Úkolníku a to taky bude provedeno
                    Application.Exit();
                Obecne.IkonaStav(true); // Opět aktivujeme ikonu
            }
        }

        /// <summary>
        /// Reší, zda je stejná chyby po sobě a případně upraví čítače
        /// </summary>
        /// <param name="porovnavaciChyba">Číslo chyby se kterou se má chyba porovnat</param>
        private void StejnychChybZaSebou(int porovnavaciChyba)
        {
            if (KodChyby != porovnavaciChyba) // Pokud se chyby sobě nerovnaj, tak vynulujeme řadu a poslední chyba bude nastavena dle nové poslední chyby (tamta se stala předposlední)
            {
                StejnychChybVRade = 0;
                KodChyby = porovnavaciChyba;
            }
            else // Pokud se chyby shodují, tak se zvýší čítač stejných chyb za sebou
                StejnychChybVRade += 1;
        }

        /// <summary>
        /// Pokud byl překročen počet stejných chyb za sebou, tak se o tom zobrazí info a ukončí se Úkolník
        /// </summary>
        private void StejnaChybaFurt()
        {
            Obecne.ZobrazZpravu("V Úkolníku se vyskytuje neustále jedna a ta samá chyba, takže bude ukončen.", "Chyba vedle chyby", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Nastaveni.NastavStavAplikace((int)Nastaveni.StavyAplikace.koncici);
            Application.Exit();
        }

        /// <summary>
        /// Kontroluje, zda už nebylo těch chyb až příliš za sebou
        /// </summary>
        /// <returns>Pokud chyb bylo 6 a víc, tak se vrátí True, jinak False</returns>
        private bool DlouhaRadaChyb()
        {
            if (StejnychChybVRade > 5)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// Statická třída pro zobrazování vyjimek
    /// </summary>
    public static class Vyjimky
    {
        /// <summary>
        /// Zobrazí vyjimku, která nastala
        /// </summary>
        /// <param name="text">Komentář k vyjimce</param>
        /// <param name="vyjimka">Samotná vyjimka</param>
        /// <param name="podrobnyVypis">Zda se má zobrazit podrobný výpis vyjimky nebo jen komentář</param>
        public static void VypisVyjimek(string text, Exception vyjimka, bool podrobnyVypis = false)
        {
            if (Nastaveni.PodrobnostiVyjimek == true || podrobnyVypis == true) // Pokud má uživatel nastavené podrobné vyjimky nebo pro tuto vyjimku je nastaven podrobný výpis, tak bude ke komentáři vyjimky přidán podrobný popis vyjimky
                text += "\n" + vyjimka.ToString();
            Obecne.ZobrazZpravu(text, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error); // Zobrazíme popis vyjimky
        }
    }

    /// <summary>
    /// Statická třída obsahující funkce a proměnné, které jsou potřebné pro Úkolník
    /// </summary>
    public static class Obecne
    {
        /// <summary>
        /// Všechny povolené typy událostí i s jejich hodnotou, které je používána v databázi
        /// </summary>
        public enum UdalostiTypy {
            /// <summary>
            /// Událost symbolizující úkol (něco, co by mělo být splněno do určité doby)
            /// </summary>
            ukol, 
            /// <summary>
            /// Událost symbolizujcíí událost (něco, co nastane v určitou dobu)
            /// </summary>
            udalost, 
            /// <summary>
            /// Událost symbolizující písemku (něco, co by mělo v určitou dobu nastat)
            /// </summary>
            pisemka, 
            /// <summary>
            /// Událost symbolizující narozeniny (něco co je každým rokem)
            /// </summary>
            narozeniny};
        /// <summary>
        /// Typy upozornění na události
        /// </summary>
        public enum UpozorneniTypy { 
            /// <summary>
            /// Když se na událost neupozorňuje
            /// </summary>
            neupozornit, 
            /// <summary>
            /// Upozornění na událost je automatické
            /// </summary>
            upozornitAutomaticky, 
            /// <summary>
            /// Upozornění na událost je nastaveno uživatelem
            /// </summary>
            upozornitVlastni, 
            /// <summary>
            /// Na událost bylo upozorněno dle automatického upozornění
            /// </summary>
            upozornenoAutomaticky, 
            /// <summary>
            /// Na událost bylo upozorněno dle uživatelského upozornění
            /// </summary>
            upozornenoVlastni};
        /// <summary>
        /// Kolekce všech názvů událostí pro našeptávání
        /// </summary>
        public static AutoCompleteStringCollection PametNadpis;
        /// <summary>
        /// Odkaz na nabídku ikony, aby šla vypínat před dialogy
        /// </summary>
        private static ContextMenuStrip IkonaNabidka = null;

        /// <summary>
        /// Zjistí zda je nabídka ikony aktivní
        /// </summary>
        /// <returns>Vrací true pokud je aktivní, jinak false</returns>
        public static bool IkonaAktivni()
        {
            if (IkonaNabidka == null)
                return false;
            return IkonaNabidka.Enabled;
        }

        /// <summary>
        /// Nastavuje stav nabídce ikony, zda je aktivní nebo ne
        /// </summary>
        /// <param name="aktivni">Stav, který má být nastaven nabídce ikony, zda má být povolená (true) nebo zakázánaá (false)</param>
        public static void IkonaStav(bool aktivni)
        {
            if (IkonaNabidka == null)
                return;
            IkonaNabidka.Enabled = aktivni;
        }

        /// <summary>
        /// Nastaví odkaz na nabídku ikony,  aby se k ní dalo přistupovat
        /// </summary>
        /// <param name="ikona"></param>
        public static void IkonaNastav(ContextMenuStrip ikona)
        {
            IkonaNabidka = ikona;
        }
        
        /// <summary>
        /// Zjistí verzi Úkolníku
        /// </summary>
        /// <returns>Vrátí verzi Úkolníku</returns>
        public static string DejVerzi()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Zjistí název zadaného typu události
        /// </summary>
        /// <param name="typ">Typ události</param>
        /// <returns>Vrátí název této události</returns>
        public static string DejNazevTypuUdalosti(int typ)
        {
            switch (typ)
            {
                case (int) UdalostiTypy.ukol:
                    return "Úkol";
                case (int) UdalostiTypy.udalost:
                    return "Událost";
                case (int) UdalostiTypy.pisemka:
                    return "Písemka";
                case (int) UdalostiTypy.narozeniny:
                    return "Narozeniny";
                default:
                    return "Neznámá";
            }
        }

        /// <summary>
        /// Posune událost o určený interval
        /// </summary>
        /// <param name="IDudalost">ID události, která má být přesunuta</param>
        /// <param name="typ">Typ události, která se přesouvá</param>
        /// <param name="interval">MySQL interval po přesun dne</param>
        public static void PresunUdalost(string IDudalost, int typ, string interval)
        {
            Databaze db = new Databaze();
            db.Dotaz("UPDATE udalosti SET Kdy=DATE_ADD(Kdy, INTERVAL 1 " + interval + "), Upozorneni=DATE_ADD(Upozorneni, INTERVAL 1 " + interval + ") WHERE ID=" + IDudalost + ";"); // Posuneme událost o interval, který je určen pro danou nabídku (hodina, den, týden, měsíc, rok) včetně upozornění
            db.Dotaz("UPDATE udalosti SET Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " WHERE Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoAutomaticky).ToString() + " AND Upozorneni>NOW();"); // Pokud díky posunutí je upozornění v budoucnu, tak opět umožníme upozornění (automatické upozornění)
            db.Dotaz("UPDATE udalosti SET Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitVlastni).ToString() + " WHERE Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornenoVlastni).ToString() + " AND Upozorneni>NOW();"); // Pokud díky posunutí je upozornění v budoucnu, tak opět umožníme upozornění (uživatelské upozornění)
            if (typ == (int)Obecne.UdalostiTypy.narozeniny) // Pokud to jsou narozeniny, tak si musí pohlídat, zda nebylo vyskočeno z roku 4 a pak je třeba udělat aktualizaci svátků
            {
                db.Dotaz("UPDATE udalosti SET Kdy=DATE_ADD(Kdy, INTERVAL 4-YEAR(Kdy) YEAR) WHERE Kdy>'0004-12-31 23:59:59' AND Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ";");
                Obecne.OnZmenaVeSvatcich();
            }
            db.Close();
            Obecne.OnZmenaVUdalostech(); // Provedeme aktualizaci událostí
        }

        /// <summary>
        /// Načte a zobrazí svátky pro daný den
        /// </summary>
        /// <param name="datum">Pro který den se mají zjišťovat svátky</param>
        /// <param name="label">Label ve kterém budou svátky vypsány</param>
        /// <param name="narozeniny">Zda se mají do svátků počítat i narozeniny</param>
        /// <param name="dnes">Zda se má psát Dnes nebo Tento den</param>
        public static void Svatky(string datum, Label label, bool narozeniny = false, bool dnes = false)
        {
            string text = ""; // Text, který bude zobrazen v zadaném Labelu
            string uvod; // Jak budou svátky uváděny
            if (dnes == false) // Zda mají být uvedeny svátky Tento den
                uvod = "Tento den ";
            else // Nebo Dnes
                uvod = "Dnes ";
            label.ForeColor = System.Drawing.SystemColors.ControlText; // Barvu textu nastavíme na normální barvu pro Labely
            Databaze db = new Databaze();
            db.Dotaz("SELECT * FROM vyznamne_dny WHERE Datum='" + datum + "';"); // Vytáhneme pro daný den významné dny
            while (db.DalsiVysledek() == true) // Pokud bylo něco nalezeno
            {
                text = uvod + "je " + db.DejVysledekString("Den") + ".\n"; // Vytvoříme text obsahující info, jaký je ten den významný den
                if (db.DejVysledekString("Upozornit") == "1") // Pokud má být tento den zvýrazněn, tak text bude nastaven na zvýrazňovací barvu
                    label.ForeColor = System.Drawing.SystemColors.Highlight;
            }
            db.Dotaz("SELECT * FROM svatky WHERE Datum='" + datum + "';"); // Vytáhneme pro daný den svátky
            while (db.DalsiVysledek() == true) // Pokud bylo něco nalezeno
            {
                if (db.DejAktualneVysledku() == 1) // Pokud to je první svátek pro daný den, tak bude připsán úvod
                    text += uvod + "má svátek ";
                else // Pokud je to už druhý svátek, tak se připíše spojka a
                    text += " a ";
                text += db.DejVysledekString("Jmeno"); // Vloží se, kdo má svátek
                if (db.DejVysledekString("Upozornit") == "1") // Pokud na tento svátek má být upozorněno, tak se nastaví zvýrazňující barva
                    label.ForeColor = System.Drawing.SystemColors.Highlight;
            }
            if (db.DejAktualneVysledku() > 0) // Pokud byl nějaký svátek nalezen, tak větu dokončíme tečkou a odřádkujeme
                text += ".\n";
            if (narozeniny == true) // Pokud se mají zobrazovat i narozeniny
            {
                db.Dotaz("SELECT * FROM udalosti WHERE DATE(Kdy)='0004-" + DateTime.Today.ToString("MM-dd") + "' AND Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " ORDER BY Nadpis;"); // Vytáhneme kdo má ten den narozeniny
                while (db.DalsiVysledek() == true) // Pokud někdo má narozeniny
                {
                    if (db.DejAktualneVysledku() == 1) // Pokud jsou to první narozeniny pro daný den, tak se napíšou úvodní kecy
                        text += uvod + "má narozeniny ";
                    else // Pokud to jsou už druhé či více narozenin pro ten den, tak pouze spojka a
                        text += " a ";
                    text += db.DejVysledekString("Nadpis") + " (" + (DateTime.Today.Year - db.DejVysledekInt("Zprava")).ToString() + ")"; // Kdo má narozeniny a kolik mu je let se uloží do textu
                }
                if (db.DejAktualneVysledku() > 0) // Jestliže byly nalezeny nějaké narozeniny, tak se na konec přidá tečka a zvýrazní se text
                {
                    text += ".";
                    label.ForeColor = System.Drawing.SystemColors.Highlight;
                }
            }
            db.Close();
            label.Text = text; // Výsledný text s přehledem kdo má svátek, co je za den, kdo má narozeniny necháme zobrazit v Labelu
        }

        /// <summary>
        /// Připraví narozeniny pro přehled, který přesahuje rok (například seznam nejbližích narozenin zasahuje i do příštího roku a je třeba to nějak setřídit) nebo pro výpočet věku u někoho, kdo má další narozeniny až příští rok
        /// </summary>
        public static void PripravNarozeninyPrehled()
        {
            Databaze db = new Databaze();
            db.Dotaz("UPDATE udalosti SET Splneno=0 WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND DATE(Kdy)>='0004-" + DateTime.Today.ToString("MM-dd") + "';"); // Kde je měsíc a den větší nebo roven než dnešek, tak je nastavena nula, protože ty narozeniny budou ještě letos
            db.Dotaz("UPDATE udalosti SET Splneno=1 WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND DATE(Kdy)<'0004-" + DateTime.Today.ToString("MM-dd") + "';"); // Kde je měsíc a den menší než dnešek, tak je nastavena jendička, protože ty narozeniny budou až příští rok
            db.Close();
        }

        /// <summary>
        /// Stará se o vyplnění kalendáře, aby byly zvýrazněny data, kdy jsou nějaké události, ale je přednačtena jen část dat z důvodu úspory paměti
        /// </summary>
        /// <param name="kalendar">Odkaz na kalendář, který má být vyplněn</param>
        /// <param name="jenNesplnene">Zda se mají zobrazit jen nesplněné události</param>
        public static void VyplnKalendar(MonthCalendar kalendar, bool jenNesplnene = false)
        {
            kalendar.RemoveAllBoldedDates(); // Smaže předchozí zvýrazněná data
            kalendar.RemoveAllAnnuallyBoldedDates(); // Smaže předchozí zvýrazněné narozeniny
            DateTime minulyMesic = DateTime.Now; // Bude sloužit jako minulý měsíc, protože je zbytečné načítat data na celý rok, když budou vidět třeba jen 2 měsíce, takže tímto načteme nejbližší měsíce pro případ skoku na dnes
            minulyMesic = minulyMesic.AddMonths(1 - kalendar.CalendarDimensions.Height); // Tímto skočíme z aktuálního měsíce do minulého
            minulyMesic = minulyMesic.AddDays(1 - minulyMesic.Day); // A tímhle skočíme na prvního toho měsíce
            DateTime nasledujiciMesic = minulyMesic; // Následující měsíc toho aktuálního dostaneme z minulého měsíce
            nasledujiciMesic = nasledujiciMesic.AddMonths(1 + kalendar.CalendarDimensions.Height); // Poskočíme do aktuálního měsíce a pak o tolik měsíců, kolik jich lze zobrazit 
            nasledujiciMesic = nasledujiciMesic.AddDays(-1); // Jsme na začátku měsíce o jeden měsíc napřed, ale odebereme den a jsme na konci toho správného a nemusel jsem ani řešit kolik má který měsíc dnů
            SelectionRange rozmeziAktualni = new SelectionRange(minulyMesic, nasledujiciMesic); // Vytvoříme rozmezí aktuálních měsíců, které slouží pro skok na Dnes
            SelectionRange rozmezi = kalendar.GetDisplayRange(true); // Získáme rozmezí, které je vidět v kalendáři
            rozmezi.Start = rozmezi.Start.AddMonths(-kalendar.CalendarDimensions.Height); // To rozmezí rozšíříme i o tolik měsíců zpátky, aby při posunu měsíců v kalendáři o jedno zpět jsme stále byli v tom rozmezí a tím pádem byla přednačtená data
            rozmezi.End = rozmezi.End.AddMonths(kalendar.CalendarDimensions.Height); // To rozmezí rozšíříme i o tolik měsíců dopředu, aby při posunu měsíců v kalendáři o jedno dopředu jsme stále byli v tom rozmezí a tím pádem byla přednačtená data
            Databaze db = new Databaze();
            string splnenost = "";
            if (jenNesplnene == true) // Zda budou zobrazena bude jen nesplněná data
                splnenost = "Splneno=0 AND ";
            db.Dotaz("SELECT DISTINCT Kdy FROM udalosti WHERE " + splnenost + "((DATE(Kdy)>='" + rozmezi.Start.Year.ToString() + "-" + rozmezi.Start.Month.ToString() + "-" + rozmezi.Start.Day.ToString() + "' AND DATE(Kdy)<='" + rozmezi.End.Year.ToString() + "-" + rozmezi.End.Month.ToString() + "-" + rozmezi.End.Day.ToString() + "') OR (DATE(Kdy)>='" + rozmeziAktualni.Start.Year.ToString() + "-" + rozmeziAktualni.Start.Month.ToString() + "-" + rozmeziAktualni.Start.Day.ToString() + "' AND DATE(Kdy)<='" + rozmeziAktualni.End.Year.ToString() + "-" + rozmeziAktualni.End.Month.ToString() + "-" + rozmeziAktualni.End.Day.ToString() + "'));"); // Získáme unikátní data událostí z obou rozmezí
            while(db.DalsiVysledek()) // Postupně tam budeme vkládat data událostí
                kalendar.AddBoldedDate(db.DejVysledekDatumCas("Kdy"));
            string spojka = "AND";
            if (rozmezi.Start.Month > rozmezi.End.Month || (rozmezi.Start.Month == rozmezi.End.Month && rozmezi.Start.Day > rozmezi.End.Day)) // Pokud jsou měsíce souvislé, tak tam bude v dotazu AND, pokud nejsou souvislé, protože jsou souvislé přes rok, tak tam bude OR
                spojka = "OR";
            db.Dotaz("SELECT DISTINCT Kdy FROM udalosti WHERE Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + " AND (DATE(Kdy)>='0004-" + rozmezi.Start.ToString("MM") + "-" + rozmezi.Start.ToString("dd") + "' " + spojka + " DATE(Kdy)<='0004-" + rozmezi.End.ToString("MM") + "-" + rozmezi.End.ToString("dd") + "');"); // Získáme unikátní data narozenin v zadaném rozmezí
            while (db.DalsiVysledek()) // Postupně je přdiáváme do ročních zvýrazněných dat
                kalendar.AddAnnuallyBoldedDate(db.DejVysledekDatumCas("Kdy"));
            db.Close();
        }

        /// <summary>
        /// Zobrazí zadanou zprávu se zadaným nadpisem zvolenými tlačítky a zvolenou ikonkou
        /// </summary>
        /// <param name="text">Text, který bude zobrazen ve zprávě</param>
        /// <param name="nadpis">Nadpis zprávy</param>
        /// <param name="tlacitka">Jaká tlačítka se mají zobrazit</param>
        /// <param name="ikonka">Jaká ikonka se má zobrazit</param>
        /// <returns>Vrátí výsledek zobrazeného dialogu</returns>
        public static DialogResult ZobrazZpravu(string text, string nadpis, MessageBoxButtons tlacitka, MessageBoxIcon ikonka)
        {
            Obecne.IkonaStav(false); // Vypneme ikonu, aby nešlo obejít dialog
            DialogResult vysledek = MessageBox.Show(text, nadpis, tlacitka, ikonka); // Otevřeme okno a získáme výsledek dialogu
            Obecne.IkonaStav(true); // Opět zapneme ikonu
            return vysledek; // Vrátíme výsledek dialogu
        }

        /// <summary>
        /// Získá umístění pro nové okno
        /// </summary>
        /// <param name="lokace">Umístění, kde se má zobrazit nové okno</param>
        /// <param name="velikost">Velikost nového okna</param>
        /// <param name="posunVpravo">O kolik se má umístění změnit na ose X od lokace</param>
        /// <param name="posunDolu">O kolik se má umístění změnit na ose Y od lokace</param>
        /// <returns>Vrací umístění okna pro nové okno</returns>
        public static Point UmisteniOkna(Point lokace, Size velikost, int posunVpravo = 50, int posunDolu = 50)
        {
            int x = lokace.X + posunVpravo; // Základní X souřadnice bude o kousek dál vpravo než původní okno
            if (x + velikost.Width > Screen.GetWorkingArea(lokace).X + Screen.GetWorkingArea(lokace).Width) // Kontrola, zda okno nepřečnívá na pravém okraji obrazovky, pokud ano, tak ho posuneme více vlevo
                x = x - (x + velikost.Width - Screen.GetWorkingArea(lokace).X - Screen.GetWorkingArea(lokace).Width) - 25;
            if (x < Screen.GetWorkingArea(lokace).X) // Kontrola, zda okno nepřečnívá na levém okraji obrazovky, pokud ano, tak ho posuneme více vpravo
                x = Screen.GetWorkingArea(lokace).X + 25;
            int y = lokace.Y + posunDolu; // Základní Y souřadnice bude o kousek níž než původní okno
            if (y + velikost.Height > Screen.GetWorkingArea(lokace).Y + Screen.GetWorkingArea(lokace).Height) // Kontrola, zda okno nepřečnívá na dolním okraji obrazovky, pokud ano, tak se malinko posune výš
                y = y - (y + velikost.Height - Screen.GetWorkingArea(lokace).Y - Screen.GetWorkingArea(lokace).Height) - 25;
            if (y < Screen.GetWorkingArea(lokace).Y) // Kontrola, zda okno nepřečnívá na horním okraji obrazovky, pokud ano, tak se malinko posune níž
                y = Screen.GetWorkingArea(lokace).Y + 25;
            return new Point(x, y);
        }

        /// <summary>
        /// Odstraní z textu problematické znaky, které by při ukládání do databáze mohly dělat menší problémy
        /// </summary>
        /// <param name="text">Text, který mý být ošetřen na problematické znaky</param>
        /// <returns>Vrací text očištěný od znaků, které by dělaly problémy při ukládání do databáze</returns>
        public static string OdstranProblemoveZnaky(string text)
        {
            text = text.Replace("\\", "\\\\"); // Zpětná lomítka nahradit (zde jsou dvě za sebou, protože C# si je escapuje, ale do databáze by dal jen jedno a to by udělalo nepořádek)
            text = text.Replace("'", "\\'");
            return text;
        }

        /// <summary>
        /// Odstraní z textu problematické znaky pro HTTP POST metodu, tedy odstraní &
        /// </summary>
        /// <param name="text">Text, který se má očistit od problematických znaků</param>
        /// <returns>Vrátí text bez problematických znaků</returns>
        public static string OdstranProblemoveHTTPPOSTZnaky(string text)
        {
            return text.Replace("&", "%26");
        }

        /// <summary>
        /// Odesílá data na web, aby autor měl přístup k chybám nebo připomínkám a návrhům
        /// </summary>
        /// <param name="data">Data která se mají odeslat na web</param>
        /// <param name="adresa">Kam se mají odeslat</param>
        /// <param name="zobrazitNezdar">Zda se má zobrazit nezdar při odesílání (využíváno pro nezobrazení nezdaru při odesílání chybového souboru)</param>
        /// <returns>Odpověď od webu na odeslání dat</returns>
        public static string PosliDataNaWeb(string data, string adresa, bool zobrazitNezdar = true)
        {
            System.IO.Stream dataStream = null; // Datový stream odesílající data na web
            System.Net.WebResponse odpoved = null; // Odpověď serveru na odeslaná data
            System.IO.StreamReader reader = null; // Čtení odpovědi
            string odpovezeno = "ERROR"; // Základní odpověď (předpokládá se chyba v odeslání z důvodu internetu, ale pokud je vše ok, tak je tato odpověď nahrazena skutečnou odpovědí)
            try
            {
                System.Net.WebRequest pozadavek = System.Net.WebRequest.Create(adresa); // Vytvoříme požadavek na danou webovou adresu
                pozadavek.Method = "POST"; // Využijem metodu POST
                byte[] obsah = System.Text.Encoding.UTF8.GetBytes(data); // Zakódujeme data
                pozadavek.ContentType = "application/x-www-form-urlencoded"; // Typ obsahu
                pozadavek.ContentLength = obsah.Length; // Nastavíme délku obsahu
                dataStream = pozadavek.GetRequestStream(); // Otevřeme datový stream na zadanou adresu
                dataStream.Write(obsah, 0, obsah.Length); // Začneme hrnout data do streamu
                odpoved = pozadavek.GetResponse(); // Získáme odpověď server na požadavek
                dataStream = odpoved.GetResponseStream(); // Odpověď jde do datového streamu
                reader = new System.IO.StreamReader(dataStream); // Dostaneme odpověď ze streamu
                odpovezeno = reader.ReadToEnd(); // Přečteme si odpověď
            }
            catch (Exception exc) // Pokud došlo k vyjimce, tak jí zobrazíme (pokud není nastaveno nezobrazování)
            {
                if (zobrazitNezdar == true)
                    Vyjimky.VypisVyjimek("Pravděpodobně není funkční připojení k internetu nebo cílový server neodpovídá. Akci prosím opakuj později.", exc);
            }
            finally
            {
                if (dataStream != null) // Pokud datový stream byl vytvořen, tak ho uzavřeme
                    dataStream.Close();
                if (odpoved != null) // Pokud bylo vytvořeno získávání odpovědi, tak ji uzavřeme
                    odpoved.Close();
                if (reader != null) // Pokud bylo otevření čtení odpovědi, tak ji uzavřeme
                    reader.Close();
            }
            return odpovezeno; // Vrátíme odpověď serveru na požadavek
        }

        /// <summary>
        /// Hnadler na změnu v událostech
        /// </summary>
        public delegate void ZmenaVUdalostechHandler();
        /// <summary>
        /// Událost, která značí, že došlo k nějaké změně v událostech
        /// </summary>
        public static event ZmenaVUdalostechHandler ZmenaVUdalostech;
        /// <summary>
        /// Vyvolá událost Změny v událostech
        /// </summary>
        public static void OnZmenaVUdalostech()
        {
            if (ZmenaVUdalostech != null)
                ZmenaVUdalostech();
        }

        /// <summary>
        /// Handler na změnu ve svátcích
        /// </summary>
        public delegate void ZmenaVeSvatcichHandler();
        /// <summary>
        /// Událost, která značí, že došlo ke změně ve svátcích
        /// </summary>
        public static event ZmenaVeSvatcichHandler ZmenaVeSvatcich;
        /// <summary>
        /// Vyvolá událost Změny ve svátcích, která kromě jiného způsobí, že se znovu připravěj narozeniny pro meziroční operace
        /// </summary>
        public static void OnZmenaVeSvatcich()
        {
            Obecne.PripravNarozeninyPrehled();
            if (ZmenaVeSvatcich != null)
                ZmenaVeSvatcich();
        }
    }
}

using System;
using System.Windows.Forms;

namespace Ukolnik
{
    /// <summary>
    /// Má na starosti zobrazování upozorňování na události
    /// </summary>
    public partial class Alarm : Timer
    {
        /// <summary>
        /// Inicializuje potřebné věci
        /// </summary>
        public Alarm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Deaktivuje alarm
        /// </summary>
        public void Vypnout()
        {
            if (this.Enabled == true)
            {
                this.Enabled = false;
                Obecne.ZmenaVUdalostech -= AlarmKontrola;
            }
        }

        /// <summary>
        /// Aktivuje alarm
        /// </summary>
        public void Zapnout()
        {
            if (this.Enabled == false)
            {
                this.Enabled = true;
                Obecne.ZmenaVUdalostech += AlarmKontrola;
            }
        }

        /// <summary>
        /// Zobrazí případné upozornění když budou a pak si zjistí, kdy má být další upozornění
        /// </summary>
        public void AlarmKontrola()
        {
            this.Enabled = false; // Aby nedocházelo k násobným kontrolám
            Obecne.ZmenaVUdalostech -= AlarmKontrola;
            ZobrazAlarmy();
            DalsiAlarm();
            Obecne.ZmenaVUdalostech += AlarmKontrola;
            this.Enabled = true;
        }

        /// <summary>
        /// Zjistí, kdy se má zobrazit další upozornění, pokud je to déle než za pět minut, tak za dalších 5 minut bude provedena kontrola, aby nedocházelo k případným zpožděním
        /// </summary>
        private void DalsiAlarm()
        {
            int dalsi = 300000;
            Databaze db = new Databaze();
            db.Dotaz("SELECT Upozorneni FROM udalosti WHERE (Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitVlastni).ToString() + ") AND (Splneno=0 OR Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ") ORDER BY Upozorneni ASC LIMIT 1;"); // Vytáhne události, na které se má upozornit a nebylo na ně už upozorněno a také nejsou ještě splněny (splněnost v případě narozenin je ignorována, protože je využívána k přechodu na další rok)
            DateTime kdy;
            while (db.DalsiVysledek())
            {
                DateTime nyni = DateTime.Now;
                kdy = db.DejVysledekDatumCas("Upozorneni"); // Kdy má být další upozornění
                if (kdy >= nyni.AddMinutes(5)) // Pokud je další upozornění za víc než 5 minut, tak další kontrola bude za 5 minut, prevence před zpožděním při velkém časovém rozdílu
                    dalsi = 300000;
                else if (kdy <= nyni) // Pokud už upozornění mělo být, tak další kontrola bude za sekundu, aby nedošlo k přehlcení
                    dalsi = 1000;
                else
                    dalsi = ((int)(kdy - nyni).TotalSeconds) * 1000; // Spočítáme si, za jak dlouho s emá upozornit na událost
            }
            if (dalsi <= 0) // Kdyby náhodou byl rozdíl časů menší než sekunda, tak další kontrola je až za sekundu (ochrana před přehlcením)
                dalsi = 1000;
            db.Close();
            this.Interval = dalsi;
        }

        /// <summary>
        /// Provede zobrazení upozornění na události, kde už je čas
        /// </summary>
        private void ZobrazAlarmy()
        {
            lock (this) // Po probuzení ze spánku by mohlo být trošku víc alarmů
            {
                Databaze db = new Databaze();
                db.Dotaz("SELECT ID FROM udalosti WHERE (Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitAutomaticky).ToString() + " OR Upozorneno=" + ((int)Obecne.UpozorneniTypy.upozornitVlastni).ToString() + ") AND Upozorneni<=NOW() AND (Splneno=0 OR Typ=" + ((int)Obecne.UdalostiTypy.narozeniny).ToString() + ") ORDER BY Upozorneni ASC;"); // Vytáhne události, na které se má upozornit a nebylo na ně už upozorněno a také nejsou ještě splněny (splněnost v případě narozenin je ignorována, protože je využívána k přechodu na další rok), tahá je v pořadí od nejstaršího dle doby upozornění
                while (db.DalsiVysledek())
                {
                    Obecne.IkonaStav(false); // Aby se nedal obejít dialog ikonkou
                    Upozorneni alarm = new Upozorneni(db.DejVysledekInt("ID")); // Předá se upozorněnímu ID události
                    if (DialogResult.Yes == alarm.ShowDialog()) // Pokud bylo při upozornění kliknuto na splněno, tak dojde k aktualizaci událostí
                        Obecne.OnZmenaVUdalostech();
                    Obecne.IkonaStav(true);
                }
                db.Close();
            }
        }

        /// <summary>
        /// Když tikne alarm, tak se kontroluje, zda se nemá na nějaké události upozornit
        /// </summary>
        private void alarm_Tick(object sender, EventArgs e)
        {
            AlarmKontrola();
        }
    }
}

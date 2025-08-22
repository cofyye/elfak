using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace ATM_WinForm.Entiteti
{
    public abstract class Racun
    {
        [DisplayName("Broj racuna")]
        public virtual int Br_racuna { get; protected set; }

        [DisplayName("Datum otvaranja")]
        public virtual DateTime Datum_otvaranja { get; set; }

        [DisplayName("Tekuci saldo")]
        public virtual string Tekuci_saldo { get; set; }


        [Browsable(false)]
        public virtual string Tip {  get; set; }

        [DisplayName("Valuta")]
        public virtual string Valuta { get; set; }

        //MAPIRANJE RACUN-BANKA
        [Browsable(false)]
        public virtual Banka JePovezan { get; set; }

        //MAPIRANJE RACUN-KLIJENT
        [Browsable(false)]
        public virtual Klijent Koristi { get; set; }

        //MAPIRANJE RACUN-KARTICA
        [Browsable(false)]
        public virtual IList<Kartica> Kartice { get; set; }

        [Browsable(false)]
        public virtual IList<RacunOvlascenoLice> OvlascenaLica { get; set; }

        public Racun()
        {
            //MAPIRANJE RACUN-KARTICA
            Kartice = new List<Kartica>();
            OvlascenaLica = new List<RacunOvlascenoLice>();
        }

    }

    public class DevizniRacun : Racun 
    {
        [DisplayName("Valuta")]
        public override string Valuta { get; set; }
    }
    public class DinarskiRacun : Racun {
       
    }


}

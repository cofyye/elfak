using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public class FizickoLice : Klijent
    {
      
        [DisplayName("JMBG")]
        public virtual string JMBG { get; set; }

        [DisplayName("Datum rodjenja")]
        public virtual DateTime Datum_rodjenja { get; set; }

        [DisplayName("Ime")]
        public virtual string LIme { get; set; }

        [DisplayName("Ime roditelja")]
        public virtual string Ime_roditelja { get; set; }

        [DisplayName("Prezime")]
        public virtual string Prezime { get; set; }

        [DisplayName("Broj licne karte")]
        public virtual string Br_licne_karte { get; set; }

        [DisplayName("Mesto izdavanja")]
        public virtual string Mesto_izdavanja { get; set; }
    }
}

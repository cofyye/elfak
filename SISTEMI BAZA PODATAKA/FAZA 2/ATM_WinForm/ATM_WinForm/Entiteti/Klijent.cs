using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public abstract class Klijent
    {
        [DisplayName("ID")]
        public virtual int Id { get; set; }

        [DisplayName("Naziv")]
        public virtual string Naziv { get; set; }

        [DisplayName("Broj telefona")]
        public virtual string Br_tel { get; set; }

        [DisplayName("Email")]
        public virtual string Email { get; set; }

        [DisplayName("Adresa")]
        public virtual string Adresa { get; set; }

        //MAPIRANJE KLIJENT-RACUN
        [Browsable(false)]
        public virtual IList<Racun> Racuni { get; set; }

        //MAPIRANJE OVLASCENO KLIJENT-KOMENTAR
        [Browsable(false)]
        public virtual IList<KomentarKlijenta> Komentari { get; set; }

        public Klijent()
        {
            //MAPIRANJE KLIJENT-RACUN
            Racuni = new List<Racun>();
            Komentari = new List<KomentarKlijenta>();
        }
    }
}

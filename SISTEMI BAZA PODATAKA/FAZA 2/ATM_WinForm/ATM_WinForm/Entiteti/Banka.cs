using System.Collections.Generic;
using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public class Banka
    {
        public virtual int Id { get; protected set; }

        public virtual string Ime { get; set; }

        public virtual string Email { get; set; }

        public virtual string Web_adresa { get; set; }

        public virtual string Adresa_centrale { get; set; }

        //MAPIRANJE BANKA-FILIJA
        public virtual IList<Filijala> Filijala { get; set; }


        //MAPIRANJE BANKA-BANKA_BR_TEL
        public virtual IList<BankaBrTelefona> BrojeviTelefona { get; set; }


        //MAIRANJE BANKA-RACUN
        public virtual IList<Racun> Racuni { get; set; }

        public Banka()
        {
            //MAPIRANJE BANKA-FILIJALA
            Filijala = new List<Filijala>();

            //MAPIRANJE BANKA-BANKA_BR_TEL
            BrojeviTelefona = new List<BankaBrTelefona>();

            //MAPIRANJE BANKA-RACUN
            Racuni = new List<Racun>();
        }
    }
}

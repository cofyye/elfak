using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WinForm.Entiteti
{
    public class KomentarKlijenta
    {
        [Browsable(false)]
        public virtual int Id { get; protected set; }

        [DisplayName("Komentar")]
        public virtual string Komentar { get; set; }


        //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
        [Browsable(false)]
        public virtual Klijent PripadaKlijentu { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WinForm.Entiteti
{
    public class RacunOvlascenoLice
    {
        [Browsable(false)]
        public virtual int Id { get; protected set; }

        [DisplayName("Ime ovlascenog lica")]
        public virtual string ImeOvlascenogLica { get; set; }


        //MAPIRANJE OVLASCENO LICE-RACUN
        [Browsable(false)]
        public virtual Racun PripadaRacunu { get; set; }

    }
}

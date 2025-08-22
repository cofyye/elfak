using System.Collections.Generic;
using System.ComponentModel;


namespace ATM_WinForm.Entiteti
{
    public class Koristi_Za_Podizanje_Novca
    {
        [DisplayName("ID")]
        public virtual int Id { get; set; }

        [DisplayName("ID BANKOMATA")]
        public virtual Bankomat Bankomat { get; set; }

        [DisplayName("ID KARTICE")]
        public virtual Kartica Kartica { get; set; }

        [DisplayName("ID TRANSAKCIJE")]
        public virtual Transakcija Transakcija { get; set; }
    }
}

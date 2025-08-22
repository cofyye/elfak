using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public class PravnoLice : Klijent
    {
        [DisplayName("Poreski ID")]
        public virtual string Poreski_id { get; set; }
    }
}

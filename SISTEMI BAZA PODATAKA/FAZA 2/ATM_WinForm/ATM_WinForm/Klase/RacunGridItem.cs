using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WinForm.Klase
{
    internal class RacunGridItem
    {
        public int Br_racuna { get; set; }
        public DateTime Datum_otvaranja { get; set; }
        public string Tekuci_saldo { get; set; }
        public string Tip { get; set; }
        public string Valuta { get; set; } // Ovdje dodajemo valutu
    }
}

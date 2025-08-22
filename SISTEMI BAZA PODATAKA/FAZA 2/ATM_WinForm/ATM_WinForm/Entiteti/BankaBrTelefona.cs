using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public class BankaBrTelefona
    {
        [Browsable(false)]
        public virtual int Id { get; protected set; }

        [DisplayName("Broj telefona")]
        public virtual string BrTelefona { get; set; }


        //MAPIRANJE BANKA_BR_TEL-BANKA
        [Browsable(false)]
        public virtual Banka PripadaBanci { get; set; }
    }
}

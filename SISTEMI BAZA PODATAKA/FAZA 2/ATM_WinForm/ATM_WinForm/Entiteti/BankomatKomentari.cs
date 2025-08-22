using System.ComponentModel;

namespace ATM_WinForm.Entiteti
{
    public class BankomatKomentari
    {
        [Browsable(false)]
        public virtual int Id { get; protected set; }

        [DisplayName("Komentar")]
        public virtual string Komentar { get; set; }


        //MAPIRANJE KOMENTAR-BANKOMAT
        [Browsable(false)]
        public virtual Bankomat PripadaBankomatu { get; set; }
    }
}

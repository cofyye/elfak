using System.ComponentModel;


namespace ATM_WinForm.Entiteti
{
    public class OtklonjenaGreskaServisa
    {
        [Browsable(false)]
        public virtual int Id { get; protected set; }

        [DisplayName("Otklonjena greska")]
        public virtual string Otklonjena_greska { get; set; }


        //MAPIRANJE OTKLONJENA GRESKA-SERVIS
        [Browsable(false)]
        public virtual Servis PripadaServisu { get; set; }
    }
}

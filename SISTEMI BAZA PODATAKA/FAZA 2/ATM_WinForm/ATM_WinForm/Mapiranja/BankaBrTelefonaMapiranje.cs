using System.Linq;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class BankaBrTelefonaMapiranje : ClassMap<ATM_WinForm.Entiteti.BankaBrTelefona>
    {
        public BankaBrTelefonaMapiranje()
        {
            Table("BR_TEL_BANKE");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.BrTelefona, "BROJ_TELEFONA");

            //MAPIRANJE BANKA_BR_TEL-BANKA
            References(x => x.PripadaBanci).Column("ID_BANKE").LazyLoad();
        }
    }
}

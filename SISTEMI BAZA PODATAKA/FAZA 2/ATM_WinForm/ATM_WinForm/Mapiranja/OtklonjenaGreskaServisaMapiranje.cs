using System.Linq;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class OtklonjenaGreskaServisaMapiranje : ClassMap<ATM_WinForm.Entiteti.OtklonjenaGreskaServisa>
    {
        public OtklonjenaGreskaServisaMapiranje()
        {
            Table("OTKLONJENE_GRESKE_SERVISA");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Otklonjena_greska, "OTKLONJENA_GRESKA");

            //MAPIRANJE BANKA_BR_TEL-BANKA
            References(x => x.PripadaServisu).Column("KOD_SERVISA").LazyLoad();
        }
    }
}

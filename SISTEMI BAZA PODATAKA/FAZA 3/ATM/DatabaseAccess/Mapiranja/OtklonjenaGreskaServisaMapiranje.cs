using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja;

internal class OtklonjenaGreskaServisaMapiranje : ClassMap<DatabaseAccess.Entiteti.OtklonjenaGreskaServisa>
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

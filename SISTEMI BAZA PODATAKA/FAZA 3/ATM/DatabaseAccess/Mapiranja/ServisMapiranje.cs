using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja;

internal class ServisMapiranje : ClassMap<DatabaseAccess.Entiteti.Servis>
{
    public ServisMapiranje()
    {
        Table("SERVIS");

        Id(x => x.Kod, "KOD").GeneratedBy.TriggerIdentity();

        Map(x => x.Firma, "FIRMA");

        //MAPIRANJE veze 1:N  --> SERVIS-BANKOMAT
        References(x => x.ServisiraniBankomat).Column("ID_BANKOMATA").LazyLoad();

        //MAPIRANJE veze 1:N --> OTKLONJENA GRESKA-SERVIS
        HasMany(x => x.Otklonjene_Greske).KeyColumn("KOD_SERVISA").LazyLoad().Cascade.All().Inverse();

    }
}

using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja;

internal class TransakcijaMapiranje : ClassMap<DatabaseAccess.Entiteti.Transakcija>
{
    public TransakcijaMapiranje()
    {
        Table("TRANSAKCIJA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Podignuti_iznos, "PODIGNUTI_IZNOS");
        Map(x => x.Vreme_Podizanja_Novca, "VREME");
        Map(x => x.Datum_Podizanja_Novca, "DATUM");

        //TERNARNA
        HasMany(x => x.Koristi_Za_Podizanje_Novca).KeyColumn("ID_TRANSAKCIJE").LazyLoad().Cascade.All().Inverse();
    }
}

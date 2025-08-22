using FluentNHibernate.Mapping;
using DatabaseAccess.Entiteti;


namespace DatabaseAccess.Mapiranja;

internal class PravnoLiceMapiranje : SubclassMap<PravnoLice>
{
    public PravnoLiceMapiranje()
    {
        Table("PRAVNO_LICE");

        KeyColumn("ID");

        Map(x => x.Poreski_id, "PORESKI_ID");

    }
}

using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja;

internal class RacunOvlascenoLiceMapiranje : ClassMap<DatabaseAccess.Entiteti.RacunOvlascenoLice>
{
    public RacunOvlascenoLiceMapiranje()
    {
        Table("OVLASCENO_LICE_RACUNA");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.ImeOvlascenogLica, "IME_OVLASCENOG_LICA");

        //MAPIRANJE OVLASCENO_LICE_RACUNA-RACUN
        References(x => x.PripadaRacunu).Column("BR_RACUNA").LazyLoad();
    }
}

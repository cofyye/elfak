using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class RacunOvlascenoLiceMapiranje : ClassMap<ATM_WinForm.Entiteti.RacunOvlascenoLice>
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
}

using DatabaseAccess.Entiteti;
using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja
{
    internal class KarticaMapiranje : ClassMap<Kartica>
    {
        public KarticaMapiranje()
        {

            //Mapiranje tabele
            Table("KARTICA");

            
            //mapiranje podklasa
            DiscriminateSubClassesOnColumn("TIP");

            //mapiranje primarnog kljuca
            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            Map(x => x.Datum_izdavanje, "DATUM_IZDAVANJA");
            Map(x => x.Datum_isteka, "DATUM_ISTEKA");
            Map(x => x.Dnevni_limit, "DNEVNI_LIMIT");
            

            //MAPIRANJE KARTICA-RACUN
            References(x => x.Odgovara).Column("BR_RACUNA").LazyLoad();

            //TERNARNA
            HasMany(x => x.Koristi_Za_Podizanje_Novca).KeyColumn("ID_KARTICE").LazyLoad().Cascade.All().Inverse();
            
        }
    }

    internal class DebitnaKarticaMapiranje : SubclassMap<DebitnaKartica>
    {
        public DebitnaKarticaMapiranje()
        {
            DiscriminatorValue("debitna");
        }
    }

    internal class KreditnaKarticaMapiranje : SubclassMap<KreditnaKartica>
    {
        public KreditnaKarticaMapiranje()
        {
            DiscriminatorValue("kreditna");
            Map(x => x.Max_iznos_zaduzenja, "MAX_IZNOS_ZADUZENJA");
            Map(x => x.Max_datum_vracanja_duga, "MAX_DATUM_VRACANJA_DUGA");
        }
    }
}

using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja
{
    internal class BankomatMapiranje : ClassMap<Entiteti.Bankomat>
    {
        public BankomatMapiranje()
        {
            Table("BANKOMAT");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Lokacija, "LOKACIJA");
            Map(x => x.Status, "STATUS");
            Map(x => x.Proizvodjac, "PROIZVODJAC");
            Map(x => x.Datum_Poslednjeg_Servisa, "DATUM_POSLEDNJEG_SERVISA");

            //MAPIRANJE veze 1:N  --> BANKOMAT-FILIJALA
            References(x => x.InstaliranUFilijali).Column("RBR_FILIJALE").LazyLoad();

            //MAPIRANJE veze 1:N --> BANKOMAT-KOMENTAR
            HasMany(x => x.Komentari).KeyColumn("ID_BANKOMATA").LazyLoad().Cascade.All().Inverse();

            //MAPIRANJE veze 1:N --> BANKOMAT-SERVIS
            HasMany(x => x.Servisi).KeyColumn("ID_BANKOMATA").LazyLoad().Cascade.All().Inverse();
            
            //TERNARNA
            HasMany(x => x.Koristi_Za_Podizanje_Novca).KeyColumn("ID_BANKOMATA").LazyLoad().Cascade.All().Inverse();
        }
    }
}

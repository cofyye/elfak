using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja;

internal class KlijentMapiranje : ClassMap<Entiteti.Klijent>
{
    public KlijentMapiranje()
    {
        Table("KLIJENT");

        Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

        Map(x => x.Naziv, "NAZIV");
        Map(x => x.Br_tel, "BR_TEL");
        Map(x => x.Adresa, "ADRESA");
        Map(x => x.Email, "EMAIL");

        

        //MAPIRANJE veze 1:N --> KLIJENT-RACUNI
        HasMany(x => x.Racuni).KeyColumn("ID_KLIJENTA").LazyLoad().Cascade.All().Inverse();

        //MAPIRANJE veze 1:N --> KLIJENT-KOMENTAR
        HasMany(x => x.Komentari).KeyColumn("ID_KLIJENTA").LazyLoad().Cascade.All().Inverse();
    }
}

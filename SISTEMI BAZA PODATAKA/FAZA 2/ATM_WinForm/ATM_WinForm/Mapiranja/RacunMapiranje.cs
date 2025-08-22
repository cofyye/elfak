using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ATM_WinForm.Entiteti;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class RacunMapiranje : ClassMap<Racun>
    {
        public RacunMapiranje()
        {

            //Mapiranje tabele
            Table("RACUN");

            //mapiranje podklasa
            DiscriminateSubClassesOnColumn("TIP");

            //mapiranje primarnog kljuca
            Id(x => x.Br_racuna, "BR_RACUNA").GeneratedBy.TriggerIdentity();
            Map(x => x.Datum_otvaranja, "DATUM_OTVARANJA");
            Map(x => x.Tekuci_saldo, "TEKUCI_SALDO");

            //MAPIRANJE RACUN-BANKA
            References(x => x.JePovezan).Column("ID_BANKE").LazyLoad();
            //MAPIRANJE RACUN-KLIJENT
            References(x => x.Koristi).Column("ID_KLIJENTA").LazyLoad();

            //MAPIRANJE veze 1:N --> RACUN-OVLASCENA LICA
            HasMany(x => x.OvlascenaLica).KeyColumn("BR_RACUNA").LazyLoad().Cascade.All().Inverse();

            //MAPIRANJE veze 1:N --> RACUNI-KARTICE
            HasMany(x => x.Kartice).KeyColumn("BR_RACUNA").LazyLoad().Cascade.All().Inverse();

        }
    }

    public class DevizniRacunMapiranje : SubclassMap<DevizniRacun>
    {
        public DevizniRacunMapiranje()
        {
            DiscriminatorValue("devizni");
            Map(x => x.Valuta, "VALUTA");
        }
    }

    public class DinarskiRacunMapiranje : SubclassMap<DinarskiRacun>
    {
        public DinarskiRacunMapiranje()
        {
            DiscriminatorValue("dinarski");
        }
    }
}

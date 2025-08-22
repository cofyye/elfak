using FluentNHibernate.Mapping;
using DatabaseAccess.Entiteti;

namespace DatabaseAccess.Mapiranja
{
    internal class FilijalaMapiranje : ClassMap<Filijala>
    {
        public FilijalaMapiranje()
        {
            //Mapiranje tabele
            Table("FILIJALA");

            //mapiranje primarnog kljuca
            Id(x => x.Rbr_filijale, "RBR_FILIJALE").GeneratedBy.TriggerIdentity();

            //mapiranje svojstava
            Map(x => x.Adresa, "ADRESA");
            Map(x => x.Br_telefona, "BR_TELEFONA");
            Map(x => x.Radno_vreme, "RADNO_VREME");

            //MAPIRANJE 1:N  --> FILIJALA-BANKA
            References(x => x.PripadaBanci).Column("ID_BANKE").LazyLoad();

            //MAPIRANJE veze 1:N --> FILIJALA-BANKOMAT
            HasMany(x => x.Bankomati).KeyColumn("RBR_FILIJALE").LazyLoad().Inverse();//MATA OBRISAO .Cascade.All() pre Inverse jer sa tim nece da radi sad
        }
    }
}

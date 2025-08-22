using System.Linq;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class KomentarKlijentaMapiranje : ClassMap<ATM_WinForm.Entiteti.KomentarKlijenta>
    {
        public KomentarKlijentaMapiranje()
        {
            Table("KOM_KLIJENT");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Komentar, "KOMENTAR");

            //MAPIRANJE KOMENTAR KLIJENTA-KLIJENT
            References(x => x.PripadaKlijentu).Column("ID_KLIJENTA").LazyLoad();
        }
    }
}

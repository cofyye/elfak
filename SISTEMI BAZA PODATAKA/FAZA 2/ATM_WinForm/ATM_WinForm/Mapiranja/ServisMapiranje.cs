using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class ServisMapiranje : ClassMap<ATM_WinForm.Entiteti.Servis>
    {
        public ServisMapiranje()
        {
            Table("SERVIS");

            Id(x => x.Kod, "KOD").GeneratedBy.TriggerIdentity();

            Map(x => x.Firma, "FIRMA");

            //MAPIRANJE veze 1:N  --> SERVIS-BANKOMAT
            References(x => x.ServisiraniBankomat).Column("ID_BANKOMATA").LazyLoad();

            //MAPIRANJE veze 1:N --> OTKLONJENA GRESKA-SERVIS
            HasMany(x => x.Otklonjene_Greske).KeyColumn("KOD_SERVISA").LazyLoad().Cascade.All().Inverse();

        }
    }
}

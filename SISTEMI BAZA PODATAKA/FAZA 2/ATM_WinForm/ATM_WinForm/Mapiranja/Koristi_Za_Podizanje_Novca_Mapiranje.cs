using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ATM_WinForm.Entiteti;

namespace ATM_WinForm.Mapiranja
{
    public class Koristi_Za_Podizanje_Novca_Mapiranje : ClassMap<ATM_WinForm.Entiteti.Koristi_Za_Podizanje_Novca>
    {
        public Koristi_Za_Podizanje_Novca_Mapiranje()
        {
            Table("KORISTI_ZA_PODIZANJE_NOVCA");

            //TERNARNA
            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();
            References(x => x.Bankomat).Column("ID_BANKOMATA");
            References(x => x.Kartica).Column("ID_KARTICE");
            References(x => x.Transakcija).Column("ID_TRANSAKCIJE");
        }
    }
}

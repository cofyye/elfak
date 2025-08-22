using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class TransakcijaMapiranje : ClassMap<ATM_WinForm.Entiteti.Transakcija>
    {
        public TransakcijaMapiranje()
        {
            Table("TRANSAKCIJA");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Podignuti_iznos, "PODIGNUTI_IZNOS");
            Map(x => x.Vreme_Podizanja_Novca, "VREME");
            Map(x => x.Datum_Podizanja_Novca, "DATUM");

            //TERNARNA
            HasMany(x => x.Koristi_Za_Podizanje_Novca).KeyColumn("ID_TRANSAKCIJE").LazyLoad().Cascade.All().Inverse();
        }
    }
}

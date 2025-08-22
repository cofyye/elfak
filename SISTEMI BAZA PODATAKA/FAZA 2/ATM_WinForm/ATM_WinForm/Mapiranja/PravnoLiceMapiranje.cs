using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ATM_WinForm.Entiteti;


namespace ATM_WinForm.Mapiranja
{
    public class PravnoLiceMapiranje : SubclassMap<PravnoLice>
    {
        public PravnoLiceMapiranje()
        {
            Table("PRAVNO_LICE");

            KeyColumn("ID");

            Map(x => x.Poreski_id, "PORESKI_ID");

        }
    }
}

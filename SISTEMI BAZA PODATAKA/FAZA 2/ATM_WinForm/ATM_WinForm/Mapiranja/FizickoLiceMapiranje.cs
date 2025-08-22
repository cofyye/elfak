using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using ATM_WinForm.Entiteti;

namespace ATM_WinForm.Mapiranja
{
    public class FizickoLiceMapiranje : SubclassMap<FizickoLice>
    {
        public FizickoLiceMapiranje()
        {
            Table("FIZICKO_LICE");

            KeyColumn("ID");

            Map(x => x.JMBG, "JMBG");
            Map(x => x.LIme, "LIME");
            Map(x => x.Ime_roditelja, "IME_RODITELJA");
            Map(x => x.Prezime, "PREZIME");
            Map(x => x.Datum_rodjenja, "DATUM_RODJENJA");
            Map(x => x.Br_licne_karte, "BR_LICNE_KARTE");
            Map(x => x.Mesto_izdavanja, "MESTO_IZDAVANJA");

            //mapiranje veze 1:N Prodavnica-Odeljenje
            //HasMany(x => x.Filijala).KeyColumn("ID_BANKE").LazyLoad();
            //HasMany(x => x.Filijala).KeyColumn("ID_BANKE").LazyLoad().Cascade.All();

        }
    }
}

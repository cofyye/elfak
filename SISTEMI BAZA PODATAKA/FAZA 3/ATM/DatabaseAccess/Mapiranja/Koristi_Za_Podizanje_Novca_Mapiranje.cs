using FluentNHibernate.Mapping;

namespace DatabaseAccess.Mapiranja
{
    internal class Koristi_Za_Podizanje_Novca_Mapiranje : ClassMap<Entiteti.Koristi_Za_Podizanje_Novca>
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

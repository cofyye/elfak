using FluentNHibernate.Mapping;

namespace ATM_WinForm.Mapiranja
{
    public class BankomatKomentariMapiranje : ClassMap<ATM_WinForm.Entiteti.BankomatKomentari>
    {
        public BankomatKomentariMapiranje()
        {
            Table("KOM_BANKOMATA");

            Id(x => x.Id, "ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Komentar, "KOMENTAR");

            //MAPIRANJE BANKA_BR_TEL-BANKA
            References(x => x.PripadaBankomatu).Column("ID_BANKOMATA").LazyLoad();
        }
    }
}

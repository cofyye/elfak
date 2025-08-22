using System;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using ATM_WinForm.Mapiranja;

namespace ATM_WinForm
{
    class DataLayer
    {
        private static ISessionFactory _sessionFactory = null;
        private static object _locker = new object();

        public static ISession GetSession()
        {
            if (_sessionFactory == null)
            {
                lock (_locker)
                {
                    if (_sessionFactory == null)
                    {
                        _sessionFactory = CreateSessionFactory();
                    }
                }
            }
            return _sessionFactory.OpenSession();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                var cfg = OracleManagedDataClientConfiguration.Oracle10
                .ConnectionString(c =>
                    c.Is("DATA SOURCE=gislab-oracle.elfak.ni.ac.rs:1521/SBP_PDB;PERSIST SECURITY INFO=True;USER ID=S18724;Password=nekasifra123"));

                return Fluently.Configure()
                    .Database(cfg)
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BankaMapiranje>())
                    //.ExposeConfiguration(BuildSchema)
                    .BuildSessionFactory();
            }
            catch (Exception ec)
            {
                System.Windows.Forms.MessageBox.Show(ec.ToString());
                //System.Windows.Forms.MessageBox.Show(ec.Message);
                return null;
            }
        }
    }
}
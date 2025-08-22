using ATM_WinForm.Entiteti;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using static ATM_WinForm.DTOs;

namespace ATM_WinForm
{
    internal class DTOManager
    {
        #region Banke
        public static List<BankaBasic> VratiSveBanke()
        {
            List<BankaBasic> bankaList = new List<BankaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Banka> banke = s.Query<Banka>();

                foreach (Banka b in banke)
                {
                    bankaList.Add(new BankaBasic(b.Id, b.Ime, b.Email, b.Web_adresa, b.Adresa_centrale));
                }

                s.Close();

            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return bankaList;
        }

        public static BankaBasic VratiBanku(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var b = s.Load<Banka>(id);
                BankaBasic banka = new BankaBasic(b.Id, b.Ime, b.Email, b.Web_adresa, b.Adresa_centrale);

                s.Close();

                return banka;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static bool ProveraEmailBanke(string email)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int count = s.Query<ATM_WinForm.Entiteti.Banka>().Where(banka => banka.Email == email).ToList().Count;

                s.Close();

                return count > 0;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);  

                return false;
            }
        }

        public static bool ProveraWebAdreseBanke(string webadresa)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                int count = s.Query<ATM_WinForm.Entiteti.Banka>().Where(banka => banka.Web_adresa == webadresa).ToList().Count;

                s.Close();

                return count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public static void DodajBanku(BankaBasic banka)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Banka b = new Banka
                {
                    Ime = banka.Ime,
                    Email = banka.Email,
                    Web_adresa = banka.Web_adresa,
                    Adresa_centrale = banka.Adresa_centrale
                };

                s.SaveOrUpdate(b);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniBanku(BankaBasic banka)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Banka b = s.Load<Banka>(banka.Id);

                b.Ime = banka.Ime;
                b.Email = banka.Email;
                b.Web_adresa = banka.Web_adresa;
                b.Adresa_centrale = banka.Adresa_centrale;

                s.Update(b);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiBanku(int bankaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Banka b = s.Load<Banka>(bankaId);

                
                s.Delete(b);

                s.Flush();
                s.Close();
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region BrojeviTelefona Banke
        public static List<BankaBrTelefonaBasic> VratiSveBrojeveTelefonaOdBanke(int bankaId)
        {
            Console.WriteLine(bankaId);
            List<BankaBrTelefonaBasic> brTelefonaList = new List<BankaBrTelefonaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<BankaBrTelefona> telefoni = from t in s.Query<BankaBrTelefona>()
                                                        where t.PripadaBanci.Id == bankaId
                                                        select t;


                foreach (BankaBrTelefona b in telefoni)
                {
                    BankaBasic banka = VratiBanku(b.PripadaBanci.Id);
                    brTelefonaList.Add(new BankaBrTelefonaBasic(b.Id, b.BrTelefona, banka));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return brTelefonaList;
        }

        public static void DodajBrTelefona(BankaBrTelefonaBasic brTelefona)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var banka = s.Load<Banka>(brTelefona.PripadaBanci.Id);

                BankaBrTelefona b = new BankaBrTelefona
                {
                    BrTelefona = brTelefona.BrTelefona,
                    PripadaBanci = banka
                };

                s.SaveOrUpdate(b);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniBrTelefona(BankaBrTelefonaBasic brTelefona)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Banka banka = s.Load<Banka>(brTelefona.PripadaBanci.Id);
                BankaBrTelefona t = s.Load<BankaBrTelefona>(brTelefona.Id);

                t.BrTelefona = brTelefona.BrTelefona;
                t.PripadaBanci = banka;

                s.Update(t);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiBrojTelefonaBanke(int brojTelefonaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                BankaBrTelefona telefon = s.Load<BankaBrTelefona>(brojTelefonaId);

                s.Delete(telefon);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        #endregion

        #region Filijale

        public static List<FilijalaBasic> VratiSveFilijale()
        {
            List<FilijalaBasic> filijalaList = new List<FilijalaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Filijala> filijale = s.Query<Filijala>();

                foreach (Filijala f in filijale)
                {
                    BankaBasic banka = VratiBanku(f.PripadaBanci.Id);
                    filijalaList.Add(new FilijalaBasic(f.Rbr_filijale, f.Adresa, f.Br_telefona, f.Radno_vreme, banka));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return filijalaList;
        }

        public static List<FilijalaBasic> VratiSveFilijaleOdBanke(int bankaId)
        {
            List<FilijalaBasic> filijalaList = new List<FilijalaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Filijala> filijale = from o in s.Query<Filijala>()
                                                 where o.PripadaBanci.Id == bankaId
                                                 select o;

                foreach (Filijala f in filijale)
                {
                    BankaBasic banka = VratiBanku(f.PripadaBanci.Id);
                    filijalaList.Add(new FilijalaBasic(f.Rbr_filijale, f.Adresa, f.Br_telefona, f.Radno_vreme, banka));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return filijalaList;
        }

        public static FilijalaBasic VratiFilijaluBankomata(int bankomatId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var bankomat = s.Load<Bankomat>(bankomatId);

                var filijala = VratiFilijalu(bankomat.InstaliranUFilijali.Rbr_filijale);

                s.Close();

                return filijala;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static FilijalaBasic VratiFilijalu(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var f = s.Load<Filijala>(id);
                var b = VratiBanku(f.PripadaBanci.Id);
                FilijalaBasic filijala = new FilijalaBasic(f.Rbr_filijale, f.Adresa, f.Br_telefona, f.Radno_vreme, b);

                s.Close();

                return filijala;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void DodajFilijalu(FilijalaBasic filijala)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var banka = s.Load<Banka>(filijala.PripadaBanci.Id);

                Filijala f = new Filijala
                {
                    Adresa = filijala.Adresa,
                    Br_telefona = filijala.Br_telefona,
                    Radno_vreme = filijala.Radno_vreme,
                    PripadaBanci = banka
                };

                s.SaveOrUpdate(f);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniFilijalu(FilijalaBasic filijala)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Banka banka = s.Load<Banka>(filijala.PripadaBanci.Id);
                Filijala f = s.Load<Filijala>(filijala.Rbr_filijale);

                f.Adresa = filijala.Adresa;
                f.Br_telefona = filijala.Br_telefona;
                f.Radno_vreme = filijala.Radno_vreme;
                f.PripadaBanci = banka;

                s.Update(f);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiFilijalu(int filijalaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Filijala filijala = s.Load<Filijala>(filijalaId);

                s.Delete(filijala);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Bankomati

        public static List<BankomatBasic> VratiSveBankomate()
        {
            List<BankomatBasic> bankomatList = new List<BankomatBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Bankomat> bankomati = s.Query<Bankomat>();

                foreach (Bankomat b in bankomati)
                {
                    var fil = b.InstaliranUFilijali;
                    FilijalaBasic filijala = VratiFilijalu(fil.Rbr_filijale);
                    bankomatList.Add(new BankomatBasic(b.Id, b.Lokacija, b.Proizvodjac, b.Status, b.Datum_Poslednjeg_Servisa, filijala));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bankomatList;
        }

        public static BankomatBasic VratiBankomat(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var b = s.Load<Bankomat>(id);

                FilijalaBasic filijala = VratiFilijalu(b.InstaliranUFilijali.Rbr_filijale);
                var bankomat = new BankomatBasic(b.Id, b.Lokacija, b.Proizvodjac, b.Status, b.Datum_Poslednjeg_Servisa, filijala);

                s.Close();

                return bankomat;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static List<BankomatBasic> VratiSveBankomateOdFilijale(int filijalaId)
        {
            List<BankomatBasic> bankomatList = new List<BankomatBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                Filijala filijala = s.Load<Filijala>(filijalaId);

                foreach (Bankomat b in filijala.Bankomati)
                {
                    FilijalaBasic f = VratiFilijalu(filijalaId);
                    bankomatList.Add(new BankomatBasic(b.Id, b.Lokacija, b.Proizvodjac, b.Status, b.Datum_Poslednjeg_Servisa, f));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bankomatList;
        }

        public static void DodajBankomat(BankomatBasic bankomat)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var filijala = s.Load<Filijala>(bankomat.InstaliranUFilijali.Rbr_filijale);

                Bankomat b = new Bankomat
                {
                    Lokacija = bankomat.Lokacija,
                    Proizvodjac = bankomat.Proizvodjac,
                    Status = bankomat.Status,
                    Datum_Poslednjeg_Servisa = bankomat.Datum_Poslednjeg_Servisa,
                    InstaliranUFilijali = filijala
                };

                s.SaveOrUpdate(b);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniBankomat(BankomatBasic bankomat)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                // Učitavanje postojeće filijale
                var filijala = s.Load<Filijala>(bankomat.InstaliranUFilijali.Rbr_filijale);

                // Učitavanje postojećeg Bankomat objekta iz baze
                var existingBankomat = s.Load<Bankomat>(bankomat.Id);

                if (existingBankomat != null)
                {
                    // Ažuriranje postojećeg Bankomat objekta
                    existingBankomat.Lokacija = bankomat.Lokacija;
                    existingBankomat.Proizvodjac = bankomat.Proizvodjac;
                    existingBankomat.Status = bankomat.Status;
                    existingBankomat.Datum_Poslednjeg_Servisa = bankomat.Datum_Poslednjeg_Servisa;
                    existingBankomat.InstaliranUFilijali = filijala;

                    // Sačuvajte promene
                    s.Update(existingBankomat);
                }

                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public static void IzbrisiBankomat(int bankomatId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Bankomat bankomat = s.Load<Bankomat>(bankomatId);

                s.Delete(bankomat);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        #region Ovlascena Lica Racuna
        public static List<RacunOvlascenoLiceBasic> VratiSvaOvlascenaLicaOdRacuna(int racunId)
        {
            List<RacunOvlascenoLiceBasic> ovlascenaLicaList = new List<RacunOvlascenoLiceBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<RacunOvlascenoLice> lica = from t in s.Query<RacunOvlascenoLice>()
                                                        where t.PripadaRacunu.Br_racuna == racunId
                                                        select t;

                foreach (RacunOvlascenoLice rol in lica)
                {
                    RacunBasic racun = VratiRacun(rol.PripadaRacunu.Br_racuna);
                    ovlascenaLicaList.Add(new RacunOvlascenoLiceBasic(rol.Id, rol.ImeOvlascenogLica, racun));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return ovlascenaLicaList;
        }

        public static void DodajOvlascenoLice(RacunOvlascenoLiceBasic ovlascenoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var racun = s.Load<Racun>(ovlascenoLice.PripadaRacunu.Br_racuna);

                RacunOvlascenoLice rol = new RacunOvlascenoLice
                {
                    ImeOvlascenogLica = ovlascenoLice.ImeOvlascenogLica,
                    PripadaRacunu = racun
                };

                s.SaveOrUpdate(rol);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniOvlascenoLice(RacunOvlascenoLiceBasic ovlascenoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Racun racun = s.Load<Racun>(ovlascenoLice.PripadaRacunu.Br_racuna);
                RacunOvlascenoLice rol = s.Load<RacunOvlascenoLice>(ovlascenoLice.Id);

                rol.ImeOvlascenogLica = ovlascenoLice.ImeOvlascenogLica;
                rol.PripadaRacunu = racun;

                s.Update(rol);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiOvlascenoLiceRacuna(int rolId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                RacunOvlascenoLice rol = s.Load<RacunOvlascenoLice>(rolId);

                s.Delete(rol);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        #endregion

        #region Racuni 
        public static List<RacunBasic> VratiSveRacune()
        {
            List<RacunBasic> racunList = new List<RacunBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Racun> racuni = s.Query<Racun>();

                foreach (Racun r in racuni)
                {
                    BankaBasic banka = VratiBanku(r.JePovezan.Id);
                    KlijentBasic klijent = VratiKlijenta(r.Koristi.Id);
                    racunList.Add(new RacunBasic(r.Br_racuna, r.Datum_otvaranja, r.Tekuci_saldo, r.Tip, r.Valuta, banka, klijent));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return racunList;
        }

        public static List<RacunBasic> VratiSveRacuneOdBanke(int bankaId)
        {
            List<RacunBasic> racunList = new List<RacunBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Racun> racuni = from r in s.Query<Racun>()
                                            where r.JePovezan.Id == bankaId
                                            select r;

                foreach (Racun r in racuni)
                {
                    BankaBasic banka = VratiBanku(r.JePovezan.Id);
                    KlijentBasic klijent = VratiKlijenta(r.Koristi.Id);
                    racunList.Add(new RacunBasic(r.Br_racuna, r.Datum_otvaranja, r.Tekuci_saldo, r.Tip, r.Valuta, banka, klijent));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return racunList;
        }

        public static List<RacunBasic> VratiSveRacuneOdKlijenta(int klijentId)
        {
            List<RacunBasic> racunList = new List<RacunBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Racun> racuni = from r in s.Query<Racun>()
                                            where r.Koristi.Id == klijentId
                                            select r;

                foreach (Racun r in racuni)
                {
                    BankaBasic banka = VratiBanku(r.JePovezan.Id);
                    KlijentBasic klijent = VratiKlijenta(r.Koristi.Id);
                    racunList.Add(new RacunBasic(r.Br_racuna, r.Datum_otvaranja, r.Tekuci_saldo, r.Tip, r.Valuta, banka, klijent));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return racunList;
        }

        public static RacunBasic VratiRacun(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var r = s.Load<Racun>(id);
                BankaBasic banka = VratiBanku(r.JePovezan.Id);
                KlijentBasic klijent = VratiKlijenta(r.Koristi.Id);
                RacunBasic racun = new RacunBasic(r.Br_racuna, r.Datum_otvaranja, r.Tekuci_saldo, r.Tip, r.Valuta, banka, klijent);

                s.Close();

                return racun;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void DodajRacun(RacunBasic racun)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var klijent  = s.Load<Klijent>(racun.Koristi.Id);
                var banka = s.Load<Banka>(racun.JePovezan.Id);

                if (racun.Tip == "devizni")
                {
                    DevizniRacun r =  new DevizniRacun
                    {
                        Tip = racun.Tip,
                        Valuta = racun.Valuta,
                        Tekuci_saldo = racun.Tekuci_saldo,
                        Datum_otvaranja = racun.Datum_otvaranja,
                        Koristi = klijent,
                        JePovezan = banka

                    };

                    s.SaveOrUpdate(r);
                }
                else if (racun.Tip == "dinarski")
                {
                    DinarskiRacun r = new DinarskiRacun
                    {
                        Tip = racun.Tip,
                        Tekuci_saldo = racun.Tekuci_saldo,
                        Datum_otvaranja = racun.Datum_otvaranja,
                        Koristi = klijent,
                        JePovezan = banka
                    };

                    s.SaveOrUpdate(r);
                }

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniRacun(RacunBasic racun)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                // Učitavanje postojećeg Računa
                var klijent = s.Load<Klijent>(racun.Koristi.Id);
                var banka = s.Load<Banka>(racun.JePovezan.Id);

                // Učitavanje postojeće Kartice objekta iz baze
                var existingRacun = s.Load<Racun>(racun.Br_racuna);

                if (existingRacun != null)
                {
                    // Ažuriranje postojeće Kartice objekta
                    existingRacun.Datum_otvaranja = racun.Datum_otvaranja;
                    existingRacun.Tekuci_saldo = racun.Tekuci_saldo;
                    existingRacun.Tip = racun.Tip;
                    existingRacun.JePovezan = banka;

                    if (existingRacun.Tip == "debitna")
                    {
                        existingRacun.Valuta = racun.Valuta;
 
                    }

                    existingRacun.Koristi = klijent;
                    // Sačuvajte promene
                    s.Update(existingRacun);
                }

                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiRacun(int racunId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Racun racun = s.Load<Racun>(racunId);

                s.Delete(racun);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Klijent
        public static KlijentBasic VratiKlijenta(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var k = s.Load<Klijent>(id);
                KlijentBasic klijent = new KlijentBasic(k.Id, k.Br_tel, k.Email, k.Adresa);

                s.Close();

                return klijent;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static List<KlijentBasic> VratiSveKlijente(int bankaId = -1)
        {
            List<KlijentBasic> klijentList = new List<KlijentBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IQueryable<KlijentBasic> klijenti;
                if (bankaId == -1)
                {
                    klijenti = from k in s.Query<Klijent>()
                               select new KlijentBasic
                               (
                                   k.Id,
                                   k.Br_tel,
                                   k.Email,
                                   k.Adresa,
                                   k.Naziv
                               );
                }
                else
                {
                    klijenti = from k in s.Query<Klijent>()
                               from r in k.Racuni
                               where r.JePovezan.Id == bankaId
                               select new KlijentBasic
                               (
                                   k.Id,
                                   k.Br_tel,
                                   k.Email,
                                   k.Adresa,
                                   k.Naziv
                               );
                }

                klijentList = klijenti.ToList();


                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return klijentList;
        }

        public static List<FizickoLiceBasic> VratiSvaFizickaLica(int bankaId = -1)
        {
            List<FizickoLiceBasic> fizickoList = new List<FizickoLiceBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IQueryable<FizickoLiceBasic> fizicko;
                if (bankaId == -1)
                {
                    fizicko = from k in s.Query<FizickoLice>()
                              select new FizickoLiceBasic
                              (
                                   k.JMBG,
                                   k.Datum_rodjenja,
                                   k.LIme,
                                   k.Ime_roditelja,
                                   k.Prezime,
                                   k.Br_licne_karte,
                                   k.Mesto_izdavanja,
                                   k.Id,
                                   k.Naziv,
                                   k.Br_tel,
                                   k.Email,
                                   k.Adresa
                              );
                }
                else
                {
                    fizicko = from k in s.Query<FizickoLice>()
                              from r in k.Racuni
                              where r.JePovezan.Id == bankaId
                              select new FizickoLiceBasic
                              (
                                   k.JMBG,
                                   k.Datum_rodjenja,
                                   k.LIme,
                                   k.Ime_roditelja,
                                   k.Prezime,
                                   k.Br_licne_karte,
                                   k.Mesto_izdavanja,
                                   k.Id,
                                   k.Naziv,
                                   k.Br_tel,
                                   k.Email,
                                   k.Adresa
                              );
                }

                fizickoList = fizicko.ToList();


                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return fizickoList;
        }

        public static List<PravnoLiceBasic> VratiSvaPravnaLica(int bankaId = -1)
        {
            List<PravnoLiceBasic> pravnoList = new List<PravnoLiceBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IQueryable<PravnoLiceBasic> pravno;
                if (bankaId == -1)
                {
                    pravno = from p in s.Query<PravnoLice>()
                             select new PravnoLiceBasic
                             (
                                   p.Poreski_id,
                                   p.Id,
                                   p.Naziv,
                                   p.Br_tel,
                                   p.Email,
                                   p.Adresa
                             );
                }
                else
                {
                    pravno = from p in s.Query<PravnoLice>()
                             from r in p.Racuni
                             where r.JePovezan.Id == bankaId
                             select new PravnoLiceBasic
                             (
                                   p.Poreski_id,
                                   p.Id,
                                   p.Naziv,
                                   p.Br_tel,
                                   p.Email,
                                   p.Adresa
                             );
                }

                pravnoList = pravno.ToList();


                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

            return pravnoList;

        }



        public static void DodajFizickoLice(FizickoLiceBasic fizickoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                FizickoLice fl = new FizickoLice
                {
                    Ime_roditelja = fizickoLice.Ime_roditelja,
                    Br_licne_karte = fizickoLice.Br_licne_karte,
                    Datum_rodjenja = fizickoLice.Datum_rodjenja,
                    Mesto_izdavanja = fizickoLice.Mesto_izdavanja,
                    JMBG = fizickoLice.JMBG,
                    LIme = fizickoLice.LIme,
                    Prezime = fizickoLice.Prezime,
                    Adresa = fizickoLice.Adresa,
                    Br_tel = fizickoLice.Br_tel,
                    Email = fizickoLice.Email,
                    Naziv = fizickoLice.Naziv,
                };

                s.Save(fl);

                
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void DodajPravnoLice(PravnoLiceBasic pravnoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                PravnoLice pr = new PravnoLice
                {
                    Poreski_id = pravnoLice.Poreski_id,
                    Adresa = pravnoLice.Adresa,
                    Br_tel = pravnoLice.Br_tel,
                    Email = pravnoLice.Email,
                    Naziv = pravnoLice.Naziv,
                };

                s.Save(pr);


                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
        public static void IzmeniPravnoLice(PravnoLiceBasic pravnoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();


                // Učitavanje postojeće Kartice objekta iz baze
                var existingPravnoLice = s.Load<PravnoLice>(pravnoLice.Id);

                if (existingPravnoLice != null)
                {
                    // Ažuriranje postojeće Kartice objekta
                    existingPravnoLice.Poreski_id = pravnoLice.Poreski_id;
                    existingPravnoLice.Adresa = pravnoLice.Adresa;
                    existingPravnoLice.Br_tel = pravnoLice.Br_tel;
                    existingPravnoLice.Email = pravnoLice.Email;
                    existingPravnoLice.Naziv = pravnoLice.Naziv;

                    // Sačuvajte promene
                    s.Update(existingPravnoLice);
                }

                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniFizickoLice(FizickoLiceBasic fizickoLice)
        {
            try
            {
                ISession s = DataLayer.GetSession();


                // Učitavanje postojeće Kartice objekta iz baze
                var existingFizickoLice = s.Load<FizickoLice>(fizickoLice.Id);

                if (existingFizickoLice != null)
                {
                    // Ažuriranje postojeće Kartice objekta
                    existingFizickoLice.JMBG = fizickoLice.JMBG;
                    existingFizickoLice.Datum_rodjenja = fizickoLice.Datum_rodjenja;
                    existingFizickoLice.LIme = fizickoLice.LIme;
                    existingFizickoLice.Ime_roditelja = fizickoLice.Ime_roditelja;
                    existingFizickoLice.Prezime = fizickoLice.Prezime;
                    existingFizickoLice.Br_licne_karte = fizickoLice.Br_licne_karte;
                    existingFizickoLice.Mesto_izdavanja = fizickoLice.Mesto_izdavanja;
                    existingFizickoLice.Br_tel = fizickoLice.Br_tel;
                    existingFizickoLice.Email = fizickoLice.Email;
                    existingFizickoLice.Adresa = fizickoLice.Adresa;

                    // Sačuvajte promene
                    s.Update(existingFizickoLice);
                }

                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiKlijenta(int klijentId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Klijent klijent = s.Load<Klijent>(klijentId);

                s.Delete(klijent);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static bool ProveraPonavljanjaLicneKarteFizickihLica(string licnaKarta)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<FizickoLice> fizickaLica = s.Query<FizickoLice>();

                foreach (FizickoLice k in fizickaLica)
                {
                    if (k.Br_licne_karte == licnaKarta)
                    {
                        return false;
                    }
                }

                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }

        #endregion

        #region KomentarKlijenta
        public static List<KomentarKlijentaBasic> VratiSveKomentareKlijenta(int klijentId)
        {
            List<KomentarKlijentaBasic> komentariKlijentaList = new List<KomentarKlijentaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<KomentarKlijenta> komentari = from k in s.Query<KomentarKlijenta>()
                                                       where k.PripadaKlijentu.Id == klijentId
                                                          select k;

                foreach (KomentarKlijenta kom in komentari)
                {
                    KlijentBasic klijent = VratiKlijenta(kom.PripadaKlijentu.Id);
                    komentariKlijentaList.Add(new KomentarKlijentaBasic(kom.Id, kom.Komentar, klijent));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return komentariKlijentaList;
        }

        public static void DodajKomentarKlijenta(KomentarKlijentaBasic komentar)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var klijent = s.Load<Klijent>(komentar.PripadaKlijentu.Id);

                KomentarKlijenta kom = new KomentarKlijenta
                {
                    Komentar = komentar.Komentar,
                    PripadaKlijentu = klijent
                };

                s.SaveOrUpdate(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniKomentarKlijenta(KomentarKlijentaBasic komentar)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Klijent klijent = s.Load<Klijent>(komentar.PripadaKlijentu.Id);
                KomentarKlijenta kom = s.Load<KomentarKlijenta>(komentar.Id);

                kom.Komentar = komentar.Komentar;
                kom.PripadaKlijentu = klijent;

                s.Update(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiKomentarKlijenta(int komKlijentaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                KomentarKlijenta kom = s.Load<KomentarKlijenta>(komKlijentaId);

                s.Delete(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region KomentarBankomata
        public static List<BankomatKomentariBasic> VratiSveKomentareBankomata(int bankomatId)
        {
            List<BankomatKomentariBasic> komentariBankomataList = new List<BankomatKomentariBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<BankomatKomentari> komentari = from k in s.Query<BankomatKomentari>()
                                                          where k.PripadaBankomatu.Id == bankomatId
                                                          select k;

                foreach (BankomatKomentari kom in komentari)
                {
                    BankomatBasic bankomat = VratiBankomat(kom.PripadaBankomatu.Id);
                    komentariBankomataList.Add(new BankomatKomentariBasic(kom.Id, kom.Komentar, bankomat));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return komentariBankomataList;
        }

        public static void DodajKomentarBankomata(BankomatKomentariBasic komentar)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var bankomat = s.Load<Bankomat>(komentar.PripadaBankomatu.Id);

                BankomatKomentari kom = new BankomatKomentari
                {
                    Komentar = komentar.Komentar,
                    PripadaBankomatu = bankomat
                };

                s.SaveOrUpdate(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniKomentarBankomat(BankomatKomentariBasic komentar)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Bankomat bankomat = s.Load<Bankomat>(komentar.PripadaBankomatu.Id);
                BankomatKomentari kom = s.Load<BankomatKomentari>(komentar.Id);

                kom.Komentar = komentar.Komentar;
                kom.PripadaBankomatu = bankomat;

                s.Update(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiKomentarBankomata(int komBankomatId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                BankomatKomentari kom = s.Load<BankomatKomentari>(komBankomatId);

                s.Delete(kom);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Kartica 

        public static KarticaBasic VratiKarticu(int id)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var k = s.Load<Kartica>(id);
                RacunBasic racun = VratiRacun(k.Odgovara.Br_racuna);
                KarticaBasic kartica = new KarticaBasic(k.Id, k.Datum_izdavanje, k.Datum_isteka, k.Dnevni_limit, k.Tip, k.Max_iznos_zaduzenja, k.Max_datum_vracanja_duga, racun);
                string aa = kartica.Max_datum_vracanja_duga.ToString();
                if(aa != null)
                {
                    kartica.Tip = "kreditna";
                }
                s.Close();
                return kartica;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static List<KarticaBasic> VratiSveKarticeOdRacuna(int racunId)
        {
            List<KarticaBasic> karticaList = new List<KarticaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Kartica> kartice = from k in s.Query<Kartica>()
                                            where k.Odgovara.Br_racuna == racunId
                                            select k;

                Console.WriteLine(karticaList.ToString());

                foreach (Kartica k in kartice)
                {

                    RacunBasic racun = VratiRacun(k.Odgovara.Br_racuna);
                    karticaList.Add(new KarticaBasic(k.Id, k.Datum_izdavanje, k.Datum_isteka, k.Dnevni_limit, k.Tip, k.Max_iznos_zaduzenja, k.Max_datum_vracanja_duga, racun));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return karticaList;
        }

        public static void DodajKarticu(KarticaBasic kartica)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var racun = s.Load<Racun>(kartica.Odgovara.Br_racuna);

                if(kartica.Tip == "kreditna")
                {
                    KreditnaKartica k = new KreditnaKartica
                    {
                        Datum_izdavanje = kartica.Datum_izdavanje,
                        Datum_isteka = kartica.Datum_isteka,
                        Tip = kartica.Tip,
                        Dnevni_limit = kartica.Dnevni_limit,
                        Max_iznos_zaduzenja = kartica.Max_iznos_zaduzenja,
                        Max_datum_vracanja_duga = kartica.Max_datum_vracanja_duga,
                        Odgovara = racun

                    };

                    s.SaveOrUpdate(k);
                }
                else if(kartica.Tip == "debitna"){
                    DebitnaKartica k = new DebitnaKartica
                    {
                        Datum_izdavanje = kartica.Datum_izdavanje,
                        Datum_isteka = kartica.Datum_isteka,
                        Tip = kartica.Tip,
                        Dnevni_limit = kartica.Dnevni_limit,
                        Odgovara = racun
                    };

                    s.SaveOrUpdate(k);
                }

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniKarticu(KarticaBasic kartica)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                // Učitavanje postojećeg Računa
                var racun = s.Load<Racun>(kartica.Odgovara.Br_racuna);

                // Učitavanje postojeće Kartice objekta iz baze
                var existingKartica= s.Load<Kartica>(kartica.Id);

                if (existingKartica != null)
                {
                    // Ažuriranje postojeće Kartice objekta
                    existingKartica.Datum_izdavanje = kartica.Datum_izdavanje;
                    existingKartica.Datum_isteka = kartica.Datum_isteka;
                    existingKartica.Tip = kartica.Tip;
                    existingKartica.Dnevni_limit = kartica.Dnevni_limit;

                    if (existingKartica.Tip == "kreditna")
                    {
                        existingKartica.Max_iznos_zaduzenja = kartica.Max_iznos_zaduzenja;
                        existingKartica.Max_datum_vracanja_duga = kartica.Max_datum_vracanja_duga;
                    }
                    existingKartica.Odgovara = racun;
                    // Sačuvajte promene
                    s.Update(existingKartica);
                }

                s.Flush();
                s.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiKarticu(int karticaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Kartica kartica = s.Load<Kartica>(karticaId);

                s.Delete(kartica);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region ServisBankomata
        public static List<ServisBasic> VratiSveServiseBankomata(int bankomatId)
        {
            List<ServisBasic> servisiBankomataList = new List<ServisBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<Servis> servisi = from sv in s.Query<Servis>()
                                              where sv.ServisiraniBankomat.Id == bankomatId
                                              select sv;

                Console.WriteLine(servisi.Count());

                foreach (Servis se in servisi)
                {
                    BankomatBasic bankomat = VratiBankomat(se.ServisiraniBankomat.Id);
                    servisiBankomataList.Add(new ServisBasic(se.Kod, se.Firma, bankomat));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return servisiBankomataList;
        }

        public static void DodajServisBankomata(ServisBasic servis)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var bankomat = s.Load<Bankomat>(servis.ServisiraniBankomat.Id);

                Servis sv = new Servis
                {
                    Firma = servis.Firma,
                    ServisiraniBankomat = bankomat,
                };

                //Console.WriteLine(sv.Firma);
                //Console.WriteLine(sv.ServisiraniBankomat.Id);

                s.SaveOrUpdate(sv);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniServisBankomat(ServisBasic servis)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Bankomat bankomat = s.Load<Bankomat>(servis.ServisiraniBankomat.Id);
                Servis sv = s.Load<Servis>(servis.Kod);

                sv.Firma = servis.Firma;
                sv.ServisiraniBankomat = bankomat;

                s.Update(sv);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiServisBankomata(int servisId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Servis sv = s.Load<Servis>(servisId);

                s.Delete(sv);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static ServisBasic VratiServis(int kod)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var b = s.Load<Servis>(kod);

                BankomatBasic bankomat = VratiBankomat(b.ServisiraniBankomat.Id);
                var servis = new ServisBasic(b.Kod, b.Firma, bankomat);

                s.Close();

                return servis;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region Otklonjena Greska Servisa
        public static List<OtklonjenaGreskaServisaBasic> VratiSveOtklonjeneGreskeServisa(int kodServisa)
        {
            List<OtklonjenaGreskaServisaBasic> otklonjeneGreskeList = new List<OtklonjenaGreskaServisaBasic>();
            try
            {
                ISession s = DataLayer.GetSession();

                IEnumerable<OtklonjenaGreskaServisa> greske = from g in s.Query<OtklonjenaGreskaServisa>()
                                                          where g.PripadaServisu.Kod == kodServisa
                                                              select g;

                foreach (OtklonjenaGreskaServisa gr in greske)
                {
                    ServisBasic servis = VratiServis(gr.PripadaServisu.Kod);
                    otklonjeneGreskeList.Add(new OtklonjenaGreskaServisaBasic(gr.Id, gr.Otklonjena_greska, servis));
                }

                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return otklonjeneGreskeList;
        }

        public static void DodajOtklonjenuGreskuServisa(OtklonjenaGreskaServisaBasic greska)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                var servis = s.Load<Servis>(greska.PripadaServisu.Kod);

                OtklonjenaGreskaServisa gr = new OtklonjenaGreskaServisa
                {
                    Otklonjena_greska = greska.Otklonjena_greska,
                    PripadaServisu = servis
                };

                s.SaveOrUpdate(gr);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzmeniOtklonjenuGreskuServisa(OtklonjenaGreskaServisaBasic greska)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Servis servis = s.Load<Servis>(greska.PripadaServisu.Kod);
                OtklonjenaGreskaServisa gres = s.Load<OtklonjenaGreskaServisa>(greska.Id);

                gres.Otklonjena_greska = greska.Otklonjena_greska;
                gres.PripadaServisu = servis;

                s.Update(gres);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void IzbrisiOtklonjenuGreskuServisa(int greskaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                OtklonjenaGreskaServisa gr = s.Load<OtklonjenaGreskaServisa>(greskaId);

                s.Delete(gr);

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        #region Transakcija

        public static List<PregledBasic> VratiSveTransakcijeOdBankomata(int bankomatId)
        {
            List<PregledBasic> pregledList = new List<PregledBasic>();
            try
            {
                ISession s = DataLayer.GetSession();
                var koristi = from k in s.Query<Koristi_Za_Podizanje_Novca>()
                              where k.Bankomat.Id == bankomatId
                              select new PregledBasic(
                                  k.Transakcija.Id,
                                  k.Bankomat.Id,
                                  k.Kartica.Id,
                                  k.Transakcija.Podignuti_iznos,
                                  k.Transakcija.Datum_Podizanja_Novca,
                                  k.Transakcija.Vreme_Podizanja_Novca
                              );

                pregledList = koristi.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return pregledList;
        }

        public static void DodajTransakciju(TransakcijaBasic transakcija, int bankomatId, int karticaId)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Transakcija t = new Transakcija
                {
                    Podignuti_iznos = transakcija.Podignuti_iznos,
                    Datum_Podizanja_Novca = transakcija.Datum_Podizanja_Novca,
                    Vreme_Podizanja_Novca = transakcija.Vreme_Podizanja_Novca,
                };

                s.Save(t);

                Bankomat b = s.Get<Bankomat>(bankomatId);
                Kartica k = s.Get<Kartica>(karticaId);

                if(Int32.Parse(k.Odgovara.Tekuci_saldo) < Int32.Parse(t.Podignuti_iznos))
                {
                    MessageBox.Show("Tekuci saldo je manji od novca koji zelite da podignete.");
                    s.Delete(t);
                    return;
                }

                if (Int32.Parse(k.Dnevni_limit) < Int32.Parse(t.Podignuti_iznos))
                {
                    MessageBox.Show("Dnevni limit je manji od novca koji zelite da podignete.");
                    s.Delete(t);
                    return;
                }

                int oduzimanje = (Int32.Parse(k.Odgovara.Tekuci_saldo)) - (Int32.Parse(t.Podignuti_iznos));

                var racun = k.Odgovara;

                racun.Tekuci_saldo = oduzimanje.ToString();

                s.Update(racun);

                Koristi_Za_Podizanje_Novca kzpn = new Koristi_Za_Podizanje_Novca
                {
                    Transakcija = t,
                    Bankomat = b,
                    Kartica = k
                };

                s.Save(kzpn);

                MessageBox.Show("Uspesno ste dodali transakciju!");

                s.Flush();
                s.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        #endregion
    }
}

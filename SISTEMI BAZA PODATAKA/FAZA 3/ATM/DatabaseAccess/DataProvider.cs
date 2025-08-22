using DatabaseAccess.DTOs;
using DatabaseAccess.Entiteti;
using DatabaseAccess.Mapiranja;
using NHibernate;
using NHibernate.Stat;
using System.Security.Cryptography.Pkcs;

namespace DatabaseAccess;

public static class DataProvider
{
    #region Banka
    public static List<BankaView> VratiSveBanke()
    {
        ISession? s = null;

        List<BankaView> banke = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Banka> sveBanke = from o in s.Query<Banka>()
                                          select o;

            foreach (Banka b in sveBanke)
            {
                banke.Add(new BankaView(b));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return banke;
    }

    public static BankaView VratiBanku(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var b = s.Load<Banka>(id);
            BankaView banka = new BankaView(b);

            s.Close();

            return banka;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }
    public static void DodajBanku(BankaView banka)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Banka b = new()
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

    public static int IzmeniBanku(BankaView banka)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Banka b = s.Load<Banka>(banka.Id);

            if (b != null)
            {
                b.Ime = banka.Ime;
                b.Email = banka.Email;
                b.Web_adresa = banka.Web_adresa;
                b.Adresa_centrale = banka.Adresa_centrale;

                s.Update(b);

                s.Flush();
                s.Close();

                return 1;
            }
            else
            {
                Console.WriteLine("Banka sa ovim Id-jem ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public static int IzbrisiBanku(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Banka b = s.Load<Banka>(id);

            if (b != null)
            {
                s.Delete(b);

                s.Flush();
                s.Close();
                return b.Id;
            }
            else
            {
                Console.WriteLine("Banka sa ovim id-jem ne postoji!\n");
                return b!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public static List<BankaBrTelefonaView> VratiBrojeveTelefona(int idBanke)
    {
        List<BankaBrTelefonaView> brojeviTelefona = new List<BankaBrTelefonaView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<BankaBrTelefona>  telefoni = from t in s.Query<BankaBrTelefona>()
                                             where t.PripadaBanci!.Id == idBanke
                                             select t;

            foreach (BankaBrTelefona t in telefoni)
            {
                BankaView banka = VratiBanku(t.PripadaBanci!.Id);
                brojeviTelefona.Add(new BankaBrTelefonaView(t.Id, t.BrTelefona!, banka));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return brojeviTelefona;
    }

    public static int DodajBrTelefonaBanke(BankaBrTelefonaView telefon, int idBanka)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Banka b = s.Load<Banka>(idBanka);

            BankaBrTelefona t = new BankaBrTelefona
            {
                BrTelefona = telefon.BrTelefona,
                PripadaBanci = b
            };

             s.Save(t);
             s.Flush();

            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniBrojTelefonaBanke(BankaBrTelefonaView telefon)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            BankaBrTelefona brtel = s.Load<BankaBrTelefona>(telefon.Id);


            if (brtel != null)
            {
                brtel.BrTelefona = telefon.BrTelefona;

                s.Update(brtel);

                s.Flush();
                s.Close();

                return brtel.Id;
            }
            else
            {
                Console.WriteLine($"Broj telefona banke sa Id-jem {telefon.Id} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiBrojTelefonaBanke(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            BankaBrTelefona brtel = s.Load<BankaBrTelefona>(id);

            if (brtel != null)
            {
                s.Delete(brtel);

                s.Flush();
                s.Close();
                return brtel.Id;
            }
            else
            {
                Console.WriteLine($"Broj telefona banke sa id-jem {id} ne postoji!\n");
                return brtel!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    #endregion

    #region Filijala
    public static List<FilijalaView> VratiSveFilijale()
    {

        ISession? s = null;

        List<FilijalaView> filijale = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Filijala> sveFilijale = from o in s.Query<Filijala>()
                                                select o;

            foreach (Filijala f in sveFilijale)
            {
                filijale.Add(new FilijalaView(f));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return filijale;
    }

    public static List<FilijalaView> VratiSveFilijaleOdBanke(int bankaId)
    {
        List<FilijalaView> filijalaList = new List<FilijalaView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Filijala> filijale = from o in s.Query<Filijala>()
                                             where o.PripadaBanci!.Id == bankaId
                                             select o;

            foreach (Filijala f in filijale)
            {
                BankaView banka = VratiBanku(f.PripadaBanci!.Id);
                filijalaList.Add(new FilijalaView(f.Rbr_filijale, f.Adresa, f.Br_telefona!, f.Radno_vreme!, banka));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return filijalaList;
    }

    public static FilijalaView VratiFilijalu(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var f = s.Load<Filijala>(id);
            var b = VratiBanku(f.PripadaBanci!.Id);
            FilijalaView filijala = new FilijalaView(f.Rbr_filijale, f.Adresa, f.Br_telefona!, f.Radno_vreme!, b);

            s.Close();

            return filijala;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }


    public static int DodajFilijalu(FilijalaView filijala, int idBanka)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Banka b =  s.Load<Banka>(idBanka);

            if(b == null)
            {
                return 0;
            }

            Filijala f = new Filijala
            {
                Adresa = filijala.Adresa!,
                Radno_vreme = filijala.Radno_vreme,
                Br_telefona = filijala.Br_telefona,
                PripadaBanci = b
            };

             s.Save(f);
             s.Flush();

            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniFilijalu(FilijalaView filijala)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Filijala f = s.Load<Filijala>(filijala.Rbr_filijale);
            

            if (f != null)
            {
                f.Adresa = filijala.Adresa!;
                f.Radno_vreme = filijala.Radno_vreme;
                f.Br_telefona = filijala.Br_telefona;

                s.Update(f);

                s.Flush();
                s.Close();
                return 1;
            }
            else
            {
                Console.WriteLine("Filijala sa ovim rednim brojem ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiFilijalu(int rbr)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Filijala f = s.Load<Filijala>(rbr);

            if (f != null)
            {
                s.Delete(f);

                s.Flush();
                s.Close();
                return f.Rbr_filijale;
            }
            else
            {
                Console.WriteLine("Filijala sa ovim rednim brojem ne postoji!\n");
                return f!.Rbr_filijale;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    #endregion

    #region Racun
    public static List<RacunView> VratiSveRacune()
    {

        ISession? s = null;

        List<RacunView> racuni = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Racun> sveRacune = from o in s.Query<Racun>()
                                                select o;

            foreach (Racun r in sveRacune)
            {
                racuni.Add(new RacunView(r));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return racuni;
    }


    public static List<RacunView> VratiSveRacuneOdBanke(int bankaId)
    {
        List<RacunView> racunList = new List<RacunView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Racun> racuni = from o in s.Query<Racun>()
                                             where o.JePovezan.Id == bankaId
                                             select o;

            foreach (Racun r in racuni)
            {
                BankaView banka = VratiBanku(r.JePovezan.Id);
                racunList.Add(new RacunView(r));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return racunList;
    }

    public static RacunView VratiRacun(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var r = s.Load<Racun>(id);
            var b = VratiBanku(r.JePovezan!.Id);
            var k = VratiKlijenta(r.Koristi!.Id);
            RacunView racun= new RacunView(r,b,k);

            s.Close();

            return racun;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }



    public static int IzbrisiRacun(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Racun r = s.Load<Racun>(id);

            if (r != null)
            {
                s.Delete(r);

                s.Flush();
                s.Close();
                return r.Br_racuna;
            }
            else
            {
                Console.WriteLine("Racun sa ovim id-jem ne postoji!\n");
                return r!.Br_racuna;
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }


    public static int IzmeniRacun(RacunView racun)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Racun r = s.Load<Racun>(racun.Br_racuna);

            

            if (r != null)
            {
                if (r.Tip == null && racun.Tip==null && racun.Valuta==null)//ako je dinarski racun i u zahtevu nije trazena promena tipa i valute
                {
                    r.Datum_otvaranja = racun.Datum_otvaranja!;
                    r.Tekuci_saldo = racun.Tekuci_saldo!;

                    s.Update(r);

                    s.Flush();
                    s.Close();

                    return 0;
                }
                else if (racun.Tip == "devizni" && racun.Valuta != null && racun.Valuta != "DIN" && r.Valuta != null)//ako je devizni racun
                {
                    r.Datum_otvaranja = racun.Datum_otvaranja!;
                    r.Tekuci_saldo = racun.Tekuci_saldo!;
                    r.Valuta = racun.Valuta!;

                    s.Update(r);

                    s.Flush();
                    s.Close();

                    return 0;
                }
                else
                {
                    return -1;//pokusava da promeni tip ili valutu gde nije moguce
                }
            }
            else
            {
                return -2;//ne postoji
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -2;
        }

    }


    public async static Task<int> DodajRacun(RacunView racun, int idBanka, int idKlijenta)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Banka b = await s.LoadAsync<Banka>(idBanka);
            Klijent k = await s.LoadAsync<Klijent>(idKlijenta);
            Racun r;

            if(b != null && k != null)
            {
                if (racun.Tip == "devizni" )//ne mora da se proverava da li je valuta razlicita od null jer to radi baza podataka za mene
                {
                    r = new DevizniRacun
                    {
                        Datum_otvaranja = racun.Datum_otvaranja,
                        Tekuci_saldo = racun.Tekuci_saldo!,
                        Tip = racun.Tip,
                        Valuta = racun.Valuta,
                        JePovezan = b,
                        Koristi = k,
                    };
                    await s.SaveAsync(r);
                    await s.FlushAsync();
                }
                else if (racun.Tip == "dinarski")
                {
                    r = new DinarskiRacun
                    {

                        Datum_otvaranja = racun.Datum_otvaranja,
                        Tekuci_saldo = racun.Tekuci_saldo!,
                        Tip = racun.Tip,
                        JePovezan = b,
                        Koristi = k,
                    };
                    await s.SaveAsync(r);
                    await s.FlushAsync();
                }
                return 0;//sve okej
            }
            else
            {
                return -1;//unesena Id banke ili Id klijenta koji ne postoje u bazi podataka
            }
            
           
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static List<RacunOvlascenoLiceView> VratiSvaOvlascenaLica(int idRacuna)
    {
        List<RacunOvlascenoLiceView> lica = new List<RacunOvlascenoLiceView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<RacunOvlascenoLice> ovlascenaLica = from l in s.Query<RacunOvlascenoLice>()
                                                            where l.PripadaRacunu!.Br_racuna == idRacuna
                                                            select l;

            foreach (RacunOvlascenoLice lice in ovlascenaLica)
            {
                RacunView racun = VratiRacun(lice.PripadaRacunu!.Br_racuna);
                lica.Add(new RacunOvlascenoLiceView(lice.Id, lice.ImeOvlascenogLica!, racun));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return lica;
    }

    public async static Task DodajOvlascenoLice(RacunOvlascenoLiceView lice, int idRacuna)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Racun r = await s.LoadAsync<Racun>(idRacuna);

            RacunOvlascenoLice l = new RacunOvlascenoLice
            {
                ImeOvlascenogLica = lice.ImeOvlascenogLica,
                PripadaRacunu = r
            };

            await s.SaveAsync(l);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniOvlascenoLice(RacunOvlascenoLiceView lice)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            RacunOvlascenoLice l = s.Load<RacunOvlascenoLice>(lice.Id);


            if (l != null)
            {
                l.ImeOvlascenogLica = lice.ImeOvlascenogLica;

                s.Update(l);

                s.Flush();
                s.Close();

                return l.Id;
            }
            else
            {
                Console.WriteLine($"Ovlasceno lice sa Id-jem {lice.Id} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiOvlascenoLice(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            RacunOvlascenoLice lice = s.Load<RacunOvlascenoLice>(id);

            if (lice != null)
            {
                s.Delete(lice);

                s.Flush();
                s.Close();
                return lice.Id;
            }
            else
            {
                Console.WriteLine($"Ovlasceno lice sa id-jem {id} ne postoji!\n");
                return lice!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
    #endregion

    #region Klijent
    public static List<KlijentView> VratiSveKlijente()
    {
        ISession? s = null;

        List<KlijentView> klijenti = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Klijent> sviKlijenti= from o in s.Query<Klijent>()
                                          select o;

            foreach (Klijent k in sviKlijenti)
            {
                klijenti.Add(new KlijentView(k));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return klijenti;
    }

    public static int IzbrisiKlijenta(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Klijent k = s.Load<Klijent>(id);

            if (k != null)
            {
                s.Delete(k);

                s.Flush();
                s.Close();
                return k.Id;
            }
            else
            {
                Console.WriteLine("Klijent sa ovim id-jem ne postoji!\n");
                return k!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }


    public static KlijentView VratiKlijenta(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var k = s.Load<Klijent>(id);
            KlijentView klijent = new KlijentView(k);

            s.Close();

            return klijent;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }

    public static List<KomentarKlijentaView> VratiSveKomentareKlijenta(int idKlijenta)
    {
        List<KomentarKlijentaView> komentari = new List<KomentarKlijentaView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<KomentarKlijenta> klijKomentari = from k in s.Query<KomentarKlijenta>()
                                                           where k.PripadaKlijentu!.Id == idKlijenta
                                                           select k;

            foreach (KomentarKlijenta kom in klijKomentari)
            {
                KlijentView klijent = VratiKlijenta(kom.PripadaKlijentu!.Id);
                komentari.Add(new KomentarKlijentaView(kom.Id, kom.Komentar!, klijent));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return komentari;
    }

    public async static Task DodajKomentarKlijenta(KomentarKlijentaView komentar, int idKlijenta)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Klijent k = await s.LoadAsync<Klijent>(idKlijenta);

            KomentarKlijenta kom = new KomentarKlijenta
            {
                Komentar = komentar.Komentar,
                PripadaKlijentu = k
            };

            await s.SaveAsync(kom);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniKomentarKlijenta(KomentarKlijentaView komentar)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            KomentarKlijenta kom = s.Load<KomentarKlijenta>(komentar.Id);


            if (kom != null)
            {
                kom.Komentar = komentar.Komentar;

                s.Update(kom);

                s.Flush();
                s.Close();

                return kom.Id;
            }
            else
            {
                Console.WriteLine($"Komentar klijenta sa Id-jem {komentar.Id} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiKomentarKlijenta(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            KomentarKlijenta kom = s.Load<KomentarKlijenta>(id);

            if (kom != null)
            {
                s.Delete(kom);

                s.Flush();
                s.Close();
                return kom.Id;
            }
            else
            {
                Console.WriteLine($"Komentar klijenta sa id-jem {id} ne postoji!\n");
                return kom!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    #endregion

    #region Fizicka Lica
    public static List<FizickoLiceView> VratiSvaFizickaLica()
    {
        ISession? s = null;

        List<FizickoLiceView> fizickaLica = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<FizickoLice> svaFL= from o in s.Query<FizickoLice>()
                                          select o;

            foreach (FizickoLice fl in svaFL)
            {
                fizickaLica.Add(new FizickoLiceView(fl));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return fizickaLica;
    }

    public static void DodajFizickoLice(FizickoLiceView fizickoLice)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            FizickoLice fl = new()
            {
                Id = fizickoLice.Id,
                Br_tel = fizickoLice.Br_tel,
                Email = fizickoLice.Email,
                Adresa = fizickoLice.Adresa,
                JMBG = fizickoLice.JMBG,
                Datum_rodjenja = fizickoLice.Datum_rodjenja,
                LIme = fizickoLice.LIme,
                Ime_roditelja = fizickoLice.Ime_roditelja,
                Prezime = fizickoLice.Prezime,
                Br_licne_karte = fizickoLice.Br_licne_karte,
                Mesto_izdavanja = fizickoLice.Mesto_izdavanja,
                Naziv = fizickoLice.LIme + fizickoLice.Prezime
            };

            s.SaveOrUpdate(fl);

            s.Flush();
            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void IzmeniFizickoLice(FizickoLiceView fizickoLice)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            FizickoLice fl = s.Load<FizickoLice>(fizickoLice.Id);
            if (fl != null)
            {
                fl.Br_tel = fizickoLice.Br_tel;
                fl.Email = fizickoLice.Email;
                fl.Adresa = fizickoLice.Adresa;
                fl.JMBG = fizickoLice.JMBG;
                fl.Datum_rodjenja = fizickoLice.Datum_rodjenja;
                fl.LIme = fizickoLice.LIme;
                fl.Ime_roditelja = fizickoLice.Ime_roditelja;
                fl.Prezime = fizickoLice.Prezime;
                fl.Br_licne_karte = fizickoLice.Br_licne_karte;
                fl.Mesto_izdavanja = fizickoLice.Mesto_izdavanja;
                fl.Naziv = fizickoLice.LIme + fizickoLice.Prezime;

                s.Update(fl);
                s.Flush();
                s.Close();
            }
            else
            {
                Console.WriteLine("Fizicko lice sa ovim id-jem ne postoji.\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }




    public static int IzbrisiFizickoLice(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            FizickoLice fl = s.Load<FizickoLice>(id);

            if (fl != null)
            {
                s.Delete(fl);

                s.Flush();
                s.Close();
                return fl.Id;
            }
            else
            {
                Console.WriteLine("Fizicko Lice sa ovim id-jem ne postoji!\n");
                return fl.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    #endregion

    #region Pravno Lice

    public static List<PravnoLiceView> VratiSvaPravnaLica()
    {
        ISession? s = null;

        List<PravnoLiceView> pravnaLica = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<PravnoLice> svaPL = from o in s.Query<PravnoLice>()
                                             select o;

            foreach (PravnoLice pl in svaPL)
            {
                pravnaLica.Add(new PravnoLiceView(pl));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return pravnaLica;
    }

    public static void DodajPravnoLice(PravnoLiceView pravnoLice)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            PravnoLice pl = new()
            {
                Id = pravnoLice.Id,
                Br_tel = pravnoLice.Br_tel,
                Email = pravnoLice.Email,
                Adresa = pravnoLice.Adresa,
                Naziv = pravnoLice.Naziv,
                Poreski_id = pravnoLice.Poreski_id
            };

            s.SaveOrUpdate(pl);

            s.Flush();
            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static void IzmeniPravnoLice(PravnoLiceView pravnoLice)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            PravnoLice pl = s.Load<PravnoLice>(pravnoLice.Id);
            if (pl != null)
            {
                pl.Br_tel = pravnoLice.Br_tel;
                pl.Email = pravnoLice.Email;
                pl.Adresa = pravnoLice.Adresa;
                pl.Naziv = pravnoLice.Naziv;
                pl.Poreski_id = pravnoLice.Poreski_id;

                s.Update(pl);
                s.Flush();
                s.Close();
            }
            else
            {
                Console.WriteLine("Pravno lice sa ovim id-jem ne postoji.\n");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }




    public static int IzbrisiPravnoLice(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            PravnoLice pl = s.Load<PravnoLice>(id);

            if (pl != null)
            {
                s.Delete(pl);

                s.Flush();
                s.Close();
                return pl.Id;
            }
            else
            {
                Console.WriteLine("Pravno Lice sa ovim id-jem ne postoji!\n");
                return pl.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
    #endregion

    #region Bankomat
    public static List<BankomatView> VratiSveBankomate()
    {
        ISession? s = null;

        List<BankomatView> bankomati = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Bankomat> sviBankomati = from b in s.Query<Bankomat>()
                                          select b;

            foreach (Bankomat ban in sviBankomati)
            {
                bankomati.Add(new BankomatView(ban));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return bankomati;
    }

    public static BankomatView VratiBankomat(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var b = s.Load<Bankomat>(id);
            BankomatView bankomat = new BankomatView(b);

            s.Close();

            return bankomat;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }

    public static List<BankomatView> VratiSveBankomateOdFilijale(int filijalaId)
    {
        List<BankomatView> bankomatiList = new List<BankomatView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Bankomat> bankomati = from b in s.Query<Bankomat>()
                                             where b.InstaliranUFilijali!.Rbr_filijale == filijalaId
                                             select b;

            foreach (Bankomat ban in bankomati)
            {
                FilijalaView filijala = VratiFilijalu(ban.InstaliranUFilijali!.Rbr_filijale);
                bankomatiList.Add(new BankomatView(ban.Id, ban.Lokacija!, ban.Proizvodjac!, ban.Status!, ban.Datum_Poslednjeg_Servisa, filijala));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return bankomatiList;
    }

    public async static Task DodajBankomat(BankomatView bankomat, int rbrFilijala)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Filijala f = await s.LoadAsync<Filijala>(rbrFilijala);

            Bankomat b = new Bankomat
            {
                Lokacija = bankomat.Lokacija,
                Proizvodjac = bankomat.Proizvodjac,
                Status = bankomat.Status,
                Datum_Poslednjeg_Servisa = bankomat.Datum_Poslednjeg_Servisa,
                InstaliranUFilijali = f
            };

            await s.SaveAsync(b);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static void IzmeniBankomat(BankomatView bankomat)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Bankomat b = s.Load<Bankomat>(bankomat.Id);


            if (b != null)
            {
                b.Status = bankomat.Status;
                b.Proizvodjac = bankomat.Proizvodjac;
                b.Datum_Poslednjeg_Servisa = bankomat.Datum_Poslednjeg_Servisa;
                b.Lokacija = bankomat.Lokacija;

                s.Update(b);

                s.Flush();
                s.Close();
            }
            else
            {
                Console.WriteLine("Bankomat sa ovim id-jem ne postoji.\n");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    public static int IzbrisiBankomat(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Bankomat b = s.Load<Bankomat>(id);

            if (b != null)
            {
                s.Delete(b);

                s.Flush();
                s.Close();
                return b.Id;
            }
            else
            {
                Console.WriteLine("Bankomat sa ovim id-jem ne postoji!\n");
                return b!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public static List<BankomatKomentariView> VratiSveKomentareBankomata(int idBankomata)
    {
        List<BankomatKomentariView> komentari = new List<BankomatKomentariView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<BankomatKomentari> bankKomentari = from k in s.Query<BankomatKomentari>()
                                                    where k.PripadaBankomatu.Id == idBankomata
                                                    select k;

            foreach (BankomatKomentari kom in bankKomentari)
            {
                BankomatView bankomat = VratiBankomat(kom.PripadaBankomatu!.Id);
                komentari.Add(new BankomatKomentariView(kom.Id, kom.Komentar!, bankomat));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return komentari;
    }

    public async static Task DodajKomentarBankomata(BankomatKomentariView komentar, int idBankomata)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Bankomat b = await s.LoadAsync<Bankomat>(idBankomata);

            BankomatKomentari k = new BankomatKomentari
            {
                Komentar = komentar.Komentar,
                PripadaBankomatu = b
            };

            await s.SaveAsync(k);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniKomentarBankomata(BankomatKomentariView komentar)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            BankomatKomentari kom = s.Load<BankomatKomentari>(komentar.Id);


            if (kom != null)
            {
                kom.Komentar = komentar.Komentar;

                s.Update(kom);

                s.Flush();
                s.Close();

                return kom.Id;
            }
            else
            {
                Console.WriteLine($"Komentar bankomata sa Id-jem {komentar.Id} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiKomentarBankomata(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            BankomatKomentari kom = s.Load<BankomatKomentari>(id);

            if (kom != null)
            {
                s.Delete(kom);

                s.Flush();
                s.Close();
                return kom.Id;
            }
            else
            {
                Console.WriteLine($"Komentar bankomata sa id-jem {id} ne postoji!\n");
                return kom!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
    #endregion

    #region Servis Bankomata
    public static List<ServisView> VratiSveServiseOdBankomata(int bankomatId)
    {
        List<ServisView> servisiList = new List<ServisView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Servis> servisi = from ser in s.Query<Servis>()
                                              where ser.ServisiraniBankomat!.Id == bankomatId
                                              select ser;

            foreach (Servis sr in servisi)
            {
                BankomatView bankomat = VratiBankomat(sr.ServisiraniBankomat!.Id);
                servisiList.Add(new ServisView(sr.Kod, sr.Firma!, bankomat));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return servisiList;
    }

    public async static Task DodajServis(ServisView servis, int idBankomata)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Bankomat b = await s.LoadAsync<Bankomat>(idBankomata);

            Servis ser = new Servis
            {
                Firma = servis.Firma,
                ServisiraniBankomat = b,
            };

            await s.SaveAsync(ser);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniServis(ServisView servis)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Servis ser = s.Load<Servis>(servis.Kod);


            if (ser != null)
            {
                ser.Firma = servis.Firma;

                s.Update(ser);

                s.Flush();
                s.Close();

                return servis.Kod;
            }
            else
            {
                Console.WriteLine($"Servis sa kodom {servis.Kod} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiServis(int kod)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Servis ser = s.Load<Servis>(kod);

            if (ser != null)
            {
                s.Delete(ser);

                s.Flush();
                s.Close();
                return ser.Kod;
            }
            else
            {
                Console.WriteLine("Servis sa ovim id-jem ne postoji!\n");
                return ser!.Kod;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }

    public static ServisView VratiServis(int kod)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var ser = s.Load<Servis>(kod);
            ServisView servis = new ServisView(ser);

            s.Close();

            return servis;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }

    public static List<OtklonjenaGreskaServisaView> VratiOtklonjeneGreske(int kod)
    {
        List<OtklonjenaGreskaServisaView> greskeList = new List<OtklonjenaGreskaServisaView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<OtklonjenaGreskaServisa> greske = from g in s.Query<OtklonjenaGreskaServisa>()
                                          where g.PripadaServisu!.Kod == kod
                                          select g;

            foreach (OtklonjenaGreskaServisa gr in greske)
            {
                ServisView servis = VratiServis(gr.PripadaServisu!.Kod);
                greskeList.Add(new OtklonjenaGreskaServisaView(gr.Id, gr.Otklonjena_greska!, servis));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return greskeList;
    }

    public async static Task DodajOtklonjenuGresku(OtklonjenaGreskaServisaView greska, int kodServisa)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Servis sr = await s.LoadAsync<Servis>(kodServisa);

            OtklonjenaGreskaServisa g = new OtklonjenaGreskaServisa
            {
               Otklonjena_greska = greska.Otklonjena_greska,
               PripadaServisu = sr
            };

            await s.SaveAsync(g);
            await s.FlushAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    public static int IzmeniOtklonjenuGresku(OtklonjenaGreskaServisaView greska)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            OtklonjenaGreskaServisa gr = s.Load<OtklonjenaGreskaServisa>(greska.Id);


            if (gr != null)
            {
                gr.Otklonjena_greska = greska.Otklonjena_greska;

                s.Update(gr);

                s.Flush();
                s.Close();

                return gr.Id;
            }
            else
            {
                Console.WriteLine($"Otklonjena greska sa Id-jem {greska.Id} ne postoji.\n");
                return 0;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }

    }

    public static int IzbrisiOtklonjenuGresku(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            OtklonjenaGreskaServisa gr = s.Load<OtklonjenaGreskaServisa>(id);

            if (gr != null)
            {
                s.Delete(gr);

                s.Flush();
                s.Close();
                return gr.Id;
            }
            else
            {
                Console.WriteLine("Greska sa ovim id-jem ne postoji!\n");
                return gr!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
    #endregion

    #region Kartica
    public static List<KarticaView> VratiSveKartice()
    {

        ISession? s = null;

        List<KarticaView> kartica = new();

        try
        {
            s = DataLayer.GetSession();


            IEnumerable<Kartica> sveKartice= from o in s.Query<Kartica>()
                                           select o;

            foreach (Kartica k in sveKartice)
            {
                kartica.Add(new KarticaView(k));
            }
        }
        catch (Exception)
        {
            return null!;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }

        return kartica;
    }


    public static List<KarticaView> PreuzmiKarticeOdRacuna(int brRacuna)
    {
        List<KarticaView> karticaList = new List<KarticaView>();
        try
        {
            ISession s = DataLayer.GetSession();

            IEnumerable<Kartica> kartice = from o in s.Query<Kartica>()
                                        where o.Odgovara!.Br_racuna == brRacuna
                                        select o;

            foreach (Kartica k in kartice)
            {
                
                karticaList.Add(new KarticaView(k));
            }

            s.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return karticaList;
    }

    public static KarticaView VratiKarticu(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            var k = s.Load<Kartica>(id);
            var r = VratiRacun(k.Odgovara!.Br_racuna);
            KarticaView kartica = new KarticaView(k, r);

            s.Close();

            return kartica;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null!;
        }
    }



    public static int IzbrisiKarticu(int id)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Kartica k = s.Load<Kartica>(id);

            if (k != null)
            {
                s.Delete(k);

                s.Flush();
                s.Close();
                return k.Id;
            }
            else
            {
                Console.WriteLine("Kartica sa ovim id-jem ne postoji!\n");
                return k!.Id;
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }


    public static int IzmeniKarticu(KarticaView kartica)
    {
        try
        {
            ISession s = DataLayer.GetSession();

            Kartica k = s.Load<Kartica>(kartica.Id);



            if (k != null)
            {
                if (kartica.Tip == "debitna" && kartica.Max_iznos_zaduzenja == null)//ako je debitna kartica
                {
                    k.Datum_izdavanje= kartica!.Datum_izdavanje!;
                    k.Datum_isteka = kartica.Datum_isteka!;
                    k.Dnevni_limit = kartica.Dnevni_limit;
                    

                    s.Update(k);

                    s.Flush();
                    s.Close();

                    return 0;
                }
                else if (kartica.Tip == "kreditna" && kartica.Max_iznos_zaduzenja != null && kartica?.Max_datum_vracanja_duga != null )//ako je devizni racun
                {
                    k.Datum_izdavanje = kartica!.Datum_izdavanje!;
                    k.Datum_isteka = kartica.Datum_isteka!;
                    k.Dnevni_limit = kartica.Dnevni_limit;

                    k.Max_iznos_zaduzenja = kartica.Max_iznos_zaduzenja;
                    k.Max_datum_vracanja_duga = kartica.Max_datum_vracanja_duga;


                    s.Update(k);

                    s.Flush();
                    s.Close();

                    return 0;
                }
                else
                {
                    return -1;//pokusava da promeni tip ili valutu gde nije moguce
                }
            }
            else
            {
                return -2;//ne postoji
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -2;
        }

    }


    public async static Task<int> DodajKarticu(KarticaView kartica, int brRacuna)
    {
        ISession? s = null;

        try
        {
            s = DataLayer.GetSession();
            Racun r = await s.LoadAsync<Racun>(brRacuna);
            
            Kartica k;

            if (r != null)
            {
                if (kartica.Tip == "kreditna")
                {
                    k = new KreditnaKartica
                    {
                        Datum_izdavanje = kartica.Datum_izdavanje,
                        Datum_isteka = kartica.Datum_isteka,
                        Dnevni_limit = kartica.Dnevni_limit,
                        Tip = kartica.Tip,
                        Max_iznos_zaduzenja = kartica.Max_iznos_zaduzenja,
                        Max_datum_vracanja_duga = kartica.Max_datum_vracanja_duga,
                        Odgovara = r
                    };
                    await s.SaveAsync(k);
                    await s.FlushAsync();
                }
                else if (kartica.Tip == "debitna")
                {
                    k = new DebitnaKartica
                    {

                        Datum_izdavanje = kartica.Datum_izdavanje,
                        Datum_isteka = kartica.Datum_isteka,
                        Dnevni_limit = kartica.Dnevni_limit,
                        Tip = kartica.Tip,
                        Odgovara = r
                    };
                    await s.SaveAsync(k);
                    await s.FlushAsync();
                }
                return 0;//sve okej
            }
            else
            {
                return -1;//unesen br racuna koji ne postoje u bazi podataka
            }


        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
        finally
        {
            s?.Close();
            s?.Dispose();
        }
    }

    #endregion

    #region Transakcija 
    public static List<PregledView> VratiSveTransakcijeBankomata(int bankomatId)
    {
        List<PregledView> pregledList = new List<PregledView>();
        try
        {
            ISession s = DataLayer.GetSession();
            var koristi = from k in s.Query<Koristi_Za_Podizanje_Novca>()
                          where k.Bankomat!.Id == bankomatId
                          select new PregledView(
                              k.Transakcija!.Id,
                              k.Bankomat!.Id,
                              k.Kartica!.Id,
                              k.Transakcija.Podignuti_iznos!,
                              k.Transakcija.Datum_Podizanja_Novca!,
                              k.Transakcija.Vreme_Podizanja_Novca!
                          );

            pregledList = koristi.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return pregledList;
    }


    public static int DodajTransakciju(TransakcijaView transakcija, int bankomatId, int karticaId)
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

            if (Int32.Parse(k.Odgovara!.Tekuci_saldo) < Int32.Parse(t.Podignuti_iznos!))
            {
                s.Delete(t);
                return -1; //Tekuci saldo je manji od novca koji zelite da podignete.
            }

            if (Int32.Parse(k.Dnevni_limit!) < Int32.Parse(t.Podignuti_iznos!))
            {
                s.Delete(t);
                return -2; //Dnevni limit je manji od novca koji zelite da podignete.

            }

            int oduzimanje = (Int32.Parse(k.Odgovara.Tekuci_saldo)) - (Int32.Parse(t.Podignuti_iznos!));

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

            s.Flush();
            s.Close();
            return 0;

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return 0;
        }
    }

    #endregion
}


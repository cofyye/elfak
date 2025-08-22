using ATM_WinForm.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ATM_WinForm
{
    public class DTOs
    {
        public class BankaBasic
        {
            [DisplayName("ID")]
            public int Id { get; protected set; }

            [DisplayName("Ime")]
            public string Ime { get; set; }

            [DisplayName("Email")]
            public string Email { get; set; }

            [DisplayName("Web Adresa")]
            public string Web_adresa { get; set; }

            [DisplayName("Adresa Centrale")]
            public string Adresa_centrale { get; set; }

            [Browsable(false)]
            public virtual IList<FilijalaBasic> Filijala { get; set; }

            [Browsable(false)]
            public virtual IList<RacunBasic> Racuni { get; set; }

            [Browsable(false)]
            public virtual IList<BankaBrTelefonaBasic> BrojeviTelefona { get; set; }

            public BankaBasic(int id, string ime, string email, string webAdresa, string adresaCentrale)
            {
                this.Id = id;
                this.Ime = ime;
                this.Email = email;
                this.Web_adresa = webAdresa;
                this.Adresa_centrale = adresaCentrale;
            }
            public BankaBasic()
            {
                Filijala = new List<FilijalaBasic>();
                Racuni = new List<RacunBasic>();
                BrojeviTelefona = new List<BankaBrTelefonaBasic>();
            }
        }

        public class FilijalaBasic
        {
            [DisplayName("Redni broj")]
            public virtual int Rbr_filijale { get; protected set; }

            [DisplayName("Adresa")]
            public virtual string Adresa { get; set; }

            [DisplayName("Broj telefona")]
            public virtual string Br_telefona { get; set; }

            [DisplayName("Radno vreme")]
            public virtual string Radno_vreme { get; set; }

            [DisplayName("Ime banke")]
            public virtual string Ime_banke { get; set; }

            //MAPIRANJE FILIJALA-BANKA
            [Browsable(false)]
            public virtual BankaBasic PripadaBanci { get; set; }

            //MAPIRANJE FILIJAL-BANKOMAT
            [Browsable(false)]
            public virtual IList<BankomatBasic> Bankomati { get; set; }

            public FilijalaBasic(int id, string adresa, string brTelefona, string radnoVreme, BankaBasic banka)
            {
                this.Rbr_filijale = id;
                this.Adresa = adresa;
                this.Br_telefona = brTelefona;
                this.Radno_vreme = radnoVreme;
                this.PripadaBanci = banka;
                this.Ime_banke = banka.Ime;
            }
            public FilijalaBasic()
            {
                Bankomati = new List<BankomatBasic>();
            }
        }

        public class BankomatKomentariBasic
        {
            [Browsable(false)]
            public virtual int Id { get; protected set; }

            [DisplayName("Komentar")]
            public virtual string Komentar { get; set; }


            //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
            [Browsable(false)]
            public virtual BankomatBasic PripadaBankomatu { get; set; }

            public BankomatKomentariBasic(int id, string komentar, BankomatBasic bankomat)
            {
                this.Id = id;
                this.Komentar = komentar;
                this.PripadaBankomatu = bankomat;
            }

            public BankomatKomentariBasic() { }
        }

        public class BankomatBasic
        {
            [DisplayName("ID")]
            public virtual int Id { get; protected set; }

            [DisplayName("Lokacija")]
            public string Lokacija { get; set; }

            [DisplayName("Proizvodjac")]
            public virtual string Proizvodjac { get; set; }

            [DisplayName("Status")]
            public virtual string Status { get; set; }

            [DisplayName("Datum poslednjeg servisa")]
            public virtual DateTime Datum_Poslednjeg_Servisa { get; set; }

            [Browsable(false)]
            public virtual FilijalaBasic InstaliranUFilijali { get; set; }

            [Browsable(false)]
            public virtual IList<BankomatKomentariBasic> Komentari { get; set; }

            [Browsable(false)]
            public virtual IList<ServisBasic> Servisi { get; set; }

            [Browsable(false)]
            public virtual IList<Koristi_Za_Podizanje_NovcaBasic> Koristi_Za_Podizanje_NovcaBasics { get; set; }

            public BankomatBasic(int id, string lokacija, string proizvodjac, string status, DateTime datum, FilijalaBasic filijala)
            {
                this.Id = id;
                this.Lokacija = lokacija;
                this.Proizvodjac = proizvodjac;
                this.Status = status;
                this.Datum_Poslednjeg_Servisa = datum;
            }

            public BankomatBasic() 
            {
                Komentari = new List<BankomatKomentariBasic>();
                Servisi = new List<ServisBasic>();
                Koristi_Za_Podizanje_NovcaBasics = new List<Koristi_Za_Podizanje_NovcaBasic>();
            }
        }

        public class RacunOvlascenoLiceBasic
        {
            [Browsable(false)]
            public virtual int Id { get; protected set; }

            [DisplayName("Ime ovlascenog lica")]
            public virtual string ImeOvlascenogLica { get; set; }


            //MAPIRANJE BANKA_BR_TEL-BANKA
            [Browsable(false)]
            public virtual RacunBasic PripadaRacunu { get; set; }

            public RacunOvlascenoLiceBasic(int id, string imeLica, RacunBasic racun)
            {
                this.Id = id;
                this.ImeOvlascenogLica = imeLica;
                this.PripadaRacunu = racun;
            }

            public RacunOvlascenoLiceBasic() { }
        }

        public class RacunBasic
        {
            [DisplayName("Broj racuna")]
            public virtual int Br_racuna { get; protected set; }

            [DisplayName("Datum otvaranja")]
            public virtual DateTime Datum_otvaranja { get; set; }

            [DisplayName("Tekuci saldo")]
            public virtual string Tekuci_saldo { get; set; }


            [Browsable(false)]
            public string Tip { get; set; }

            [DisplayName("Valuta")]
            public virtual string Valuta { get; set; }


            //MAPIRANJE RACUN-BANKA
            [Browsable(false)]
            public virtual BankaBasic JePovezan { get; set; }

            //MAPIRANJE RACUN-KLIJENT
            [Browsable(false)]
            public virtual KlijentBasic Koristi { get; set; }

            //MAPIRANJE RACUN-KARTICA
            [Browsable(false)]
            public virtual IList<KarticaBasic> Kartice { get; set; }

            [Browsable(false)]
            public virtual IList<RacunOvlascenoLiceBasic> OvlascenaLica { get; set; }

            public RacunBasic(int br_rac, DateTime datum, string tekuci_saldo, string tip, string valuta, BankaBasic banka, KlijentBasic klijent)
            {
                this.Br_racuna = br_rac;
                this.Datum_otvaranja = datum;
                this.Tekuci_saldo = tekuci_saldo;
                this.Tip = tip;
                this.Valuta = valuta;
                this.JePovezan = banka;
                this.Koristi = klijent;
            }

            public RacunBasic()
            {
                Kartice = new List<KarticaBasic>();
                OvlascenaLica = new List<RacunOvlascenoLiceBasic>();
            }
        }

        public class KlijentBasic
        {
            [DisplayName("ID")]
            public virtual int Id { get; protected set; }

            [DisplayName("Naziv")]
            public virtual string Naziv { get; set; }

            [DisplayName("Broj telefona")]
            public virtual string Br_tel { get; set; }

            [DisplayName("Email")]
            public virtual string Email { get; set; }

            [DisplayName("Adresa")]
            public virtual string Adresa { get; set; }

            //MAIRANJE KLIJENT-RACUN
            [Browsable(false)]
            public virtual IList<RacunBasic> Racuni { get; set; }

            //MAPIRANJE OVLASCENO KLIJENT-KOMENTAR
            [Browsable(false)]
            public virtual IList<KomentarKlijentaBasic> Komentari { get; set; }

            public KlijentBasic(int id, string telefon, string email, string adresa,string naziv="")
            {
                this.Id = id;
                this.Br_tel = telefon;
                this.Email = email;
                this.Adresa = adresa;
                this.Naziv = naziv;
            }

            public KlijentBasic()
            {
                //MAPIRANJE KLIJENT-RACUN
                Racuni = new List<RacunBasic>();
                //MAPIRANJE KLIJENT-KOMENTAR
                Komentari = new List<KomentarKlijentaBasic>();
            }
        }

        public class KomentarKlijentaBasic
        {
            [Browsable(false)]
            public virtual int Id { get; protected set; }

            [DisplayName("Komentar")]
            public virtual string Komentar { get; set; }


            //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
            [Browsable(false)]
            public virtual KlijentBasic PripadaKlijentu { get; set; }

            public KomentarKlijentaBasic(int id, string komentar, KlijentBasic klijent)
            {
                this.Id = id;
                this.Komentar = komentar;
                this.PripadaKlijentu = klijent;
            }

            public KomentarKlijentaBasic() { }
        }

        public class PravnoLiceBasic : KlijentBasic
        {
            [DisplayName("Poreski ID")]
            public virtual string Poreski_id { get; set; }

            public PravnoLiceBasic(string poreski_id, int id, string naziv, string telefon, string email, string adresa)
                : base(id, telefon, email, adresa)
            {
                Poreski_id = poreski_id;
                this.Naziv = naziv;
            }

            public PravnoLiceBasic() { }
        }

        public class FizickoLiceBasic : KlijentBasic
        {
            [DisplayName("JMBG")]
            public virtual string JMBG { get; set; }

            [DisplayName("Datum rodjenja")]
            public virtual DateTime Datum_rodjenja { get; set; }

            [DisplayName("Ime")]
            public virtual string LIme { get; set; }

            [DisplayName("Ime roditelja")]
            public virtual string Ime_roditelja { get; set; }

            [DisplayName("Prezime")]
            public virtual string Prezime { get; set; }

            [DisplayName("Broj licne karte")]
            public virtual string Br_licne_karte { get; set; }

            [DisplayName("Mesto izdavanja")]
            public virtual string Mesto_izdavanja { get; set; }

            public FizickoLiceBasic(string JMBG, DateTime datum_rodjenja, string lIme, string ime_roditelja, string prezime, string br_licne_karte, string mesto_izdavanja, int id, string naziv, string telefon, string email, string adresa)
                : base(id, telefon, email, adresa)
            {
                this.JMBG = JMBG;
                Datum_rodjenja = datum_rodjenja;
                LIme = lIme;
                Ime_roditelja = ime_roditelja;
                Prezime = prezime;
                Br_licne_karte = br_licne_karte;
                Mesto_izdavanja = mesto_izdavanja;
                this.Naziv = naziv;
            }

            public FizickoLiceBasic() { }
        }

        public class BankaBrTelefonaBasic
        {
            [Browsable(false)]
            public virtual int Id { get; protected set; }

            [DisplayName("Broj telefona")]
            public virtual string BrTelefona { get; set; }


            //MAPIRANJE BANKA_BR_TEL-BANKA
            [Browsable(false)]
            public virtual BankaBasic PripadaBanci { get; set; }

            public BankaBrTelefonaBasic(int id, string brTel, BankaBasic banka)
            {
                this.Id = id;
                this.BrTelefona = brTel;
                this.PripadaBanci = banka;
            }

            public BankaBrTelefonaBasic() { }
        }

        public class KarticaBasic
        {
            [DisplayName("ID")]
            public virtual int Id { get; protected set; }

            [DisplayName("Datum izdavanja")]
            public virtual DateTime Datum_izdavanje { get; set; }

            [DisplayName("Datum isteka")]
            public virtual DateTime Datum_isteka { get; set; }

            [DisplayName("Dnevni limit")]
            public virtual string Dnevni_limit { get; set; }

            [Browsable(false)]
            public virtual string Tip { get; set; }

            [DisplayName("Maksimalan iznos zaduzenja")]
            public virtual string Max_iznos_zaduzenja { get; set; }

            [DisplayName("Max datum vracanja duga")]
            public virtual DateTime Max_datum_vracanja_duga { get; set; }

            //MAPIRANJE KARTICA-RACUN
            [Browsable(false)]
            public virtual RacunBasic Odgovara { get; set; }

            //TERNARNA
            [Browsable(false)]
            public virtual IList<Koristi_Za_Podizanje_Novca> Koristi_Za_Podizanje_Novca { get; set; }


            public KarticaBasic(int id, DateTime datum_izdavanja, DateTime datum_isteka, string dnevni_limit, string tip,string max_iznos_zaduzenja, DateTime max_datum_vracanja_duga, RacunBasic racun)
            {
                this.Id = id;
                this.Datum_izdavanje = datum_izdavanja;
                this.Datum_isteka = datum_isteka;
                this.Dnevni_limit = dnevni_limit;
                this.Tip = tip;
                this.Max_iznos_zaduzenja = max_iznos_zaduzenja;
                this.Max_datum_vracanja_duga = max_datum_vracanja_duga;
                this.Odgovara = racun;
            }

            public KarticaBasic() 
            {
            Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_Novca>();
            }
        }

        public class ServisBasic
        {
            [DisplayName("KOD")]
            public virtual int Kod { get; protected set; }

            [DisplayName("FIRMA")]
            public virtual string Firma { get; set; }

            //MAPIRANJE SERVIS-BANKOMAT
            [Browsable(false)]
            public virtual BankomatBasic ServisiraniBankomat { get; set; }

            //MAPIRANJE SERVIS-OTKLONJENE GRESKE
            [Browsable(false)]
            public virtual IList<OtklonjenaGreskaServisaBasic> OtklonjeneGreske { get; set; }

            public ServisBasic(int kod, string firma, BankomatBasic bankomat)
            {

                this.Kod = kod;
                this.Firma = firma;
                this.ServisiraniBankomat = bankomat;
            }

            public ServisBasic() 
            {
                //MAPIRANJE SERVIS-OTKLONJENE GRESKE
                OtklonjeneGreske = new List<OtklonjenaGreskaServisaBasic>();
            }
        }

        public class OtklonjenaGreskaServisaBasic
        {
            [Browsable(false)]
            public virtual int Id { get; protected set; }

            [DisplayName("Otklonjena greska")]
            public virtual string Otklonjena_greska { get; set; }


            //MAPIRANJE OTKLONJENA GRESKA-SERVIS
            [Browsable(false)]
            public virtual ServisBasic PripadaServisu { get; set; }


            public OtklonjenaGreskaServisaBasic(int id, string greska, ServisBasic servis)
            {
                this.Id = id;
                this.Otklonjena_greska = greska;
                this.PripadaServisu = servis;
            }

            public OtklonjenaGreskaServisaBasic() { }
        }

        public class TransakcijaBasic
        {
            [DisplayName("ID")]
            public virtual int Id { get; set; }

            [DisplayName("PODIGNUTI IZNOS")]
            public virtual string Podignuti_iznos { get; set; }

            [DisplayName("DATUM PODIZANJA NOVCA")]
            public virtual DateTime Datum_Podizanja_Novca { get; set; }

            [DisplayName("VREME PODIZANJA NOVCA")]
            public virtual string Vreme_Podizanja_Novca { get; set; }

            //TERNARNA
            [Browsable(false)]
            public virtual IList<Koristi_Za_Podizanje_Novca> Koristi_Za_Podizanje_Novca { get; set; }

            public TransakcijaBasic(int id, string podignuti_iznos, DateTime datum_Podizanja_Novca, string vreme_Podizanja_Novca)
            {
                Id = id;
                Podignuti_iznos = podignuti_iznos;
                Datum_Podizanja_Novca = datum_Podizanja_Novca;
                Vreme_Podizanja_Novca = vreme_Podizanja_Novca;
            }

            public TransakcijaBasic()
            {
                Koristi_Za_Podizanje_Novca= new List<Koristi_Za_Podizanje_Novca>();
            }
        }

        public class Koristi_Za_Podizanje_NovcaBasic
        {
            [DisplayName("ID")]
            public virtual int Id { get; set; }

            [DisplayName("ID BANKOMATA")]
            public virtual BankomatBasic Bankomat { get; set; }

            [DisplayName("ID KARTICE")]
            public virtual KarticaBasic Kartica { get; set; }

            [DisplayName("ID TRANSAKCIJE")]
            public virtual TransakcijaBasic Transakcija { get; set; }

            Koristi_Za_Podizanje_NovcaBasic(int id, BankomatBasic bankomat, KarticaBasic kartica, TransakcijaBasic transakcija)
            {
                Bankomat = bankomat;
                Kartica = kartica;
                Transakcija = transakcija;
            }

            Koristi_Za_Podizanje_NovcaBasic() { }
        }

        public class PregledBasic
        {
            [DisplayName("ID TRANSAKCIJE")]
            public virtual int TransakcijaId { get; set; }

            [DisplayName("ID BANKOMATA")]
            public virtual int BankomatId { get; set; }

            [DisplayName("ID KARTICE")]
            public virtual int KarticaId { get; set; }

            [DisplayName("PODIGNUT NOVAC")]
            public virtual string Podignut_Novac { get; set;}

            [DisplayName("DATUM")]
            public virtual string Datum { get; set; }

            [DisplayName("VREME")]
            public virtual string Vreme {  get; set; }

            public PregledBasic(int transakcijaId, int bankomatId, int karticaId, string podignut_Novac, DateTime datum, string vreme)
            {
                TransakcijaId = transakcijaId;
                BankomatId = bankomatId;
                KarticaId = karticaId;
                Podignut_Novac = podignut_Novac;
                Datum = datum.ToShortDateString();
                Vreme = vreme;
            }
        }
    }
}
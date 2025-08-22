using DatabaseAccess.Entiteti;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DatabaseAccess.DTOs
{
    public class FilijalaView
    {
        public int Rbr_filijale { get; protected set; }
        public string? Adresa { get; set; }
        public string? Br_telefona { get; set; }
        public string? Radno_vreme { get; set; }
        public string? Ime_banke { get; set; }
        public BankaView? PripadaBanci { get; set; }
        public IList<BankomatView>? Bankomati { get; set; }

        // Parameterless constructor
        public FilijalaView()
        {
            Bankomati = new List<BankomatView>();
        }

        
        public FilijalaView(string? adresa, string? br_telefona, string? radno_vreme)
        {
            Adresa = adresa;
            Br_telefona = br_telefona;
            Radno_vreme = radno_vreme;
        }

        internal FilijalaView(Filijala f)
        {
            Rbr_filijale = f.Rbr_filijale;
            Adresa = f.Adresa;
            Br_telefona = f.Br_telefona;
            Radno_vreme = f.Radno_vreme;
            PripadaBanci = new BankaView(f.PripadaBanci!);
            Ime_banke = PripadaBanci.Ime;
        }

        internal FilijalaView(int id, string adresa, string brTelefona, string radnoVreme, BankaView banka)
        {
            Rbr_filijale = id;
            Adresa = adresa;
            Br_telefona = brTelefona;
            Radno_vreme = radnoVreme;
            PripadaBanci = banka;
            Ime_banke = banka.Ime;
        }

        internal FilijalaView(string adresa, string brTelefona, string radnoVreme, BankaView pripadaBanci)
        {
            Adresa = adresa;
            Br_telefona = brTelefona;
            Radno_vreme = radnoVreme;
            PripadaBanci = pripadaBanci;
            Ime_banke = pripadaBanci.Ime;
        }

        public void SetRbr(int rbr)
        {
            Rbr_filijale = rbr;
        }
    }
}
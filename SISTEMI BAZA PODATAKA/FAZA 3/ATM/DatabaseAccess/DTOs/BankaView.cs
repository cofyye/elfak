using System.Text.Json.Serialization;
using DatabaseAccess.Entiteti;
using NHibernate.Event;
namespace DatabaseAccess.DTOs;

public class BankaView
{
    public int Id { get; protected set; }
    public string? Ime { get; set; }
    public string? Email { get; set; }
    public string? Web_adresa { get; set; }
    public string? Adresa_centrale { get; set; }
    public IList<FilijalaView>? Filijala { get; set; }
    public IList<RacunView>? Racuni { get; set; }
    public IList<BankaBrTelefonaView>? BrojeviTelefona { get; set; }

    public BankaView()
    {
        Filijala = new List<FilijalaView>();
        Racuni = new List<RacunView>();
        BrojeviTelefona = new List<BankaBrTelefonaView>();
    }

    internal BankaView(Banka b)
    {
        this.Id = b.Id;
        this.Ime = b.Ime;
        this.Email = b.Email;
        this.Web_adresa = b.Web_adresa;
        this.Adresa_centrale = b.Adresa_centrale;
    }

   
    internal BankaView( string ime, string email, string web_adresa, string adresa_centrale)
    {
        this.Ime = ime;
        this.Email = email;
        this.Web_adresa = web_adresa;
        this.Adresa_centrale = adresa_centrale;
    }

    // Method to set the ID internally
    public void SetId(int id)
    {
        this.Id = id;
    }
}

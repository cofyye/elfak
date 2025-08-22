using DatabaseAccess.Entiteti;

namespace DatabaseAccess.DTOs;

public class BankomatView
{
    public int Id { get; protected set; }
    public string? Lokacija { get; set; }
    public string? Proizvodjac { get; set; }
    public string? Status { get; set; }
    public DateTime Datum_Poslednjeg_Servisa { get; set; }
    public FilijalaView? InstaliranUFilijali { get; set; }
    public IList<BankomatKomentariView>? Komentari { get; set; }
    public IList<ServisView>? Servisi { get; set; }
    public IList<Koristi_Za_Podizanje_NovcaView>? Koristi_Za_Podizanje_NovcaView{ get; set; }

    public BankomatView()
    {
        Komentari = new List<BankomatKomentariView>();
        Servisi = new List<ServisView>();
        Koristi_Za_Podizanje_NovcaView = new List<Koristi_Za_Podizanje_NovcaView>();
    }

    internal BankomatView(Bankomat b)
    {
        this.Id = b.Id;
        this.Lokacija = b.Lokacija;
        this.Proizvodjac = b.Proizvodjac;
        this.Status = b.Status;
        this.Datum_Poslednjeg_Servisa = b.Datum_Poslednjeg_Servisa;
    }

    internal BankomatView(int id, string lokacija, string proizvodjac, string status, DateTime datum, FilijalaView filijala)
    {
        this.Id = id;
        this.Lokacija = lokacija;
        this.Proizvodjac = proizvodjac;
        this.Status = status;
        this.Datum_Poslednjeg_Servisa = datum;
        this.InstaliranUFilijali = filijala;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
}

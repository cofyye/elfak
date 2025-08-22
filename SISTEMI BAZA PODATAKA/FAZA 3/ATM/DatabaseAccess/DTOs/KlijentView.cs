namespace DatabaseAccess.DTOs;
using DatabaseAccess.Entiteti;

public class KlijentView
{

    public int Id { get; protected set; }
    public string? Naziv { get; set; }
    public string? Br_tel { get; set; }
    public string? Email { get; set; }
    public string? Adresa { get; set; }


    //MAIRANJE KLIJENT-RACUN
    public IList<RacunView>? Racuni { get; set; }

    //MAPIRANJE OVLASCENO KLIJENT-KOMENTAR
    public IList<KomentarKlijentaView>? Komentari { get; set; }

    internal KlijentView(Klijent k)
    {
        this.Id = k.Id;
        this.Br_tel = k.Br_tel;
        this.Email = k.Email;
        this.Adresa = k.Adresa;
        this.Naziv = k.Naziv;
    }

    internal KlijentView(int id, string telefon, string email, string adresa, string naziv = "")
    {
        this.Id = id;
        this.Br_tel = telefon;
        this.Email = email;
        this.Adresa = adresa;
        this.Naziv = naziv;
    }



    internal KlijentView()
    {
        //MAPIRANJE KLIJENT-RACUN
        Racuni = new List<RacunView>();
        //MAPIRANJE KLIJENT-KOMENTAR
        Komentari = new List<KomentarKlijentaView>();
    }
}

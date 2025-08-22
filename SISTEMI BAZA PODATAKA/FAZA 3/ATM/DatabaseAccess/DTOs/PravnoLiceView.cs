namespace DatabaseAccess.DTOs;
using DatabaseAccess.Entiteti;

public class PravnoLiceView : KlijentView
{
    public string? Poreski_id { get; set; }

    internal PravnoLiceView(string poreski_id, int id, string naziv, string telefon, string email, string adresa)
        : base(id, telefon, email, adresa)
    {
        Poreski_id = poreski_id;
        this.Naziv = naziv;
    }

    public PravnoLiceView() { }


    //za pregled
    internal PravnoLiceView(PravnoLice pravno)
    {
        this.Id = pravno.Id;
        this.Poreski_id = pravno.Poreski_id;
        this.Naziv = pravno.Naziv;
        this.Br_tel = pravno.Br_tel;
        this.Email = pravno.Email;
        this.Adresa = pravno.Adresa;
    }

    public void SetId(int id)
    {
        Id = id;
    }
}

namespace DatabaseAccess.DTOs;

public class KomentarKlijentaView
{

    public int Id { get; protected set; }
    public string? Komentar { get; set; }

    //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
    public KlijentView? PripadaKlijentu { get; set; }

    public KomentarKlijentaView() { }

    internal KomentarKlijentaView(int id, string komentar, KlijentView klijent)
    {
        this.Id = id;
        this.Komentar = komentar;
        this.PripadaKlijentu = klijent;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
}

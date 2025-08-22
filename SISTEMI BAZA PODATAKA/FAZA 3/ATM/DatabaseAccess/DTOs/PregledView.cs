namespace DatabaseAccess.DTOs;

public class PregledView
{
    public int TransakcijaId { get; set; }
    public int BankomatId { get; set; }
    public int KarticaId { get; set; }
    public string? Podignut_Novac { get; set; }
    public string? Datum { get; set; }
    public string? Vreme { get; set; }

    public PregledView() { }

    internal PregledView(int transakcijaId, int bankomatId, int karticaId, string podignut_Novac, DateTime datum, string vreme)
    {
        TransakcijaId = transakcijaId;
        BankomatId = bankomatId;
        KarticaId = karticaId;
        Podignut_Novac = podignut_Novac;
        Datum = datum.ToShortDateString();
        Vreme = vreme;
    }
}

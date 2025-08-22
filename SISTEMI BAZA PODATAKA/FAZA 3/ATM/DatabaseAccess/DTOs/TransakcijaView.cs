namespace DatabaseAccess.DTOs;

public class TransakcijaView
{
    public int Id { get; set; }
    public string? Podignuti_iznos { get; set; }
    public DateTime Datum_Podizanja_Novca { get; set; }
    public string? Vreme_Podizanja_Novca { get; set; }

    //TERNARNA
    public IList<Koristi_Za_Podizanje_NovcaView>? Koristi_Za_Podizanje_Novca { get; set; }

    public TransakcijaView()
    {
        Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_NovcaView>();
    }

    internal TransakcijaView(int id, string podignuti_iznos, DateTime datum_Podizanja_Novca, string vreme_Podizanja_Novca)
    {
        Id = id;
        Podignuti_iznos = podignuti_iznos;
        Datum_Podizanja_Novca = datum_Podizanja_Novca;
        Vreme_Podizanja_Novca = vreme_Podizanja_Novca;
    }

}

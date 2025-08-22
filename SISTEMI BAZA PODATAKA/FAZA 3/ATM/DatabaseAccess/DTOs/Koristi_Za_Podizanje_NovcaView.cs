namespace DatabaseAccess.DTOs;

public class Koristi_Za_Podizanje_NovcaView
{
    public int Id { get; set; }
    public BankomatView? Bankomat { get; set; }
    public KarticaView? Kartica { get; set; }
    public TransakcijaView? Transakcija { get; set; }

    public Koristi_Za_Podizanje_NovcaView() { }

    internal Koristi_Za_Podizanje_NovcaView(int id, BankomatView bankomat, KarticaView kartica, TransakcijaView transakcija)
    {
        Bankomat = bankomat;
        Kartica = kartica;
        Transakcija = transakcija;
    }
}

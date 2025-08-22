namespace DatabaseAccess.DTOs;

public class BankomatKomentariView
{
    public int Id { get; protected set; }
    public string? Komentar { get; set; }


    //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
    public BankomatView? PripadaBankomatu { get; set; }

    public BankomatKomentariView() { }

    internal BankomatKomentariView(int id, string komentar, BankomatView bankomat)
    {
        this.Id = id;
        this.Komentar = komentar;
        this.PripadaBankomatu = bankomat;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
}

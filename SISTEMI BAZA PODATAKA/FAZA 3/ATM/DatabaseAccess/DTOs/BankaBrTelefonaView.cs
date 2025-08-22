namespace DatabaseAccess.DTOs;

public class BankaBrTelefonaView
{ 
    public int Id { get; protected set; }
    public string? BrTelefona { get; set; }

    //MAPIRANJE BANKA_BR_TEL-BANKA
    public BankaView? PripadaBanci { get; set; }

    public BankaBrTelefonaView() { }

    internal BankaBrTelefonaView(int id, string brTel, BankaView banka)
    {
        this.Id = id;
        this.BrTelefona = brTel;
        this.PripadaBanci = banka;
    }

    public void SetId(int id)
    {
        this.Id = id;
    }
}

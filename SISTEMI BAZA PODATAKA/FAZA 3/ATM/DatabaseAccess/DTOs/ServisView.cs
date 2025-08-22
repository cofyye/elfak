using DatabaseAccess.Entiteti;

namespace DatabaseAccess.DTOs;

public class ServisView
{
    public int Kod { get; protected set; }
    public string? Firma { get; set; }

    //MAPIRANJE SERVIS-BANKOMAT
    public BankomatView? ServisiraniBankomat { get; set; }

    //MAPIRANJE SERVIS-OTKLONJENE GRESKE
    public IList<OtklonjenaGreskaServisaView>? OtklonjeneGreske { get; set; }

    public ServisView()
    {
        //MAPIRANJE SERVIS-OTKLONJENE GRESKE
        OtklonjeneGreske = new List<OtklonjenaGreskaServisaView>();
    }

    internal ServisView(Servis s)
    {
        this.Kod = s.Kod;
        this.Firma = s.Firma;

        BankomatView b = DataProvider.VratiBankomat(s.ServisiraniBankomat!.Id);
        this.ServisiraniBankomat = b;
    }


    internal ServisView(int kod, string firma, BankomatView bankomat)
    {

        this.Kod = kod;
        this.Firma = firma;
        this.ServisiraniBankomat = bankomat;
    }

    public void SetKod(int kod)
    {
        this.Kod = kod;
    }
}

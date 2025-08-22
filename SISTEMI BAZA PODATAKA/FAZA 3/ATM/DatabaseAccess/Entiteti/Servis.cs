namespace DatabaseAccess.Entiteti;

internal class Servis
{
    internal protected virtual int Kod { get; protected set; }
    internal protected virtual string? Firma { get; set; }

    //MAPIRANJE SERVIS-BANKOMAT
    internal protected virtual Bankomat? ServisiraniBankomat { get; set; }

    //MAPIRANJE SERVIS-OTKLONJENA GRESKA
    internal protected virtual IList<OtklonjenaGreskaServisa> Otklonjene_Greske { get; set; }

    internal Servis()
    {
        Otklonjene_Greske = new List<OtklonjenaGreskaServisa>();
    }
}
namespace DatabaseAccess.Entiteti;

internal class OtklonjenaGreskaServisa
{
    internal protected virtual int Id { get; protected set; }
    internal protected virtual string? Otklonjena_greska { get; set; }


    //MAPIRANJE OTKLONJENA GRESKA-SERVIS
    internal protected virtual Servis? PripadaServisu { get; set; }
}

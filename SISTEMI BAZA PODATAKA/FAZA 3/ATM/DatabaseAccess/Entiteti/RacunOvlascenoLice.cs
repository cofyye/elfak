namespace DatabaseAccess.Entiteti;

internal class RacunOvlascenoLice
{
    internal protected virtual int Id { get; protected set; }
    internal protected virtual string? ImeOvlascenogLica { get; set; }


    //MAPIRANJE OVLASCENO LICE-RACUN
    internal protected virtual Racun? PripadaRacunu { get; set; }

}

namespace DatabaseAccess.Entiteti;

internal class KomentarKlijenta
{
    internal protected virtual int Id { get; protected set; }
    internal protected virtual string? Komentar { get; set; }


    //MAPIRANJE OVLASCENO KOMENTAR-KLIJENT
    internal protected virtual Klijent? PripadaKlijentu { get; set; }
}

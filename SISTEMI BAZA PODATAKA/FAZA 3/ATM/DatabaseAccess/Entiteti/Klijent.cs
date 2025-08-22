namespace DatabaseAccess.Entiteti;

internal abstract class Klijent
{
    internal protected virtual int Id { get; set; }
    internal protected virtual string? Naziv { get; set; }
    internal protected virtual string? Br_tel { get; set; }
    internal protected virtual string? Email { get; set; }
    internal protected virtual string? Adresa { get; set; }

    //MAPIRANJE KLIJENT-RACUN
    internal protected virtual IList<Racun> Racuni { get; set; }

    //MAPIRANJE OVLASCENO KLIJENT-KOMENTAR
    internal protected virtual IList<KomentarKlijenta> Komentari { get; set; }

    internal Klijent()
    {
        //MAPIRANJE KLIJENT-RACUN
        Racuni = new List<Racun>();
        Komentari = new List<KomentarKlijenta>();
    }
}

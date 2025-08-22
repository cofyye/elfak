namespace DatabaseAccess.Entiteti;

internal class Transakcija
{
    internal protected virtual  int Id { get; protected set; }
    internal protected virtual string? Podignuti_iznos { get; set; }
    internal protected virtual DateTime Datum_Podizanja_Novca { get; set; }
    internal protected virtual string? Vreme_Podizanja_Novca { get; set; }

    //TERNARNA
    internal protected virtual IList<Koristi_Za_Podizanje_Novca> Koristi_Za_Podizanje_Novca { get; set; }

    internal Transakcija()
    {
        Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_Novca>();
    }
}

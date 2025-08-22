namespace DatabaseAccess.Entiteti;

internal class Koristi_Za_Podizanje_Novca
{
    internal protected virtual int Id { get; set; }
    internal protected virtual Bankomat? Bankomat { get; set; }
    internal protected virtual Kartica? Kartica { get; set; }
    internal protected virtual Transakcija? Transakcija { get; set; }
}

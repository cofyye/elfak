namespace DatabaseAccess.Entiteti;

internal abstract class Kartica
{
    internal protected virtual int Id { get; protected set; }
    internal protected virtual DateTime Datum_izdavanje { get; set; }
    internal protected virtual DateTime Datum_isteka { get; set; }
    internal protected virtual string? Dnevni_limit { get; set; }
    internal protected virtual string? Tip { get; set; }
    internal protected virtual string? Max_iznos_zaduzenja { get; set; }
    internal protected virtual DateTime Max_datum_vracanja_duga { get; set; }

    //MAPIRANJE KARTICA-RACUN
    internal protected virtual Racun? Odgovara { get; set; }

    //TERNARNA
    internal protected virtual IList<Koristi_Za_Podizanje_Novca> Koristi_Za_Podizanje_Novca { get; set; }

    internal Kartica()
    {
        Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_Novca>();
    }

}

internal class DebitnaKartica : Kartica
{

}

internal class KreditnaKartica : Kartica
{
    internal protected override string? Max_iznos_zaduzenja { get; set; }

    internal protected override DateTime Max_datum_vracanja_duga { get; set; }
}

namespace DatabaseAccess.Entiteti;

internal abstract class Racun
{
    internal protected virtual int Br_racuna { get; protected set; }
    internal protected virtual DateTime Datum_otvaranja { get; set; }
    internal protected virtual string Tekuci_saldo { get; set; }
    internal protected virtual string Tip {  get; set; }
    internal protected virtual string Valuta { get; set; }

    //MAPIRANJE RACUN-BANKA
    internal protected virtual Banka JePovezan { get; set; }

    //MAPIRANJE RACUN-KLIJENT
    internal protected virtual Klijent Koristi { get; set; }

    //MAPIRANJE RACUN-KARTICA
    internal protected virtual IList<Kartica> Kartice { get; set; }
    internal protected virtual IList<RacunOvlascenoLice> OvlascenaLica { get; set; }

    internal Racun()
    {
        //MAPIRANJE RACUN-KARTICA
        Kartice = new List<Kartica>();
        OvlascenaLica = new List<RacunOvlascenoLice>();
    }

}

internal class DevizniRacun : Racun 
{
    internal protected override string? Valuta { get; set; }//mozda ne treba protected
}
internal class DinarskiRacun : Racun {
   
}

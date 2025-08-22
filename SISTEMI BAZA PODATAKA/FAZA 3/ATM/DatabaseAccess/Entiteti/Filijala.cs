
namespace DatabaseAccess.Entiteti;

internal class Filijala
{

    internal protected virtual int Rbr_filijale { get; protected set; }


    internal protected virtual string Adresa { get; set; }


    internal protected virtual string? Br_telefona { get; set; }


    internal protected virtual string? Radno_vreme { get; set; }

    //MAPIRANJE FILIJALA-BANKOMAT

    internal protected virtual IList<Bankomat> Bankomati { get; set; }


    //MAPIRANJE FILIJALA-BANKA

    internal protected virtual Banka? PripadaBanci { get; set; }


    internal Filijala()
    {
        //MAPIRANJE FILIJALA-BANKA
        Bankomati = new List<Bankomat>();
    }
}

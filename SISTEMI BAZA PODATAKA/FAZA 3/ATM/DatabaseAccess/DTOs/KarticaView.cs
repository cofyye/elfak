using DatabaseAccess.Entiteti;

namespace DatabaseAccess.DTOs;

public class KarticaView
{
    public int Id { get; protected set; }
    public DateTime Datum_izdavanje { get; set; }
    public DateTime Datum_isteka { get; set; }
    public string? Dnevni_limit { get; set; }
    public string? Tip { get; set; }
    public string? Max_iznos_zaduzenja { get; set; }
    public DateTime Max_datum_vracanja_duga { get; set; }

    //MAPIRANJE KARTICA-RACUN
    public  RacunView? Odgovara { get; set; }

    //TERNARNA
    public IList<Koristi_Za_Podizanje_NovcaView>? Koristi_Za_Podizanje_Novca { get; set; }

    public void SetRbr(int id)
    {
        Id = id;
    }

    public KarticaView()
    {
        Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_NovcaView>();
    }
    internal KarticaView(int id, DateTime datum_izdavanja, DateTime datum_isteka, string dnevni_limit, string tip, string max_iznos_zaduzenja, DateTime max_datum_vracanja_duga, RacunView racun)
    {
        this.Id = id;
        this.Datum_izdavanje = datum_izdavanja;
        this.Datum_isteka = datum_isteka;
        this.Dnevni_limit = dnevni_limit;
        this.Tip = tip;
        this.Max_iznos_zaduzenja = max_iznos_zaduzenja;
        this.Max_datum_vracanja_duga = max_datum_vracanja_duga;
        this.Odgovara = racun;
    }

    internal KarticaView(Kartica k)
    {
        this.Id = k.Id;
        this.Datum_izdavanje = k.Datum_izdavanje;
        this.Datum_isteka = k.Datum_isteka;
        this.Dnevni_limit = k.Dnevni_limit;
        this.Tip = k.Tip;
        this.Max_iznos_zaduzenja = k.Max_iznos_zaduzenja;
        this.Max_datum_vracanja_duga = k.Max_datum_vracanja_duga;
        //this.Odgovara = k.Racun;
    }

    internal KarticaView(Kartica k,RacunView r)
    {
        this.Id = k.Id;
        this.Datum_izdavanje = k.Datum_izdavanje;
        this.Datum_isteka = k.Datum_isteka;
        this.Dnevni_limit = k.Dnevni_limit;
        this.Tip = k.Tip;
        this.Max_iznos_zaduzenja = k.Max_iznos_zaduzenja;
        this.Max_datum_vracanja_duga = k.Max_datum_vracanja_duga;
        this.Odgovara = r;
    }



}

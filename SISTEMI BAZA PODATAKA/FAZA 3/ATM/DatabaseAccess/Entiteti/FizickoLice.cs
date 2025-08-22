namespace DatabaseAccess.Entiteti;

internal class FizickoLice : Klijent
{ //moguce da treba da budu atributi internal protected virtual
    public virtual string? JMBG { get; set; }

    public virtual DateTime Datum_rodjenja { get; set; }

    public virtual string? LIme { get; set; }

    public virtual string? Ime_roditelja { get; set; }

    public virtual string? Prezime { get; set; }

    public virtual string? Br_licne_karte { get; set; }

    public virtual string? Mesto_izdavanja { get; set; }
}

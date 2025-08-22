namespace DatabaseAccess.Entiteti;

internal class BankaBrTelefona
{
    internal protected virtual int Id { get; protected set; }

    internal protected virtual string? BrTelefona { get; set; }


    //MAPIRANJE BANKA_BR_TEL-BANKA
    internal protected virtual Banka? PripadaBanci { get; set; }
}

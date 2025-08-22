namespace DatabaseAccess.Entiteti;

internal class BankomatKomentari
{
    internal protected virtual int Id { get; protected set; }

    internal protected virtual string? Komentar { get; set; }


    //MAPIRANJE KOMENTAR-BANKOMAT
    internal protected virtual Bankomat PripadaBankomatu { get; set; }
}

namespace DatabaseAccess.Entiteti
{
    internal class Banka
    {
        internal protected virtual int Id { get; protected set; }

        internal protected virtual string? Ime { get; set; }

        internal protected virtual string? Email { get; set; }

        internal protected virtual string? Web_adresa { get; set; }

        internal protected virtual string? Adresa_centrale { get; set; }

        //MAPIRANJE BANKA-FILIJA
        internal protected virtual IList<Filijala> Filijala { get; set; }


        //MAPIRANJE BANKA-BANKA_BR_TEL
        internal protected virtual IList<BankaBrTelefona> BrojeviTelefona { get; set; }


        //MAIRANJE BANKA-RACUN
        internal protected virtual IList<Racun> Racuni { get; set; }

        internal Banka()
        {
            //MAPIRANJE BANKA-FILIJALA
            Filijala = new List<Filijala>();

            //MAPIRANJE BANKA-BANKA_BR_TEL
            BrojeviTelefona = new List<BankaBrTelefona>();

            //MAPIRANJE BANKA-RACUN
            Racuni = new List<Racun>();
        }
    }
}

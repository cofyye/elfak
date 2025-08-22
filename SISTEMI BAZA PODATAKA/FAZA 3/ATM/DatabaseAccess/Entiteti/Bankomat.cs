namespace DatabaseAccess.Entiteti
{
    internal class Bankomat
    {
        
        internal protected virtual int Id { get; protected set; }

        internal protected virtual string? Lokacija { get; set; }

        internal protected virtual string? Proizvodjac { get; set; }

        internal protected virtual string? Status { get; set; }

        internal protected virtual DateTime Datum_Poslednjeg_Servisa { get; set; }


        //MAPIRANJE BANKOMAT-FILIJALA
        internal protected virtual Filijala? InstaliranUFilijali { get; set; }

        //MAPIRANJE BANKOMAT-KOMENTARI BANKOMATA
        internal protected virtual IList<BankomatKomentari> Komentari { get; set; }

        //MAPIRANJE BANKOMAT- SERVIS BANKOMATA
        internal protected virtual IList<Servis> Servisi { get; set; }

        //TERNARNA
        internal protected virtual IList<Koristi_Za_Podizanje_Novca> Koristi_Za_Podizanje_Novca { get; set; }

        internal Bankomat()
        {
            Komentari = new List<BankomatKomentari>();
            Servisi = new List<Servis>();

            //TERNARNA
            Koristi_Za_Podizanje_Novca = new List<Koristi_Za_Podizanje_Novca>();
        }

        

    }
}
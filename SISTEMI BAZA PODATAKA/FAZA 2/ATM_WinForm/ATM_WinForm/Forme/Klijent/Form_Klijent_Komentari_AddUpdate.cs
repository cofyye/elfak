using ATM_WinForm.Entiteti;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Klijent
{
    public partial class Form_Klijent_Komentari_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int klijentId = -1;
        private readonly ATM_WinForm.DTOs.KomentarKlijentaBasic komentar = null;
        public Form_Klijent_Komentari_AddUpdate(string type, ATM_WinForm.DTOs.KomentarKlijentaBasic komentar, int klijentId)
        {
            InitializeComponent();

            this.type = type;
            this.klijentId = klijentId;
            this.komentar = komentar;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaKomentaraBtn.Text = "Dodaj Komentar";

                        this.Text = "ATM - Dodaj Komentar";

                        break;
                    }
                case "update":
                    {
                        AkcijaKomentaraBtn.Text = "Izmeni Komentar";

                        AddUpdateTxt.Text = komentar.Komentar;

                        this.Text = "ATM - Izmeni Komentar";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaKomentaraBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddUpdateTxt.Text == ""
                   )
                {
                    MessageBox.Show("Polje ne sme biti prazno!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.KomentarKlijentaBasic k = new DTOs.KomentarKlijentaBasic
                            {
                                Komentar = AddUpdateTxt.Text,
                                PripadaKlijentu = DTOManager.VratiKlijenta(klijentId)
                            };

                            DTOManager.DodajKomentarKlijenta(k);

                            MessageBox.Show("Uspesno ste dodali novi komentar!");

                            break;
                        }
                    case "update":
                        {
                            this.komentar.Komentar = AddUpdateTxt.Text;
                            this.komentar.PripadaKlijentu = DTOManager.VratiKlijenta(klijentId);
                            


                            DTOManager.IzmeniKomentarKlijenta(this.komentar);

                            MessageBox.Show("Uspesno ste izmenili komentar!");

                            break;
                        }
                    default: break;
                }
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.ToString());
            }

            this.Close();
        }
    }
}

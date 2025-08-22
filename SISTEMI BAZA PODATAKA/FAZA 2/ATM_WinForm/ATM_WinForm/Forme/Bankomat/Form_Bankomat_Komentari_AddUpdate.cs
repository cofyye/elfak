using System;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_Bankomat_Komentari_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int bankomatId = -1;
        private readonly ATM_WinForm.DTOs.BankomatKomentariBasic komentar = null;
        public Form_Bankomat_Komentari_AddUpdate(string type, ATM_WinForm.DTOs.BankomatKomentariBasic komentar, int bankomatId)
        {
            InitializeComponent();

            this.type = type;
            this.bankomatId = bankomatId;
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
                        AkcijaKomentaraBtn.Text = "Izmeni Bankomat";

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
                            DTOs.BankomatKomentariBasic k = new DTOs.BankomatKomentariBasic
                            {
                                Komentar = AddUpdateTxt.Text,
                                PripadaBankomatu = DTOManager.VratiBankomat(bankomatId)
                            };

                            DTOManager.DodajKomentarBankomata(k);

                            MessageBox.Show("Uspesno ste dodali novi komentar!");

                            break;
                        }
                    case "update":
                        {
                            this.komentar.Komentar = AddUpdateTxt.Text;
                            this.komentar.PripadaBankomatu = DTOManager.VratiBankomat(bankomatId);

                            DTOManager.IzmeniKomentarBankomat(this.komentar);

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

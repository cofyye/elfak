using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ATM_WinForm
{
    public partial class Form_Banka_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly ATM_WinForm.DTOs.BankaBasic banka = null;

        public Form_Banka_AddUpdate(string type, ATM_WinForm.DTOs.BankaBasic banka)
        {
            InitializeComponent();
               
            this.type = type;
            this.banka = banka;

            switch(this.type)
            {
                case "add":
                    {
                        AkcijaBankeBtn.Text = "Dodaj Banku";

                        this.Text = "ATM - Dodaj Banku";

                        break;
                    }
                case "update":
                    {
                        AkcijaBankeBtn.Text = "Izmeni Banku";

                        ImeTxtBx.Text = banka.Ime;
                        EmailTxtBx.Text = banka.Email;
                        WebAdresaTxtBx.Text = banka.Web_adresa;
                        AdresaCentraleTxtBx.Text = banka.Adresa_centrale;

                        this.Text = "ATM - Izmeni Banku";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaBankeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(ImeTxtBx.Text == "" || 
                   EmailTxtBx.Text == "" || 
                   WebAdresaTxtBx.Text == "" || 
                   AdresaCentraleTxtBx.Text == ""
                   )
                {
                    MessageBox.Show("Polja ne smeju biti prazna!");
                    return;
                }

                Match emailMatch = Regex.Match(EmailTxtBx.Text, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", RegexOptions.IgnoreCase);

                if(!emailMatch.Success)
                {
                    MessageBox.Show("Unesite validnu email adresu!");
                    return;
                }

                if (this.type == "add" || this.banka.Email != EmailTxtBx.Text)
                {
                    if (DTOManager.ProveraEmailBanke(EmailTxtBx.Text))
                    {
                        MessageBox.Show("Ova email adresa vec postoji u bazi!");
                        return;
                    }
                }

                if (this.type == "add" || this.banka.Web_adresa != WebAdresaTxtBx.Text)
                {
                    if (DTOManager.ProveraWebAdreseBanke(WebAdresaTxtBx.Text))
                    {
                        MessageBox.Show("Ova web adresa vec postoji u bazi!");
                        return;
                    }
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.BankaBasic banka = new DTOs.BankaBasic
                            {
                                Ime = ImeTxtBx.Text,
                                Email = EmailTxtBx.Text,
                                Web_adresa = WebAdresaTxtBx.Text,
                                Adresa_centrale = AdresaCentraleTxtBx.Text
                            };

                            DTOManager.DodajBanku(banka);

                            MessageBox.Show("Uspesno ste dodali banku!");

                            break;
                        }
                    case "update":
                        {
                            this.banka.Ime = ImeTxtBx.Text;
                            this.banka.Email = EmailTxtBx.Text;
                            this.banka.Web_adresa = WebAdresaTxtBx.Text;
                            this.banka.Adresa_centrale = AdresaCentraleTxtBx.Text;

                            DTOManager.IzmeniBanku(banka);

                            MessageBox.Show("Uspesno ste izmenili banku!");

                            break;
                        }
                    default: break;
                }

                this.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }
    }
}

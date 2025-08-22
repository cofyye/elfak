using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_OtklonjeneGreske_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int servisKod = -1;
        private readonly ATM_WinForm.DTOs.OtklonjenaGreskaServisaBasic greska = null;
        public Form_OtklonjeneGreske_AddUpdate(string type, ATM_WinForm.DTOs.OtklonjenaGreskaServisaBasic greska, int servisKod)
        {
            InitializeComponent();

            this.type = type;
            this.servisKod = servisKod;
            this.greska = greska;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaGreskaBtn.Text = "Dodaj Otklonjenu Gresku";

                        this.Text = "ATM - Dodaj otklonjenu gresku";

                        break;
                    }
                case "update":
                    {
                        AkcijaGreskaBtn.Text = "Izmeni Otklonjenu Gresku";

                        AddUpdateTxt.Text = greska.Otklonjena_greska;

                        this.Text = "ATM - Izmeni otklonjenu gresku";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaGreskaBtn_Click(object sender, EventArgs e)
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
                            DTOs.OtklonjenaGreskaServisaBasic g = new DTOs.OtklonjenaGreskaServisaBasic
                            {
                                Otklonjena_greska = AddUpdateTxt.Text,
                                PripadaServisu = DTOManager.VratiServis(servisKod)
                            };

                            DTOManager.DodajOtklonjenuGreskuServisa(g);

                            MessageBox.Show("Uspesno ste dodali novu otklonjenu gresku u arhivu!");

                            break;
                        }
                    case "update":
                        {
                            this.greska.Otklonjena_greska = AddUpdateTxt.Text;
                            this.greska.PripadaServisu = DTOManager.VratiServis(servisKod);

                            DTOManager.IzmeniOtklonjenuGreskuServisa(this.greska);

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

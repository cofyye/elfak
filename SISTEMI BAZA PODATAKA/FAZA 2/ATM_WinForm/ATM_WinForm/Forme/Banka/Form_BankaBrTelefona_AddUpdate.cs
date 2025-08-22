using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Banka
{
    public partial class Form_BankaBrTelefona_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int bankaId = -1;
        private readonly ATM_WinForm.DTOs.BankaBrTelefonaBasic brTelefona = null;
        public Form_BankaBrTelefona_AddUpdate(string type, ATM_WinForm.DTOs.BankaBrTelefonaBasic brTelefona, int bankaId)
        {
            InitializeComponent();

            this.type = type;
            this.brTelefona = brTelefona;
            this.bankaId = bankaId;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaBrTelefonaBtn.Text = "Dodaj Broj Telefona";

                        this.Text = "ATM - Dodaj Broj Telefona";

                        labelaAddEditLBL.Text = "Unesi novi broj telefona:";

                        break;
                    }
                case "update":
                    {
                        AkcijaBrTelefonaBtn.Text = "Izmeni Broj Telefona";

                        labelaAddEditLBL.Text = "Izmeni broj telefona:";
                        AddUpdateTxt.Text = brTelefona.BrTelefona;

                        this.Text = "ATM - Izmeni Broj Telefona";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaBrTelefonaBtn_Click(object sender, EventArgs e)
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
                            DTOs.BankaBrTelefonaBasic brTelefona = new DTOs.BankaBrTelefonaBasic
                            {
                                BrTelefona = AddUpdateTxt.Text,
                                PripadaBanci = DTOManager.VratiBanku(bankaId)
                            };

                            DTOManager.DodajBrTelefona(brTelefona);

                            MessageBox.Show("Uspesno ste dodali novi broj telefona!");

                            break;
                        }
                    case "update":
                        {
                            this.brTelefona.BrTelefona = AddUpdateTxt.Text;
                            this.brTelefona.PripadaBanci = DTOManager.VratiBanku(bankaId);

                            DTOManager.IzmeniBrTelefona(this.brTelefona);

                            MessageBox.Show("Uspesno ste izmenili broj telefona!");

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

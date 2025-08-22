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
    public partial class Form_Bankomat_Servisi_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int bankomatId = -1;
        private readonly ATM_WinForm.DTOs.ServisBasic servis = null;
        public Form_Bankomat_Servisi_AddUpdate(string type, ATM_WinForm.DTOs.ServisBasic servis, int bankomatId)
        {
            InitializeComponent();

            this.type = type;
            this.bankomatId = bankomatId;
            this.servis = servis;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaServisaBtn.Text = "Dodaj Servis";

                        this.Text = "ATM - Dodaj Servis";

                        break;
                    }
                case "update":
                    {
                        AkcijaServisaBtn.Text = "Izmeni Servis";

                        AddUpdateTxt.Text = servis.Firma;

                        this.Text = "ATM - Izmeni Servis";

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaServisaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AddUpdateTxt.Text == ""
                   )
                {
                    MessageBox.Show("Unesite naziv firme koja vrsi servis!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.ServisBasic s = new DTOs.ServisBasic
                            {
                                Firma = AddUpdateTxt.Text,
                                ServisiraniBankomat = DTOManager.VratiBankomat(bankomatId)
                            };

                            DTOManager.DodajServisBankomata(s);

                            MessageBox.Show("Uspesno ste dodali novi servis!");

                            break;
                        }
                    case "update":
                        {
                            this.servis.Firma = AddUpdateTxt.Text;
                            this.servis.ServisiraniBankomat = DTOManager.VratiBankomat(bankomatId);

                            DTOManager.IzmeniServisBankomat(this.servis);

                            MessageBox.Show("Uspesno ste izmenili servis!");

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

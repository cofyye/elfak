using ATM_WinForm.Klase;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Filijala
{
    public partial class Form_Filijala_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int bankaId = -1;
        private readonly ATM_WinForm.DTOs.FilijalaBasic filijala = null;
        public Form_Filijala_AddUpdate(string type, ATM_WinForm.DTOs.FilijalaBasic filijala, int bankaId)
        {
            InitializeComponent();

            BankaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            this.type = type;
            this.filijala = filijala;
            this.bankaId = bankaId;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaFilijaleBtn.Text = "Dodaj Filijalu";

                        this.Text = "ATM - Dodaj Filijalu";

                        break;
                    }
                case "update":
                    {
                        AkcijaFilijaleBtn.Text = "Izmeni Filijalu";

                        AdresaTxtBx.Text = filijala.Adresa;
                        BrTelefonaTxtBx.Text = filijala.Br_telefona;
                        RadnoVremeTxtBx.Text = filijala.Radno_vreme;

                        this.Text = "ATM - Izmeni Filijalu";

                        break;
                    }
                default: break;
            }

        }

        private void Form_Filijala_AddUpdate_Load(object sender, EventArgs e)
        {
            BankaComboBox.DisplayMember = "Text";
            BankaComboBox.ValueMember = "ID";

            List<ComboItem> lista = new List<ComboItem>();

            if (this.bankaId == -1)
            {
                var banke = DTOManager.VratiSveBanke();

                foreach (var banka in banke)
                {
                    lista.Add(new ComboItem { ID = banka.Id, Text = banka.Ime });
                }
            } 
            else
            {
                var banka = DTOManager.VratiBanku(this.bankaId);

                lista.Add(new ComboItem { ID = banka.Id, Text = banka.Ime });
            }

            BankaComboBox.DataSource = lista;

            int index = -1;
            if (this.bankaId == -1)
            {
                if (this.type == "update")
                {
                    index = lista.FindIndex(p => p.ID == filijala.PripadaBanci.Id);
                }
            }
            BankaComboBox.SelectedIndex = index != -1 ? index : 0;
        }

        private void AkcijaFilijaleBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (AdresaTxtBx.Text == "" ||
                   BrTelefonaTxtBx.Text == "" ||
                   RadnoVremeTxtBx.Text == ""
                   )
                {
                    MessageBox.Show("Polja ne smeju biti prazna!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.FilijalaBasic filijala = new DTOs.FilijalaBasic
                            {
                                Adresa = AdresaTxtBx.Text,
                                Br_telefona = BrTelefonaTxtBx.Text,
                                Radno_vreme = RadnoVremeTxtBx.Text,
                                PripadaBanci = DTOManager.VratiBanku((int)BankaComboBox.SelectedValue)
                            };

                            DTOManager.DodajFilijalu(filijala);

                            MessageBox.Show("Uspesno ste dodali filijalu!");

                            break;
                        }
                    case "update":
                        {
                            this.filijala.Adresa = AdresaTxtBx.Text;
                            this.filijala.Radno_vreme = RadnoVremeTxtBx.Text;
                            this.filijala.Br_telefona = BrTelefonaTxtBx.Text;
                            this.filijala.PripadaBanci = DTOManager.VratiBanku((int)BankaComboBox.SelectedValue);

                            DTOManager.IzmeniFilijalu(this.filijala);

                            MessageBox.Show("Uspesno ste izmenili filijalu!");

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

        private void BrTelLbl_Click(object sender, EventArgs e)
        {

        }
    }
}

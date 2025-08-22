using ATM_WinForm.Entiteti;
using ATM_WinForm.Klase;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_Bankomat_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int filijalaId = -1;
        private readonly ATM_WinForm.DTOs.BankomatBasic bankomat = null;
        public Form_Bankomat_AddUpdate(string type, ATM_WinForm.DTOs.BankomatBasic bankomat, int filijalaId)
        {
            InitializeComponent();

            FilijalaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            StatusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            StatusComboBox.SelectedIndex = 0;

            this.type = type;
            this.bankomat = bankomat;
            this.filijalaId = filijalaId;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaBankomataBtn.Text = "Dodaj Bankomat";

                        this.Text = "ATM - Dodaj Bankomat";

                        break;
                    }
                case "update":
                    {
                        AkcijaBankomataBtn.Text = "Izmeni Bankomat";

                        LokacijaTxtBx.Text = bankomat.Lokacija;
                        ProizvodjacTxtBx.Text = bankomat.Proizvodjac;
                        DateTimePicker.Text = bankomat.Datum_Poslednjeg_Servisa.ToString();
                        StatusComboBox.SelectedItem = bankomat.Status;

                        this.Text = "ATM - Izmeni Bankomat";

                        break;
                    }
                default: break;
            }
        }

        private void Form_Bankomat_AddUpdate_Load(object sender, EventArgs e)
        {
            FilijalaComboBox.DisplayMember = "Text";
            FilijalaComboBox.ValueMember = "ID";

            List<ComboItem> lista = new List<ComboItem>();

            if (this.filijalaId == -1)
            {
                var filijale = DTOManager.VratiSveFilijale();

                foreach (var filijala in filijale)
                {
                    lista.Add(new ComboItem { ID = filijala.Rbr_filijale, Text = filijala.Adresa });
                }
            }
            else
            {
                var filijala = DTOManager.VratiFilijalu(this.filijalaId);

                lista.Add(new ComboItem { ID = filijala.Rbr_filijale, Text = filijala.Adresa });
            }

            FilijalaComboBox.DataSource = lista;

            int index = -1;
            if (this.filijalaId == -1)
            {
                if (this.type == "update")
                {
                    index = lista.FindIndex(p => p.ID == bankomat.InstaliranUFilijali.Rbr_filijale);
                }
            }
            FilijalaComboBox.SelectedIndex = index != -1 ? index : 0;
        }

        private void AkcijaBankomataBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (LokacijaTxtBx.Text == "" ||
                   ProizvodjacTxtBx.Text == ""
                   )
                {
                    MessageBox.Show("Polja ne smeju biti prazna!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            DTOs.BankomatBasic bankomat = new DTOs.BankomatBasic
                            {
                                Lokacija = LokacijaTxtBx.Text,
                                Proizvodjac = ProizvodjacTxtBx.Text,
                                Status = StatusComboBox.SelectedItem.ToString(),
                                Datum_Poslednjeg_Servisa = DateTimePicker.Value,
                                InstaliranUFilijali = DTOManager.VratiFilijalu((int)FilijalaComboBox.SelectedValue)
                            };
                            
                            DTOManager.DodajBankomat(bankomat);

                            MessageBox.Show("Uspesno ste dodali bankomat!");

                            break;
                        }
                    case "update":
                        {
                            this.bankomat.Lokacija = LokacijaTxtBx.Text;
                            this.bankomat.Proizvodjac = ProizvodjacTxtBx.Text;
                            this.bankomat.Datum_Poslednjeg_Servisa = DateTimePicker.Value;
                            this.bankomat.Status = StatusComboBox.SelectedItem.ToString();
                            this.bankomat.InstaliranUFilijali = DTOManager.VratiFilijalu((int)FilijalaComboBox.SelectedValue);

                            DTOManager.IzmeniBankomat(this.bankomat);

                            MessageBox.Show("Uspesno ste izmenili bankomat!");

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

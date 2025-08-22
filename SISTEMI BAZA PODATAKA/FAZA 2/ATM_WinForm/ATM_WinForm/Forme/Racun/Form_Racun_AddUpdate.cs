using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATM_WinForm.Klase;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Racun
{
    public partial class Form_Racun_AddUpdate : Form
    {
        private readonly string type = "";
        private readonly int klijentId = -1;
        private readonly ATM_WinForm.DTOs.RacunBasic racun = null;
        public Form_Racun_AddUpdate(string type, ATM_WinForm.DTOs.RacunBasic racun, int klijentId)
        {
            InitializeComponent();

            this.type = type;
            this.racun = racun;
            this.klijentId = klijentId;

            radioButton1.Checked = true;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaRacunaBtn.Text = "Dodaj Racun";

                        this.Text = "ATM - Dodaj Racun";
                        this.AddUpdateNaslovLbl.Text = "Unesite sve potrebne informacije za dodavanje racuna:";

                        this.BankeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        this.BankeComboBox.DisplayMember = "Text";
                        this.BankeComboBox.ValueMember = "ID";
                        List<ComboItem> lista = new List<ComboItem>();

                        

                        if (this.klijentId != -1)
                        {
                            var banke = DTOManager.VratiSveBanke();

                            foreach (var banka in banke)
                            {
                                lista.Add(new ComboItem { ID = banka.Id, Text = banka.Ime });
                            }
                        }

                        BankeComboBox.DataSource = lista;

                        break;
                    }
                case "update":
                    {
                        AkcijaRacunaBtn.Text = "Azuriraj Racun";

                        this.Text = "ATM - Azuriraj Racun";
                        this.AddUpdateNaslovLbl.Text = "Unesite sve potrebne informacije za azuriranje racuna:";

                        DatumOtvaranjaRacunaDateTimePicker.Text = racun.Datum_otvaranja.ToString();


                        if (racun.Tip == "devizni")
                        {
                            radioButton1.Checked = true;
                            ValutaTxtBx.Enabled = true;
                            ValutaTxtBx.Text = racun.Valuta;
                            
                        }
                        else if(racun.Tip == "dinarski")
                        {
                            radioButton2.Checked = true;
                            ValutaTxtBx.Enabled = false;
                        }

                        radioButton1.Enabled = false;
                        radioButton2.Enabled = false;

                        TekuciSaldoTxtBx.Text = racun.Tekuci_saldo;

                        this.BankeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        this.BankeComboBox.DisplayMember = "Text";
                        this.BankeComboBox.ValueMember = "ID";
                        List<ComboItem> lista = new List<ComboItem>();
                        var banka = DTOManager.VratiBanku(racun.JePovezan.Id);
                        lista.Add(new ComboItem { ID = banka.Id, Text = banka.Ime });
                        BankeComboBox.DataSource = lista;
                        BankeComboBox.SelectedIndex = 0;
                        BankeComboBox.Enabled = false;

                        break;
                    }
                default: break;
            }
        }

        private void AkcijaRacunaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DatumOtvaranjaRacunaDateTimePicker.Text == "" ||
                   TekuciSaldoTxtBx.Text == "" ||
                   BankeComboBox.Text == ""
                   )
                {
                    MessageBox.Show("Polja ne smeju biti prazna!");
                    return;
                }

                switch (this.type)
                {
                    case "add":
                        {
                            string tip = "";

                            if (radioButton1.Checked)
                            {
                                tip = "devizni";
                            }

                            if (radioButton2.Checked)
                            {
                                tip = "dinarski";
                            }


                            DTOs.RacunBasic racun = new DTOs.RacunBasic
                            {
                                Datum_otvaranja = DatumOtvaranjaRacunaDateTimePicker.Value,
                                Tekuci_saldo = TekuciSaldoTxtBx.Text,
                                Tip = tip,
                                JePovezan = DTOManager.VratiBanku((int)BankeComboBox.SelectedValue),
                                Valuta = ValutaTxtBx.Text,
                                Koristi = DTOManager.VratiKlijenta(klijentId),
                            };

                            DTOManager.DodajRacun(racun);

                            MessageBox.Show("Uspesno ste dodali racun!");

                            break;
                        }
                    case "update":
                        {
                            this.racun.Datum_otvaranja = DatumOtvaranjaRacunaDateTimePicker.Value;
                            this.racun.Tekuci_saldo = TekuciSaldoTxtBx.Text;
                            this.racun.JePovezan = DTOManager.VratiBanku((int)BankeComboBox.SelectedValue);
                            this.racun.Koristi = DTOManager.VratiKlijenta(racun.Koristi.Id);

                            if (radioButton1.Checked)
                            {
                                this.racun.Tip = "devizni";
                                this.racun.Valuta = ValutaTxtBx.Text;
                            }

                            if (radioButton2.Checked)
                            {
                                this.racun.Tip = "dinarski";
                            }
                            DTOManager.IzmeniRacun(this.racun);

                            MessageBox.Show("Uspesno ste izmenili racun!");

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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                ValutaTxtBx.Enabled = false;
            }
            else
            {
                ValutaTxtBx.Enabled = true;
            }
        }
    }
}

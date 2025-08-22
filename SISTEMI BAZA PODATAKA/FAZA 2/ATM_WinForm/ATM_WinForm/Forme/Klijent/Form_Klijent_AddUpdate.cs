using ATM_WinForm.Entiteti;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Klijent
{
    public partial class Form_Klijent_AddUpdate : Form
    {

        private readonly string type = "";
        private readonly ATM_WinForm.DTOs.FizickoLiceBasic fizickoLice = null;
        private readonly ATM_WinForm.DTOs.PravnoLiceBasic pravnoLice = null;

        public Form_Klijent_AddUpdate(string type, ATM_WinForm.DTOs.FizickoLiceBasic fizickoLice,ATM_WinForm.DTOs.PravnoLiceBasic pravnoLice)
        {
            InitializeComponent();

            this.type = type;
            this.fizickoLice = fizickoLice;
            this.pravnoLice = pravnoLice;

            switch (this.type)
            {
                case "add":
                    {
                        AkcijaKlijentaBtn.Text = "Dodaj Klijenta";

                        this.Text = "ATM - Dodaj Klijenta";

                        KlijentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                        KlijentComboBox.SelectedIndex = 0;

                        break;
                    }
                case "update":
                    {
                        AkcijaKlijentaBtn.Text = "Izmeni Klijenta";


                        if(pravnoLice != null)//pravnoLice je
                        {
                            NazivTxtBx.Text = pravnoLice.Naziv;
                            PoreskiIdTxtBx.Text = pravnoLice.Poreski_id.ToString();
                            BrTelTxtBx.Text = pravnoLice.Br_tel;
                            EmailTxtBx.Text = pravnoLice.Email;
                            AdresaTxtBx.Text = pravnoLice.Adresa;

                            KlijentComboBox.SelectedIndex = 0;
                            KlijentComboBox.Enabled = false;
                        }
                        else if(fizickoLice != null)//fizicko lice je 
                        {
                            JMBGTxtBx.Text = fizickoLice.JMBG;
                            DatumRodjenjaDateTimePicker.Value = fizickoLice.Datum_rodjenja;
                            LimeTxtBx.Text = fizickoLice.LIme;
                            ImeRoditeljaTxtBx.Text = fizickoLice.Ime_roditelja;
                            PrezimeTxtBx.Text = fizickoLice.Prezime;
                            BrLicneKarteTxtBx.Text = fizickoLice.Br_licne_karte;
                            MestoIzdavanjaTxtBx.Text = fizickoLice.Mesto_izdavanja;

                            BrTelTxtBx.Text = fizickoLice.Br_tel;
                            EmailTxtBx.Text = fizickoLice.Email;
                            AdresaTxtBx.Text = fizickoLice.Adresa;

                            KlijentComboBox.SelectedIndex = 1;
                            KlijentComboBox.Enabled = false;

                        }

                        this.Text = "ATM - Izmeni Klijenta";

                        break;
                    }
                default: break;
            }


        }

        private void KlijentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (KlijentComboBox.SelectedItem.ToString() == "Fizicka lica")
            {
                PoreskiIdTxtBx.Enabled = false;
                NazivTxtBx.Enabled = false;

                LimeTxtBx.Enabled = true;
                ImeRoditeljaTxtBx.Enabled = true;
                PrezimeTxtBx.Enabled = true;
                BrLicneKarteTxtBx.Enabled = true;
                MestoIzdavanjaTxtBx.Enabled = true;
                DatumRodjenjaDateTimePicker.Enabled = true;
                JMBGTxtBx.Enabled = true;

            }
            else
            {
                LimeTxtBx.Enabled = false;
                ImeRoditeljaTxtBx.Enabled = false;
                PrezimeTxtBx.Enabled = false;
                BrLicneKarteTxtBx.Enabled = false;
                MestoIzdavanjaTxtBx.Enabled = false;
                DatumRodjenjaDateTimePicker.Enabled = false;
                JMBGTxtBx.Enabled = false;

                PoreskiIdTxtBx.Enabled = true;
                NazivTxtBx.Enabled = true;
            }
        }

        private void AkcijaKlijentaBtn_Click(object sender, EventArgs e)
        {
            try
            {
               

                switch (this.type)
                {
                    case "add":
                        {
                            string tip = KlijentComboBox.SelectedItem.ToString();
                            if(tip == "Pravna lica")
                            {
                                if (PoreskiIdTxtBx.Text == "" ||
                                    NazivTxtBx.Text == "" ||
                                    BrTelTxtBx.Text == "" ||
                                    EmailTxtBx.Text == "" ||
                                    AdresaTxtBx.Text == "")
                                {
                                    MessageBox.Show("Polja ne smeju biti prazna!");
                                    return;
                                }

                                if (!ProveraPodatakaPravnogLica())
                                {
                                    return;
                                }

                                DTOs.PravnoLiceBasic pravno = new DTOs.PravnoLiceBasic
                                {
                                    Poreski_id = PoreskiIdTxtBx.Text,
                                    Naziv = NazivTxtBx.Text,
                                    Br_tel = BrTelTxtBx.Text,
                                    Email = EmailTxtBx.Text,
                                    Adresa = AdresaTxtBx.Text
                                };
                                DTOManager.DodajPravnoLice(pravno);

                                MessageBox.Show("Uspesno ste dodali klijenta!");

                                break;
                            }
                            else
                            {
                                if (JMBGTxtBx.Text == "" ||
                                    DatumRodjenjaDateTimePicker.Value == null ||  
                                    LimeTxtBx.Text == "" ||
                                    ImeRoditeljaTxtBx.Text == "" ||
                                    PrezimeTxtBx.Text == "" ||
                                    BrLicneKarteTxtBx.Text == "" ||
                                    MestoIzdavanjaTxtBx.Text == "" ||
                                    BrTelTxtBx.Text == "" ||
                                    EmailTxtBx.Text == "" ||
                                    AdresaTxtBx.Text == "")
                                {
                                    MessageBox.Show("Polja ne smeju biti prazna!");
                                    return;
                                }
                                if (!ProveraPodatakaFizickogLica(this.type))
                                {
                                    return;
                                }
                                DTOs.FizickoLiceBasic fizicko = new DTOs.FizickoLiceBasic
                                {
                                    JMBG = JMBGTxtBx.Text,
                                    Datum_rodjenja = DatumRodjenjaDateTimePicker.Value,
                                    LIme = LimeTxtBx.Text,
                                    Ime_roditelja = ImeRoditeljaTxtBx.Text,
                                    Prezime = PrezimeTxtBx.Text,
                                    Br_licne_karte = BrLicneKarteTxtBx.Text,
                                    Mesto_izdavanja = MestoIzdavanjaTxtBx.Text,
                                    Naziv = LimeTxtBx.Text + " " + PrezimeTxtBx.Text,
                                    Br_tel = BrTelTxtBx.Text,
                                    Email = EmailTxtBx.Text,
                                    Adresa = AdresaTxtBx.Text
                                };

                                DTOManager.DodajFizickoLice(fizicko);

                                MessageBox.Show("Uspesno ste dodali klijenta!");

                                break;
                            }
                        }
                    case "update":
                        {
                            string tip = KlijentComboBox.SelectedItem.ToString();
                            if (tip == "Pravna lica")
                            {
                                if (!ProveraPodatakaPravnogLica())
                                {
                                    return;
                                }
                                this.pravnoLice.Naziv = NazivTxtBx.Text;
                                this.pravnoLice.Adresa = AdresaTxtBx.Text;
                                this.pravnoLice.Br_tel = BrTelTxtBx.Text;
                                this.pravnoLice.Email = EmailTxtBx.Text;
                                this.pravnoLice.Poreski_id = PoreskiIdTxtBx.Text;
                                


                                DTOManager.IzmeniPravnoLice(this.pravnoLice);

                                MessageBox.Show("Uspesno ste izmenili klijenta!");

                                
                            }
                            else if(tip == "Fizicka lica")
                            {
                                if (!ProveraPodatakaFizickogLica(this.type))
                                {
                                    return;
                                }
                                this.fizickoLice.LIme = LimeTxtBx.Text;
                                this.fizickoLice.JMBG = JMBGTxtBx.Text;
                                this.fizickoLice.Datum_rodjenja = DatumRodjenjaDateTimePicker.Value;
                                this.fizickoLice.LIme = LimeTxtBx.Text;
                                this.fizickoLice.Ime_roditelja = ImeRoditeljaTxtBx.Text;
                                this.fizickoLice.Prezime = PrezimeTxtBx.Text;
                                this.fizickoLice.Br_licne_karte = BrLicneKarteTxtBx.Text;
                                this.fizickoLice.Mesto_izdavanja = MestoIzdavanjaTxtBx.Text;
                                this.fizickoLice.Br_tel = BrTelTxtBx.Text;
                                this.fizickoLice.Email = EmailTxtBx.Text;
                                this.fizickoLice.Adresa = AdresaTxtBx.Text;

                                DTOManager.IzmeniFizickoLice(this.fizickoLice);

                                MessageBox.Show("Uspesno ste izmenili klijenta!");
                            }


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

        private bool ProveraPodatakaFizickogLica(string tip)
        {
            if (BrLicneKarteTxtBx.Text.Length != 9)
            {
                MessageBox.Show("Broj licne karte mora da sadrzi tacno 9 karaktera!\n");
                return false;
            }

            if (!Regex.IsMatch(BrLicneKarteTxtBx.Text, @"^\d{9}$"))
            {
                MessageBox.Show("Broj licne karte mora da sadrzi samo brojke!\n");
                return false;
            }

            if (!DTOManager.ProveraPonavljanjaLicneKarteFizickihLica(BrLicneKarteTxtBx.Text) && tip == "add")
            {
                MessageBox.Show("Morate uneti svoj unikatan broj licne karte!\n");
                return false;
            }

            if (JMBGTxtBx.Text.Length != 13)
            {
                MessageBox.Show("JMBG mora da sadrzi tacno 13 karaktera!\n");
                return false;
            }

            if (!Regex.IsMatch(JMBGTxtBx.Text, @"^\d{13}$"))
            {
                MessageBox.Show("JMBG mora da sadrzi samo brojke!\n");
                return false;
            }

            if (!Regex.IsMatch(EmailTxtBx.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email adresa nije validna!\n");
                return false;
            }

            return true;
        }

        private bool ProveraPodatakaPravnogLica()
        {
            if (!Regex.IsMatch(EmailTxtBx.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email adresa nije validna!\n");
                return false;
            }

            return true;
        }

    }
}

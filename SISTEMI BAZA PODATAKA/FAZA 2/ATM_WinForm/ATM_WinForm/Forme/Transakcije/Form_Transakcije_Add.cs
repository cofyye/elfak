using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Klijent;
using ATM_WinForm.Klase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Transakcije
{
    public partial class Form_Transakcije_Add : Form
    {
        private readonly int bankomatId = -1;
        public Form_Transakcije_Add(int bankomatId)
        {
            InitializeComponent();

            this.bankomatId = bankomatId;

            KlijentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            KarticaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void DodajTransakcijuBtn_Click(object sender, EventArgs e)
        {
            if (PodignutIznosTxtBx.Text == "" ||
                DatumPodizanjaNovcaDateTimePicker.Text == "" ||
                VremeDatePicker.Text == ""
                )
            {
                MessageBox.Show("Polja ne smeju biti prazna!");
                return;
            }
            if(KarticaComboBox.SelectedValue == null)
            {
                MessageBox.Show("Klijent mora imati karticu da bi izvrsio transakciju!");
                return;

                
            }

            DTOs.TransakcijaBasic transakcija = new DTOs.TransakcijaBasic
            {
                Podignuti_iznos = PodignutIznosTxtBx.Text,
                Datum_Podizanja_Novca = DatumPodizanjaNovcaDateTimePicker.Value,
                Vreme_Podizanja_Novca = VremeDatePicker.Value.ToString("HH:mm:ss"),
            };

            DTOManager.DodajTransakciju(transakcija, this.bankomatId, (int)KarticaComboBox.SelectedValue);

            this.Close();
        }

        private void PopuniKlijente()
        {
            KlijentComboBox.DisplayMember = "Text";
            KlijentComboBox.ValueMember = "ID";

            List<ComboItem> listaKlijenata = new List<ComboItem>();

            var klijenti = DTOManager.VratiSveKlijente();

            foreach (var klijent in klijenti)
            {
                listaKlijenata.Add(new ComboItem { ID = klijent.Id, Text = klijent.Id + " - " + klijent.Naziv });
            }

            KlijentComboBox.DataSource = listaKlijenata;

            if (listaKlijenata.Count > 0)
            {
                KlijentComboBox.SelectedIndex = 0;
            }

            PopuniKarticeZaKlijenta();
        }

        private void PopuniKarticeZaKlijenta() 
        {
            KarticaComboBox.DisplayMember = "Text";
            KarticaComboBox.ValueMember = "ID";

            List<ComboItem> listaKartica = new List<ComboItem>();

            var racuni = DTOManager.VratiSveRacuneOdKlijenta((int)KlijentComboBox.SelectedValue);

            foreach (var racun in racuni)
            {
                var kartice = DTOManager.VratiSveKarticeOdRacuna(racun.Br_racuna);
                foreach (var kartica in kartice)
                {
                    listaKartica.Add(new ComboItem { ID = kartica.Id, Text = $"Kartica iz {racun.JePovezan.Ime} sa ID: {kartica.Id}" });
                }
            }

            KarticaComboBox.DataSource = listaKartica;

            if (listaKartica.Count > 0)
            {
                KarticaComboBox.SelectedIndex = 0;
            }
        }

        private void Form_Transakcije_Add_Load(object sender, EventArgs e)
        {
            PopuniKlijente();
        }

        private void KlijentComboBox_DropDownClosed(object sender, EventArgs e)
        {
            PopuniKarticeZaKlijenta();
        }
    }
}

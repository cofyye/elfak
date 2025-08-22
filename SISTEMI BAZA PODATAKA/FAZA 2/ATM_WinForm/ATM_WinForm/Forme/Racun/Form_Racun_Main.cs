using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Kartica;
using ATM_WinForm.Forme.Racun;
using NHibernate;

namespace ATM_WinForm.Forme.Racun
{
    public partial class Form_Racun_Main : Form
    {
        private readonly int bankaId = -1;
        private readonly int klijentId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.RacunBasic> racuni = null;
        public Form_Racun_Main(int bankaId = -1, int klijentId = -1)
        {
            InitializeComponent();
            this.bankaId = bankaId;
            this.klijentId = klijentId;

            RacunGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void Form_Racun_Main_Load(object sender, EventArgs e)
        {
            if (this.bankaId == -1 && this.klijentId == -1)
            {
                racuni = DTOManager.VratiSveRacune();
                DodajRacunBtn.Visible = true;
            }
            else if(this.bankaId != -1)
            {
                racuni = DTOManager.VratiSveRacuneOdBanke(this.bankaId);
                PrikaziKorisnikaBtn.Text = "Prikazi Banku";
            }
            else if(this.klijentId != -1)
            {
                racuni = DTOManager.VratiSveRacuneOdKlijenta(this.klijentId);
                PrikaziKorisnikaBtn.Text = "Prikazi Banku";
            }

            bindingSource.DataSource = racuni;
            RacunGrid.DataSource = bindingSource;

            RacunGrid.AllowUserToAddRows = false;
        }

        private void RacunGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (RacunGrid.SelectedRows.Count > 0)
            {
                IzbrisiRacunBtn.Enabled = true;
                IzmeniRacunBtn.Enabled = true;
                PrikaziKorisnikaBtn.Enabled = true;
            }
            else
            {
                IzbrisiRacunBtn.Enabled = false;
                IzmeniRacunBtn.Enabled = false;
                PrikaziKorisnikaBtn.Enabled = false;
            }
            if (this.klijentId == -1)
                DodajRacunBtn.Visible = false;
        }
             
           
        private void PrikaziKorisnikaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                // Dobijanje selektovanog reda
                DataGridViewRow selectedRow = RacunGrid.SelectedRows[0];

                // Dobijanje objekta Racun iz selektovanog reda
                DTOs.RacunBasic racun = (DTOs.RacunBasic)selectedRow.DataBoundItem;


                ATM_WinForm.Entiteti.Klijent klijent = s.Get<ATM_WinForm.Entiteti.Klijent>(racun.Koristi.Id);
                ATM_WinForm.Entiteti.Banka banka = s.Get<ATM_WinForm.Entiteti.Banka>(racun.JePovezan.Id);

                if(this.bankaId == -1)
                    MessageBox.Show($"Naziv banke:  {banka.Ime}\n");
                else if(this.klijentId == -1)
                    MessageBox.Show($"Naziv klijenta:  {klijent.Naziv}\nBroj telefona: {klijent.Br_tel}\nEmail:{klijent.Email}");

                s.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void PrikaziKarticeBtn_Click(object sender, EventArgs e)
        {
            if (RacunGrid.SelectedCells.Count > 0)
            {
                int rowIndex = RacunGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var racun = RacunGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunBasic;
                    var SpisakSvihKarticaForm = new Form_Kartica_Main(racun.Br_racuna);
                    SpisakSvihKarticaForm.ShowDialog();
                }
            }
        }

        private void OvlascenaLicaBtn_Click(object sender, EventArgs e)
        {
            if (RacunGrid.SelectedCells.Count > 0)
            {
                int rowIndex = RacunGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var racun = RacunGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunBasic;
                    var SpisakSvihOvlascenihLicaForm = new Form_Racun_OvlascenoLice_Main(racun.Br_racuna);
                    SpisakSvihOvlascenihLicaForm.ShowDialog();
                }
            }
        }

        private void DodajRacunBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniRacunForm = new Form_Racun_AddUpdate("add", null, klijentId);
            dodajIzmeniRacunForm.ShowDialog();
            this.PopuniPodacima();
        }

        private void PopuniPodacima()
        {
            racuni.Clear();
            racuni = DTOManager.VratiSveRacuneOdKlijenta(klijentId);
            bindingSource.DataSource = racuni;
            RacunGrid.DataSource = bindingSource;
        }

        private void IzmeniRacunBtn_Click(object sender, EventArgs e)
        {
            if (RacunGrid.SelectedCells.Count > 0)
            {
                int rowIndex = RacunGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var racun = RacunGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunBasic;
                    if (racun.Valuta == null)
                        racun.Tip = "dinarski";
                    else
                        racun.Tip = "devizni";
                    var dodajIzmeniRacunForm = new Form_Racun_AddUpdate("update", racun, this.klijentId);
                    dodajIzmeniRacunForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiRacunBtn_Click(object sender, EventArgs e)
        {
            if (RacunGrid.SelectedCells.Count > 0)
            {
                int rowIndex = RacunGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var racun = RacunGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani racun?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiRacun(racun.Br_racuna);

                        MessageBox.Show("Uspesno ste izbrisali racun!");

                        RacunGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }
    }
}

using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Filijala;
using ATM_WinForm.Forme.Klijent;
using ATM_WinForm.Forme.Transakcije;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_Bankomat_Main : Form
    {
        private int filijalaId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.BankomatBasic> bankomati = null;
        public Form_Bankomat_Main(int filijalaId = -1)
        {
            InitializeComponent();
            this.filijalaId = filijalaId;

            BankomatGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Bankomat_Main_Load(object sender, EventArgs e)
        {
            if (this.filijalaId == -1)
            {
                bankomati = DTOManager.VratiSveBankomate();
            }
            else
            {
                bankomati = DTOManager.VratiSveBankomateOdFilijale(this.filijalaId);
            }

            bindingSource.DataSource = bankomati;
            BankomatGrid.DataSource = bindingSource;

            BankomatGrid.AllowUserToAddRows = false;
        }

        private void DodajBankomatBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniBankomatForm = new Form_Bankomat_AddUpdate("add", null, this.filijalaId);
            dodajIzmeniBankomatForm.ShowDialog();
            PopuniPodacima();
        }

        private void PopuniPodacima()
        {
            bankomati.Clear();

            if (this.filijalaId == -1)
            {
                bankomati = DTOManager.VratiSveBankomate();
            }
            else
            {
                bankomati = DTOManager.VratiSveBankomateOdFilijale(this.filijalaId);
            }
            bindingSource.DataSource = bankomati;
            BankomatGrid.DataSource = bindingSource;
        }

        private void BankomatGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedRows.Count > 0)
            {
                IzbrisiBankomatBtn.Enabled = true;
                IzmeniBankomatBtn.Enabled = true;
            }
            else
            {
                IzbrisiBankomatBtn.Enabled = false;
                IzmeniBankomatBtn.Enabled = false;
            }
        }

        private void IzbrisiBankomatBtn_Click(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankomatGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var bankomat = BankomatGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani bankomat?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiBankomat(bankomat.Id);

                        MessageBox.Show("Uspesno ste izbrisali bankomat!");

                        BankomatGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void IzmeniBankomatBtn_Click(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankomatGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var bankomat = BankomatGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatBasic;
                    if (this.filijalaId == -1)
                    {
                        this.filijalaId = (DTOManager.VratiFilijaluBankomata(bankomat.Id)).Rbr_filijale;
                    }
                    var dodajIzmeniBankomatForm = new Form_Bankomat_AddUpdate("update", bankomat, this.filijalaId);
                    dodajIzmeniBankomatForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void PrikaziKomentareBtn_Click(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankomatGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var bankomat = BankomatGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatBasic;
                    var ListaKomentaraForm = new Form_Bankomat_Komentari_Main(bankomat.Id);
                    ListaKomentaraForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void PrikaziServiseBtn_Click(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankomatGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var bankomat = BankomatGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatBasic;
                    var ListaServisaForm = new Form_Servis_Main(bankomat.Id);
                    ListaServisaForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void PrikaziTransakcijeBtn_Click(object sender, EventArgs e)
        {
            if (BankomatGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankomatGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var bankomat = BankomatGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatBasic;
                    var TransakcijaForm = new Form_Transakcija_Main(bankomat.Id);
                    TransakcijaForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }
    }
}

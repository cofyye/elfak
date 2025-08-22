using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ATM_WinForm.Forme.Banka;
using ATM_WinForm.Forme.Klijent;
using ATM_WinForm.Forme.Racun;
using NHibernate;

namespace ATM_WinForm
{
    public partial class Form_Banka_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.BankaBasic> banke = null;

        public Form_Banka_Main()
        {
            InitializeComponent();

            BankaGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            BankaGrid.ClearSelection();
        }

        private void Form_Banka_Main_Load(object sender, EventArgs e)
        {
            banke = DTOManager.VratiSveBanke();

            bindingSource.DataSource = banke;
            BankaGrid.DataSource = bindingSource;

            BankaGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            banke.Clear();
            banke = DTOManager.VratiSveBanke();
            bindingSource.DataSource = banke;
            BankaGrid.DataSource = bindingSource;
        }

        private void DodajBankuBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniBankuForm = new Form_Banka_AddUpdate("add", null);
            dodajIzmeniBankuForm.ShowDialog();
            this.PopuniPodacima();
        }

        private void BankaGrid_SelectionChanged(object sender, EventArgs e)
        {
            if(BankaGrid.SelectedRows.Count > 0)
            {
                IzbrisiBankuBtn.Enabled = true;
                IzmeniBankuBtn.Enabled = true;
                BtnPregledFilijala.Enabled = true;
                SpisakBrojevaTelefonaBtn.Enabled = true;
                SpisakSvihRacunaBtn.Enabled = true;
            } else
            {
                IzbrisiBankuBtn.Enabled = false;
                IzmeniBankuBtn.Enabled = false;
                BtnPregledFilijala.Enabled= false;
                SpisakBrojevaTelefonaBtn.Enabled= false;
                SpisakSvihRacunaBtn.Enabled = false;
            }
        }

        private void IzbrisiBankuBtn_Click(object sender, EventArgs e)
        {

            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabranu banku?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //
                                       
                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiBanku(banka.Id);

                        PopuniPodacima();
                        MessageBox.Show("Brisanje banke je uspesno obavljeno!");
                    }
                }
            }
        }

        private void IzmeniBankuBtn_Click(object sender, EventArgs e)
        {
            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;
                    var dodajIzmeniBankuForm = new Form_Banka_AddUpdate("update", banka);
                    dodajIzmeniBankuForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void BtnPregledFilijala_Click(object sender, EventArgs e)
        {
            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;
                    var FilijalaForm = new Form_Filijala_Main(banka.Id);
                    FilijalaForm.ShowDialog();
                }
            }
        }

        private void SpisakBrojevaTelefonaBtn_Click(object sender, EventArgs e)
        {
            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;
                    var SpisakBrojevaTelefonaForm = new Form_Banka_SpisakBrojeva_Main(banka.Id);
                    SpisakBrojevaTelefonaForm.ShowDialog();
                }
            }
        }

        private void SpisakSvihRacunaBtn_Click(object sender, EventArgs e)
        {
            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;
                    var SpisakSvihRacunaForm  = new Form_Racun_Main(banka.Id);
                    SpisakSvihRacunaForm.ShowDialog();
                }
            }
        }

        private void PregledKlijenataBtn_Click(object sender, EventArgs e)
        {
            if (BankaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var banka = BankaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBasic;
                    var KlijentForm = new Form_Klijent_Main(banka.Id);
                    KlijentForm.ShowDialog();
                }
            }
        }
    }
}

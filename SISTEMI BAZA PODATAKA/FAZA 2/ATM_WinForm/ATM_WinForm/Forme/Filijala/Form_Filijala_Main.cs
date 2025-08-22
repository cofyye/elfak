using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Bankomat;
using ATM_WinForm.Forme.Filijala;
using ATM_WinForm.Forme.Racun;

namespace ATM_WinForm
{
    public partial class Form_Filijala_Main : Form
    {
        private readonly int bankaId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.FilijalaBasic> filijale = null;
        public Form_Filijala_Main(int bankaId = -1)
        {
            InitializeComponent();
            this.bankaId = bankaId;

            FilijalaGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Filijala_Main_Load(object sender, EventArgs e)
        {
            if(this.bankaId == -1)
            {
                filijale = DTOManager.VratiSveFilijale();
            }
            else
            {
                filijale = DTOManager.VratiSveFilijaleOdBanke(this.bankaId);
            }

            bindingSource.DataSource = filijale;
            FilijalaGrid.DataSource = bindingSource;

            FilijalaGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            filijale.Clear();

            if (this.bankaId == -1)
            {
                filijale = DTOManager.VratiSveFilijale();
            }
            else
            {
                filijale = DTOManager.VratiSveFilijaleOdBanke(this.bankaId);
            }
            bindingSource.DataSource = filijale;
            FilijalaGrid.DataSource = bindingSource;
        }

        private void DodajFilijaluBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniFilijaluForm = new Form_Filijala_AddUpdate("add", null, this.bankaId);
            dodajIzmeniFilijaluForm.ShowDialog();
            PopuniPodacima();
        }

        private void FilijalaGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (FilijalaGrid.SelectedRows.Count > 0)
            {
                IzbrisiFilijaluBtn.Enabled = true;
                IzmeniFilijaluBtn.Enabled = true;
            }
            else
            {
                IzbrisiFilijaluBtn.Enabled = false;
                IzmeniFilijaluBtn.Enabled = false;
            }
        }

        private void IzbrisiFilijaluBtn_Click(object sender, EventArgs e)
        {
            if (FilijalaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = FilijalaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var filijala = FilijalaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.FilijalaBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabranu filijalu?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiFilijalu(filijala.Rbr_filijale);

                        MessageBox.Show("Uspesno ste izbrisali filijalu!");

                        FilijalaGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void IzmeniFilijaluBtn_Click(object sender, EventArgs e)
        {
            if (FilijalaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = FilijalaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var filijala = FilijalaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.FilijalaBasic;
                    var dodajIzmeniFilijaluForm = new Form_Filijala_AddUpdate("update", filijala, this.bankaId);
                    dodajIzmeniFilijaluForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void PrikaziBankomateBtn_Click(object sender, EventArgs e)
        {
            if (FilijalaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = FilijalaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var filijala = FilijalaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.FilijalaBasic;
                    var SpisakSvihBankomataForm = new Form_Bankomat_Main(filijala.Rbr_filijale);
                    SpisakSvihBankomataForm.ShowDialog();
                }
            }
        }
    }
}

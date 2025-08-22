using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_Bankomat_Komentari_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.BankomatKomentariBasic> komentariBankomataLista = null;
        private readonly int bankomatId = -1;
        public Form_Bankomat_Komentari_Main(int bankomatId)
        {
            InitializeComponent();

            this.bankomatId = bankomatId;

            KomentariGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Bankomat_Komentari_Main_Load(object sender, EventArgs e)
        {
            komentariBankomataLista = DTOManager.VratiSveKomentareBankomata(this.bankomatId);

            bindingSource.DataSource = komentariBankomataLista;
            KomentariGrid.DataSource = bindingSource;

            KomentariGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            komentariBankomataLista.Clear();

            komentariBankomataLista = DTOManager.VratiSveKomentareBankomata(this.bankomatId);

            bindingSource.DataSource = komentariBankomataLista;
            KomentariGrid.DataSource = bindingSource;
        }

        private void DodajKomentarBtn_Click(object sender, EventArgs e)
        {
            var DodajIzmeniKomentarForm = new Form_Bankomat_Komentari_AddUpdate("add", null, this.bankomatId);
            DodajIzmeniKomentarForm.ShowDialog();
            PopuniPodacima();
        }

        private void IzmeniKomentarBtn_Click(object sender, EventArgs e)
        {
            if (KomentariGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KomentariGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var komentar = KomentariGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatKomentariBasic;
                    var DodajIzmeniKomentarForm = new Form_Bankomat_Komentari_AddUpdate("update", komentar, this.bankomatId);
                    DodajIzmeniKomentarForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiKomentarBtn_Click(object sender, EventArgs e)
        {
            if (KomentariGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KomentariGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var komentar = KomentariGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankomatKomentariBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani komentar?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiKomentarBankomata(komentar.Id);

                        MessageBox.Show("Uspesno ste izbrisali komentar!");

                        KomentariGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void KomentariGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (KomentariGrid.SelectedRows.Count > 0)
            {
                IzmeniKomentarBtn.Enabled = true;
                IzbrisiKomentarBtn.Enabled = true;
            }
            else
            {
                IzmeniKomentarBtn.Enabled = false;
                IzbrisiKomentarBtn.Enabled = false;
            }
        }
    }
}

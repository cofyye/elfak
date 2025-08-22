using ATM_WinForm.Forme.Banka;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Klijent
{
    public partial class Form_Klijent_Komentari_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.KomentarKlijentaBasic> komentariKlijenataLista = null;
        private readonly int klijentId = -1;
        public Form_Klijent_Komentari_Main(int klijentId)
        {
            InitializeComponent();

            this.klijentId = klijentId;

            KomentariGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Klijent_Komentari_Main_Load(object sender, EventArgs e)
        {
            komentariKlijenataLista = DTOManager.VratiSveKomentareKlijenta(this.klijentId);

            bindingSource.DataSource = komentariKlijenataLista;
            KomentariGrid.DataSource = bindingSource;

            KomentariGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            komentariKlijenataLista.Clear();

            komentariKlijenataLista = DTOManager.VratiSveKomentareKlijenta(this.klijentId);

            bindingSource.DataSource = komentariKlijenataLista;
            KomentariGrid.DataSource = bindingSource;
        }

        private void DodajKomentarBtn_Click(object sender, EventArgs e)
        {
            var DodajIzmeniKomentarForm = new Form_Klijent_Komentari_AddUpdate("add", null, this.klijentId);
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
                    var komentar = KomentariGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KomentarKlijentaBasic;
                    var DodajIzmeniKomentarForm = new Form_Klijent_Komentari_AddUpdate("update", komentar, this.klijentId);
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

                    var komentar = KomentariGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KomentarKlijentaBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani komentar?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiKomentarKlijenta(komentar.Id);

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

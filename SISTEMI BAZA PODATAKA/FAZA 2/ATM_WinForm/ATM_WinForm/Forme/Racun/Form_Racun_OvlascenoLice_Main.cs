using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Racun
{
    public partial class Form_Racun_OvlascenoLice_Main : Form
    {
        private readonly int racunId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.RacunOvlascenoLiceBasic> ovlascenaLica = null;
        public Form_Racun_OvlascenoLice_Main(int racunId)
        {
            InitializeComponent();
            this.racunId = racunId;

            LicaGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Racun_OvlascenoLice_Main_Load(object sender, EventArgs e)
        {
            ovlascenaLica = DTOManager.VratiSvaOvlascenaLicaOdRacuna(this.racunId);

            bindingSource.DataSource = ovlascenaLica;
            LicaGrid.DataSource = bindingSource;

            LicaGrid.AllowUserToAddRows = false;
        }

        private void DodajLiceBtn_Click(object sender, EventArgs e)
        {
            var dodajOvlascenoLiceForm = new Form_Racun_OvlascenoLice_AddUpdate("add", null, this.racunId);
            dodajOvlascenoLiceForm.ShowDialog();
            PopuniPodacima();
        }

        private void PopuniPodacima()
        {
            ovlascenaLica.Clear();

            ovlascenaLica = DTOManager.VratiSvaOvlascenaLicaOdRacuna(this.racunId);

            bindingSource.DataSource = ovlascenaLica;
            LicaGrid.DataSource = bindingSource;
        }

        private void IzmeniLiceBtn_Click(object sender, EventArgs e)
        {
            if (LicaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = LicaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var brTelefona = LicaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunOvlascenoLiceBasic;
                    var dodajIzmeniOvlscenoLiceForm = new Form_Racun_OvlascenoLice_AddUpdate("update", brTelefona, this.racunId);
                    dodajIzmeniOvlscenoLiceForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiLiceBtn_Click(object sender, EventArgs e)
        {
            if (LicaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = LicaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var ovlascenoLice = LicaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.RacunOvlascenoLiceBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrano ovlasceno lice?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiOvlascenoLiceRacuna(ovlascenoLice.Id);

                        MessageBox.Show("Uspesno ste izbrisali ovlasceno lice!");

                        LicaGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void LicaGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (LicaGrid.SelectedRows.Count > 0)
            {
                IzbrisiLiceBtn.Enabled = true;
                IzmeniLiceBtn.Enabled = true;
            }
            else
            {
                IzbrisiLiceBtn.Enabled = false;
                IzmeniLiceBtn.Enabled = false;
            }
        }
    }
}

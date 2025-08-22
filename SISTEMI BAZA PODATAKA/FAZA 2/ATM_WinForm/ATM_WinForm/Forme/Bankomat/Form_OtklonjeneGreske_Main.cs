using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Bankomat
{
    public partial class Form_OtklonjeneGreske_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.OtklonjenaGreskaServisaBasic> greskeServisaLista = null;
        private readonly int servisKod = -1;
        public Form_OtklonjeneGreske_Main(int servisKod)
        {
            InitializeComponent();

            this.servisKod = servisKod;

            GreskeGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_OtklonjeneGreske_Main_Load(object sender, EventArgs e)
        {
            greskeServisaLista = DTOManager.VratiSveOtklonjeneGreskeServisa(this.servisKod);

            bindingSource.DataSource = greskeServisaLista;
            GreskeGrid.DataSource = bindingSource;

            GreskeGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            greskeServisaLista.Clear();

            greskeServisaLista = DTOManager.VratiSveOtklonjeneGreskeServisa(this.servisKod);

            bindingSource.DataSource = greskeServisaLista;
            GreskeGrid.DataSource = bindingSource;
        }

        private void DodajGreskuBtn_Click(object sender, EventArgs e)
        {
            var DodajIzmeniGreskuForm = new Form_OtklonjeneGreske_AddUpdate("add", null, this.servisKod);
            DodajIzmeniGreskuForm.ShowDialog();
            PopuniPodacima();
        }

        private void IzmeniGreskuBtn_Click(object sender, EventArgs e)
        {
            if (GreskeGrid.SelectedCells.Count > 0)
            {
                int rowIndex = GreskeGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var greska = GreskeGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.OtklonjenaGreskaServisaBasic;
                    var DodajIzmeniGreskuForm = new Form_OtklonjeneGreske_AddUpdate("update", greska, this.servisKod);
                    DodajIzmeniGreskuForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiGreskuBtn_Click(object sender, EventArgs e)
        {
            if (GreskeGrid.SelectedCells.Count > 0)
            {
                int rowIndex = GreskeGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var greska = GreskeGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.OtklonjenaGreskaServisaBasic;

                    //
                    string poruka = "Da li zelite da obrisete odabranu otklonjenu gresku?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiOtklonjenuGreskuServisa(greska.Id);

                        MessageBox.Show("Uspesno ste izbrisali otklonjenu gresku!");

                        GreskeGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void GreskeGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (GreskeGrid.SelectedRows.Count > 0)
            {
                IzmeniGreskuBtn.Enabled = true;
                IzbrisiGreskuBtn.Enabled = true;
            }
            else
            {
                IzmeniGreskuBtn.Enabled = false;
                IzbrisiGreskuBtn.Enabled = false;
            }
        }
    }
}

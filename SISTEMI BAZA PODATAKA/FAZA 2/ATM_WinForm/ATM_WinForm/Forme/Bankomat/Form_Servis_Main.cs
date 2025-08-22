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
    public partial class Form_Servis_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.ServisBasic> servisBankomataLista = null;
        private readonly int bankomatId = -1;

        public Form_Servis_Main(int bankomatId)
        {
            InitializeComponent();

            this.bankomatId = bankomatId;

            ServisiGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Servis_Main_Load(object sender, EventArgs e)
        {
            servisBankomataLista = DTOManager.VratiSveServiseBankomata(this.bankomatId);

            bindingSource.DataSource = servisBankomataLista;
            ServisiGrid.DataSource = bindingSource;

            ServisiGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            servisBankomataLista.Clear();

            servisBankomataLista = DTOManager.VratiSveServiseBankomata(this.bankomatId);

            bindingSource.DataSource = servisBankomataLista;
            ServisiGrid.DataSource = bindingSource;
        }

        private void DodajServisBtn_Click(object sender, EventArgs e)
        {
            var DodajIzmeniServisForm = new Form_Bankomat_Servisi_AddUpdate("add", null, this.bankomatId);
            DodajIzmeniServisForm.ShowDialog();
            PopuniPodacima();
        }

        private void IzmeniServisBtn_Click(object sender, EventArgs e)
        {
            if (ServisiGrid.SelectedCells.Count > 0)
            {
                int rowIndex = ServisiGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var servis = ServisiGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.ServisBasic;
                    var DodajIzmeniServisForm = new Form_Bankomat_Servisi_AddUpdate("update", servis, this.bankomatId);
                    DodajIzmeniServisForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiServisBtn_Click(object sender, EventArgs e)
        {
            if (ServisiGrid.SelectedCells.Count > 0)
            {
                int rowIndex = ServisiGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var servis = ServisiGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.ServisBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani servis?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiServisBankomata(servis.Kod);

                        MessageBox.Show("Uspesno ste izbrisali servis!");

                        ServisiGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void ServisiGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (ServisiGrid.SelectedRows.Count > 0)
            {
                IzmeniServisBtn.Enabled = true;
                IzbrisiServisBtn.Enabled = true;
            }
            else
            {
                IzmeniServisBtn.Enabled = false;
                IzbrisiServisBtn.Enabled = false;
            }
        }

        private void PrikaziGreskeBtn_Click(object sender, EventArgs e)
        {
            if (ServisiGrid.SelectedCells.Count > 0)
            {
                int rowIndex = ServisiGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var servis = ServisiGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.ServisBasic;
                    var ListaGresakaForm = new Form_OtklonjeneGreske_Main(servis.Kod);
                    ListaGresakaForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }
    }
}

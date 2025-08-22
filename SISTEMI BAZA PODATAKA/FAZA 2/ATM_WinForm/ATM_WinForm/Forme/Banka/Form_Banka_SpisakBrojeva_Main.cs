using NHibernate;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Banka
{
    public partial class Form_Banka_SpisakBrojeva_Main : Form
    {
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.BankaBrTelefonaBasic> brojeviTelefonaLista = null;
        private readonly int bankaId = -1;
        public Form_Banka_SpisakBrojeva_Main(int bankaId)
        {
            InitializeComponent();
            this.bankaId = bankaId;

            BankaBrTelGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Banka_SpisakBrojeva_Main_Load(object sender, System.EventArgs e)
        {
           
            brojeviTelefonaLista = DTOManager.VratiSveBrojeveTelefonaOdBanke(this.bankaId);

            bindingSource.DataSource = brojeviTelefonaLista;
            BankaBrTelGrid.DataSource = bindingSource;

            BankaBrTelGrid.AllowUserToAddRows = false;

        }

        private void DodajBrojTelefonaBtn_Click(object sender, System.EventArgs e)
        {
            var dodajIzmeniBrTelefonaForm = new Form_BankaBrTelefona_AddUpdate("add", null, this.bankaId);
            dodajIzmeniBrTelefonaForm.ShowDialog();
            PopuniPodacima();
        }

        private void BankaBrTelGrid_SelectionChanged(object sender, System.EventArgs e)
        {
            if (BankaBrTelGrid.SelectedRows.Count > 0)
            {
                IzmeniBrojTelefonaBtn.Enabled = true;
                IzbrisiBrojTelefonaBtn.Enabled = true;
            }
            else
            {
                IzmeniBrojTelefonaBtn.Enabled = false;
                IzbrisiBrojTelefonaBtn.Enabled = false;
            }
        }

        private void PopuniPodacima()
        {
            brojeviTelefonaLista.Clear();

            brojeviTelefonaLista = DTOManager.VratiSveBrojeveTelefonaOdBanke(this.bankaId);

            bindingSource.DataSource = brojeviTelefonaLista;
            BankaBrTelGrid.DataSource = bindingSource;
        }

        private void IzmeniBrojTelefonaBtn_Click(object sender, System.EventArgs e)
        {
            if (BankaBrTelGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaBrTelGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var brTelefona = BankaBrTelGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBrTelefonaBasic;
                    var dodajIzmeniBrTelefonaForm = new Form_BankaBrTelefona_AddUpdate("update", brTelefona, this.bankaId);
                    dodajIzmeniBrTelefonaForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void IzbrisiBrojTelefonaBtn_Click(object sender, System.EventArgs e)
        {
            if (BankaBrTelGrid.SelectedCells.Count > 0)
            {
                int rowIndex = BankaBrTelGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var brTelefona = BankaBrTelGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.BankaBrTelefonaBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabrani broj telefona?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiBrojTelefonaBanke(brTelefona.Id);

                        MessageBox.Show("Uspesno ste izbrisali broj telefona!");

                        BankaBrTelGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ATM_WinForm.Forme.Racun;

namespace ATM_WinForm.Forme.Klijent
{
    public partial class Form_Klijent_Main : Form
    {
        private readonly int bankaId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.KlijentBasic> klijent = null;
        List<ATM_WinForm.DTOs.PravnoLiceBasic> pravnoLice = null;
        List<ATM_WinForm.DTOs.FizickoLiceBasic> fizickoLice = null;
        public Form_Klijent_Main(int bankaId = -1)
        {
            InitializeComponent();

            this.bankaId = bankaId;
            this.KlijentComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.KlijentComboBox.SelectedIndex = 0;
            

            KlijentGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            KlijentGrid.ClearSelection();
        }

        private void Form_Klijent_Main_Load(object sender, EventArgs e)
        {

            klijent = DTOManager.VratiSveKlijente(this.bankaId);

            bindingSource.DataSource = klijent;
            KlijentGrid.DataSource = bindingSource;

            KlijentGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            klijent.Clear();
            klijent = DTOManager.VratiSveKlijente(this.bankaId);
  

            bindingSource.DataSource = klijent;
            KlijentGrid.DataSource = bindingSource;
        }

        //pravna lica - 1
        private void KlijentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(this.KlijentComboBox.SelectedIndex.ToString() == "1")
            {

                    pravnoLice = DTOManager.VratiSvaPravnaLica(this.bankaId);

                    bindingSource.DataSource = pravnoLice;
                    KlijentGrid.DataSource = bindingSource;

                    KlijentGrid.AllowUserToAddRows = false;
            }
            else if(this.KlijentComboBox.SelectedIndex.ToString() == "2")
            {
                fizickoLice = DTOManager.VratiSvaFizickaLica(this.bankaId);

                bindingSource.DataSource = fizickoLice;
                KlijentGrid.DataSource = bindingSource;

                KlijentGrid.AllowUserToAddRows = false;
            }
            else
            {
                klijent = DTOManager.VratiSveKlijente(this.bankaId);

                bindingSource.DataSource = klijent;
                KlijentGrid.DataSource = bindingSource;

                KlijentGrid.AllowUserToAddRows = false;
            }
        }

        private void PrikaziKomentareBtn_Click(object sender, EventArgs e)
        {
            if (KlijentGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KlijentGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var klijent = KlijentGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KlijentBasic;
                    var ListaKomentaraForm = new Form_Klijent_Komentari_Main(klijent.Id);
                    ListaKomentaraForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void DodajKlijentaBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniKlijentaForm = new Form_Klijent_AddUpdate("add", null, null);
            dodajIzmeniKlijentaForm.ShowDialog();
            this.PopuniPodacima();
        }

        private void IzmeniKlijentaBtn_Click(object sender, EventArgs e)
        {
            if (KlijentGrid.SelectedCells.Count > 0)
            {
                if (KlijentComboBox.SelectedIndex == 1)//pravno lice
                {
                    int rowIndex = KlijentGrid.SelectedCells[0].RowIndex;
                    if (rowIndex != -1)
                    {
                        var pravno = KlijentGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.PravnoLiceBasic;
                        var dodajIzmeniKlijentForm = new Form_Klijent_AddUpdate("update", null, pravno);
                        dodajIzmeniKlijentForm.ShowDialog();
                        bindingSource.ResetBindings(false);
                        
                    }
                }
                else if (KlijentComboBox.SelectedIndex == 2)//fizicko lice
                {
                    int rowIndex = KlijentGrid.SelectedCells[0].RowIndex;
                    if (rowIndex != -1)
                    {
                        var fizicko = KlijentGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.FizickoLiceBasic;
                        var dodajIzmeniKlijentForm = new Form_Klijent_AddUpdate("update", fizicko, null);
                        dodajIzmeniKlijentForm.ShowDialog();
                        bindingSource.ResetBindings(false);
                        
                    }
                }
                this.PopuniPodacima();
            }
        }

        private void IzbrisiKlijentaBtn_Click(object sender, EventArgs e)
        {
            if (KlijentGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KlijentGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var kartica = KlijentGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KlijentBasic;

                    //
                    string poruka = "Da li zelite da obrisete izabranog klijenta?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiKlijenta(kartica.Id);

                        MessageBox.Show("Uspesno ste izbrisali karticu!");

                        KlijentGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }

        private void PrikaziRacuneBtn_Click(object sender, EventArgs e)
        {
            if (KlijentGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KlijentGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var klijent = KlijentGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KlijentBasic;
                    var ListaRacunaForm = new Form_Racun_Main(-1, klijent.Id);
                    ListaRacunaForm.ShowDialog();
                    bindingSource.ResetBindings(false);
                }
            }
        }

        private void KlijentGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (KlijentComboBox.SelectedIndex != 0)
            {
                if (KlijentGrid.SelectedRows.Count > 0)
                {
                    IzmeniKlijentaBtn.Enabled = true;
                }
                else
                {
                    IzmeniKlijentaBtn.Enabled = false;
                }
            }
            else
                IzmeniKlijentaBtn.Enabled = false;
            IzbrisiKlijentaBtn.Enabled = true;
        }
    }
}

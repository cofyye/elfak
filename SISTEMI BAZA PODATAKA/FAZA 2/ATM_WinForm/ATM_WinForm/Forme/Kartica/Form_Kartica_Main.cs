using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;

namespace ATM_WinForm.Forme.Kartica
{
    public partial class Form_Kartica_Main : Form
    {
        private readonly int racunId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.KarticaBasic> kartice = null;
        public Form_Kartica_Main(int racunId = -1)
        {
            InitializeComponent();
            this.racunId = racunId;

            KarticaGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Kartica_Main_Load(object sender, EventArgs e)
        {
            kartice = DTOManager.VratiSveKarticeOdRacuna(this.racunId);
            bindingSource.DataSource = kartice;
            KarticaGrid.DataSource = bindingSource;

            KarticaGrid.AllowUserToAddRows = false;
        }

        private void DodajKarticuBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniKarticuForm = new Form_Kartica_AddUpdate("add", null, this.racunId);
            dodajIzmeniKarticuForm.ShowDialog();
            PopuniPodacima();
        }

        private void PopuniPodacima()
        {
            kartice.Clear();
            kartice = DTOManager.VratiSveKarticeOdRacuna(this.racunId);
            bindingSource.DataSource = kartice;
            KarticaGrid.DataSource = bindingSource;
        }

        private void IzmeniKarticuBtn_Click(object sender, EventArgs e)
        {
            if (KarticaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KarticaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {
                    var kartica = KarticaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KarticaBasic;
                    if (kartica.Max_iznos_zaduzenja != null)
                        kartica.Tip = "kreditna";
                    else
                        kartica.Tip = "debitna";
                    var dodajIzmeniBankomatForm = new Form_Kartica_AddUpdate("update", kartica, this.racunId);
                    dodajIzmeniBankomatForm.ShowDialog();
                    bindingSource.ResetBindings(false);

                }
            }
        }

        private void KarticaGrid_SelectionChanged(object sender, EventArgs e)
        {
            if (KarticaGrid.SelectedRows.Count > 0)
            {
                IzbrisiKarticuBtn.Enabled = true;
                IzmeniKarticuBtn.Enabled = true;
            }
            else
            {
                IzbrisiKarticuBtn.Enabled = false;
                IzmeniKarticuBtn.Enabled = false;
            }
        }

        private void IzbrisiKarticuBtn_Click(object sender, EventArgs e)
        {
            if (KarticaGrid.SelectedCells.Count > 0)
            {
                int rowIndex = KarticaGrid.SelectedCells[0].RowIndex;
                if (rowIndex != -1)
                {

                    var kartica = KarticaGrid.SelectedRows[0].DataBoundItem as ATM_WinForm.DTOs.KarticaBasic;
                    
                    //
                    string poruka = "Da li zelite da obrisete izabranu karticu?";
                    string title = "Pitanje";
                    MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
                    DialogResult result = MessageBox.Show(poruka, title, buttons);
                    //


                    if (result == DialogResult.OK)
                    {
                        DTOManager.IzbrisiKarticu(kartica.Id);

                        MessageBox.Show("Uspesno ste izbrisali karticu!");

                        KarticaGrid.Rows.RemoveAt(rowIndex);
                    }


                }
            }
        }
    }
}

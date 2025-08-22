using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Klijent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM_WinForm.Forme.Transakcije
{
    public partial class Form_Transakcija_Main : Form
    {
        private readonly int bankomatId = -1;
        private readonly BindingSource bindingSource = new BindingSource();
        List<ATM_WinForm.DTOs.PregledBasic> pregled = null;
        public Form_Transakcija_Main(int bankomatId = -1)
        {
            InitializeComponent();

            this.bankomatId = bankomatId;

            TransakcijaGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void Form_Transakcija_Main_Load(object sender, EventArgs e)
        {
            pregled = DTOManager.VratiSveTransakcijeOdBankomata(this.bankomatId);
            

            bindingSource.DataSource = pregled;
            TransakcijaGrid.DataSource = bindingSource;

            TransakcijaGrid.AllowUserToAddRows = false;
        }

        private void PopuniPodacima()
        {
            pregled.Clear();
            pregled = DTOManager.VratiSveTransakcijeOdBankomata(this.bankomatId);
            bindingSource.DataSource = pregled;
            TransakcijaGrid.DataSource = bindingSource;
        }

        private void DodajTransakcijuBtn_Click(object sender, EventArgs e)
        {
            var dodajIzmeniKlijentaForm = new Form_Transakcije_Add(this.bankomatId);
            dodajIzmeniKlijentaForm.ShowDialog();
            this.PopuniPodacima();
        }
    }
}

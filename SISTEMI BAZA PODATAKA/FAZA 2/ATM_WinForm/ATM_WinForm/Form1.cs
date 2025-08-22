using System;
using System.Windows.Forms;
using NHibernate;
using ATM_WinForm.Entiteti;
using ATM_WinForm.Forme.Racun;
using ATM_WinForm.Forme.Klijent;
using ATM_WinForm.Forme.Bankomat;
using static ATM_WinForm.DTOs;

namespace ATM_WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdReadBank_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                //Ucitavaju se podaci o banci za zadatim brojem
                ATM_WinForm.Entiteti.Banka b = s.Load<ATM_WinForm.Entiteti.Banka>(1);

                MessageBox.Show(b.Ime);

                s.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void cmdCreateBank_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();


                Entiteti.Banka p = new Entiteti.Banka();

                //p = s.Load<Entiteti.Prodavnica>(81);

                p.Ime = "Postanska stedionica";
                p.Email = "info@postanskabank.rs";
                p.Web_adresa = "https://www.posted.co.rs/";
                p.Adresa_centrale = "Bulevar dr Zorana Đinđića 15v";


                //s.Save(p);
                s.SaveOrUpdate(p);

                s.Flush();
                s.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void cmdReadBankName_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                //Ucitavaju se podaci o prodavnici za zadatim brojem
                Filijala o = s.Load<Filijala>(12);

                MessageBox.Show(o.PripadaBanci.Ime);

                s.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void cmdReadFilijaleList_Click(object sender, EventArgs e)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                //Ucitavaju se podaci o prodavnici za zadatim brojem
                ATM_WinForm.Entiteti.Banka b = s.Load<ATM_WinForm.Entiteti.Banka>(1);

                foreach (Filijala f in b.Filijala)
                {
                    MessageBox.Show(f.Adresa + " " + f.Br_telefona);
                }

                s.Close();
            }
            catch (Exception ec)
            {
                MessageBox.Show(ec.Message);
            }
        }

        private void SectionBanka_Click(object sender, EventArgs e)
        {
            var BankaMainForm = new Form_Banka_Main();
            BankaMainForm.ShowDialog();
        }


        private void SectionKlijentiBtn_Click(object sender, EventArgs e)
        {
            var KlijentMainForm = new Form_Klijent_Main(); 
            KlijentMainForm.ShowDialog();
        }

        private void SectionBankomatiBtn_Click(object sender, EventArgs e)
        {
            var BankomatMainForm = new Form_Bankomat_Main();
            BankomatMainForm.ShowDialog();
        }
    }
}

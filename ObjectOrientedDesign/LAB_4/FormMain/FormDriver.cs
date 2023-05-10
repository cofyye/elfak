using FormMain;
using WinFormData;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace Forms
{
    public partial class FormDriver : Form
    {

        private FormCategory formCategory = new FormCategory();
        private FormRestriction formRestriction = new FormRestriction();

        public FormDriver()
        {
            InitializeComponent();
        }

        private void BtnNewCategory_Click(object sender, EventArgs e)
        {
            formCategory.ShowDialog();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnNewRestriction_Click(object sender, EventArgs e)
        {
            formRestriction.ShowDialog();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if(TextBoxName.Text == "" || TextBoxLastName.Text == "" || DTPBirthDate.Text == "" || DTPExpiredDate.Text == "" || DTPUntilDate.Text == "" || TextBoxDriverLicenseNum.Text == "" || TextBoxPlace.Text == "" || ComboBoxGender.Text == "")
            {
                MessageBox.Show("Moraju biti popunjena sva polja !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TextBoxName.Text.All(c => Char.IsLetter(c) && (Char.IsLower(c) || Char.IsUpper(c))))
            {
                MessageBox.Show("Ime mora sadrzati samo mala ili velika slova !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TextBoxLastName.Text.All(c => Char.IsLetter(c) && (Char.IsLower(c) || Char.IsUpper(c))))
            {
                MessageBox.Show("Prezime mora sadrzati samo mala ili velika slova !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TextBoxDriverLicenseNum.Text.All(c => Char.IsDigit(c)))
            {
                MessageBox.Show("Broj vozacke dozvole mora biti u numerickom formatu !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string dayBornDate = DTPBirthDate.Text.Substring(0, 2);
            string monthBornDate = DTPBirthDate.Text.Substring(3, 2);
            string yearBornDate = DTPBirthDate.Text.Substring(6, 4);

            string dayLicenseFromDate = DTPExpiredDate.Text.Substring(0, 2);
            string monthLicenseFromDate = DTPExpiredDate.Text.Substring(3, 2);
            string yearLicenseFromDate = DTPExpiredDate.Text.Substring(6, 4);

            string dayLicenseToDate = DTPUntilDate.Text.Substring(0, 2);
            string monthLicenseToDate = DTPUntilDate.Text.Substring(3, 2);
            string yearLicenseToDate = DTPUntilDate.Text.Substring(6, 4);

            Driver driver = new Driver();

            driver.FirstName = TextBoxName.Text;
            driver.LastName = TextBoxLastName.Text;
            driver.LicenseNumber = TextBoxDriverLicenseNum.Text;
            driver.Gender = ComboBoxGender.Text;
            driver.BornDate = new DateTime(Int32.Parse(yearBornDate), Int32.Parse(monthBornDate), Int32.Parse(dayBornDate));
            driver.LicenseFrom = new DateTime(Int32.Parse(yearLicenseFromDate), Int32.Parse(monthLicenseFromDate), Int32.Parse(dayLicenseFromDate));
            driver.LicenseTo = new DateTime(Int32.Parse(yearLicenseToDate), Int32.Parse(monthLicenseToDate), Int32.Parse(dayLicenseToDate));

            if(!DriverList.Instance.addDriver(driver))
            {
                MessageBox.Show("Vozac postoji u listi !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }
    }
}
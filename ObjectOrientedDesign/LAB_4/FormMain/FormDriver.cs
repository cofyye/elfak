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

        private readonly FormCategory formCategory = new();
        private readonly FormRestriction formRestriction = new();
        private readonly List<Category> categories = new();
        private readonly List<Category> restrictions = new();


        public FormDriver()
        {
            InitializeComponent();
            ComboBoxGender.SelectedIndex = 0;
            ComboBoxGender.DropDownStyle = ComboBoxStyle.DropDownList;
            LoadCategories();
            LoadRestrictions();
        }

        private void LoadCategories()
        {
            DataGridViewCategories.DataSource = categories.ToList();
        }

        private void LoadRestrictions()
        {
            DataGridViewRestrictions.DataSource = restrictions.ToList();
        }

        private bool IsInCategory(Category category)
        {
            for(int i = 0; i < categories.Count; i++)
            {
                if (categories[i].CategoryValue == category.CategoryValue)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsInRestriction(Category category)
        {
            for (int i = 0; i < restrictions.Count; i++)
            {
                if (restrictions[i].CategoryValue == category.CategoryValue)
                {
                    return true;
                }
            }

            return false;
        }

        private void EmptyFields()
        {
            PictureBoxAvatar.ImageLocation = null;
            TextBoxDriverLicenseNum.Text = "";
            TextBoxName.Text = "";
            TextBoxLastName.Text = "";
            TextBoxPlace.Text = "";
            DTPBirthDate.Text = DateTime.Now.ToString();
            DTPUntilDate.Text = DateTime.Now.ToString();
            DTPExpiredDate.Text = DateTime.Now.ToString();
            ComboBoxGender.SelectedIndex = 0;
            categories.Clear();
            DataGridViewCategories.DataSource = categories.ToList();
            restrictions.Clear();
            DataGridViewRestrictions.DataSource = restrictions.ToList();
        }

        private void BtnNewCategory_Click(object sender, EventArgs e)
        {
            formCategory.ShowDialog();

            Category cat = formCategory.Category;

            if (cat.CategoryValue != null && cat.DateFrom != null && cat.DateTo != null)
            {
                if(IsInCategory(cat))
                {
                    MessageBox.Show("Vec imate ovu kategoriju !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                categories.Add(cat);
            }

            LoadCategories();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            EmptyFields();
            this.Close();
        }

        private void BtnNewRestriction_Click(object sender, EventArgs e)
        {

            if(categories.Count < 1)
            {
                MessageBox.Show("Morate dodati barem jednu kategoriju !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formRestriction.InjectCategories(categories);
            formRestriction.ShowDialog();

            Category cat = formRestriction.Restriction;

            if (cat.CategoryValue != null && cat.DateFrom != null && cat.DateTo != null)
            {
                if (IsInRestriction(cat))
                {
                    MessageBox.Show("Vec imate ovu zabranu !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                restrictions.Add(cat);
            }

            LoadRestrictions();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (TextBoxName.Text == "" || TextBoxLastName.Text == "" || DTPBirthDate.Text == "" || DTPExpiredDate.Text == "" || DTPUntilDate.Text == "" || TextBoxDriverLicenseNum.Text == "" || TextBoxPlace.Text == "" || ComboBoxGender.Text == "")
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

            if (PictureBoxAvatar.ImageLocation == null)
            {
                MessageBox.Show("Niste uneli sliku !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            Driver driver = new()
            {
                FirstName = TextBoxName.Text,
                LastName = TextBoxLastName.Text,
                LicenseNumber = TextBoxDriverLicenseNum.Text,
                Place = TextBoxPlace.Text,
                Gender = ComboBoxGender.Text,
                Photo = PictureBoxAvatar.ImageLocation,
                BornDate = new DateTime(Int32.Parse(yearBornDate), Int32.Parse(monthBornDate), Int32.Parse(dayBornDate)),
                LicenseFrom = new DateTime(Int32.Parse(yearLicenseFromDate), Int32.Parse(monthLicenseFromDate), Int32.Parse(dayLicenseFromDate)),
                LicenseTo = new DateTime(Int32.Parse(yearLicenseToDate), Int32.Parse(monthLicenseToDate), Int32.Parse(dayLicenseToDate)),
                Categories = categories,
                Restrctions = restrictions
            };

            if (!DriverList.Instance.AddDriver(driver))
            {
                MessageBox.Show("Vozac postoji u listi !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show(driver.Categories.Count.ToString() + " - " + driver.Restrctions.Count.ToString());

            EmptyFields();
            this.Close();
        }

        private void ButtonPicture_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PictureBoxAvatar.ImageLocation = openFileDialog1.FileName;
            }
        }
    }
}
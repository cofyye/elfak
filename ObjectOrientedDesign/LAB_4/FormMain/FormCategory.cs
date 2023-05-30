using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormData;

namespace FormMain
{
    public partial class FormCategory : Form
    {
        private readonly Category category = new();
        public FormCategory()
        {
            InitializeComponent();
            ComboBoxCategories.SelectedIndex = 0;
            ComboBoxCategories.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void EmptyFields()
        {
            ComboBoxCategories.SelectedIndex = 0;
            DTPDateFrom.Text = DateTime.Now.ToString();
            DTPDateTo.Text = DateTime.Now.ToString();
        }

        public Category Category
        {
            get
            {
                Category cat = new(category.CategoryValue, category.DateFrom, category.DateTo);
                category.CategoryValue = "";
                category.DateFrom = "";
                category.DateTo = "";
                return cat;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            EmptyFields();
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            category.CategoryValue = ComboBoxCategories.Text;
            category.DateFrom = DTPDateFrom.Text;
            category.DateTo = DTPDateTo.Text;

            EmptyFields();
            this.Close();
        }
    }
}

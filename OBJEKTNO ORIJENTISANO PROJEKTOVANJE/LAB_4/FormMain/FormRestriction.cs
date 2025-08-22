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
    public partial class FormRestriction : Form
    {
        private readonly Category restriction = new();
        private List<Category> categories = new();
        public FormRestriction()
        {
            InitializeComponent();
            ComboBoxCategories.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void InjectCategories(List<Category> cats)
        {
            categories = cats;

            if (categories.Count > 0) return;

            for(int i = 0; i < categories.Count; i++)
            {
                ComboBoxCategories.Items.Add(categories[i].CategoryValue);
            }

            if(categories.Count > 0)
            {
                ComboBoxCategories.SelectedIndex = 0;
            }
        }

        private void EmptyFields()
        {
            ComboBoxCategories.Items.Clear();
            DTPDateFrom.Text = DateTime.Now.ToString();
            DTPDateTo.Text = DateTime.Now.ToString();
        }

        public Category Restriction
        {
            get
            {
                Category res = new(restriction.CategoryValue, restriction.DateFrom, restriction.DateTo);
                restriction.CategoryValue = "";
                restriction.DateFrom = "";
                restriction.DateTo = "";
                return res;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            EmptyFields();
            this.Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            restriction.CategoryValue = ComboBoxCategories.Text;
            restriction.DateFrom = DTPDateFrom.Text;
            restriction.DateTo = DTPDateTo.Text;

            EmptyFields();
            this.Close();
        }
    }
}

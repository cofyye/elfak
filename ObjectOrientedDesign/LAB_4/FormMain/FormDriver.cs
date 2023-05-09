using FormMain;

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
    }
}
namespace Forms
{
    public partial class FormMain : Form
    {
        private FormDriver formDriver = new FormDriver();
        public FormMain()
        {
            InitializeComponent();
        }

        private void BtnAddDriver_Click(object sender, EventArgs e)
        {
            formDriver.ShowDialog();
        }
    }
}
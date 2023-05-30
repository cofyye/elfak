using System.Windows.Forms;
using WinFormData;

namespace Forms
{
    public partial class FormMain : Form
    {
        private readonly FormDriver formDriver = new();
        private void LoadDrivers()
        {
            DataGridViewDrivers.DataSource = DriverList.Instance.DriverListValues.ToList();
        }

        public FormMain()
        {
            InitializeComponent();
            ComboBoxSort.SelectedIndex = 0;
            ComboBoxSort.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BtnAddDriver_Click(object sender, EventArgs e)
        {
            formDriver.ShowDialog();
            LoadDrivers();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da zelite da zatvorite program?", "Zatvaranje programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Start();
            LoadDrivers();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTimeValue.Text = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy.");
        }
    }
}
using System.Windows.Forms;
using WinFormData;
using static System.Windows.Forms.DataFormats;

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
            formDriver.FormClosed += FormDriverClosedEventHandler;
            formDriver.ShowDialog();
        }

        private void FormDriverClosedEventHandler(object sender, FormClosedEventArgs e)
        {
            string firstName = DriverList.Instance.DriverListValues[DriverList.Instance.DriverListValues.Count - 1].FirstName;
            string lastName = DriverList.Instance.DriverListValues[DriverList.Instance.DriverListValues.Count - 1].LastName;
            string licenseNumber = DriverList.Instance.DriverListValues[DriverList.Instance.DriverListValues.Count - 1].LicenseNumber;

            DataGridViewDrivers.Rows.Add(firstName, lastName, licenseNumber);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da zelite da zatvorite program?", "Zatvaranje programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
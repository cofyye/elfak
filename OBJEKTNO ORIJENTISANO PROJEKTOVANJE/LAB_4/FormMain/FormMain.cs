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

        private void SortList(List<Driver> drivers, int value)
        {
            if (value == 1)
                Sort(DriverList.Instance.DriverListValues, 1);
            if (value == 2)
                Sort(DriverList.Instance.DriverListValues, 2);
            if (value == 3)
                Sort(DriverList.Instance.DriverListValues, 3);
        }

        private void Sort(List<Driver> drivers, int value)
        {
            switch (value)
            {
                case 1:
                    drivers.Sort((v1, v2) => v1.LicenseNumber.CompareTo(v2.LicenseNumber));
                    break;
                case 2:
                    drivers.Sort((v1, v2) => v1.FirstName.CompareTo(v2.FirstName));
                    break;
                case 3:
                    drivers.Sort((v1, v2) => v1.LastName.CompareTo(v2.LastName));
                    break;
            }
        }

        public FormMain()
        {
            InitializeComponent();
            ComboBoxSort.SelectedIndex = 0;
            ComboBoxSort.DropDownStyle = ComboBoxStyle.DropDownList;
            DriverList.Instance.SortListDelegate = new DriverList.SortDelegate(this.SortList);
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

        private void BtnDeleteDriver_Click(object sender, EventArgs e)
        {
            if (DataGridViewDrivers.SelectedRows.Count < 1)
            {
                MessageBox.Show("Morate izabrati nekog vozaca kako bi ga izbrisali !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = DataGridViewDrivers.SelectedRows[0].Index;

            if (!DriverList.Instance.DeleteDriver(DriverList.Instance.DriverListValues.ElementAt(index).LicenseNumber))
            {
                MessageBox.Show("Greska prilikom brisanja vozaca !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadDrivers();
        }

        private void BtnChangeDriver_Click(object sender, EventArgs e)
        {
            if (DataGridViewDrivers.SelectedRows.Count < 1)
            {
                MessageBox.Show("Morate izabrati nekog vozaca kako bi ga izmenili !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = DataGridViewDrivers.SelectedRows[0].Index;

            FormDriver.IsEditable = true;
            FormDriver.EditableIndex = index;
            formDriver.InjectDriver(DriverList.Instance.DriverListValues.ElementAt(index));
            formDriver.ShowDialog();
            LoadDrivers();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            int value;
            if (ComboBoxSort.SelectedItem.ToString() == "Br Vozacke Dozvole")
                value = 1;
            else if (ComboBoxSort.SelectedItem.ToString() == "Ime")
                value = 2;
            else
                value = 3;

            DriverList.Instance.SortList(value);
            LoadDrivers();
        }
    }
}
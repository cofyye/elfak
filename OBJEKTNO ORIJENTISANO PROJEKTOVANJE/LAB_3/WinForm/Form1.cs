using System.Text.RegularExpressions;
using WinFormData;

namespace WinForm
{
    public partial class Form1 : Form
    {
        private static Person toChangePerson = null;
        public Form1()
        {
            PersonList.Instance.SortListDelegate = new PersonList.SortDelegate(this.SortList);
            InitializeComponent();
        }

        private void deleteRecords()
        {
            fieldFirstName.Text = "";
            fieldLastName.Text = "";
            fieldPhone.Text = "";
            fieldAddress.Text = "";
            fieldBirthDate.Text = DateTime.Now.ToString();
            toChangePerson = null;
        }

        private bool isCorrectPhone()
        {
            if (!Regex.Match(fieldPhone.Text, "[+][\\d]{3} [\\d]{2} [\\d]{1,10}$").Success)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void loadPersons()
        {
            listPerson.DisplayMember = "FullPerson";
            listPerson.ValueMember = "Phone";
            listPerson.DataSource = PersonList.Instance.PersonListValues.ToList();
        }

        private void SortList(List<Person> persons, int value)
        {

            if (value == 1)
            {
                for (int i = 0; i < persons.Count - 1; i++)
                {
                    for (int j = i + 1; j < persons.Count; j++)
                    {
                        if (string.Compare(persons[i].FirstName, persons[j].FirstName) > 0)
                        {
                            Person temp = persons[i];
                            persons[i] = persons[j];
                            persons[j] = temp;
                        }
                    }
                }
            }
            else if (value == 2)
            {
                for (int i = 0; i < persons.Count - 1; i++)
                {
                    for (int j = i + 1; j < persons.Count; j++)
                    {
                        if (string.Compare(persons[i].LastName, persons[j].LastName) > 0)
                        {
                            Person temp = persons[i];
                            persons[i] = persons[j];
                            persons[j] = temp;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < persons.Count - 1; i++)
                {
                    for (int j = i + 1; j < persons.Count; j++)
                    {
                        if (persons[i].Date > persons[j].Date)
                        {
                            Person temp = persons[i];
                            persons[i] = persons[j];
                            persons[j] = temp;
                        }
                    }
                }
            }
        }

        private void FieldFirstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == '\b')
            {
            }
            else
            {
                MessageBox.Show("Moguc je samo unos slova ! Vas unos je : " + e.KeyChar, "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void FieldLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || e.KeyChar == '\b')
            {
            }
            else
            {
                MessageBox.Show("Moguc je samo unos slova ! Vas unos je : " + e.KeyChar, "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (fieldFirstName.Text == "" || fieldLastName.Text == "" || fieldPhone.Text == "" || fieldBirthDate.Text == "" || fieldAddress.Text == "")
            {
                MessageBox.Show("Polja ne smeju biti prazna !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isCorrectPhone())
            {
                MessageBox.Show("Broj telefona nije u dobrom formatu. Mora biti +381 11 1234567890", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string day = fieldBirthDate.Text.Substring(0, 2);
            string month = fieldBirthDate.Text.Substring(3, 2);
            string year = fieldBirthDate.Text.Substring(6, 4);

            Person person = new Person();

            person.FirstName = fieldFirstName.Text;
            person.LastName = fieldLastName.Text;
            person.Phone = fieldPhone.Text;
            person.Address = fieldAddress.Text;
            person.Date = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));

            if (PersonList.Instance.isInList(person) && toChangePerson == null)
            {
                MessageBox.Show("Osoba vec postoji u listi", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (toChangePerson != null)
            {
                toChangePerson.FirstName = fieldFirstName.Text;
                toChangePerson.LastName = fieldLastName.Text;
                toChangePerson.Phone = fieldPhone.Text;
                toChangePerson.Address = fieldAddress.Text;
                toChangePerson.Date = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                MessageBox.Show("Uspesno ste izmenili osobu !", "Uspesno !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                PersonList.Instance.addPerson(person);
                MessageBox.Show("Uspesno ste dodali novu osobu !", "Uspesno !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.deleteRecords();
            this.ActiveControl = fieldFirstName;
            this.loadPersons();
            toChangePerson = null;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.deleteRecords();
            MessageBox.Show("Ponistili ste sva polja !", "Uspesno !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void DeletePersonListBtn_Click(object sender, EventArgs e)
        {
            PersonList.Instance.PersonListValues.Clear();
            MessageBox.Show("Uspesno ste izbrisali celu listu !", "Uspesno !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.loadPersons();
        }

        private void DeletePersonBtn_Click(object sender, EventArgs e)
        {
            String phone = listPerson.SelectedValue as String;
            if (!PersonList.Instance.deletePerson(phone))
            {
                MessageBox.Show("Osoba ne postoji !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Uspesno ste izbrisali osobu !", "Uspesno !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.loadPersons();
        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Vreme : " + DateTime.Now.ToString("dd.MM.yyyy. HH:mm"), "Trenutno vreme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ListPerson_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String phone = listPerson.SelectedValue as String;

            Person person = PersonList.Instance.getPerson(phone);

            if (person == null)
            {
                MessageBox.Show("Niste izabrali osobu !", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fieldFirstName.Text = person.FirstName;
            fieldLastName.Text = person.LastName;
            fieldBirthDate.Text = person.Date.ToString("dd.MM.yyyy.");
            fieldPhone.Text = person.Phone;
            fieldAddress.Text = person.Address;

            toChangePerson = person;
        }

        private void FieldPhone_Leave(object sender, EventArgs e)
        {
            if (!isCorrectPhone())
            {
                MessageBox.Show("Broj telefona nije u dobrom formatu. Mora biti +381 11 1234567890", "Greska !", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da zelite da zatvorite program?", "Zatvaranje programa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void SortListBtn_Click(object sender, EventArgs e)
        {
            int value = 1;
            if (fieldSortListValue.SelectedItem.ToString() == "PO IMENU")
            {
                value = 1;
            }
            else if (fieldSortListValue.SelectedItem.ToString() == "PO PREZIMENU")
            {
                value = 2;
            }
            else
            {
                value = 3;
            }

            PersonList.Instance.SortList(value);
            this.loadPersons();
        }

        private void ShowTimeBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vreme : " + DateTime.Now.ToString("dd.MM.yyyy. HH:mm"), "Trenutno vreme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
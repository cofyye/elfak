using System;

namespace WinFormData
{
    public class Person : Object
    {
        #region Atributes

        String firstName;
        String lastName;
        String phone;
        String address;
        DateTime date;

        #endregion

        #region Properties

        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }


        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public String FullPerson
        {
            get
            {
                return "Ime: " + firstName + ", Prezime: " + lastName + ", Tel: " + phone + ", Adresa: " + address + ", Datum: " + date.ToString("dd.MM.yyyy.");
            }
        }

        #endregion

        #region Constructors

        public Person()
        {
        }

        public Person(String firstName, String lastName, String phone, String address, DateTime date)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.address = address;
            this.date = date;
        }

        #endregion
    }
}

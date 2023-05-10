using System;

namespace WinFormData
{
    public class Driver : Object
    {
        #region Atributes

        String firstName;
        String lastName;
        String licenseNumber;
        String place;
        String gender;
        DateTime bornDate;
        DateTime licenseFrom;
        DateTime licenseTo;

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

        public String LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }

        public String Place
        {
            get { return place; }
            set { place = value; }
        }
        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public DateTime BornDate
        {
            get { return bornDate; }
            set { bornDate = value; }
        }

        public DateTime LicenseFrom
        {
            get { return licenseFrom; }
            set { licenseFrom = value; }
        }

        public DateTime LicenseTo
        {
            get { return licenseTo; }
            set { licenseTo = value; }
        }

        #endregion

        #region Constructors

        public Driver()
        {
        }

        public Driver(String firstName, String lastName, String licenseNumber, String place, String gender, DateTime bornDate, DateTime licenseFrom, DateTime licenseTo)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.licenseNumber = licenseNumber;
            this.place = place;
            this.gender = gender;
            this.bornDate = bornDate;
            this.licenseFrom = licenseFrom;
            this.licenseTo = licenseTo;
        }
        #endregion
    }
}
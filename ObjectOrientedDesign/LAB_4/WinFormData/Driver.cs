using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WinFormData
{
    public class Driver : Object
    {
        #region Atributes

        private String firstName;
        private String lastName;
        private String licenseNumber;
        private String place;
        private String gender;
        private String photo;
        private DateTime bornDate;
        private DateTime licenseFrom;
        private DateTime licenseTo;
        private List<Category> categories;
        private List<Category> restrictions;

        #endregion

        #region Properties

        [DisplayName("Ime")]
        public String FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DisplayName("Prezime")]
        public String LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [DisplayName("Br Vozacke Dozvole")]
        public String LicenseNumber
        {
            get { return licenseNumber; }
            set { licenseNumber = value; }
        }

        [Browsable(false)]
        public String Photo
        {
            get { return photo; }
            set { photo = value; }
        }

        [Browsable(false)]
        public String Place
        {
            get { return place; }
            set { place = value; }
        }

        [Browsable(false)]
        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        [Browsable(false)]
        public DateTime BornDate
        {
            get { return bornDate; }
            set { bornDate = value; }
        }

        [Browsable(false)]
        public DateTime LicenseFrom
        {
            get { return licenseFrom; }
            set { licenseFrom = value; }
        }

        [Browsable(false)]
        public DateTime LicenseTo
        {
            get { return licenseTo; }
            set { licenseTo = value; }
        }

        [Browsable(false)]
        public List<Category> Categories
        {
            get { return categories; }
            set { categories = value; }
        }

        [Browsable(false)]
        public List<Category> Restrctions
        {
            get { return restrictions; }
            set { restrictions = value; }
        }

        #endregion

        #region Constructors

        public Driver()
        {
            this.categories = new List<Category>();
            this.restrictions = new List<Category>();
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
            this.categories = new List<Category>();
            this.restrictions = new List<Category>();
        }
        #endregion
    }
}
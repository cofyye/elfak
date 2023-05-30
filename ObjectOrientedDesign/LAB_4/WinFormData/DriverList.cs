using System;
using System.Collections.Generic;

namespace WinFormData
{
    public class DriverList
    {
        public delegate void SortDelegate(List<Driver> drivers);
        public SortDelegate SortListDelegate { get; set; }

        #region Atributes

        private readonly List<Driver> drivers;

        #endregion

        #region Properties
        public List<Driver> DriverListValues
        {
            get
            {
                return drivers;
            }
        }
        #endregion

        #region Constructors

        private DriverList()
        {
            drivers = new List<Driver>();
        }

        #endregion

        #region Methodes

        public bool AddDriver(Driver driver)
        {
            if (IsInList(driver))
                return false;

            drivers.Insert(0, driver);
            return true;
        }

        public bool AddCategory(Driver driver, List<Category> categories)
        {
            driver.Categories = categories;
            return true;
        }

        public bool DeletePerson(Driver driver)
        {
            if (!IsInList(driver))
                return false;

            drivers.Remove(driver);
            return true;
        }

        public bool DeleteDriver(String licenseNumber)
        {
            Driver tmp = null;

            foreach (var v in drivers)
            {
                if (v.LicenseNumber == licenseNumber)
                {
                    tmp = v;
                    break;
                }
            }

            if (tmp != null)
            {
                drivers.Remove(tmp);
                return true;
            }

            return false;
        }

        public bool IsInList(Driver driver)
        {
            foreach (var v in drivers)
            {
                if (v.LicenseNumber == driver.LicenseNumber)
                    return true;
            }

            return false;
        }

        public bool IsInList(String licenseNumber)
        {
            foreach (var v in drivers)
            {
                if (v.LicenseNumber == licenseNumber)
                    return true;
            }

            return false;
        }

        public Driver GetDriver(String licenseNumber)
        {
            foreach (var v in drivers)
            {
                if (v.LicenseNumber == licenseNumber)

                    return v;
            }

            return null;
        }

        public void SortList(int value)
        {
            if (SortListDelegate != null)
            {
                SortListDelegate(drivers);
            }
        }

        #endregion


        private static DriverList _instance = null;
        public static DriverList Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DriverList();

                return _instance;
            }
        }
    }
}
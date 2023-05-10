using System;
using System.Collections.Generic;

namespace WinFormData
{
    public class DriverList
    {
        public delegate void SortDelegate(List<Driver> drivers);
        public SortDelegate SortListDelegate { get; set; }

        #region Atributes

        private List<Driver> drivers;

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

        public bool addDriver(Driver driver)
        {
            if (isInList(driver))
                return false;

            drivers.Add(driver);
            return true;
        }

        public bool deletePerson(Driver driver)
        {
            if (!isInList(driver))
                return false;

            drivers.Remove(driver);
            return true;
        }

        public bool deleteDriver(String licenseNumber)
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

        public bool isInList(Driver driver)
        {
            foreach (var v in drivers)
            {
                if (v.LicenseNumber == driver.LicenseNumber)
                    return true;
            }

            return false;
        }

        public bool isInList(String licenseNumber)
        {
            foreach (var v in drivers)
            {
                if (v.LicenseNumber == licenseNumber)
                    return true;
            }

            return false;
        }

        public Driver getDriver(String licenseNumber)
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
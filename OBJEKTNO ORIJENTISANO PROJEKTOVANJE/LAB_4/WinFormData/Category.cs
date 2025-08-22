using System;
using System.ComponentModel;

namespace WinFormData
{
    public class Category : Object
    {
        #region Atributes

        String categoryValue;
        String dateFrom;
        String dateTo;

        #endregion

        #region Properties

        [DisplayName("Kategorija")]
        public String CategoryValue
        {
            get { return categoryValue; }
            set { categoryValue = value; }
        }

        [DisplayName("Datum Od")]
        public String DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; }
        }

        [DisplayName("Datum Do")]
        public String DateTo
        {
            get { return dateTo; }
            set { dateTo = value; }
        }
        #endregion

        #region Constructors

        public Category()
        {
        }

        public Category(String categoryValue, String dateFrom, String dateTo)
        {
            this.categoryValue = categoryValue;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }
        #endregion
    }
}
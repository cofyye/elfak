using System;

namespace WinFormData
{
    public class Category : Object
    {
        #region Atributes

        String firstName;
        String lastName;


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
        #endregion

        #region Constructors

        public Category()
        {
        }
        #endregion
    }
}
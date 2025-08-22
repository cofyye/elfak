using System;
using System.Collections.Generic;

namespace WinFormData
{
    public class PersonList
    {
        public delegate void SortDelegate(List<Person> persons, int value);
        public SortDelegate SortListDelegate { get; set; }

        #region Atributes

        private List<Person> persons;

        #endregion

        #region Properties
        public List<Person> PersonListValues
        {
            get
            {
                return persons;
            }
        }
        #endregion

        #region Constructors

        private PersonList()
        {
            persons = new List<Person>();
        }

        #endregion

        #region Methodes

        public bool addPerson(Person person)
        {
            if (isInList(person))
                return false;

            persons.Insert(0, person);
            return true;
        }

        public bool deletePerson(Person person)
        {
            if (!isInList(person))
                return false;

            persons.Remove(person);
            return true;
        }

        public bool deletePerson(String phone)
        {
            Person tmp = null;

            foreach (var v in persons)
            {
                if (v.Phone == phone)
                {
                    tmp = v;
                    break;
                }
            }

            if (tmp != null)
            {
                persons.Remove(tmp);
                return true;
            }

            return false;
        }

        public bool isInList(Person person)
        {
            foreach (var v in persons)
            {
                if (v.Phone == person.Phone)
                    return true;
            }

            return false;
        }

        public bool isInList(String phone)
        {
            foreach (var v in persons)
            {
                if (v.Phone == phone)
                    return true;
            }

            return false;
        }

        public Person getPerson(String phone)
        {
            foreach (var v in persons)
            {
                if (v.Phone == phone)

                    return v;
            }

            return null;
        }

        public void SortList(int value)
        {
            if (SortListDelegate != null)
            {
                SortListDelegate(persons, value);
            }
        }

        #endregion


        private static PersonList _instance = null;
        public static PersonList Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PersonList();

                return _instance;
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ind_Task_7
{
    public class Person : IComparable<Person>
    {
        public static int count = 0;

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //public Person()
        //{
        //    FirstName = "First Name";
        //    LastName = "Last Name";
        //    count++;
        //    ID = count;
        //}

        public Person(string fName, string lName)
        {
            FirstName = fName;
            LastName = lName;
            count++;
            ID = count;
        }

        public int CompareTo(Person obj)
        {
            string this_full_name = LastName.ToLower() + " " + FirstName.ToLower();
            string obj_full_name = obj.LastName.ToLower() + " " + obj.FirstName.ToLower();

            if (this_full_name.CompareTo(obj_full_name) > 0)
            {
                return 1;
            }
            else if (this_full_name.CompareTo(obj_full_name) < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}

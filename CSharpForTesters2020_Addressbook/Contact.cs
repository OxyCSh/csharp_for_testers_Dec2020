using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = "";
        public string Address { get; set; }
        public string Photo { get; set; } = "C:\\bunny.png";
        public int DayOfBirth { get; set; }
        public string MonthOfBirth { get; set; }
        public int YearOfBirth { get; set; }
        public string ContactGroup { get; set; }

        public Contact(string firstName)
        {
            FirstName = firstName;
        }
    }
}

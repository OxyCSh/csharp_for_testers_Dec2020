using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Photo { get; set; } = "C:\\bunny.png";
        public int DayOfBirth { get; set; } = 1;
        public string MonthOfBirth { get; set; } = "January";
        public int YearOfBirth { get; set; } = 2000;
        public string ContactGroup { get; set; } = null;

        public Contact(string firstName)
        {
            FirstName = firstName;
        }
    }
}

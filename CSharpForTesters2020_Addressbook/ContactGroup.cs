using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    class ContactGroup
    {
        //private string name;
        //private string header = "";
        //private string footer = "";

        public ContactGroup(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Header { get; set; } = "";
        public string Footer { get; set; } = "";
    }
}

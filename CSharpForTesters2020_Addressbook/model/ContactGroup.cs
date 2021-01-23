using System;

namespace AddressbookWebTests
{
    public class ContactGroup : IEquatable<ContactGroup> // that means that 
        // a comparison method will be implemented to compare objects of ContactGroup
    {
        //private string name;
        //private string header = "";
        //private string footer = "";

        public ContactGroup(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Header { get; set; } = null;
        public string Footer { get; set; } = null;

        // an override of a standard method
        // a comparison method that defines how objects of ContactGroup should be compared
        public bool Equals(ContactGroup other)
        {
            // if 'other' is null,
            if (Object.ReferenceEquals(other, null))
            { 
                return false; // because the current object exists and is not null
            }

            // if the current object and 'other' is the same object
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }

        /* the first step in the comparison is to compare hash codes
         if they are different, then the objects are not equal and the Equals method is skipped
        if the same, then objects are compared by the Equals method*/
        // an override of a standard method
        public int GetHashCode()
        {
            return Name.GetHashCode();

            // an option is to always return 0
            // then hash codes are not compared and the Equals method is always called
            //return 0;
        }

        // in automated web testing implementing GetHashCode won't speed up the execution
        // because the browser speed and browser<->web server communication speed is slow anyway
    }
}

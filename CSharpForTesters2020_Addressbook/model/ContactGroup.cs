using System;

namespace AddressbookWebTests
{
    // class implements these two interfaces
    public class ContactGroup : IEquatable<ContactGroup>, IComparable<ContactGroup>
    // IEquatable means that a comparison method will be implemented to compare objects of ContactGroup
    // IComparable so we can implement own CompareTo method 
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
            // a standard check: if 'other' is null,
            if (Object.ReferenceEquals(other, null))
            { 
                return false; // because the current object exists and is not null
            }

            // a standard check: if the current object and 'other' is the same object
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            // our own implementation
            return Name == other.Name;
        }

        /* the first step in the comparison is to compare hash codes
         if they are different, then the objects are not equal and the Equals method is skipped
        if the same, then objects are compared by the Equals method*/
        // an override of a standard method
        public override int GetHashCode() // override means the method overrrides the standard method
        {
            return Name.GetHashCode();

            // an option is to always return 0
            // then hash codes are not compared and the Equals method is always called
            //return 0;
        }

        // in automated web testing implementing GetHashCode won't speed up the execution
        // because the browser speed and browser<->web server communication speed is slow anyway

        // it's an overrride of a standard method - a string representative of the instance
        // so if a test fails we can see the group name in test output or when debugging
        public override string ToString()
        {
            return "group name = " + Name; // returns the string representation of a Contact Group object
        }

        /* by default the standard Sort method uses the default comparer
         here we redefine how the default comparer should work for Contact Group
        we want it to compare group names*/
        public int CompareTo(ContactGroup other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1; // return 1 because the current object (this) is bigger
            }

            return Name.CompareTo(other.Name);
        }
    }
}

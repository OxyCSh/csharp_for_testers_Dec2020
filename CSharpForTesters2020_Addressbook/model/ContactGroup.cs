using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressbookWebTests
{
    [Table(Name = "group_list")] // to map this class/object model to a table from the DB
    // class implements these two interfaces
    public class ContactGroup : IEquatable<ContactGroup>, IComparable<ContactGroup>
    // IEquatable means that a comparison method will be implemented to compare objects of ContactGroup
    // IComparable so we can implement own CompareTo method 
    {
        //private string name;
        //private string header = "";
        //private string footer = "";

        // empty constructor is required by XML serializer
        public ContactGroup()
        {
        }

        public ContactGroup(string name)
        {
            Name = name;
        }

        // to map properties to the table columns
        [Column(Name = "group_name"), NotNull] // NotNull can be used if writing to a DB so it's known before we attempt to write to the DB
        public string Name { get; set; }

        [Column(Name = "group_header")]
        public string Header { get; set; } = null;

        [Column(Name = "group_footer")]
        public string Footer { get; set; } = null;

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }


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
        if they are different, then the objects are not equal and the slow Equals method is skipped
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
            return "name = " + Name + "\nheader = " + Header + "\nfooter = " + Footer; // returns the string representation of a Contact Group object
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

        public static List<ContactGroup> GetAllGroupsFromDB()
        {
            // option 1 - opens a connection and closes at the end (the same can be used when working with files)
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Groups select g).ToList();
            }

            // option 2
            //AddressbookDB db = new AddressbookDB(); // to establish connection to the DB
            //List<ContactGroup> groupsFromDB = (from g in db.Groups select g).ToList(); // using LINQ
            //db.Close();
        }

        public List<Contact> GetGroupContactsFromDB()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts
                        from groupContactRelation in db.GroupContactRelation.Where(p => p.GroupId == Id && p.ContactId == c.Id && c.Deprecated == "0000-00-00 00:00:00")
                        select c).Distinct().ToList();
            }
        }
    }
}

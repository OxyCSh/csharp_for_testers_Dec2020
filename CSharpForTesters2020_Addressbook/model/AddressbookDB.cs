using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
why work with a DB
1 to speed up the test execution when working with a lot of data
getting data from UI is very slow and it's much faster to get data directly from the DB
2 from a DB we can get all info at one
(all records from the DB when in the UI we might need to go through several pages;
and all details from the DB when in the UI we might need to navigate to Details view to get all the details)
but there is a risk that even if the DB stores data correctly
we might miss a bug in the UI that displays this data wrong
use sparingly, the majority of tests should use the UI/API

installed linq2DB for MySQL library package
copied system.data and connectionStrings to app.config
DbProviderFactories in system.data creates a DB connection
connectionStrings has the DB name and how to connect to the DB including login
 */


namespace AddressbookWebTests
{
    public class AddressbookDB : LinqToDB.Data.DataConnection // to make the class represent a connection with a DB
    {
        // a constructor that will establish a connection with a particular DB
        // connectionString name from app.config
        public AddressbookDB() : base("AddressBook") { }

        // a method for each table that returns a table of data
        public ITable<ContactGroup> Groups
        {
            get { return GetTable<ContactGroup>(); }
        }

        public ITable<Contact> Contacts
        {
            get { return GetTable<Contact>(); }
        }

        public ITable<GroupContactRelation> GroupContactRelation
        {
            get { return GetTable<GroupContactRelation>(); }
        }
    }
}

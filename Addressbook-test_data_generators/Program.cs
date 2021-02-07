using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressbookWebTests;
using Newtonsoft.Json;

/*
 two projects in the same solution are independent
they don't see each other and can't use each other's methods (TestBase.GenerateRandomString)
until
1 we build dependencies (the generator project - menu - Build Dependencies - Project Dependencies -
depends on the test project) so that the build happens in the right order
2 in References we add reference to the test project under Projects/Solution
3 add using AddressbookWebTests;
*/

namespace Addressbook_test_data_generators
{
    /*
    a generator that generates and saves random test data to a file so the tests can be re-run
    multiple times with exactly the same test data, e. g. when debugging
    (if using a on-the-fly generator, new test data would be generated each run)
    */
    class Program
    {

        // the exe file C:\Users\oniva\source\repos\OxyCSh\csharp_for_testers_Dec2020\Addressbook-test_data_generators\bin\Debug
        // to run open the console, navigate to this directory and run the exe file like
        // Addressbook-test_data_generators.exe groups 17 groups.csv csv
        // Addressbook-test_data_generators.exe contacts 8 contacts.xml xml
        // file created in Addressbook-test_data_generators\bin\Debug
        static void Main(string[] args)
        {
            string type = args[0]; // type of test data - groups or contacts
            int count = Convert.ToInt32(args[1]); // the 1st parameter is the number of test data records we want to generate
            StreamWriter writer = new StreamWriter(args[2]); // the 2nd parameter is the filename
            string format = args[3]; // file format

            if (type == "groups")
            {
                List<ContactGroup> groups = generateGroups(count);

                if (format == "csv")
                    writeGroupsToCSVFile(groups, writer);
                else if (format == "xml")
                    writeGroupsToXMLFile(groups, writer);
                else if (format == "json")
                    writeGroupsToJSONFile(groups, writer);
                else
                    System.Console.Out.WriteLine("Unrecognised format " + format);
            }
            else if (type == "contacts")
            {
                List<Contact> contacts = generateContacts(count);

                if (format == "xml")
                    writeContactsToXMLFile(contacts, writer);
                else if (format == "json")
                    writeContactsToJSONFile(contacts, writer);
                else
                    System.Console.Out.WriteLine("Unrecognised format " + format);
            }
            else
                System.Console.Out.WriteLine("Unrecognised type " + type);

            writer.Close();
        }

        static List<ContactGroup> generateGroups(int count)
        {
            List<ContactGroup> groups = new List<ContactGroup>();

            for (int i = 0; i < count; i++)
            {
                groups.Add(new ContactGroup(TestBase.GenerateRandomString(5))
                {
                    Header = TestBase.GenerateRandomString(10),
                    Footer = TestBase.GenerateRandomString(10)
                });
            }

            return groups;
        }

        static List<Contact> generateContacts(int count)
        {
            List<Contact> contacts = new List<Contact>();

            for (int i = 0; i < count; i++)
            {
                DateTime date = TestBase.GenerateRandomDate(1970, 2010);

                contacts.Add(new Contact(TestBase.GenerateRandomString(5))
                {
                    LastName = TestBase.GenerateRandomString(10),
                    Address = TestBase.GenerateRandomString(10),
                    DayOfBirth = date.Day,
                    MonthOfBirth = date.Month,
                    YearOfBirth = date.Year
                });
            }

            return contacts;
        }

        static void writeGroupsToCSVFile(List<ContactGroup> groups, StreamWriter writer)
        {
            foreach (ContactGroup group in groups)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void writeGroupsToXMLFile(List<ContactGroup> groups, StreamWriter writer)
        {
            // typeof to specify the type of data to be created
            new XmlSerializer(typeof(List<ContactGroup>)).Serialize(writer, groups);
        }

        static void writeGroupsToJSONFile(List<ContactGroup> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented)); // formatting is optional
        }

        static void writeContactsToXMLFile(List<Contact> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<Contact>)).Serialize(writer, contacts);
        }

        static void writeContactsToJSONFile(List<Contact> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}

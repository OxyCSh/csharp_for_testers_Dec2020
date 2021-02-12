using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactCreationTests : ContactTestBase
    {
        public static IEnumerable<Contact> RandomContactDataProvider()
        {
            List<Contact> contacts = new List<Contact>();

            for (int i = 0; i < 3; i++)
            {
                DateTime date = GenerateRandomDate(1980, 2019);

                contacts.Add(new Contact(GenerateRandomString(30))
                {
                    LastName = GenerateRandomString(50),
                    Address = GenerateRandomString(100),
                    DayOfBirth = date.Day,
                    MonthOfBirth = date.Month,
                    YearOfBirth = date.Year
                });
            }

            return contacts;
        }

        public static IEnumerable<Contact> ContactDataFromXMLFile()
        {
            return (List<Contact>)
                new XmlSerializer(typeof(List<Contact>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<Contact> ContactDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<Contact>>(File.ReadAllText(@"contacts.json"));
        }


        //[Test, TestCaseSource("RandomContactDataProvider")]
        //[Test, TestCaseSource("ContactDataFromXMLFile")]
        [Test, TestCaseSource("ContactDataFromJSONFile")]
        public void ContactCreationTest(Contact contact)
        {
            /*
            Contact contact = new Contact("John");
            contact.LastName = "Long";
            contact.Address = "Line 1\nLine 2";
            contact.DayOfBirth = new Random().Next(1, 28);
            contact.MonthOfBirth = "November";
            contact.YearOfBirth = new Random().Next(1980, 2019);
            */
            //contact.ContactGroup = "White group";

            List<Contact> oldContacts = Contact.GetAllContactsFromDB();

            application.ContactHelper.CreateContact(contact);

            Assert.AreEqual(oldContacts.Count + 1, application.ContactHelper.NumberOfContacts());

            List<Contact> newContacts = Contact.GetAllContactsFromDB();

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

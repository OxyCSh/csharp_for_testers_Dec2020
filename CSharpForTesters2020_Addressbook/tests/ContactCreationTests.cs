using System;
using System.Collections.Generic;
using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactCreationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            Contact contact = new Contact("John");
            contact.LastName = "Long";
            contact.Address = "Line 1\nLine 2";
            contact.DayOfBirth = new Random().Next(1, 28);
            contact.MonthOfBirth = "November";
            contact.YearOfBirth = new Random().Next(1980, 2019);
            //contact.ContactGroup = "White group";

            List<Contact> oldContacts = application.ContactHelper.GetContactList();

            application.ContactHelper.CreateContact(contact);

            List<Contact> newContacts = application.ContactHelper.GetContactList();

            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

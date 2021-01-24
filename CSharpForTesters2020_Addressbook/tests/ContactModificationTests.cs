using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactModificationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // arrange
            if (application.ContactHelper.NumberOfContacts() == 0)
            {
                Contact contactForModification = new Contact("Tester");
                application.ContactHelper.CreateContact(contactForModification);
            }

            Contact contact = new Contact("Mark");
            contact.LastName = "Ronson";

            List<Contact> oldContacts = application.ContactHelper.GetContactList();

            // act
            application.ContactHelper.ModifyContact(0, contact);

            // assert
            List<Contact> newContacts = application.ContactHelper.GetContactList();

            Assert.AreEqual(oldContacts.Count, newContacts.Count);

            oldContacts[0].FirstName = contact.FirstName;
            oldContacts[0].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

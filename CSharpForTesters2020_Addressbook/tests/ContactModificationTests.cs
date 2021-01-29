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

            int contactToModifyId = 0;

            Contact modifiedContact = oldContacts[contactToModifyId];

            // act
            application.ContactHelper.ModifyContact(contactToModifyId, contact);

            // assert
            Assert.AreEqual(oldContacts.Count, application.ContactHelper.NumberOfContacts());

            List<Contact> newContacts = application.ContactHelper.GetContactList();

            oldContacts[contactToModifyId].FirstName = contact.FirstName;
            oldContacts[contactToModifyId].LastName = contact.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (Contact thisContact in newContacts)
            {
                if (thisContact.Id == modifiedContact.Id)
                {
                    Assert.AreEqual(contact.FirstName, thisContact.FirstName);
                    Assert.AreEqual(contact.LastName, thisContact.LastName);
                }
            }
        }
    }
}

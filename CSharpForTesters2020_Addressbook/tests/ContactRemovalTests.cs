using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // arrange
            if (application.ContactHelper.NumberOfContacts() == 0)
            {
                Contact contact = new Contact("Tester");
                application.ContactHelper.CreateContact(contact);
            }

            List<Contact> oldContacts = Contact.GetAllContactsFromDB();

            int contactToBeRemovedIndex = 0;

            Contact toBeRemoved = oldContacts[contactToBeRemovedIndex];

            // act
            application.ContactHelper.RemoveContact(toBeRemoved);

            // assert
            Assert.AreEqual(oldContacts.Count - 1, application.ContactHelper.NumberOfContacts());

            List<Contact> newContacts = Contact.GetAllContactsFromDB();

            oldContacts.RemoveAt(contactToBeRemovedIndex);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (Contact contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}

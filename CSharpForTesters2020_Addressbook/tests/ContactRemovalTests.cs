using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthenticationTestBase
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

            List<Contact> oldContacts = application.ContactHelper.GetContactList();

            int contactToBeRemovedId = 0;

            Contact toBeRemoved = oldContacts[contactToBeRemovedId];

            // act
            application.ContactHelper.RemoveContact(contactToBeRemovedId);

            // assert
            Assert.AreEqual(oldContacts.Count - 1, application.ContactHelper.NumberOfContacts());

            List<Contact> newContacts = application.ContactHelper.GetContactList();

            oldContacts.RemoveAt(contactToBeRemovedId);

            Assert.AreEqual(oldContacts, newContacts);

            foreach (Contact contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}

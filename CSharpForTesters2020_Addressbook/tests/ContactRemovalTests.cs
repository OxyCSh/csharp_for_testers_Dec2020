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

            // act
            application.ContactHelper.RemoveContact(0);

            Assert.AreEqual(oldContacts.Count - 1, application.ContactHelper.NumberOfContacts());

            // assert
            List<Contact> newContacts = application.ContactHelper.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}

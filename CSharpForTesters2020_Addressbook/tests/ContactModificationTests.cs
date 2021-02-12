using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
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

            List<Contact> oldContacts = Contact.GetAllContactsFromDB();

            int contactToModifyIndex = 0;

            Contact modifiedContact = oldContacts[contactToModifyIndex];

            // act
            application.ContactHelper.ModifyContact(modifiedContact, contact);

            // assert
            Assert.AreEqual(oldContacts.Count, application.ContactHelper.NumberOfContacts());

            List<Contact> newContacts = Contact.GetAllContactsFromDB();

            modifiedContact.FirstName = contact.FirstName;
            modifiedContact.LastName = contact.LastName;
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

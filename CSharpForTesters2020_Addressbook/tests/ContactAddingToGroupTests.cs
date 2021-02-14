using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactAddingToGroupTests : AuthenticationTestBase
    {
        [Test]
        public void ContactAddingToGroupTest()
        {
            ContactGroup group = ContactGroup.GetAllGroupsFromDB()[0];
            List<Contact> oldGroupContacts = group.GetGroupContactsFromDB();
            Contact contact = Contact.GetAllContactsFromDB().Except(oldGroupContacts).First();

            application.ContactHelper.AddContactToGroup(contact, group);

            List<Contact> newGroupContacts = group.GetGroupContactsFromDB();
            oldGroupContacts.Add(contact);
            oldGroupContacts.Sort();
            newGroupContacts.Sort();
            Assert.AreEqual(oldGroupContacts, newGroupContacts);
        }
    }
}

using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactAddingAndRemovingToFromGroupTests : AuthenticationTestBase
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

        [Test]
        public void ContactRemovingFromGroupTest()
        {
            ContactGroup group = null;
            List<Contact> oldGroupContacts = null;

            foreach (ContactGroup contactGroup in ContactGroup.GetAllGroupsFromDB())
            {
                if (contactGroup.GetGroupContactsFromDB().Any())
                {
                    group = contactGroup;
                    oldGroupContacts = group.GetGroupContactsFromDB();
                    break;
                }
            }

            Contact contact = oldGroupContacts[0];

            application.ContactHelper.RemoveContactFromGroup(contact, group);

            List<Contact> newGroupContacts = group.GetGroupContactsFromDB();
            oldGroupContacts.Remove(contact);
            oldGroupContacts.Sort();
            newGroupContacts.Sort();
            Assert.AreEqual(oldGroupContacts, newGroupContacts);
        }
    }
}

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
            if (ContactGroup.GetAllGroupsFromDB().Count < 1)
            {
                ContactGroup newGroup = new ContactGroup("Green group");
                newGroup.Header = "Green header";
                newGroup.Footer = "Green footer";

                application.GroupHelper.CreateGroup(newGroup);
            }

            ContactGroup group = ContactGroup.GetAllGroupsFromDB()[0];

            List<Contact> groupContacts = group.GetGroupContactsFromDB();

            List<Contact> allContacts = Contact.GetAllContactsFromDB();
            if (allContacts.Count < 1 || allContacts.Except(groupContacts).Count() < 1)
            {
                Contact newContact = new Contact("Alex");
                newContact.LastName = "Cowell";

                application.ContactHelper.CreateContact(newContact);
            }

            Contact contact = Contact.GetAllContactsFromDB().Except(groupContacts).First();

            application.ContactHelper.AddContactToGroup(contact, group);

            List<Contact> newGroupContacts = group.GetGroupContactsFromDB();
            groupContacts.Add(contact);
            groupContacts.Sort();
            newGroupContacts.Sort();
            Assert.AreEqual(groupContacts, newGroupContacts);
        }

        [Test]
        public void ContactRemovingFromGroupTest()
        {
            ContactGroup group = null;
            List<Contact> oldGroupContacts = null;
            Contact contact = null;

            if (ContactGroup.GetAllGroupsFromDB().Count < 1)
            {
                ContactGroup newGroup = new ContactGroup("Purple group");
                newGroup.Header = "Purple header";
                newGroup.Footer = "Purple footer";

                application.GroupHelper.CreateGroup(newGroup);
            }

            if (Contact.GetAllContactsFromDB().Count < 1)
            {
                Contact newContact = new Contact("Colin");
                newContact.LastName = "McMarron";

                application.ContactHelper.CreateContact(newContact);
            }

            foreach (ContactGroup contactGroup in ContactGroup.GetAllGroupsFromDB())
            {
                if (contactGroup.GetGroupContactsFromDB().Any())
                {
                    group = contactGroup;
                    break;
                }
            }

            if (group == null)
            {
                group = ContactGroup.GetAllGroupsFromDB()[0];
                contact = Contact.GetAllContactsFromDB()[0];
                application.ContactHelper.AddContactToGroup(contact, group);
            }

            oldGroupContacts = group.GetGroupContactsFromDB();

            Contact contactToRemove = oldGroupContacts[0];

            application.ContactHelper.RemoveContactFromGroup(contactToRemove, group);

            List<Contact> newGroupContacts = group.GetGroupContactsFromDB();
            oldGroupContacts.Remove(contactToRemove);
            oldGroupContacts.Sort();
            newGroupContacts.Sort();
            Assert.AreEqual(oldGroupContacts, newGroupContacts);
        }
    }
}

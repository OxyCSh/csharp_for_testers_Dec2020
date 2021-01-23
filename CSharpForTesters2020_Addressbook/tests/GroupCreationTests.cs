using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            ContactGroup group = new ContactGroup("Green group");
            group.Header = "Green header";
            group.Footer = "Green footer";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            ContactGroup group = new ContactGroup("");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            // a group with a ' won't be created and the test will fail
            ContactGroup group = new ContactGroup("aaa'bbb");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }
    }
}

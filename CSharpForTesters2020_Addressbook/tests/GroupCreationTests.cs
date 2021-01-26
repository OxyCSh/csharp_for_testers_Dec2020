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

            /* use this only if this test often fails
            in this case it's quicker to compare the number of items and fail
            without spending time comparing the items in the lists;
            if the test is stable comparing count can be skipped
            as it's an unnecessary step that takes time
            */
            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            ContactGroup group = new ContactGroup("");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest_FailExpected()
        {
            // a group with a ' won't be created and the test will fail
            ContactGroup group = new ContactGroup("aaa'bbb");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

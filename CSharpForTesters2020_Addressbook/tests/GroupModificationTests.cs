using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            // arrange
            if (application.GroupHelper.NumberOfGroups() == 0)
            {
                ContactGroup groupForModification = new ContactGroup("test group");
                application.GroupHelper.CreateGroup(groupForModification);
            }

            ContactGroup group = new ContactGroup("Brown group");

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            // act
            application.GroupHelper.ModifyGroup(0, group);

            Assert.AreEqual(oldGroups.Count, application.GroupHelper.NumberOfGroups());

            // assert
            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups[0].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

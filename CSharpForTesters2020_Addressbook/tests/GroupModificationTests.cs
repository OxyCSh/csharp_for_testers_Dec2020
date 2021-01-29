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

            int groupToModifyId = 0;

            ContactGroup modifiedGroup = oldGroups[groupToModifyId];

            // act
            application.GroupHelper.ModifyGroup(groupToModifyId, group);

            // assert
            Assert.AreEqual(oldGroups.Count, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups[groupToModifyId].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (ContactGroup thisGroup in newGroups)
            {
                if (thisGroup.Id == modifiedGroup.Id)
                {
                    Assert.AreEqual(group.Name, thisGroup.Name);
                }
            }
        }
    }
}

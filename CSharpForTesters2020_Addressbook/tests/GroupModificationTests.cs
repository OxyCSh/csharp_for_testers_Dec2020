using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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

            List<ContactGroup> oldGroups = ContactGroup.GetAllGroupsFromDB();

            int groupToModifyIndex = 0;

            ContactGroup modifiedGroup = oldGroups[groupToModifyIndex];

            // act
            //application.GroupHelper.ModifyGroupAtIndex(groupToModifyId, group);
            application.GroupHelper.ModifyGroup(modifiedGroup, group);

            // assert
            Assert.AreEqual(oldGroups.Count, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = ContactGroup.GetAllGroupsFromDB();

            oldGroups[groupToModifyIndex].Name = group.Name;
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

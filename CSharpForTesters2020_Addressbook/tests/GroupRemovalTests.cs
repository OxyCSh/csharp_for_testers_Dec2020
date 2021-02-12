using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            // arrange
            if (application.GroupHelper.NumberOfGroups() == 0)
            {
                ContactGroup group = new ContactGroup("test group");
                application.GroupHelper.CreateGroup(group);
            }

            // getting a list of groups via UI
            //List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            // via DB
            List<ContactGroup> oldGroups = ContactGroup.GetAllGroupsFromDB();

            int groupToBeRemovedIndex = 0;

            ContactGroup toBeRemoved = oldGroups[groupToBeRemovedIndex];

            // act
            // before using the DB to get the list of groups, group was deleted by ID
            // but it doesn't work anymore as the order of groups in the DB is different
            //application.GroupHelper.RemoveGroupAtIndex(groupToBeRemovedId);
            application.GroupHelper.RemoveGroup(toBeRemoved);

            // assert
            Assert.AreEqual(oldGroups.Count - 1, application.GroupHelper.NumberOfGroups());

            // getting a list of groups via UI
            //List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            // via DB
            List<ContactGroup> newGroups = ContactGroup.GetAllGroupsFromDB();

            // remove deleted group from the list and compare the lists of groups
            oldGroups.RemoveAt(groupToBeRemovedIndex);

            /* as is by default the comparison method expects both to be the same object
            and they are not
            we implemented own comparison methods in ContactGroup (Equals and GetHashCode)*/
            Assert.AreEqual(oldGroups, newGroups);

            // added comparison by ID as groups can have identical names
            // so comparison by name only is unreliable
            // comparing IDs of remaining elements to the ID of the removed element
            foreach (ContactGroup group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}

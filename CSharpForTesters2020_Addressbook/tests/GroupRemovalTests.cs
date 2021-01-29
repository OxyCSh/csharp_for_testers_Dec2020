using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthenticationTestBase
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

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            int groupToBeRemovedId = 0;

            ContactGroup toBeRemoved = oldGroups[groupToBeRemovedId];

            // act
            application.GroupHelper.RemoveGroup(groupToBeRemovedId);

            // assert
            Assert.AreEqual(oldGroups.Count - 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            // remove deleted group from the list and compare the lists of groups
            oldGroups.RemoveAt(groupToBeRemovedId);

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

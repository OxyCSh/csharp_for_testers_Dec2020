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

            // act
            application.GroupHelper.RemoveGroup(0);

            // assert
            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            // remove deleted group from the list and compare the lists of groups
            oldGroups.RemoveAt(0);

            /* as is by default the comparison method expects both to be the same object
            and they are not
            we implemented own comparison methods in ContactGroup (Equals and GetHashCode)*/
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

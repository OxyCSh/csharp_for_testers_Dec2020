using NUnit.Framework;


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

            // act
            application.GroupHelper.ModifyGroup(0, group);

            // assert
        }
    }
}

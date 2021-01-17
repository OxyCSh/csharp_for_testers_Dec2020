using NUnit.Framework;


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

            // act
            application.GroupHelper.RemoveGroup(0);

            // assert
        }
    }
}

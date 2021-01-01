using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            OpenHomePage();
            login.LoginUser(new User("admin", "secret"));
            GoToGroupsPage();
            SelectGroup(2);
            RemoveGroup();
            ReturnToGroups();
            login.Logout();
        }
    }
}

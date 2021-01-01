using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            OpenHomePage();
            login.LoginUser(new User("admin", "secret"));
            GoToGroupsPage();
            InitGroupCreation();

            ContactGroup group = new ContactGroup("White group");
            group.Header = "White header";
            group.Footer = "White footer";

            FillGroupForm(group);
            SubmitNewGroup();
            ReturnToGroups();
            login.Logout();
        }
    }
}

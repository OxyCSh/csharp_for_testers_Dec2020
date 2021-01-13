using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            ContactGroup group = new ContactGroup("Brown group");
            group.Header = "Brown header";
            group.Footer = "Brown footer";

            application.GroupHelper.ModifyGroup(9, group);

            application.Login.Logout();
        }
    }
}

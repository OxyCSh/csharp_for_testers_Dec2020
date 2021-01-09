using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupRemovalTests : TestBase
    {
        [Test]
        public void GroupRemovalTest()
        {
            application.GroupHelper.RemoveGroup(1);
            
            application.Login.Logout();
        }
    }
}

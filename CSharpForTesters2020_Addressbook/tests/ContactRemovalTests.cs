using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            application.ContactHelper.RemoveContact(3);

            application.Login.Logout();
        }
    }
}

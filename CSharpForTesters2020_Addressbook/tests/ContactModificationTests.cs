using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactModificationTests : TestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            Contact contact = new Contact("Josef");

            application.ContactHelper.ModifyContact(3, contact);

            application.Login.Logout();
        }
    }
}

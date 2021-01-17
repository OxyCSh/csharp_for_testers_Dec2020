using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthenticationTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // arrange
            if (application.ContactHelper.NumberOfContacts() == 0)
            {
                Contact contact = new Contact("Tester");
                application.ContactHelper.CreateContact(contact);
            }

            // act
            application.ContactHelper.RemoveContact(0);

            // assert
        }
    }
}

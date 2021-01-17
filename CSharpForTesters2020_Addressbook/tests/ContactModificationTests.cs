using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactModificationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // arrange
            if (application.ContactHelper.NumberOfContacts() == 0)
            {
                Contact contactForModification = new Contact("Tester");
                application.ContactHelper.CreateContact(contactForModification);
            }

            Contact contact = new Contact("Josef");

            // act
            application.ContactHelper.ModifyContact(0, contact);

            // assert
        }
    }
}

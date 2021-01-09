using System;
using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            Contact contact = new Contact("Lance");
            contact.Address = "Line 1\nLine 2";
            contact.DayOfBirth = new Random().Next(1, 28);
            contact.MonthOfBirth = "November";
            contact.YearOfBirth = new Random().Next(1980, 2019);
            contact.ContactGroup = "White group";
            
            application.ContactHelper.CreateContact(contact);

            application.Login.Logout();
        }
    }
}

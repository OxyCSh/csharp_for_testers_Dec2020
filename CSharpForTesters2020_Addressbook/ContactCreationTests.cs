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
            OpenHomePage();

            login.LoginUser(new User("admin", "secret"));

            InitContactCreation();

            Contact contact = new Contact("Robert");
            contact.Address = "Line 1\nLine 2";
            contact.DayOfBirth = new Random().Next(1, 28);
            contact.MonthOfBirth = "November";
            contact.YearOfBirth = new Random().Next(1980, 2019);
            contact.ContactGroup = "Blue group";

            FillContactForm(contact);
            SubmitNewContact();

            ReturnToHomePage();

            login.Logout();
        }
    }
}

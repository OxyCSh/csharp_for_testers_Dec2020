using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactInformationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactInformationTest()
        {
            Contact contactInfoFromTable = application.ContactHelper.GetContactInfoFromTable(7);
            Contact contactInfoFromEditForm = application.ContactHelper.GetContactInfoFromEditForm(7);

            Assert.AreEqual(contactInfoFromTable, contactInfoFromEditForm);
            Assert.AreEqual(contactInfoFromTable.Address, contactInfoFromEditForm.Address);
            Assert.AreEqual(contactInfoFromTable.AllPhones, contactInfoFromEditForm.AllPhones);
            Assert.AreEqual(contactInfoFromTable.AllEmails, contactInfoFromEditForm.AllEmails);
        }

        [Test]
        public void ContactInformationViewAndEditTest([Range(0, 9)] int x)
        {
            Contact contactInfoFromView = application.ContactHelper.GetContactInfoFromView(x);
            Contact contactInfoFromEditForm = application.ContactHelper.GetContactInfoFromEditForm(x);

            Assert.AreEqual(contactInfoFromView.DetailsView, contactInfoFromEditForm.DetailsView);
        }
    }
}

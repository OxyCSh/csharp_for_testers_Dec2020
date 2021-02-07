using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class ContactInformationTests : AuthenticationTestBase
    {
        [Test]
        public void ContactInformationTest([Range(0, 8)] int x)
        {
            Contact contactInfoFromTable = application.ContactHelper.GetContactInfoFromTable(x);
            Contact contactInfoFromEditForm = application.ContactHelper.GetContactInfoFromEditForm(x);

            Assert.AreEqual(contactInfoFromTable, contactInfoFromEditForm);
            Assert.AreEqual(contactInfoFromTable.Address, contactInfoFromEditForm.Address);
            Assert.AreEqual(contactInfoFromTable.AllPhones, contactInfoFromEditForm.AllPhones);
            Assert.AreEqual(contactInfoFromTable.AllEmails, contactInfoFromEditForm.AllEmails);
        }

        [Test]
        public void ContactInformationViewAndEditTest([Range(0, 8)] int x)
        {
            Contact contactInfoFromView = application.ContactHelper.GetContactInfoFromView(x);
            Contact contactInfoFromEditForm = application.ContactHelper.GetContactInfoFromEditForm(x);

            Assert.AreEqual(contactInfoFromView.DetailsView, contactInfoFromEditForm.DetailsView);
        }
    }
}

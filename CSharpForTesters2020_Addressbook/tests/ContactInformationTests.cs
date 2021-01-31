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
            Contact contactInfoFromTable = application.ContactHelper.GetContactInfoFromTable(0);
            Contact contactInfoFromEditForm = application.ContactHelper.GetContactInfoFromEditForm(0);

            Assert.AreEqual(contactInfoFromTable, contactInfoFromEditForm);
            Assert.AreEqual(contactInfoFromTable.Address, contactInfoFromEditForm.Address);
            Assert.AreEqual(contactInfoFromTable.AllPhones, contactInfoFromEditForm.AllPhones);
            Assert.AreEqual(contactInfoFromTable.AllEmails, contactInfoFromEditForm.AllEmails);
        }
    }
}

using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    public class ContactTestBase : AuthenticationTestBase
    {
        [TearDown]
        public void CompareContactsUI_DB()
        {
            if (UI_DB_CHECK)
            {
                List<Contact> fromUI = application.ContactHelper.GetContactList();
                List<Contact> fromDB = Contact.GetAllContactsFromDB();

                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}

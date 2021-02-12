using NUnit.Framework;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    public class GroupTestBase : AuthenticationTestBase
    {
        [TearDown]
        public void CompareGroupsUI_DB()
        {
            if (UI_DB_CHECK) {
                List<ContactGroup> fromUI = application.GroupHelper.GetGroupList();
                List<ContactGroup> fromDB = ContactGroup.GetAllGroupsFromDB();

                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }
        }
    }
}

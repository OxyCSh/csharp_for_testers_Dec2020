﻿using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            ContactGroup group = new ContactGroup("Green group");
            group.Header = "Green header";
            group.Footer = "Green footer";
            
            application.GroupHelper.CreateGroup(group);

            application.Login.Logout();
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            ContactGroup group = new ContactGroup("");
            group.Header = "";
            group.Footer = "";
            
            application.GroupHelper.CreateGroup(group);

            application.Login.Logout();
        }
    }
}

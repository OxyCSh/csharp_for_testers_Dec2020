using NUnit.Framework;


namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : AuthenticationTestBase
    {
        [Test]
        public void GroupCreationTest()
        {
            ContactGroup group = new ContactGroup("Green group");
            group.Header = "Green header";
            group.Footer = "Green footer";
            
            application.GroupHelper.CreateGroup(group);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            ContactGroup group = new ContactGroup("");
            group.Header = "";
            group.Footer = "";
            
            application.GroupHelper.CreateGroup(group);
        }
    }
}

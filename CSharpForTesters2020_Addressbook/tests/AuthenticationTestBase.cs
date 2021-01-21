using NUnit.Framework;

namespace AddressbookWebTests
{
    /* all tests that need to be logged in inherit from this class (instead of TestBase)
     those that don't - inherit directly from TestBase*/
    public class AuthenticationTestBase : TestBase
    {
        [SetUp]
        public void SetupLogin()
        {
            application.Login.LoginUser(new User("admin", "secret"));
        }

        /* each test doesn't need to logout to save time
        [TearDown]
        public void TeardownTest()
        {
            application.Login.Logout();
        }*/
    }
}

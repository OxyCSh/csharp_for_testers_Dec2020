using NUnit.Framework;

namespace AddressbookWebTests
{
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

using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            // arrange
            // preparing test data
            User adminUser = new User("admin", "secret");
            // precondition / SUT in the required state
            application.Login.Logout();

            // act
            application.Login.LoginUser(adminUser);

            // assert
            Assert.IsTrue(application.Login.IsLoggedIn(adminUser));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            // arrange
            // preparing test data
            User adminUserWithInvalidPassword = new User("admin", "invalidpassword");
            // precondition / SUT in the required state
            application.Login.Logout();

            // act
            application.Login.LoginUser(adminUserWithInvalidPassword);

            // assert
            Assert.IsFalse(application.Login.IsLoggedIn(adminUserWithInvalidPassword));
        }
    }
}

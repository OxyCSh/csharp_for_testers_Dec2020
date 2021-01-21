using OpenQA.Selenium;

namespace AddressbookWebTests
{
    public class Login : HelperBase
    {
        public Login(ApplicationManager manager) : base(manager)
        {
        }

        // clever login method that only logs in if no user is logged in at all
        // or if a wrong user is logged in, it logs it out and then logs in as the correct user
        public void LoginUser(User user)
        {
            if (!IsLoggedIn(user))
            {
                if (IsLoggedIn())
                    Logout();

                TypeIn(By.Name("user"), user.Username);
                TypeIn(By.Name("pass"), user.Password);
                driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            }
        }

        // a clever method - first checks if any user is logged in before logging out
        public void Logout()
        {
            if (IsLoggedIn())
                driver.FindElement(By.LinkText("Logout")).Click();

            // waiting for the Login page to ensure that logout completed
            WaitForElement(By.Name("user"), 5);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        // checks if logged in as the User passed to the method
        public bool IsLoggedIn(User user)
        {
            return IsLoggedIn() &&
                driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text
                    == "("+user.Username+")";
        }
    }
}

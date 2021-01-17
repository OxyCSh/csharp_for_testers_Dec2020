using OpenQA.Selenium;

namespace AddressbookWebTests
{
    public class Login : HelperBase
    {
        public Login(ApplicationManager manager) : base(manager)
        {
        }

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

        public bool IsLoggedIn(User user)
        {
            return IsLoggedIn() &&
                driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text
                    == "("+user.Username+")";
        }
    }
}

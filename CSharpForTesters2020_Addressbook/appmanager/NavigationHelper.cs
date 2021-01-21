using OpenQA.Selenium;


namespace AddressbookWebTests
{
    public class Navigation : HelperBase
    {
        private string baseURL = "http://localhost/addressbook/";

        public Navigation(ApplicationManager manager) : base(manager)
        {
            //this.baseURL = baseURL;
        }

        // to avoid unnecessary navigation, we check if we already on the right page
        public void OpenHomePage()
        {
            if (driver.Url != baseURL)
                driver.Navigate().GoToUrl(baseURL);
        }

        // to avoid unnecessary navigation, we check if we already on the right page
        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "group.php"
                    && IsElementPresent(By.Name("new"))
                    && driver.FindElement(By.TagName("h1")).Text == "Groups")
                return;

            // added to avoid StaleElementReferenceException
            WaitForElement(By.LinkText("groups"), 5).Click();
        }
    }
}

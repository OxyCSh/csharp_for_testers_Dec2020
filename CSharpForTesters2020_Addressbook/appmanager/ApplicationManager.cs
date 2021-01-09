using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;


namespace AddressbookWebTests
{
    public class ApplicationManager
    {
        //protected IWebDriver driver;
        protected string baseURL;

        //protected Login login;
        //protected Navigation navigator;
        //protected GroupHelper groupHelper;
        //protected ContactHelper contactHelper;

        public Login Login { get; }
        public Navigation Navigator { get; }
        public GroupHelper GroupHelper { get; }
        public ContactHelper ContactHelper { get; }
        public IWebDriver Driver { get; }


        public ApplicationManager()
        {
            Driver = new FirefoxDriver();
            baseURL = "http://localhost/addressbook/";

            Login = new Login(this);
            Navigator = new Navigation(this, baseURL);
            GroupHelper = new GroupHelper(this);
            ContactHelper = new ContactHelper(this);
        }

        public void StopDriver()
        {
            try
            {
                Driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}

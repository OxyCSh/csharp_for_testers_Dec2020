using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace AddressbookWebTests
{
    public class ApplicationManager
    {
        //protected string baseURL;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        public Login Login { get; }
        public Navigation Navigator { get; }
        public GroupHelper GroupHelper { get; }
        public ContactHelper ContactHelper { get; }
        public IWebDriver Driver { get; }


        private ApplicationManager()
        {
            Driver = new FirefoxDriver();

            //baseURL = "http://localhost/addressbook/";

            Login = new Login(this);
            Navigator = new Navigation(this);
            GroupHelper = new GroupHelper(this);
            ContactHelper = new ContactHelper(this);
        }

        ~ApplicationManager()
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

        // Singleton pattern (single instance of Application Manager)
        // modified to be able to run test cases in parallel
        // each thread will have own Application Manager
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated) // if for the current thread Application Manager is not created
            { // then create a new instance for this thread
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance; // assign new instance to ThreadLocal storage

                // newInstance.Navigator.OpenHomePage(); // moved to TestBase
            }
            return app.Value;
        }
    }
}

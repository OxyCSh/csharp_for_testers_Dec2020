using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Threading;

namespace AddressbookWebTests
{
    public class ApplicationManager
    {
        //protected string baseURL;

        // this is a ThreadLocal instance that matches the current thread to an Application Manager instance
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

        /* destructor created to work with multiple threads
        so it can close browsers in all threads;
        it replaced the Stop method in Application Manager
        that was called by the [TearDown] method in TestSuiteFixture
        which could only handle one thread (not multiple)
        so only browser of one thread was closed

        TestSuiteFixture became redundand
        and is completely removed now
        
        descructor is called automatically either at the end of a run
        or when there are no more variables that store Application Manager
        it's not possible to call it*/
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

        // Singleton pattern (single instance of Application Manager) modified
        // to be able to run test cases in parallel
        // so that each thread will have own instance of Application Manager
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated) // if in the ThreadLocal storage for the current thread
            // Application Manager is not created
            { // then create a new instance for this thread
                ApplicationManager newInstance = new ApplicationManager();
                app.Value = newInstance; // assign new instance to the ThreadLocal storage for the current thread
                // otherwise the existing instance will be returned
                // app.Value stores an instance of Application Manager
                // that is different for each thread

                // newInstance.Navigator.OpenHomePage(); // moved to TestBase
            }
            return app.Value;
        }
    }
}

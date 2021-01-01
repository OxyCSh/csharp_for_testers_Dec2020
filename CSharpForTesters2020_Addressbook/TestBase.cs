using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;

        protected Login login;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();

            // option 1
            //driver = new ChromeDriver("C:\\SeleniumDrivers"); // a path to chromedriver as a parameter

            //Process cmd = new Process();
            ////cmd.StartInfo.FileName = "chromedriver.exe";
            ////cmd.StartInfo.Arguments = "-version"; // to display the version of chromedriver
            //cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.Arguments = "/C where chromedriver"; // to display the path of chromedriver
            //cmd.StartInfo.RedirectStandardInput = true;
            //cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.UseShellExecute = false;
            //cmd.Start();
            //cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            // option 2
            // didn't work initially with the installed Chrome v87 because
            // it tried to use not compatible chromedriver v85 which it found in /bin/Debug
            //driver = new ChromeDriver();

            baseURL = "http://localhost/addressbook/";
            verificationErrors = new StringBuilder();

            login = new Login(driver);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        protected void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        protected void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        protected void ReturnToGroups()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        protected void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }




        // *********************************************
        protected void InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        protected void FillGroupForm(ContactGroup group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        protected void SubmitNewGroup()
        {
            driver.FindElement(By.Name("submit")).Click();
        }


        // **********************************
        protected void RemoveGroup()
        {
            driver.FindElement(By.XPath("(//input[@name='delete'])[2]")).Click();
        }

        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }


        //***************************************
        protected void InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        protected void FillContactForm(Contact contact)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName); // text field
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(contact.Address); // text box

            //do not click on the Browse button, it'll trigger an OS dialogue window
            driver.FindElement(By.Name("photo")).SendKeys(contact.Photo); // file selector

            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.DayOfBirth.ToString()); // drop-down
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.MonthOfBirth);
            driver.FindElement(By.Name("byear")).Clear();
            driver.FindElement(By.Name("byear")).SendKeys(contact.YearOfBirth.ToString());

            new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.ContactGroup);
        }

        protected void SubmitNewContact()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
        }
    }
}

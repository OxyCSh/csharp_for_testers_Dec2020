using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SandboxTests
{
    [TestFixture]
    public class Sandbox
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;


        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            verificationErrors = new StringBuilder();
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

        [Test]
        public void JQueryDatePickerTest()
        {
            // working with jQuery date picker using jQuery API
            // http://barancev.github.io/how-to-set-datepicker-value/


            driver.Navigate().GoToUrl("https://jqueryui.com/datepicker/");

            // switch to an iframe
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("iframe.demo-frame")));

            // wait up to 30 seconds until the element is displayed
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(d => driver.FindElement(By.CssSelector("#datepicker")).Displayed);
            
            (driver as IJavaScriptExecutor).ExecuteScript(String.Format("$('{0}').datepicker('setDate', '{1}')", "#datepicker", "02/20/2002"));
        }


        // TO DO selecting from drop-downs
        [Test]
        public void JQuerySelectTest()
        {
            driver.Navigate().GoToUrl("https://select2.org/getting-started/basic-usage");

            SelectFromDropDown(By.XPath("(//select)[1]"), "New Mexico"); // standard HTML select
            SelectFromDropDown(By.XPath("(//select)[2]"), "Rhode Island"); // select2 - doesn't work
            SelectFromDropDown(By.XPath("(//select)[3]"), "Nevada"); // select2 - doesn't work
        }

        private void SelectFromDropDown(By dropdownLocator, string selectOption)
        {
            // locate the drop down
            var dropDown = driver.FindElement(dropdownLocator);
            //create a select element object 
            var selectElement = new SelectElement(dropDown);

            // select by text
            selectElement.SelectByText(selectOption);
        }

        [Test]
        public void Loops()
        {
            // an array
            string[] s = new string[] { "test1", "test2", "test3" };

            for (int i = 0; i < s.Length; i++)
            {
                System.Console.Out.Write(s[i]);
            }

            foreach (string element in s)
            {
                System.Console.Out.Write(element);
            }

            int attempt = 0;

            while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 5)
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            }

            do
            {
                System.Threading.Thread.Sleep(1000);
                attempt++;
            } while (driver.FindElements(By.Id("test")).Count == 0 && attempt < 5);
        }

        [Test]
        public void TestTest()
        {
            driver.Navigate().GoToUrl("http://localhost/addressbook/");
            driver.FindElement(By.Name("user")).SendKeys("admin");
            driver.FindElement(By.Name("pass")).SendKeys("secret");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();

            IList<IWebElement> editIcons = driver.FindElement(By.Id("maintable")).FindElements(By.XPath("//img[@alt='Details']"));
            editIcons[3].Click();

            System.Console.Out.Write(driver.FindElement(By.Id("content")).Text);

        }

        [Test]
        public bool Strings()
        {
            string s1 = "test";

            // use @ to add verbatim strings where no need to use \ to escape
            string s2 = @" t\e""
                        s
                        t ";
            s2.Trim(); // to remove spaces at the beginning and end

            string[] parts = s2.Split('\n');

            string s3 = System.String.Format("({0})", s1);

            // special characters
            // \ a back slash to screen (escape) characters: \\, \", \t tab, \n new line

            // ways to compare strings
            //return s1 == s2;
            return s1.Equals(s2, StringComparison.OrdinalIgnoreCase); // can use options, e g ignoring the case
        }
    }
}

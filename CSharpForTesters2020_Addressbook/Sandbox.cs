using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
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
    }
}

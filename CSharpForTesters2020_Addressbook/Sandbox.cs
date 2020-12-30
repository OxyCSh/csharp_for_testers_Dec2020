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
        public void jQueryDatePickerTest()
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


        // TO DO
        [Test]
        public void jQuerySelectTest()
        {
            driver.Navigate().GoToUrl("https://select2.org/getting-started/basic-usage");

            // switch to an iframe
            //driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("iframe.demo-frame")));

            // wait up to 30 seconds until the element is displayed
            new WebDriverWait(driver, TimeSpan.FromSeconds(30)).Until<bool>(d => driver.FindElement(By.CssSelector("#datepicker")).Displayed);

            (driver as IJavaScriptExecutor).ExecuteScript(String.Format("$('{0}').datepicker('setDate', '{1}')", "#datepicker", "02/20/2002"));
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper ModifyContact(int ind, Contact contact)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(ind);
            FillContactForm(contact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper InitContactModification(int ind)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + ind + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper CreateContact(Contact contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitNewContact();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(Contact contact)
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

            if (contact.ContactGroup != null && contact.ContactGroup != "")
                new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.ContactGroup);

            return this;
        }

        public ContactHelper SubmitNewContact()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper RemoveContact(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(index);
            ClickDeleteButton();
            AcceptRemoval();
            return this;
        }

        public ContactHelper AcceptRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper ClickDeleteButton()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@type='checkbox'])[" + index + "]")).Click();
            return this;
        }
    }
}

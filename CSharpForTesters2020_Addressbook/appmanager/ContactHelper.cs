using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;


namespace AddressbookWebTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper CreateContact(Contact contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitNewContact();
            ReturnToHomePage();
            return this;
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

        public ContactHelper RemoveContact(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContact(index);
            ClickDeleteButton();
            AcceptRemoval();
            return this;
        }


        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SubmitNewContact()
        {
            driver.FindElement(By.XPath("//input[@name='submit']")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            IList<IWebElement> checkboxes = driver.FindElement(By.Id("maintable")).FindElements(By.TagName("input"));
            checkboxes[index].Click();
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            IList<IWebElement> editIcons = driver.FindElement(By.Id("maintable")).FindElements(By.XPath("//img[@alt='Edit']"));
            editIcons[index].Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper ClickDeleteButton()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper AcceptRemoval()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper FillContactForm(Contact contact)
        {
            TypeIn(By.Name("firstname"), contact.FirstName); // text field
            TypeIn(By.Name("lastname"), contact.LastName);
            TypeIn(By.Name("address"), contact.Address); // text box

            //do not click on the Browse button, it'll trigger an OS dialogue window
            driver.FindElement(By.Name("photo")).SendKeys(contact.Photo); // file selector

            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.DayOfBirth.ToString()); // drop-down
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText(contact.MonthOfBirth);
            TypeIn(By.Name("byear"), contact.YearOfBirth.ToString());

            if (contact.ContactGroup != null && contact.ContactGroup != "")
                new SelectElement(driver.FindElement(By.Name("new_group"))).SelectByText(contact.ContactGroup);

            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public int NumberOfContacts()
        {
            manager.Navigator.OpenHomePage();

            try
            {
                IList<IWebElement> contactEntries = driver.FindElement(By.Id("maintable")).FindElements(By.Name("entry"));
                return contactEntries.Count;
            }
            catch
            {
                return 0;
            }
        }
    }
}

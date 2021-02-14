using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        public ContactHelper AddContactToGroup(Contact contact, ContactGroup group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        private void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectGroupToAdd(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(groupName);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        internal Contact GetContactInfoFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModificationByIndex(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new Contact(firstName)
            {
                LastName = lastName,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        internal Contact GetContactInfoFromView(int index)
        {
            manager.Navigator.OpenHomePage();
            OpenContactDetails(index);

            string detailsView = driver.FindElement(By.Id("content")).Text;

            return new Contact(null)
            {
                DetailsView = detailsView
            };
        }

        private ContactHelper OpenContactDetails(int index)
        {
            IList<IWebElement> editIcons = driver.FindElement(By.Id("maintable")).FindElements(By.XPath("//img[@alt='Details']"));
            editIcons[index].Click();
            return this;
        }

        internal Contact GetContactInfoFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));

            string firstName = cells[2].Text;
            string lastName = cells[1].Text;
            string address = cells[3].Text;
            string emails = cells[4].Text;
            string phones = cells[5].Text;

            return new Contact(firstName)
            {
                LastName = lastName,
                Address = address,
                AllPhones = phones,
                AllEmails = emails
            };
        }

        public ContactHelper ModifyContactAtIndex(int ind, Contact contact)
        {
            manager.Navigator.OpenHomePage();
            InitContactModificationByIndex(ind);
            FillContactForm(contact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ModifyContact(Contact contactToModify, Contact newContact)
        {
            manager.Navigator.OpenHomePage();
            InitContactModificationById(contactToModify.Id);
            FillContactForm(newContact);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper RemoveContactAtIndex(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContactByIndex(index);
            ClickDeleteButton();
            AcceptRemoval();
            return this;
        }

        public ContactHelper RemoveContact(Contact contact)
        {
            manager.Navigator.OpenHomePage();
            SelectContactById(contact.Id);
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
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContactByIndex(int index)
        {
            IList<IWebElement> checkboxes = driver.FindElement(By.Id("maintable")).FindElements(By.TagName("input"));
            checkboxes[index].Click();
            return this;
        }

        public ContactHelper SelectContactById(String id)
        {
            driver.FindElement(By.XPath("//input[@type='checkbox' and @id='" + id + "']")).Click();
            return this;
        }

        public ContactHelper InitContactModificationByIndex(int index)
        {
            IList<IWebElement> editIcons = driver.FindElement(By.Id("maintable")).FindElements(By.XPath("//img[@alt='Edit']"));
            editIcons[index].Click();
            return this;
        }

        public ContactHelper InitContactModificationById(string id)
        {
            driver.FindElement(By.XPath("//tr[@name='entry']//input[@id='"+id+"']//ancestor::tr//img[@alt='Edit']")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
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
            contactCache = null;
            WaitForElement(By.ClassName("msgbox"), 5);
            WaitForElement(By.Id("maintable"), 5);
            return this;
        }

        public ContactHelper FillContactForm(Contact contact)
        {
            TypeIn(By.Name("firstname"), contact.FirstName); // text field
            TypeIn(By.Name("lastname"), contact.LastName);
            TypeIn(By.Name("address"), contact.Address); // text box

            //do not click on the Browse button, it'll trigger an OS dialogue window
            driver.FindElement(By.Name("photo")).SendKeys(AppDomain.CurrentDomain.BaseDirectory+contact.Photo); // file selector, path to solution directory

            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText(contact.DayOfBirth.ToString()); // drop-down
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByIndex(contact.MonthOfBirth+1);
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
                //IList<IWebElement> contactEntries = driver.FindElement(By.Id("maintable")).FindElements(By.Name("entry"));
                //return contactEntries.Count;
                return driver.FindElement(By.Id("maintable")).FindElements(By.Name("entry")).Count;
            }
            catch
            {
                return 0;
            }
        }

        // caching
        private List<Contact> contactCache = null;

        public List<Contact> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache  = new List<Contact>();

                manager.Navigator.OpenHomePage();

                ICollection<IWebElement> elements = driver.FindElements(By.Name("entry"));

                foreach (IWebElement element in elements)
                {
                    Contact contact = new Contact(element.FindElement(By.XPath(".//td[3]")).Text); // First Name
                    contact.LastName = element.FindElement(By.XPath(".//td[2]")).Text; // Last Name
                    contact.Id = element.FindElement(By.XPath(".//td[1]/input")).GetAttribute("id"); //ID
                    contactCache.Add(contact);
                }
            }
            return new List<Contact>(contactCache);
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.OpenHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text); // a reg expression is applied to text
            // m is a part of text that matches the reg expression
            return Int32.Parse(m.Value);
        }
    }
}

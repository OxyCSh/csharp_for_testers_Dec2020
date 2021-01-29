using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace AddressbookWebTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }

        public GroupHelper CreateGroup(ContactGroup group)
        {
            manager.Navigator.GoToGroupsPage();
            InitGroupCreation();
            FillGroupForm(group);
            SubmitNewGroup();
            ReturnToGroups();
            return this;
        }

        public GroupHelper ModifyGroup(int index, ContactGroup group)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            InitGroupModification();
            FillGroupForm(group);
            SubmitGroupModification();
            ReturnToGroups();
            return this;
        }

        public GroupHelper RemoveGroup(int index)
        {
            manager.Navigator.GoToGroupsPage();
            SelectGroup(index);
            PressRemoveButton();
            ReturnToGroups();
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper SubmitNewGroup()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            IList<IWebElement> checkboxes = driver.FindElements(By.XPath("//input[@type='checkbox']"));
            checkboxes[index].Click();
            return this;
        }

        public GroupHelper PressRemoveButton()
        {
            driver.FindElement(By.XPath("//input[@name='delete']")).Click();
            groupCache = null;
            return this;
        }

        public GroupHelper FillGroupForm(ContactGroup group)
        {
            TypeIn(By.Name("group_name"), group.Name);
            TypeIn(By.Name("group_header"), group.Header);
            TypeIn(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper ReturnToGroups()
        {
            driver.FindElement(By.LinkText("group page")).Click();
            return this;
        }

        public int NumberOfGroups()
        {
            manager.Navigator.GoToGroupsPage();

            try
            {
                //IList<IWebElement> groupEntries = driver.FindElements(By.ClassName("group"));
                //return groupEntries.Count;
                return driver.FindElements(By.ClassName("group")).Count;
            }
            catch
            {
                return 0;
            }
        }

        /* caching - to speed up execution of long, complex operations
        or operations with a lot of data

        one option would be to limit the test data in the SUT
        another to implement caching
        */
        private List<ContactGroup> groupCache = null; // a list of objects of the group type

        public List<ContactGroup> GetGroupList()
        {
            if (groupCache == null)
            {
                groupCache = new List<ContactGroup>();

                manager.Navigator.GoToGroupsPage();

                ICollection<IWebElement> elements = driver.FindElements(By.ClassName("group"));
                //By.CssSelector("span.group")
                // a Collection is generic collection, it's the base interface
                // all other collections (like List) implement this interface

                foreach (IWebElement element in elements)
                {
                    //option 1
                    //ContactGroup group = new ContactGroup(element.Text);
                    //group.Id = element.FindElement(By.TagName("input")).GetAttribute("value");
                    //groupCache.Add(group);

                    // option 2 - assign values to properties right after a new object is constructed
                    //ContactGroup group = new ContactGroup(element.Text) {
                    //    Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    //};
                    //groupCache.Add(group);

                    // shorter version of option 2
                    groupCache.Add(new ContactGroup(element.Text) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactGroup>(groupCache);
            // return not the cache but a copy to keep the cache private
            // so it's not corrupted/modified during execution by another method
        }
    }
}

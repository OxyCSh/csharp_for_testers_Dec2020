using OpenQA.Selenium;
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
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            IList<IWebElement> checkboxes = driver.FindElement(By.ClassName("group")).FindElements(By.TagName("input"));
            checkboxes[index].Click();
            return this;
        }

        public GroupHelper PressRemoveButton()
        {
            driver.FindElement(By.XPath("//input[@name='delete']")).Click();
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
                IList<IWebElement> groupEntries = driver.FindElements(By.ClassName("group"));
                return groupEntries.Count;
            }
            catch
            {
                return 0;
            }
        }
    }
}

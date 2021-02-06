using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : AuthenticationTestBase
    {
        // random data provider
        // it needs to be static as NUnit generates data at test compile time
        public static IEnumerable<ContactGroup> RandomGroupDataProvider()
        {
            List<ContactGroup> groups = new List<ContactGroup>();

            for (int i = 0; i < 5; i++)
            {
                groups.Add(new ContactGroup(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                });
            }

            return groups;
        }

        // data provider
        public static IEnumerable<ContactGroup> GroupDataFromFile()
        {
            List<ContactGroup> groups = new List<ContactGroup>();

            // read from a text file, comma separated value
            string[] lines = File.ReadAllLines(@"groups.csv"); // this file is created in the project (General - Text File)
            // in the file properties CopyToOutput Directory changed to Copy If Newer

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                groups.Add(new ContactGroup(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }

            return groups;
        }


        //[Test, TestCaseSource("RandomGroupDataProvider")] // using random data generator
        [Test, TestCaseSource("GroupDataFromFile")] // using data from a file
        public void GroupCreationTest(ContactGroup group)
        {
            // before we started using random string generator
            //ContactGroup group = new ContactGroup("Green group");
            //group.Header = "Green header";
            //group.Footer = "Green footer";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            /* use this only if this test often fails
            in this case it's quicker to compare the number of items and fail
            without spending time comparing the items in the lists;
            if the test is stable comparing count can be skipped
            as it's an unnecessary step that takes time
            */
            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            ContactGroup group = new ContactGroup("");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest_FailExpected()
        {
            // a group with a ' won't be created and the test will fail
            ContactGroup group = new ContactGroup("aaa'bbb");
            group.Header = "";
            group.Footer = "";

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList();

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList();

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}

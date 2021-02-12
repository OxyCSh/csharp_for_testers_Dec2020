using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml; // and add the library to the test project
// References - Add Reference - Assemblies - Framework - System.Xml
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Linq;

namespace AddressbookWebTests
{
    [TestFixture] // attributes
    public class GroupCreationTests : GroupTestBase
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
        public static IEnumerable<ContactGroup> GroupDataFromCSVFile()
        {
            List<ContactGroup> groups = new List<ContactGroup>();

            // read from a text file, comma separated value
            string[] lines = File.ReadAllLines(@"groups.csv");
            // this file is created in the project (Add - New Item - General - Text File)
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

        // data provider
        // an XML file is better than CSV because
        // 1 generated data can contain commas which will cause lines to be split incorrectly
        // 2 not possible to see what column contains what (e. g. what's group name, what's group footer)
        // 3 lack of visibility for multiline data
        public static IEnumerable<ContactGroup> GroupDataFromXMLFile()
        {
            // the file needs to be copied to the solution folder \CSharpForTesters2020_Addressbook
            // and added to the test project Add - Existing Item
            // in the file properties CopyToOutput Directory changed to Copy If Newer
            return (List<ContactGroup>) // converting an object returned by XmlSerializer to a list of groups
                new XmlSerializer(typeof(List<ContactGroup>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        // the file needs to be copied to the solution folder \CSharpForTesters2020_Addressbook
        // and added to the test project Add - Existing Item
        // in the file properties CopyToOutput Directory changed to Copy If Newer
        public static IEnumerable<ContactGroup> GroupDataFromJSONFile()
        {
            return JsonConvert.DeserializeObject<List<ContactGroup>>(File.ReadAllText(@"groups.json"));
        }


        //[Test, TestCaseSource("RandomGroupDataProvider")] // using random data generator
        //[Test, TestCaseSource("GroupDataFromCSVFile")] // using data from a CSV file
        //[Test, TestCaseSource("GroupDataFromXMLFile")] // using data from an XML file
        [Test, TestCaseSource("GroupDataFromJSONFile")] // using data from a JSON file
        public void GroupCreationTest(ContactGroup group)
        {
            // before we started using random string generator
            //ContactGroup group = new ContactGroup("Green group");
            //group.Header = "Green header";
            //group.Footer = "Green footer";

            List<ContactGroup> oldGroups = ContactGroup.GetAllGroupsFromDB();
            // groups from the DB but we can get them from the UI application.GroupHelper.GetGroupList()

            application.GroupHelper.CreateGroup(group);

            /* use this only if this test often fails
            in this case it's quicker to compare the number of items and fail
            without spending time comparing the items in the lists;
            if the test is stable comparing count can be skipped
            as it's an unnecessary step that takes time
            */
            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = ContactGroup.GetAllGroupsFromDB();

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

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList(); // from UI

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList(); // from UI

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

            List<ContactGroup> oldGroups = application.GroupHelper.GetGroupList(); // from UI

            application.GroupHelper.CreateGroup(group);

            Assert.AreEqual(oldGroups.Count + 1, application.GroupHelper.NumberOfGroups());

            List<ContactGroup> newGroups = application.GroupHelper.GetGroupList(); // from UI

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now; // to measure how long it takes to get groups from DB
            List<ContactGroup> groupsFromDB = ContactGroup.GetAllGroupsFromDB();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine("DB time\n{0} (start time {1}, end time {2})", end.Subtract(start), start, end);

            start = DateTime.Now; // to measure how long it takes to get groups from UI
            List<ContactGroup> groupsFromUI = application.GroupHelper.GetGroupList();
            end = DateTime.Now;
            System.Console.Out.WriteLine("UI time\n{0} (start time {1}, end time {2})", end.Subtract(start), start, end);
        }
    }
}

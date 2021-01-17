namespace AddressbookWebTests
{
    public class ContactGroup
    {
        //private string name;
        //private string header = "";
        //private string footer = "";

        public ContactGroup(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public string Header { get; set; } = null;
        public string Footer { get; set; } = null;
    }
}

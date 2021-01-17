namespace AddressbookWebTests
{
    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } = null;
        public string Address { get; set; } = null;
        public string Photo { get; set; } = "C:\\bunny.png";
        public int DayOfBirth { get; set; } = 1;
        public string MonthOfBirth { get; set; } = "January";
        public int YearOfBirth { get; set; } = 2000;
        public string ContactGroup { get; set; } = null;

        public Contact(string firstName)
        {
            FirstName = firstName;
        }
    }
}

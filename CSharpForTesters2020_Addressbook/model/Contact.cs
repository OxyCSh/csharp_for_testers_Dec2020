using System;

namespace AddressbookWebTests
{
    public class Contact : IEquatable<Contact>, IComparable<Contact>
    {
        public string Id { get; set; }
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

        public bool Equals(Contact other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public override string ToString()
        {
            return "first name = " + FirstName + ", last name = "+ LastName;
        }

        public int CompareTo(Contact other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;

            if (LastName.CompareTo(other.LastName) == 0)
                return FirstName.CompareTo(other.FirstName);
            else
                return LastName.CompareTo(other.LastName);
        }
    }
}

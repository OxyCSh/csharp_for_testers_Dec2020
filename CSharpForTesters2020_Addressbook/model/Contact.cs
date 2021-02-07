﻿using System;
using System.Text.RegularExpressions;

namespace AddressbookWebTests
{
    public class Contact : IEquatable<Contact>, IComparable<Contact>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } = null;
        public string Photo { get; set; } = @"bunny.png";
        public string Address { get; set; } = null;
        public string allPhones;
        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                    return allPhones;
                else
                    return allPhones = (CleanUpPhone(HomePhone) + CleanUpPhone(MobilePhone) + CleanUpPhone(WorkPhone)).Trim();
            }
            set { allPhones = value; }
        }
        public string HomePhone { get; set; } = null;
        public string MobilePhone { get; set; } = null;
        public string WorkPhone { get; set; } = null;
        public string allEmails;
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                    return allEmails;
                else
                    return allEmails = (AddEmailIfExists(Email) + AddEmailIfExists(Email2) + AddEmailIfExists(Email3)).Trim();
            }
            set
            { allEmails = value; }
        }
        public string Email { get; set; } = null;
        public string Email2 { get; set; } = null;
        public string Email3 { get; set; } = null;
        public int DayOfBirth { get; set; } = 1;
        public int MonthOfBirth { get; set; } = 1;
        public int YearOfBirth { get; set; } = 2000;
        public string ContactGroup { get; set; } = null;

        public string detailsView;
        public string DetailsView
        {
            get
            {
                if (detailsView != null)
                    return detailsView;
                else
                {
                    if (FirstName != null && FirstName!= "") detailsView = detailsView + FirstName;
                    if (LastName != null && LastName != "") detailsView = detailsView + " " + LastName;
                    if (Address != null && Address != "") detailsView = detailsView + "\r\n" + Address;

                    if (PhoneExists(HomePhone) || PhoneExists(MobilePhone) || PhoneExists(WorkPhone))
                    {
                        detailsView = detailsView + "\r\n";
                        if (PhoneExists(HomePhone)) detailsView = detailsView + "\r\n" + "H: " + HomePhone;
                        if (PhoneExists(MobilePhone)) detailsView = detailsView + "\r\n" + "M: " + MobilePhone;
                        if (PhoneExists(WorkPhone)) detailsView = detailsView + "\r\n" + "W: " + WorkPhone;
                    }
                    if (AllEmails != null && AllEmails != "") detailsView = detailsView + "\r\n\r\n" + AllEmails;

                    return detailsView.Trim();
                }
            }
            set { detailsView = value; }
        }

        public Contact()
        {
        }

        public Contact(string firstName)
        {
            FirstName = firstName;
        }

        private string CleanUpPhone(string phone)
        {
            if (phone == null || phone == "")
                return "";

            // before using a regular expression
            //return phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";

            // parameters - string, reg expression in square brackets, replace with what
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";
        }

        private string AddEmailIfExists(string email)
        {
            if (email == null || email == "")
                return "";
            return email + "\r\n";
        }

        private bool PhoneExists(string phone)
        {
            return phone != null && phone != "";
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

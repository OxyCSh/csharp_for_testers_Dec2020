using NUnit.Framework;
using System;
using System.Text;

namespace AddressbookWebTests
{
    public class TestBase
    {
        protected ApplicationManager application;

        [SetUp]
        public void SetupApplicationManager()
        {
            application = ApplicationManager.GetInstance();
            application.Navigator.OpenHomePage();

            // option 1
            //driver = new ChromeDriver("C:\\SeleniumDrivers"); // a path to chromedriver as a parameter

            //Process cmd = new Process();
            ////cmd.StartInfo.FileName = "chromedriver.exe";
            ////cmd.StartInfo.Arguments = "-version"; // to display the version of chromedriver
            //cmd.StartInfo.FileName = "cmd.exe";
            //cmd.StartInfo.Arguments = "/C where chromedriver"; // to display the path of chromedriver
            //cmd.StartInfo.RedirectStandardInput = true;
            //cmd.StartInfo.RedirectStandardOutput = true;
            //cmd.StartInfo.CreateNoWindow = true;
            //cmd.StartInfo.UseShellExecute = false;
            //cmd.Start();
            //cmd.WaitForExit();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            // option 2
            // didn't work initially with the installed Chrome v87 because
            // it tried to use not compatible chromedriver v85 which it found in /bin/Debug
            //driver = new ChromeDriver();
        }

        // moved outside the method so the same generator is used instead of initializing one each time
        public static Random rnd = new Random();

        
        public static string GenerateRandomString(int max) // max number of characters
        {
            // Random rnd = new Random();
            /* initializes using the current time in miliseconds,
             * if the method is run several times during the same milisecond,
            the generator is initialized several times in the same milisecond
            and the same numbers/string are generated*/

            int l = Convert.ToInt32(rnd.NextDouble() * max); // random number of characters up to max
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
               builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65))); // random codes from ASCII
            }
            return builder.ToString();
        }

        public static DateTime GenerateRandomDate(int yearMin, int yearMax)
        {

            int year = rnd.Next(yearMin, yearMax);
            int month = rnd.Next(1, 12);
            int day = DateTime.DaysInMonth(year, month);
            int Day = rnd.Next(1, day);

            return new DateTime(year, month, Day);
        }
    }
}

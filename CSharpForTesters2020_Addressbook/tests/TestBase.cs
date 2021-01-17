using NUnit.Framework;


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
    }
}

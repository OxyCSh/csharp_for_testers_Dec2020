using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddressbookWebTests;

    /*
     two projects in the same solution are independent
    they don't see each other and can't use each other's methods
    until
    1 we build dependencies (the generator project - menu - Build Dependencies - Project Dependencies -
    depends on the test project) so that the build happens in the right order
    2 in References we add reference to the test project under Projects/Solution
    3 add using AddressbookWebTests;
    */

namespace Addressbook_test_data_generators
{
    /*
    a generator that saves random test data to a file so the tests can be re-run
    multiple times with exactly the same test data, e. g. when debugging
    (if using a on-the-fly generator, new test data would be generated each run)
    */
    class Program
    {

        // the exe file C:\Users\oniva\source\repos\OxyCSh\csharp_for_testers_Dec2020\Addressbook-test_data_generators\bin\Debug
        // to run open the console, navigate to this directory and run the exe file like
        // Addressbook-test_data_generators.exe 17 groups_data.csv
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]); // the 1st parameter is the number of test data records we want to generate
            StreamWriter writer = new StreamWriter(args[1]); // the 2nd parameter is the filename

            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                    TestBase.GenerateRandomString(5),
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)
                    ));
            }

            writer.Close();
        }
    }
}

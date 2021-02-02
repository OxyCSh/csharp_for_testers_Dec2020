using NUnit.Framework;

namespace AddressbookWebTests
{
    [TestFixture]
    public class SearchTests : AuthenticationTestBase
    {
        [Test]
        public void SearchTest()
        {
            System.Console.Out.WriteLine(application.ContactHelper.GetNumberOfSearchResults());
        }
    }
}

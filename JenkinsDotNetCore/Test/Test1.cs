using NUnit.Framework;

namespace JenkinsDotNetCore.Test
{
    [TestFixture, Parallelizable]
    public class Test1 : TestSetup
    {

        [Test]
        public void TestCase1()
        {
            driver.Navigate().GoToUrl("https://www.google.co.in/");
            ReportLog("Navigated to google");
        }

        [Test]
        public void TestCase2()
        {
            driver.Navigate().GoToUrl("https://login.salesforce.com/");
            ReportLog("Navigated to salesforce");
        }
    }
}
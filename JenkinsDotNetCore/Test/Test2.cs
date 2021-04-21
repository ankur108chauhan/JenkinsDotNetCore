using NUnit.Framework;

namespace JenkinsDotNetCore.Test
{
    [TestFixture, Parallelizable]
    public class Test2 : TestSetup
    {

        [Test]
        public void TestCase1()
        {
            driver.Navigate().GoToUrl("https://www.msnagile.com/");
            ReportLog("Navigated to MsnAgile");
        }

        [Test]
        public void TestCase2()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/");
            ReportLog("Navigated to Greenkart");
        }
    }
}
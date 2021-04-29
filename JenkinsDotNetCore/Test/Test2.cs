
using JenkinsDotNetCore.Utils.ReportUtil;
using NUnit.Framework;

namespace JenkinsDotNetCore.Test
{
    [TestFixture, Parallelizable]
    public class Test2 : TestSetup
    {

        [SetUp]
        public void Setup()
        {
            driver = driverUtil.Initialize();
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestCase1()
        {
            driver.Navigate().GoToUrl("https://www.instagram.com/");
            ReportLog("Navigated to Instagram");
        }

        [Test]
        public void TestCase2()
        {
            driver.Navigate().GoToUrl("https://rahulshettyacademy.com/seleniumPractise/#/");
            ReportLog("Navigated to Greenkart");
            //driver.FindElement(By.Id("username")).Click();
        }
    }
}
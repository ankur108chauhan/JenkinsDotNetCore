using NUnit.Framework;
using System;
using System.IO;

namespace JenkinsDotNetCore.Test
{
    [TestFixture, Parallelizable]
    public class Test2 : TestSetup
    {

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
            Console.WriteLine(Environment.Directory);
            Assert.Fail();
        }
    }
}

using AventStack.ExtentReports;
using JenkinsDotNetCore.Utils;
using JenkinsDotNetCore.Utils.ReportUtil;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace JenkinsDotNetCore.Test
{
    [TestFixture]
    public class TestSetup
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            ExtentService.Instance.Flush();
        }

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void EndReport()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;
                var errorMessage = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                        ? ""
                        : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message);

                var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
                        ? ""
                        : string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.StackTrace);

                Status logstatus;

                switch (status)
                {
                    case TestStatus.Failed:
                        logstatus = Status.Fail;
                        string screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        ExtentTestManager.GetTest().Log(logstatus, "Test " + logstatus);
                        ExtentTestManager.GetTest().Log(logstatus, errorMessage);
                        ExtentTestManager.GetTest().Log(logstatus, stackTrace);
                        ExtentTestManager.GetTest().Log(logstatus, "Screenshot");
                        ExtentTestManager.GetTest().AddScreenCaptureFromPath(screenShotPath);
                        break;

                    case TestStatus.Skipped:
                        logstatus = Status.Skip;
                        ExtentTestManager.GetTest().Log(logstatus, "Test " + logstatus);
                        break;

                    case TestStatus.Passed:
                        logstatus = Status.Pass;
                        ExtentTestManager.GetTest().Log(logstatus, "Test " + logstatus);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                driver.Quit();
            }
        }

        public string Capture(IWebDriver driver, string screenShotName)
        {
            string screenshotDirectory = Path.Combine(ConfigUtil.GetProjectRootDirectory(), "Screenshot");
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string title = screenShotName + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string path = screenshotDirectory + "\\" + title.Replace("(\"", "_").Replace("\")", "_") + ".png";
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }

        public void ReportLog(String message)
        {
            ExtentTestManager.GetTest().Log(Status.Info, message);
        }
    }
}
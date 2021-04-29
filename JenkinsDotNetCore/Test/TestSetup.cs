using AventStack.ExtentReports;
using JCMSFramework.Utils;
using JenkinsDotNetCore.Utils;
using JenkinsDotNetCore.Utils.ReportUtil;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using System;
using System.IO;

namespace JenkinsDotNetCore.Test
{
    [TestFixture]
    public class TestSetup
    {
        public IWebDriver driver;
        public DriverUtil driverUtil;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            driverUtil = new DriverUtil();
            ExtentTestManager.CreateParentTest(GetType().Name);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            ExtentService.Instance.Flush();
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

                switch (status)
                {
                    case TestStatus.Failed:
                        //var screenShotPath = Capture(driver, TestContext.CurrentContext.Test.Name);
                        ExtentTestManager.GetTest().Fail("Test Failed");
                        ExtentTestManager.GetTest().Fail(errorMessage);
                        ExtentTestManager.GetTest().Fail(stackTrace);
                        ExtentTestManager.GetTest().Fail("Screenshot");
                        ExtentTestManager.GetTest().Fail("Screenshot   ", CaptureScreenshot(TestContext.CurrentContext.Test.Name));
                        //ExtentTestManager.GetTest().AddScreenCaptureFromPath(screenShotPath);
                        break;

                    case TestStatus.Skipped:
                        ExtentTestManager.GetTest().Skip("Test Skipped");
                        break;

                    case TestStatus.Passed:
                        ExtentTestManager.GetTest().Pass("Test Passed");
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

        public MediaEntityModelProvider CaptureScreenshot(string name)
        {
            string screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }

        public string Capture(IWebDriver driver, string screenShotName)
        {
            string screenshotDirectory = Path.Combine(ConfigUtil.GetProjectRootDirectory(), "Report", "Screenshot");
            if (!Directory.Exists(screenshotDirectory))
            {
                Directory.CreateDirectory(screenshotDirectory);
            }
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            var title = screenShotName + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            var path = Path.Combine(screenshotDirectory, title.Replace("(\"", "_").Replace("\")", "_") + ".png");
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Png);
            return path;
        }

        public void ReportLog(String message)
        {
            ExtentTestManager.GetTest().Log(Status.Info, message);
        }
    }
}
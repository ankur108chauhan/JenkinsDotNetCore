using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;

namespace JCMSFramework.Utils
{
    public class DriverUtil
    {
        private IWebDriver driver;
        public IWebDriver Initialize()
        {
            try
            {
                string browser = TestContext.Parameters["Browser"].ToString();
                driver = SetDriver(browser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return driver;
        }

        private IWebDriver SetDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    driver = InitChromeDriver();
                    break;
                case "firefox":
                    driver = InitFirefoxDriver();
                    break;
                case "all":
                    driver = InitChromeDriver();
                    driver = InitFirefoxDriver();
                    break;
                default:
                    driver = InitChromeDriver();
                    break;
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

        private IWebDriver InitChromeDriver()
        {
            driver = new ChromeDriver();
            return driver;
        }

        private IWebDriver InitFirefoxDriver()
        {
            FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
            service.Host = "::1";
            return new FirefoxDriver(service);
        }
    }
}



using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Protractor;

namespace Core
{
    public class DriversPool
    {
        public static IWebDriver GetWebDriver(bool isProtractor = true)
        {
            if (!Configuration.Instance.Selenium.IsLocal) return null;
            string chromeDriverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..",
                Configuration.Instance.Selenium.ChromeDriverPath);

            ChromeOptions options = new ChromeOptions();
            //options.AddArguments("--disable-popup-blocking");

            IWebDriver driver = new ChromeDriver(chromeDriverPath, options);
            PrepareDriver(driver);
            driver = new NgWebDriver(driver);
            driver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Configuration.Instance.Selenium.ProtractorTimeOut));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(Configuration.Instance.Selenium.ProtractorTimeOut));
            return driver;
        }

        private static void PrepareDriver(IWebDriver driver)
        {
            if (driver == null) return;

            driver.Manage().Window.Maximize();
            driver.Url = Configuration.Instance.App.Url;
        }
    }
}
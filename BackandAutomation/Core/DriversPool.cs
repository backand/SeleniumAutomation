﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Protractor;
using System;
using System.IO;

namespace Core
{
    public class DriversPool
    {
        public static IWebDriver GetWebDriver()
        {
            var driver = !Configuration.Instance.Selenium.IsLocal ? GetRemoteWebDriver() : GetLocalWebDriver();
            PrepareDriver(driver);
            driver = SetAngularProctractorDriver(driver);
            return driver;
        }

        private static IWebDriver GetLocalWebDriver()
        {
            var chromeDriverPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..",
                Configuration.Instance.Selenium.ChromeDriverPath);

            var options = new ChromeOptions();

            return new ChromeDriver(chromeDriverPath, options);
        }

        private static IWebDriver GetRemoteWebDriver()
        {
            IWebDriver driver = new RemoteWebDriver(new Uri(Configuration.Instance.Selenium.RemoteGridHub),
                DesiredCapabilities.Chrome());


            return driver;
        }

        private static IWebDriver SetAngularProctractorDriver(IWebDriver driver)
        {
            driver = new NgWebDriver(driver);
            driver.Manage()
                .Timeouts()
                .SetPageLoadTimeout(TimeSpan.FromSeconds(Configuration.Instance.Selenium.ProtractorTimeOut));
            driver.Manage()
                .Timeouts()
                .SetScriptTimeout(TimeSpan.FromSeconds(Configuration.Instance.Selenium.ProtractorTimeOut));
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
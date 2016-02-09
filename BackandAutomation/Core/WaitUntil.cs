using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core
{
    public class WaitUntil
    {
        private const int TimeOut = 20;
        public IWebDriver Driver { get; set; }

        public WaitUntil(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement UntilElementExists(By findBy, int timeOut = TimeOut)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut));
            IWebElement element = null;
            wait.Until(driver => driver.TryFindElement(findBy, out element));
            return element;
        }

        public IAlert UntilAlertPoppesUp(int timeOut = TimeOut)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut));
            wait.IgnoreExceptionTypes(typeof(NoAlertPresentException));
            IAlert alert = wait.Until(driver => driver.SwitchTo().Alert());
            return alert;
        }
    }
}
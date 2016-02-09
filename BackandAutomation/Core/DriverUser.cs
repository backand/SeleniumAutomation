using System.Threading;
using OpenQA.Selenium;

namespace Core
{
    public class DriverUser
    {
        public DriverUser(IWebDriver driver)
        {
            Driver = driver;
            WaitUntil = new WaitUntil(Driver);
        }
        
        public IWebDriver Driver { get; set; }
        public WaitUntil WaitUntil { get; set; }
    }
}
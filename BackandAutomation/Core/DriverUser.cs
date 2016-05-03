using OpenQA.Selenium;

namespace Core
{
    public class DriverUser
    {
        protected DriverUser(IWebDriver driver)
        {
            Driver = driver;
            WaitUntil = new WaitUntil(Driver);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        protected DriverUser(DriverUser driver) : this(driver.Driver)
        {
        }

        protected ScreenshotsContainer ScreenshotsContainer { get; set; }
        public IWebDriver Driver { get; }
        protected WaitUntil WaitUntil { get; private set; }
    }
}
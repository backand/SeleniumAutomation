using OpenQA.Selenium;

namespace Core
{
    public class DriverUser
    {
        public DriverUser(IWebDriver driver)
        {
            Driver = driver;
            WaitUntil = new WaitUntil(Driver);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        public ScreenshotsContainer ScreenshotsContainer { get; set; }
        public IWebDriver Driver { get; set; }
        public WaitUntil WaitUntil { get; set; }
    }
}
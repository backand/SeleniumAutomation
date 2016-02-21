using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class AppSettingsPage : DriverUser
    {
        public AppSettingsPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
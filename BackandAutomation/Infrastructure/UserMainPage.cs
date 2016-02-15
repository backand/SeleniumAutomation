using Core;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserMainPage : DriverUser
    {
        public UserMainPage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement SettingsElement => Driver.FindElement(By.ClassName("nav-profile"));

        public UserSettings Settings => new UserSettings(Driver, SettingsElement);
    }
}
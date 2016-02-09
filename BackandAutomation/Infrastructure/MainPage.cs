using Core;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserMainPage : DriverUser
    {
        public UserMainPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
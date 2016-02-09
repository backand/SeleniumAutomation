using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Infrastructure
{
    public class BasePage : DriverUser
    {
        public BasePage(IWebDriver driver) :base(driver)
        {
            PageFactory.InitElements(Driver, this);
        }
    }
}
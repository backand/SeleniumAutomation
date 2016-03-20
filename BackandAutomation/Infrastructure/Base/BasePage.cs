using System.Collections;
using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Infrastructure.Base
{
    public class BasePage : DriverUser
    {
        public BasePage(DriverUser driverUser) : base(driverUser)
        {
            PageFactory.InitElements(Driver, this);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        public BasePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        public void SubmitScreenshot()
        {
            ScreenshotsContainer.AddScreenshot();
        }
    }
}
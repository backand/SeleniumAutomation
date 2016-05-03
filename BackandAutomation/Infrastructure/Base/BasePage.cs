using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Infrastructure.Base
{
    public abstract class BasePage : DriverUser
    {
        protected BasePage(DriverUser driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        protected BasePage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(Driver, this);
            ScreenshotsContainer = new ScreenshotsContainer(Driver);
        }

        protected void SubmitScreenshot()
        {
            ScreenshotsContainer.AddScreenshot();
        }
    }
}
using Core;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class AppSettingsPage : BasePage
    {
        public AppSettingsPage(DriverUser driver) : base(driver)
        {
            SubmitScreenshot();
        }

        public UserMainPage Delete()
        {
            DeleteElement.Click();
            OkDialog dialog = WaitUntil.UntilOkDialogPopUp();
            dialog.Ok();
            return new UserMainPage(this);
        }

        private IWebElement DeleteElement => Driver.FindElement(Selectors.ManageAppSettings.Delete);
    }
}
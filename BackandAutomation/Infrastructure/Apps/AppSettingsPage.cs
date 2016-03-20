using Core;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class AppSettingsPage : BasePage
    {
        public AppSettingsPage(IWebDriver driver) : base(driver)
        {
            SubmitScreenshot();
        }

        public UserMainPage Delete()
        {
            DeleteElement.Click();
            ModalDialog dialog = WaitUntil.UntilDialogPopUp();
            dialog.Ok();
            return new UserMainPage(Driver);
        }

        public IWebElement DeleteElement => Driver.FindElement(Selectors.ManageAppSettings.Delete);
    }
}
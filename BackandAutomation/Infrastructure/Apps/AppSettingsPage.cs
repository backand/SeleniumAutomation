using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class AppSettingsPage : DriverUser
    {
        public AppSettingsPage(IWebDriver driver) : base(driver)
        {
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
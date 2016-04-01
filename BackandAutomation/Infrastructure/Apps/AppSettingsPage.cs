using Core;
using Core.Dialogs;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    [BackandPageType(LeftMenuOption.Settings, LeftMenuOption.General)]
    public class AppSettingsPage : BackandApplicationsBasePage
    {
        public AppSettingsPage(DriverUser driver) : base(driver)
        {
        }

        public UserMainPage Delete()
        {
            DeleteElement.Click();
            OkDialog dialog = WaitUntil.UntilDialogPopUp<OkDialog>();
            dialog.Ok();
            return new UserMainPage(this);
        }

        private IWebElement DeleteElement => Driver.FindElement(Selectors.ManageAppSettings.Delete);
    }
}
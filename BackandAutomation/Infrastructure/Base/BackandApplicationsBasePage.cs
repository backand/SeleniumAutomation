using Core;
using Infrastructure.Apps;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class BackandApplicationsBasePage : BasePage
    {
        public BackandApplicationsBasePage(DriverUser driverUser) : base(driverUser)
        {
            SubmitScreenshot();
        }

        private IWebElement SettingsElement => TopNav.FindElement(Selectors.BackandApplicationsBasePage.Settings);

        public UserSettings Settings => new UserSettings(Driver, SettingsElement);

        protected IWebElement PageElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.Page);
        protected IWebElement LeftMenuElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.LeftMenu);
        protected IWebElement TopNav => Driver.FindElement(Selectors.BackandApplicationsBasePage.TopNav);

        public UserMainPage GoToHomePage()
        {
            TopNav.FindElement(Selectors.Common.GoToHomePage).Click();
            return new UserMainPage(this);
        }

        public CurrentAppComponent CurrentAppComponent => 
            new CurrentAppComponent(Driver, TopNav.FindElement(Selectors.BackandApplicationBasic.CurrentApp));
    }
}
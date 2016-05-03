using Core;
using Infrastructure.Apps;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public abstract class BackandApplicationsBasePage : BasePage
    {
        protected BackandApplicationsBasePage(DriverUser driver) : base(driver)
        {
            SubmitScreenshot();
        }

        private IWebElement SettingsElement => TopNav.FindElement(Selectors.BackandApplicationsBasePage.Settings);

        public UserSettings Settings => new UserSettings(Driver, SettingsElement);

        protected IWebElement PageElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.Page);
        private IWebElement TopNav => Driver.FindElement(Selectors.BackandApplicationsBasePage.TopNav);

        public CurrentAppComponent CurrentAppComponent =>
            new CurrentAppComponent(Driver, TopNav.FindElement(Selectors.BackandApplicationBasic.CurrentApp));

        public LeftMenu LeftMenu => new LeftMenu(this);

        public UserMainPage GoToHomePage()
        {
            TopNav.FindElement(Selectors.Common.GoToHomePage).Click();
            return new UserMainPage(this);
        }
    }
}
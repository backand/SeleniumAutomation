﻿using Core;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class BackandApplicationsBasePage : BasePage
    {
        public BackandApplicationsBasePage(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement SettingsElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.Settings);

        public UserSettings Settings => new UserSettings(Driver, SettingsElement);

        protected IWebElement PageElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.Page);
        protected IWebElement LeftMenuElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.LeftMenu);
    }
}
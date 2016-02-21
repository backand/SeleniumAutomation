﻿using Core;
using Infrastructure.Apps;
using Infrastructure.Base;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserMainPage : BackandApplicationsBasePage
    {
        public UserMainPage(IWebDriver driver) : base(driver)
        {
            IWebElement intercomElement;
            if(Driver.TryFindElement(By.ClassName("intercom-launcher-preview-close"),out intercomElement));
                intercomElement.TryClick();
        }

        public AppsFeed AppsFeed => new AppsFeed(Driver, PageElement);
    }
}
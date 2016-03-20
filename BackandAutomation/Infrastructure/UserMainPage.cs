using Core;
using Infrastructure.Apps;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserMainPage : BackandApplicationsBasePage
    {
        public UserMainPage(DriverUser driverUser) : base(driverUser)
        {
            IWebElement intercomElement;
            if(Driver.TryFindElement(By.ClassName("intercom-launcher-preview-close"),out intercomElement));
                intercomElement.TryClick();
            SubmitScreenshot();
        }

        public AppsFeed AppsFeed => new AppsFeed(Driver, PageElement);
    }
}
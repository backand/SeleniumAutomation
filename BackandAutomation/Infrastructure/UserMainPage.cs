using Infrastructure.Apps;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserMainPage : BackandApplicationsBasePage
    {
        public UserMainPage(IWebDriver driver) : base(driver)
        {
        }

        public AppsFeed AppsFeed => new AppsFeed(Driver, AppsFeedElement);

        private IWebElement AppsFeedElement => Driver.FindElement(By.Id("apps-page"));
    }
}
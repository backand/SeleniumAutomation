using System.Collections.Generic;
using System.Linq;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class AppsFeed : DriverUser
    {
        public AppsFeed(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        private IWebElement MainElement { get; }

        public IEnumerable<BackandAppPannel> AppsPannels
            =>
                MainElement.FindElements(By.CssSelector(".app-panel"))
                    .Select(element => new BackandAppPannel(Driver, element));

        public NewAppForm New()
        {
            return new NewAppForm(Driver,
                AppsPannels.FirstOrDefault(app => app.RibbonType == RibbonType.New)?.MainElement);
        }
    }
}
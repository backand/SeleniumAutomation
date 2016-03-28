using Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Infrastructure.Apps
{
    public class BackandAppPannel : BackandAppPannelBase
    {
        public BackandAppPannel(IWebDriver driver, IWebElement mainElement) : base(driver, mainElement)
        {
            new Actions(Driver).MoveToElement(MainElement).Perform();
        }

        private IWebElement SettingsElement => MainElement.FindElement(Selectors.AppForm.Settings);
        private IWebElement ManageAppElement => MainElement.FindElement(Selectors.AppForm.ManageApp);
        public string Name => NameElement.Text;
        public string Title => TitleElement.Text;

        public AppSettingsPage MoveToAppSettingsPage()
        {
            SettingsElement.Click();
            return new AppSettingsPage(this);
        }

        public ManageAppPage ManageApp()
        {
            ManageAppElement.Click();
            return new ManageAppPage(this);
        }
    }
}
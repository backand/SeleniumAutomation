using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class BackandAppPannel : BackandAppPannelBase
    {
        public BackandAppPannel(IWebDriver driver, IWebElement mainElement) : base(driver, mainElement)
        {
            
        }

        private IWebElement SettingsElement => MainElement.FindElement(Selectors.AppForm.Settings);
        private IWebElement ManageAppElement => MainElement.FindElement(Selectors.AppForm.ManageApp);
        public string Name => NameElement.Text;
        public string Title => TitleElement.Text;

        public AppSettingsPage MoveToAppSettingsPage()
        {
            SettingsElement.Click();
            return new AppSettingsPage(Driver);
        }

        public ManageAppPage ManageApp()
        {
            ManageAppElement.Click();
            return new ManageAppPage(Driver);
        }
    }
}
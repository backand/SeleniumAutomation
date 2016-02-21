using System.Collections.Generic;
using System.Linq;
using Core;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class NewAppForm : BackandAppPannelBase
    {
        public NewAppForm(IWebDriver driver, IWebElement mainElement) : base(driver, mainElement)
        {
        }

        public string Name
        {
            get { return NameElement.Text; }
            set { Driver.Hover(NameElement).SendKeys(value); }
        }

        public string Title
        {
            get { return TitleElement.Text; }
            set { TitleElement.SendKeys(value); }
        }

        public KickstartPage Submit()
        {
            MainElement.FindElement(Selectors.AppForm.SubmitNew).Click();
            var dialog = WaitUntil.UntilDialogPopUp();
            dialog.Ok();
            return new KickstartPage(Driver);
        }
    }

    public class KickstartPage : BackandApplicationsBasePage
    {
        public KickstartPage(IWebDriver driver) : base(driver)
        {
        }

        public CurrentAppComponent CurrentAppComponent
            => new CurrentAppComponent(Driver, TopNav.FindElement(Selectors.Kickstart.CurrentApp));
    }

    public class CurrentAppComponent : DriverUser
    {
        public CurrentAppComponent(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        public IWebElement MainElement { get; set; }
        public string Name => Current.Text;

        private IWebElement Current => OptionElements.Single(option => option.Selected);

        private IEnumerable<IWebElement> OptionElements => MainElement.FindElements(Selectors.Kickstart.Option);
    }
}
using System.Collections.Generic;
using System.Linq;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class CurrentAppComponent : DriverUser
    {
        public CurrentAppComponent(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        public IWebElement MainElement { get; set; }
        public string Name => Current.Text;

        private IWebElement Current => OptionElements.Single(option => option.Selected);

        private IEnumerable<IWebElement> OptionElements
            => MainElement.FindElements(Selectors.BackandApplicationBasic.Option);
    }
}
using System.Linq;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class BackandAppPannelBase : DriverUser
    {
        protected BackandAppPannelBase(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        internal IWebElement MainElement { get; set; }
        protected IWebElement NameElement => MainElement.FindElement(Selectors.AppForm.AppHead);
        protected IWebElement TitleElement => MainElement.FindElement(Selectors.AppForm.Title);

        public RibbonType? RibbonType
        {
            get
            {
                var ribbonType = MainElement.FindElement(By.ClassName(Selectors.AppForm.RibbonElementSelector))
                    .GetClasses()
                    .FirstOrDefault(c => c != Selectors.AppForm.RibbonElementSelector && c.Contains("ui-ribbon-"))?
                    .TrimStart("ui-ribbon-".ToCharArray())
                    .ToEnum<RibbonType>();
                return ribbonType;
            }
        }
    }
}
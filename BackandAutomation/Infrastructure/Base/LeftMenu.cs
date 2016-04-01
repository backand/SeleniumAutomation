using System.Collections.Generic;
using System.Linq;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class LeftMenu : DriverUser
    {
        public LeftMenu(DriverUser driver) : base(driver)
        {
            _feedFactory = new FeedFactory(this);
        }

        private IWebElement MainElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.LeftMenu);
        private readonly FeedFactory _feedFactory;

        public T FetchPage<T>(string pageName = null) where T : BackandApplicationsBasePage
        {
            IEnumerable<IWebElement> elements =
                MainElement.GetChildren().Where(element => element.TagName == "li");

            List<IWebElement> expandedOptions =
                elements.Where(option => option.IsOpen() || (option.Text.Contains("Objects") && !option.IsOpen()))
                    .ToList();

            foreach (IWebElement expandedOption in expandedOptions)
            {
                expandedOption.FindElement(By.TagName("a")).Click();
            }
            return _feedFactory.Create<T>(pageName);
        }
    }
}
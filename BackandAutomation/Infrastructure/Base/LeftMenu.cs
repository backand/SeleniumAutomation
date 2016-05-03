using System.Linq;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class LeftMenu : DriverUser
    {
        private readonly FeedFactory _feedFactory;

        public LeftMenu(DriverUser driver) : base(driver)
        {
            _feedFactory = new FeedFactory(this);
        }

        private IWebElement MainElement => Driver.FindElement(Selectors.BackandApplicationsBasePage.LeftMenu);

        public T FetchPage<T>(string pageName = null) where T : BackandApplicationsBasePage
        {
            var elements =
                MainElement.GetChildren().Where(element => element.TagName == "li");

            var expandedOptions =
                elements.Where(option => option.IsOpen() || (option.Text.Contains("Objects") && !option.IsOpen()))
                    .ToList();

            foreach (var expandedOption in expandedOptions)
            {
                expandedOption.FindElement(By.TagName("a")).Click();
            }
            return _feedFactory.Create<T>(pageName);
        }
    }
}
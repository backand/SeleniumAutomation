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

        public BackandApplicationsBasePage Create(params LeftMenuOption[] options)
        {
            return _feedFactory.Create(options);
        }
    }
}
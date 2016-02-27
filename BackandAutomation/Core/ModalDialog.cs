using OpenQA.Selenium;

namespace Core
{
    public class ModalDialog : DriverUser
    {
        public ModalDialog(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        public ModalDialog(DriverUser driver, IWebElement mainElement) : base(driver.Driver)
        {
            MainElement = mainElement;
        }

        public ModalDialog(IWebDriver driver) : base(driver)
        {
            MainElement = Driver.FindElement(Selectors.ModalDialog.MainElement);
        }

        public IWebElement MainElement { get; set; }

        public string Title => MainElement.FindElement(Selectors.ModalDialog.Title).Text;

        public void Ok()
        {
            MainElement.FindElement(Selectors.ModalDialog.Ok).Click();
        }
    }
}
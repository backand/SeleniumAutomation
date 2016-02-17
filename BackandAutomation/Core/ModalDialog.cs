using OpenQA.Selenium;

namespace Core
{
    public class ModalDialog : DriverUser
    {
        public ModalDialog(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
        }

        public IWebElement MainElement { get; set; }

        public string Title => MainElement.FindElement(Selectors.ModalDialog.Title).Text;

        public void Ok()
        {
            MainElement.FindElement(Selectors.ModalDialog.Ok).Click();
        }
    }
}
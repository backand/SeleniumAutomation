using OpenQA.Selenium;

namespace Core
{
    public class ModalDialog : DriverUser
    {
        public ModalDialog(IWebDriver driver) : base(driver)
        {
            MainElement = Driver.FindElement(Selectors.ModalDialog.MainElement);
        }

        protected IWebElement MainElement { get; }
    }
}
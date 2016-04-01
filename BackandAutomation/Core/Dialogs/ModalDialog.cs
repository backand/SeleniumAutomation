using OpenQA.Selenium;

namespace Core.Dialogs
{
    public abstract class ModalDialog : DriverUser
    {
        protected ModalDialog(IWebDriver driver) : base(driver)
        {
            MainElement = Driver.FindElement(Selectors.ModalDialog.MainElement);
        }

        protected IWebElement MainElement { get; }

        protected void SubmitAction(string action)
        {
            MainElement.FindElement(By.CssSelector($"[ng-click*='{action}']")).Click();
        }

        protected void FindElementAndSendKeys(string id, string value)
        {
            FindElement(id).SendKeys(value);
        }

        protected IWebElement FindElement(string id) => MainElement.FindElement(By.Id(id));
    }
}
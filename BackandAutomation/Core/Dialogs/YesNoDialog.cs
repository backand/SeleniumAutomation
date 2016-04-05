using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class YesNoDialog : ModalDialog
    {
        public YesNoDialog(IWebDriver driver) : base(driver)
        {
        }

        public void Yes()
        {
            Driver.FindElement(By.CssSelector(".modal-footer [ng-click*='true']")).Click();
        }

        public void No()
        {
            Driver.FindElement(By.CssSelector(".modal-footer [ng-click*='false']")).Click();
        }
    }
}
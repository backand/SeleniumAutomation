using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class OkDialog : ModalDialog
    {
        public OkDialog(IWebDriver driver) : base(driver)
        {
        }
        
        public string Title => MainElement.FindElement(Selectors.ModalDialog.Title).Text;
        
        public void Ok()
        {
            MainElement.FindElement(Selectors.ModalDialog.Ok).Click();
        }
    }
}
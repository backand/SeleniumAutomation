using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class NewAppForm : BackandAppPannelBase
    {
        public NewAppForm(IWebDriver driver, IWebElement mainElement) : base(driver, mainElement)
        {
        }

        public string Name
        {
            get { return NameElement.Text; }
            set { Driver.Hover(NameElement).SendKeys(value); }
        }

        public string Title
        {
            get { return TitleElement.Text; }
            set { TitleElement.SendKeys(value); }
        }

        public KickstartPage Submit()
        {
            MainElement.FindElement(Selectors.AppForm.SubmitNew).Click();
            ModalDialog dialog = WaitUntil.UntilDialogPopUp();
            dialog.Ok();
            return new KickstartPage(Driver);
        }
    }
}
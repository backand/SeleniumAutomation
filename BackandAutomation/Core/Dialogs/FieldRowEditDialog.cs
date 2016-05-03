using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class FieldRowEditDialog : ModalDialog
    {
        public FieldRowEditDialog(IWebDriver driver) : base(driver)
        {
        }

        public void Delete()
        {
            MainElement.FindElement(By.Id("delete-field-button")).Click();
            WaitUntil.UntilDialogPopUp<YesNoDialog>().Yes();
        }
    }
}
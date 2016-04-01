using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class NewRowDialog : ModalDialog
    {
        public NewRowDialog(IWebDriver driver) : base(driver)
        {
        }

        private IWebElement NameElement => MainElement.FindElement(By.Id("name_text"));
        private IWebElement DescriptionEement => MainElement.FindElement(By.Id("description_textarea"));
        private IWebElement UserElement => MainElement.FindElement(By.Id("name_text"));

        public string Name
        {
            get { return NameElement.Text; }
            set { NameElement.SendKeys(value); }
        }

        public string Description
        {
            get { return DescriptionEement.Text; }
            set { DescriptionEement.SendKeys(value); }
        }

        public void Save()
        {
            SubmitAction("saveRow");
        }

        public NewRowDialog SaveAndNew()
        {
            SubmitAction("saveAndNew");
            return WaitUntil.UntilDialogPopUp<NewRowDialog>();
        }

        public void Cancel()
        {
            SubmitAction("cancelEditRow");
        }
    }
}
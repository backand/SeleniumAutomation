using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class NewObjectDialog : ModalDialog
    {
        public NewObjectDialog(IWebDriver driver) : base(driver)
        {
        }

        public NewObjectDialog SetName(string name)
        {
            FindElementAndSendKeys("object-name", name);
            return this;
        }

        public AddFieldDialog AddObject()
        {
            SubmitAction("addObject");
            return WaitUntil.UntilDialogPopUp<AddFieldDialog>();
        }
    }
}
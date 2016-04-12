using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core.Dialogs
{
    public class AddFieldDialog : ModalDialog
    {
        public AddFieldDialog(IWebDriver driver) : base(driver)
        {
        }

        public AddFieldDialog Name(string name)
        {
            FindElementAndSendKeys("field-name", name);
            return this;
        }

        public AddFieldDialog Type(FieldType type)
        {
            var element = FindElement("field-type");
            var select = new SelectElement(element);
            select.SelectByValue(type.ToText());
            return this;
        }

        public AddFieldDialog Add()
        {
            SubmitAction("addField");
            return WaitUntil.UntilDialogPopUp<AddFieldDialog>();
        }

        public void Close()
        {
            SubmitAction("cancelEditField");
        }
    }
}
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    public class EditPopup : DriverUser
    {
        public EditPopup(DriverUser driverUser, IWebElement popupElement) : base(driverUser)
        {
            MainElement = popupElement.FindElement(By.ClassName("editable-controls"));
        }

        private IWebElement MainElement { get; }
        private IWebElement TextElement => MainElement.FindElement(Selectors.ItemsPage.EditField);

        public string Text
        {
            get { return TextElement.Text; }
            set
            {
                TextElement.Clear();
                TextElement.SendKeys(value);
            }
        }

        public void Ok()
        {
            IWebElement okElement = MainElement.FindElement(By.CssSelector(".glyphicon-ok")).GetParent();
            okElement.Click();
        }

        public void Cancel()
        {
            IWebElement cancelElement = MainElement.FindElement(By.CssSelector(".glyphicon-remove")).GetParent();
            cancelElement.Click();
        }

        public NewRowDialog Edit()
        {
            IWebElement editElement = MainElement.FindElement(By.CssSelector("[ng-click*='editRow']"));
            editElement.Click();
            return WaitUntil.UntilNewRowDialogPopUp();
        }
    }
}
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    public class EditableField : DriverUser
    {
        public EditableField(DriverUser driverUser, IWebElement fieldElement) : base(driverUser)
        {
            EditElement = fieldElement;
            MainElement = fieldElement.GetParent();
        }

        private IWebElement EditElement { get; }

        private IWebElement MainElement { get; }

        public string Text => MainElement.Text;

        public EditPopup Edit()
        {
            EditElement.Click();
            return new EditPopup(this, MainElement);
        }
    }
}
using System;
using System.Linq;
using Core;
using Core.Dialogs;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    public class FieldRow : DriverUser
    {
        public FieldRow(DriverUser driverUser, IWebElement element) : base(driverUser)
        {
            MainElement = element;
            Driver.Hover(MainElement);
        }

        private IWebElement MainElement { get; }
        public string FieldName => MainElement.FindElement(By.CssSelector("text > .field-name-label")).Text;

        public FieldType FieldType
        {
            get
            {
                var typeText = MainElement.FindElement(By.CssSelector("text > :not(.field-name-label)")).Text;
                typeText = typeText.Trim('(', ')');
                var fieldTypes = Enum.GetValues(typeof (FieldType)).Cast<FieldType>();
                var fieldType = fieldTypes.FirstOrDefault(type => type.ToString().ToLower() == typeText);
                return fieldType;
            }
        }

        public FieldRowEditDialog Edit()
        {
            MainElement.FindElement(By.CssSelector("[ng-click*='onEditField']")).Click();
            return WaitUntil.UntilDialogPopUp<FieldRowEditDialog>();
        }
    }
}
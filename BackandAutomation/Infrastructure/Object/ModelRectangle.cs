using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Dialogs;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    public class ModelRectangle : DriverUser
    {
        public ModelRectangle(DriverUser driverUser, IWebElement element) : base(driverUser)
        {
            MainElement = element;
        }

        private IWebElement MainElement { get; }

        private IEnumerable<IWebElement> RowElements
            => MainElement.FindElements(By.CssSelector("[ng-repeat*='node.fields']"));

        public FieldRow GetFieldRow(Predicate<FieldRow> condition)
        {
            return
                RowElements.Select(element => new FieldRow(this, element))
                    .FirstOrDefault(fieldRow => condition(fieldRow));
        }

        public FieldRow GetFieldRow(string fieldName, FieldType fieldType)
        {
            return GetFieldRow(row => row.FieldName == fieldName && row.FieldType == fieldType);
        }

        public AddFieldDialog New()
        {
            MainElement.FindElement(By.ClassName("add-field-icon")).Click();
            return WaitUntil.UntilDialogPopUp<AddFieldDialog>();
        }
    }
}
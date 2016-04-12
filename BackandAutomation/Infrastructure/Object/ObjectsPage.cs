using System.Collections.Generic;
using Core;
using Core.Dialogs;
using Infrastructure.Base;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    [BackandPageType(LeftMenuOption.Objects, LeftMenuOption.DynamicObject)]
    public class ObjectsPage : BackandApplicationsBasePage
    {
        public ObjectsPage(DriverUser driver) : base(driver)
        {
        }

        public IEnumerable<GridRow> GetRows()
        {
            var elements = PageElement.FindElements(By.CssSelector(".ui-grid-viewport .ui-grid-row"));
            var rows = new List<GridRow>();
            var realRowPlace = elements.Count/2;
            for (var i = 0; i < realRowPlace; i++)
            {
                var row = new GridRow(this, elements[realRowPlace + i], elements[i]);
                rows.Add(row);
            }
            return rows;
        }

        private NewRowDialog AddRow()
        {
            PageElement.FindElement(By.CssSelector("[ng-click*='newRow']")).Click();
            return WaitUntil.UntilDialogPopUp<NewRowDialog>();
        }

        public void AddRow(string name, string description)
        {
            var dialog = AddRow();
            dialog.Name = name;
            dialog.Description = description;
            dialog.Save();
        }

        public void DeleteSelectedRows()
        {
            var deleteElement = PageElement.FindElement(Selectors.ItemsPage.Delete);
            deleteElement.Click();
            WaitUntil.UntilDialogPopUp<OkDialog>().Ok();
        }

        public void Refresh()
        {
            var refreshButton = PageElement.FindElement(Selectors.ItemsPage.Refresh);
            refreshButton.Click();
        }
    }
}
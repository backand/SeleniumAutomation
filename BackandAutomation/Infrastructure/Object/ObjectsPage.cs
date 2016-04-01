using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core;
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
            ReadOnlyCollection<IWebElement> elements = PageElement.FindElements(By.CssSelector(".ui-grid-viewport .ui-grid-row"));
            List<GridRow> rows = new List<GridRow>();
            int realRowPlace = elements.Count / 2;
            for (int i = 0; i < realRowPlace; i++)
            {
                GridRow row = new GridRow(this, elements[realRowPlace + i], elements[i]);
                rows.Add(row);
            }
            return rows;
        }

        private NewRowDialog AddRow()
        {
            PageElement.FindElement(By.CssSelector("[ng-click*='newRow']")).Click();
            return WaitUntil.UntilNewRowDialogPopUp();
        }

        public void AddRow(string name, string description)
        {
            NewRowDialog dialog = AddRow();
            dialog.Name = name;
            dialog.Description = description;
            dialog.Save();
        }

        public void DeleteSelectedRows()
        {
            IWebElement deleteElement = PageElement.FindElement(Selectors.ItemsPage.Delete);
            deleteElement.Click();
            WaitUntil.UntilOkDialogPopUp().Ok();
        }

        public void Refresh()
        {
            IWebElement refreshButton = PageElement.FindElement(Selectors.ItemsPage.Refresh);
            refreshButton.Click();
        }
    }
}
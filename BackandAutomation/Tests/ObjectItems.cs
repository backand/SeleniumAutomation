using System;
using System.Linq;
using Core;
using Infrastructure;
using Infrastructure.Apps;
using Infrastructure.Base;
using Infrastructure.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;
using Tests.Utils;

namespace Tests
{
    [TestClass]
    [InstantLogin]
    public class ObjectItems : BackandTestClassBase
    {
        private const string Name = "NewRow";
        private const string Description = "NewDescription";
        private ObjectsItemsPage _itemsPage;

        protected override void TestInitializeExtension()
        {
            base.TestInitializeExtension();
            BackandApplicationsBasePage page = CreateNewApp();
            BackandApplicationsBasePage backandPage = page.LeftMenu.Create(LeftMenuOption.Objects, LeftMenuOption.Items);
            _itemsPage = backandPage as ObjectsItemsPage;
            Assert.IsNotNull(_itemsPage);
            _itemsPage.AddRow(Name, Description);
        }

        [TestMethod]
        [CreateApp]
        public void CreateDataRow()
        {
            GridRow row =
                _itemsPage.GetRows().FirstOrDefault(r => r.Name.Text == Name && r.Description.Text == Description);
            Assert.IsNotNull(row);
        }

        [TestMethod]
        [CreateApp]
        public void DeleteDataRow()
        {
            GridRow row = _itemsPage.GetRows().First();
            string id = row.Id;
            row.Select();
            _itemsPage.DeleteSelectedRows();
            Assert.IsFalse(_itemsPage.GetRows().Any(r => r.Id == id));
        }

        [TestMethod]
        [CreateApp]
        public void UpdateDataRow()
        {
            GridRow row = _itemsPage.GetRows().First();
            string id = row.Id;
            EditPopup edit = row.Name.Edit();
            const string newName = "NewEditText";
            edit.Text = newName;
            edit.Ok();

            _itemsPage.Refresh();
            row = _itemsPage.GetRows().FirstOrDefault(r => r.Id == id);
            Assert.IsNotNull(row);
            Assert.AreEqual(newName, row.Name.Text);
        }

        private BackandApplicationsBasePage CreateNewApp()
        {
            AppsFeed feed = Page.AppsFeed;
            NewAppForm newAppForm = feed.New();
            newAppForm.Name = Utilities.GenerateString("TestName");
            newAppForm.Title = Utilities.GenerateString("TestTitle");
            return newAppForm.Submit();
        }
    }
}
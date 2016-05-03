using System.Linq;
using Infrastructure.Object;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            int i = 1;int b = 2;

            Assert.AreEqual(i + b, 3);
        }
    }

    [CreateApp]
    [TestClass]
    public class SimpleCrudTests : BackandTestClassBase
    {
        private const string Name = "NewRow";
        private const string Description = "NewDescription";
        private ObjectsPage _itemsPage;

        protected override void TestInitializeExtension()
        {
            base.TestInitializeExtension();
            _itemsPage = ApplicationsPage.LeftMenu.FetchPage<ObjectsPage>("items");
            Assert.IsNotNull(_itemsPage);
            _itemsPage.AddRow(Name, Description);
        }

        [TestMethod]
        public void CreateDataRow()
        {
            var row =
                _itemsPage.GetRows().FirstOrDefault(r => r.Name.Text == Name && r.Description.Text == Description);
            Assert.IsNotNull(row);
        }

        [TestMethod, Timeout(360000)]
        public void DeleteDataRow()
        {
            var row = _itemsPage.GetRows().First();
            var id = row.Id;
            row.Select();
            _itemsPage.DeleteSelectedRows();
            Assert.IsFalse(_itemsPage.GetRows().Any(r => r.Id == id));
        }

        [TestMethod, Timeout(360000)]
        public void UpdateDataRow()
        {
            var row = _itemsPage.GetRows().First();
            var id = row.Id;
            var edit = row.Name.Edit();
            const string newName = "NewEditText";
            edit.Text = newName;
            edit.Ok();

            _itemsPage.Refresh();
            row = _itemsPage.GetRows().FirstOrDefault(r => r.Id == id);
            Assert.IsNotNull(row);
            Assert.AreEqual(newName, row.Name.Text);
        }
    }
}
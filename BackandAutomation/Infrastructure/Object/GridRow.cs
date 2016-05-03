using Core;
using OpenQA.Selenium;

namespace Infrastructure.Object
{
    public class GridRow : DriverUser
    {
        public GridRow(DriverUser driverUser, IWebElement rowElement, IWebElement rowBehindElement) : base(driverUser)
        {
            MainElement = rowElement;
            RowBehindElement = rowBehindElement;
        }

        private IWebElement RowBehindElement { get; }

        private IWebElement MainElement { get; }

        private IWebElement NameElement => MainElement.FindElement(Selectors.ItemsPage.GridRow.Name);
        private IWebElement DescriptionElement => MainElement.FindElement(Selectors.ItemsPage.GridRow.Description);
        private IWebElement UserElement => MainElement.FindElement(Selectors.ItemsPage.GridRow.User);

        public string Id => MainElement.FindElement(Selectors.ItemsPage.GridRow.Id).Text;
        public EditableField Name => new EditableField(this, NameElement);
        public EditableField Description => new EditableField(this, DescriptionElement);
        public EditableField User => new EditableField(this, UserElement);

        public void Select()
        {
            RowBehindElement.FindElement(Selectors.ItemsPage.GridRow.Select).Click();
        }
    }
}
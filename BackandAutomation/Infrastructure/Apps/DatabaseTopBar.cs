using Core;
using Infrastructure.Base;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Apps
{
    public class DatabaseTopBar : TopBar
    {
        public DatabaseTopBar(DriverUser driver) : base(driver)
        {

        }

        protected override IWebElement MainElement => Driver.FindElement(By.ClassName("db-alert"));

        public DatabaseSelect ConnectToDatabase()
        {
            ClickOnButton("database.show");
            return new DatabaseSelect(this);
        }

        public DatabaseSelect SetupDataModel()
        {
            ClickOnButton("erd_model");
            return new DatabaseSelect(this);
        }

        private void ClickOnButton(string buttonName)
        {
            IWebElement buttonElement = MainElement.FindElement(By.CssSelector($"[ui-sref={buttonName}]"));
            buttonElement.Click();
        }
    }

    public class DatabaseSelect : BackandApplicationsBasePage
    {
        public DatabaseSelect(DriverUser driver) : base(driver)
        {

        }

        private IEnumerable<DatabaseInfo> RowElements => PageElement
            .FindElements(By.CssSelector(".row .form-group label"))
            .Select(element => new DatabaseInfo(this, element.GetParent()));

        public DatabaseInfo DatabaseType => RowElements.Single(r => r.InfoType == InfoType.DatabaseType);
        public DatabaseInfo EndPoint => RowElements.Single(r => r.InfoType == InfoType.EndPoint);
        public DatabaseInfo DatabaseName => RowElements.Single(r => r.InfoType == InfoType.DatabaseName);
        public DatabaseInfo Username => RowElements.Single(r => r.InfoType == InfoType.UserName);
        public DatabaseInfo Password => RowElements.Single(r => r.InfoType == InfoType.Password);
        public DatabaseInfo UseSSH => RowElements.Single(r => r.InfoType == InfoType.UseSSH);

        public DatabaseConfiguration Connect()
        {
            PageElement.FindElement(By.CssSelector("[ng-click='dbshow.edit()']")).Click();
            return new DatabaseConfiguration(this);
        }
    }

    public class DatabaseConfiguration : BackandApplicationsBasePage
    {
        public DatabaseConfiguration(DriverUser driver) : base(driver)
        {
        }

        private IReadOnlyCollection<IWebElement> DatabaseTypeElements =>
            PageElement.FindElements(By.ClassName("icon-container"));

        public DatabaseType DatabaseType
        {
            get
            {
                IWebElement typeElement = DatabaseTypeElements.Single(element => element.GetClasses().Contains("active"));
                string active = typeElement.GetAttribute("ng-class");
                Array array = Enum.GetValues(typeof(DatabaseType));
                foreach (var type in array)
                {
                    string enumtext = type.ToString().ToLower();
                    if (active.Contains(enumtext))
                    {
                        return enumtext.ToEnum<DatabaseType>();
                    }
                }
                throw new Exception();
            }
            set
            {
                string expectedType = value.ToText();
                IWebElement selectType = DatabaseTypeElements.Single(e => e.GetAttribute("ng-class").Contains(expectedType));
                selectType.Click();
            }
        }
        public EditableDatabaseInfo EndPoint => RowElements.Single(r => r.InfoType == InfoType.EndPoint);
        public EditableDatabaseInfo DatabaseName => RowElements.Single(r => r.InfoType == InfoType.DatabaseName);
        public EditableDatabaseInfo Username => RowElements.Single(r => r.InfoType == InfoType.UserName);
        public EditableDatabaseInfo Password => RowElements.Single(r => r.InfoType == InfoType.Password);
        public EditableDatabaseInfo UseSSH => RowElements.Single(r => r.InfoType == InfoType.UseSSH);

        private IEnumerable<EditableDatabaseInfo> RowElements => PageElement
            .FindElements(By.CssSelector(".row .form-group label"))
            .Select(element => new EditableDatabaseInfo(this, element.GetParent()));

    }

    public enum DatabaseType
    {
        [EnumText("mysql")]
        MySql,
        [EnumText("postgresql")]
        PostgreSql,
        [EnumText("sqlserver")]
        SqlServer
    }

    public class DatabaseInfo : DriverUser
    {
        public DatabaseInfo(DriverUser driver, IWebElement element) : base(driver)
        {
            MainElement = element;
            IWebElement typeElement = MainElement.FindElement(InfoTypeSelector);
            string id = typeElement.GetId();
            if (id == null)
            {
                typeElement.FindElement(By.TagName("a")).Click();
                InfoType = InfoType.Password;
            }
            else
                InfoType = id.ToEnum<InfoType>();
        }

        protected virtual By InfoTypeSelector { get; set; } = By.ClassName("form-control-static");

        public InfoType InfoType { get; private set; }

        public string Value { get; set; }
        protected IWebElement MainElement { get; private set; }
    }

    public class EditableDatabaseInfo : DatabaseInfo
    {
        public EditableDatabaseInfo(DriverUser driver, IWebElement element) : base(driver, element)
        {
        }

        private IWebElement TextElement => MainElement.FindElement(By.TagName("input"));

        public string Text
        {
            get { return TextElement.GetAttribute("value"); }
            set { TextElement.SendKeys(value); }
        }

    }
    public enum InfoType
    {
        [EnumText("dbtype")]
        DatabaseType,
        [EnumText("server")]
        EndPoint,
        [EnumText("database")]
        DatabaseName,
        [EnumText("UserName")]
        UserName,
        [EnumText("usessh")]
        UseSSH,
        Password
    }
}
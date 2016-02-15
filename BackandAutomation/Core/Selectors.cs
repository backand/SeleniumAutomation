using OpenQA.Selenium;

namespace Core
{
    public static class Selectors
    {
        public class AppForm
        {
            public static By Name
                => new OrCondition(By.CssSelector("[placeholder~=title]"), By.ClassName("panel-body"));
            public static By Title
                => new OrCondition(By.Name("appName"), By.ClassName("panel-heading"));
            public static string RibbonElementSelector => "ui-ribbon-container";
            public static By SubmitNew => By.CssSelector("[type=submit]");
            public static By Settings => By.ClassName("ti-settings");
            public static By ManageApp => By.CssSelector("[type=submit]");
        }

        public class ModalDialog
        {
            public static By MainElement => By.ClassName("modal-dialog");
            public static By Ok => By.TagName("button");
            public static By Title => By.ClassName("modal-header");
        }

        public class BackandApplicationsBasePage
        {
            public static By Page => By.ClassName("page");
            public static By Settings => By.ClassName("nav-profile");
            public static By LeftMenu => By.TagName("aside");
        }
    }
}
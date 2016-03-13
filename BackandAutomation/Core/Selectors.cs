using OpenQA.Selenium;

namespace Core
{
    public static class Selectors
    {
        public class AppForm
        {
            public static By AppHead
                => new OrCondition(By.Name("appName"), By.CssSelector(".panel-heading.text-center"));

            public static By Title =>
                    new OrCondition(By.CssSelector("[placeholder='app title']"),
                        By.CssSelector(".app-panel-body .body-height"));

            public static string RibbonElementSelector => "ui-ribbon-container";
            public static By SubmitNew => Common.SubmitType;
            public static By Settings => By.ClassName("ti-settings");
            public static By ManageApp => Common.SubmitType;
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
            public static By TopNav => By.ClassName("top-nav");
        }

        public static class Common
        {
            public static By SubmitType => By.CssSelector("[type=\"submit\"]");
            public static By GoToHomePage => By.ClassName("ti-layers-alt");
        }

        public class BackandApplicationBasic
        {
            public static By CurrentApp => By.CssSelector("select[ng-model='header.currentAppName']");
            public static By Option => By.TagName("option");
        }

        public class LoginPageButtons
        {
            public static string SignInSelector = ".header-raww a.login";
            public static string SignUpSelector = ".header-raww .login-raww";
        }

        public class Login
        {

            public static By Submit => Common.SubmitType;
            public static By Password => By.CssSelector("[placeholder='Password']");
            public static By Email => By.Name("uEmail");
            public static By FullName => By.Name("uFullFirst");
            public static By ConfirmPassword => By.Name("confirm_password");
            public static By SignUp => By.CssSelector("[ui-sref='sign_up']");
        }

        public class ManageAppSettings
        {
            public static By Delete => By.CssSelector("[title='Delete the App']");
        }
    }
}
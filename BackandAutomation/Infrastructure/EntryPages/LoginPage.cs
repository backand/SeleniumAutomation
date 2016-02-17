using System;
using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Infrastructure.EntryPages
{
    public abstract class LoginPage : BasePage
    {
        protected LoginPage(IWebDriver driver) : base(driver)
        {
            //WaitUntil.UntilElementExists(By.ClassName("page-signin"));
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        public string OriginalHandle { get; set; }

        private SignInFormsFactory SignInFormsFactory => new SignInFormsFactory(Driver);

        public string Email
        {
            get { return EmailElement.Text; }
            set { EmailElement.SendKeys(value); }
        }

        public string Password
        {
            get { return PasswordElement.Text; }
            set { PasswordElement.SendKeys(value); }
        }

        private IWebElement EmailElement => Driver.FindElement(By.Name("uEmail"));
        private IWebElement PasswordElement => Driver.FindElement(By.Name("uPassword"));
        private IWebElement SubmitElement => Driver.FindElement(By.CssSelector("[type=submit]"));

        public SignInForm SignIn(SignInFormType signInFormType)
        {
            string className = $"btn-{signInFormType.ToText()}";
            var signInElement = Driver.FindElement(By.ClassName(className));

            // Get the current window handle so you can switch back later
            OriginalHandle = Driver.CurrentWindowHandle;

            // Displayed by the popup window
            var finder = new PopupWindowFinder(Driver);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            var popupWindowHandle = finder.Click(signInElement);

            Driver.SwitchTo().Window(popupWindowHandle);

            return SignInFormsFactory.Create(signInFormType, OriginalHandle);
        }

        public UserMainPage Submit()
        {
            SubmitElement.Click();
            return new UserMainPage(Driver);
        }
    }
}
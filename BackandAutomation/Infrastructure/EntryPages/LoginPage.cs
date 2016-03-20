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
        protected LoginPage(DriverUser driver) : base(driver)
        {
            //WaitUntil.UntilElementExists(By.ClassName("page-signin"));
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        public string OriginalHandle { get; set; }

        public virtual string Email
        {
            get { return EmailElement.Text; }
            set { EmailElement.SendKeys(value); }
        }

        public virtual string Password
        {
            get { return PasswordElement.Text; }
            set { PasswordElement.SendKeys(value); }
        }

        protected IWebElement EmailElement => Driver.FindElement(Selectors.Login.Email);
        protected IWebElement PasswordElement => Driver.FindElement(Selectors.Login.Password);
        protected IWebElement SubmitElement => Driver.FindElement(Selectors.Login.Submit);

        public UserMainPage Submit()
        {
            SubmitElement.Click();
            return new UserMainPage(this);
        }

        protected void OpenSignForm(SignFormType signFormType)
        {
            string className = $"btn-{signFormType.ToText()}";
            var signInElement = Driver.FindElement(By.ClassName(className));

            // Get the current window handle so you can switch back later
            OriginalHandle = Driver.CurrentWindowHandle;

            // Displayed by the popup window
            var finder = new PopupWindowFinder(Driver);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            var popupWindowHandle = finder.Click(signInElement);

            Driver.SwitchTo().Window(popupWindowHandle);
        }
    }
}
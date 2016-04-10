using System;
using System.Reflection;
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
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
        }

        protected string OriginalHandle { get; private set; }

        protected string Email
        {
            get { return EmailElement.Text; }
            set { EmailElement.SendKeys(value); }
        }

        private IWebElement EmailElement => Driver.FindElement(Selectors.Login.Email);
        protected IWebElement PasswordElement => Driver.FindElement(Selectors.Login.Password);
        private IWebElement SubmitElement => Driver.FindElement(Selectors.Login.Submit);

        protected UserMainPage Submit()
        {
            SubmitElement.Click();
            return new UserMainPage(this);
        }

        protected void OpenSignForm<T>() where T : SignInForm
        {
            // Get the current window handle so you can switch back later
            OriginalHandle = Driver.CurrentWindowHandle;

            SignInFormTypeAttribute formTypeAttribute = typeof(T).GetCustomAttribute<SignInFormTypeAttribute>();
            SignFormType signFormType = formTypeAttribute.SignFormType;
            if (signFormType == SignFormType.None)
                return;
            string className = $"btn-{signFormType.ToText()}";
            var signInElement = Driver.FindElement(By.ClassName(className));

            // Displayed by the popup window
            PopupWindowFinder finder = new PopupWindowFinder(Driver);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            string popupWindowHandle = finder.Click(signInElement);

            Driver.SwitchTo().Window(popupWindowHandle);
        }
    }
}
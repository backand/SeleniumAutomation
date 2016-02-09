using System;
using Core;
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

        public SignInForm SignIn(SignInFormType signInFormType)
        {
            string className = $"btn-{signInFormType.ToText()}";
            IWebElement signInElement = Driver.FindElement(By.ClassName(className));

            // Get the current window handle so you can switch back later
            string currentHandle = Driver.CurrentWindowHandle;

            // Displayed by the popup window
            PopupWindowFinder finder = new PopupWindowFinder(Driver);
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(20));
            string popupWindowHandle = finder.Click(signInElement);

            Driver.SwitchTo().Window(popupWindowHandle);

            return SignInFormsFactory.Create(signInFormType, currentHandle);
        }

        private SignInFormsFactory SignInFormsFactory => new SignInFormsFactory(Driver);
    }
}
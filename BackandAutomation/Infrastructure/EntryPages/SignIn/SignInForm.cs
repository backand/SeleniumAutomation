using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public abstract class SignInForm : SignForm
    {
        protected SignInForm(DriverUser driverUser, object originalWindowHandle) : base(driverUser)
        {
            OriginalWindowHandle = originalWindowHandle.ToString();
        }

        protected SignInForm(DriverUser driver) : base(driver)
        {
        }

        private string OriginalWindowHandle { get; }

        protected abstract By EmailFindBy { get; }
        protected abstract By PasswordFindBy { get; }
        protected abstract By SubmitFindBy { get; }

        protected IWebElement EmailElement => Driver.TryFindElement(EmailFindBy);
        private IWebElement PasswordElement => Driver.TryFindElement(PasswordFindBy);
        protected IWebElement SubmitElement => Driver.TryFindElement(SubmitFindBy);

        protected void Submit()
        {
            SubmitElement.Click();
        }

        public abstract UserMainPage QuickSubmit(string email, string password);

        protected virtual string Email
        {
            private get { return EmailElement.Text; }
            set { EmailElement.SendKeys(value); }
        }

        protected string Password
        {
            private get { return PasswordElement.Text; }
            set { PasswordElement.SendKeys(value); }
        }

        protected void SwitchToOriginalWindow()
        {
            Driver.SwitchTo().Window(OriginalWindowHandle);
        }
    }
}
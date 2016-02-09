using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public abstract class SignInForm : BasePage
    {
        protected SignInForm(IWebDriver driver, string originalWindowHandle) : base(driver)
        {
            OriginalWindowHandle = originalWindowHandle;
        }

        protected SignInForm(IWebDriver driver) : base(driver)
        {
        }

        protected string OriginalWindowHandle { get; set; }

        protected abstract By EmailFindBy { get; }
        protected abstract By PasswordFindBy { get; }
        protected abstract By SubmitFindBy { get; }

        private IWebElement EmailElement => Driver.FindElement(EmailFindBy);
        private IWebElement PasswordElement => Driver.FindElement(PasswordFindBy);
        private IWebElement SubmitElement => Driver.FindElement(SubmitFindBy);

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

        public void Submit()
        {
            SubmitElement.Click();
            CompleteFormLogic();
            if(!string.IsNullOrEmpty(OriginalWindowHandle))
                SwitchToOriginalWindow();
        }

        protected virtual void CompleteFormLogic()
        {
        }

        private void SwitchToOriginalWindow()
        {
            Driver.SwitchTo().Window(OriginalWindowHandle);
        }
    }
}
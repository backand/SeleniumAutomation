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

        protected IWebElement EmailElement => Driver.FindElement(EmailFindBy);
        protected IWebElement PasswordElement => Driver.FindElement(PasswordFindBy);
        protected IWebElement SubmitElement => Driver.FindElement(SubmitFindBy);

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

        public UserMainPage Submit()
        {
            SubmitElement.Click();
            CompleteFormLogic();
            if(!string.IsNullOrEmpty(OriginalWindowHandle))
                SwitchToOriginalWindow();
            WaitUntil.UntilElementDoesntExist(By.ClassName("spinner"));
            return new UserMainPage(Driver);
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
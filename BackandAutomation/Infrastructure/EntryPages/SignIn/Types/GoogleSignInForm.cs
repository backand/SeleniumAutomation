using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignFormType.Google)]
    public class GoogleSignInForm : SignInForm
    {
        public GoogleSignInForm(IWebDriver driver, string originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Id("Email");
        protected override By PasswordFindBy => By.Id("Passwd");
        protected override By SubmitFindBy => By.Id("signIn");

        public override string Email
        {
            get { return EmailElement.Text; }
            set
            {
                EmailElement.SendKeys(value);
                Driver.TryFindElement(By.Id("next"))?.Click();
            }
        }
    }
}
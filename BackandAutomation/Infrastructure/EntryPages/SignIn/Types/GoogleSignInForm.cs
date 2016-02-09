using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignInFormType.Google)]
    public class GoogleSignInForm : SignInForm
    {
        public GoogleSignInForm(IWebDriver driver, string originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Name("Email");
        protected override By PasswordFindBy => By.Name("password");
        protected override By SubmitFindBy => By.Name("commit");
    }
}
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignInFormType.GitHub)]
    public class GitHubForm : SignInForm
    {
        public GitHubForm(IWebDriver driver, string originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Name("login");
        protected override By PasswordFindBy => By.Name("password");
        protected override By SubmitFindBy => By.Name("commit");
    }
}
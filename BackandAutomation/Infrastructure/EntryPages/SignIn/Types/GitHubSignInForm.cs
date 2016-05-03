using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignFormType.GitHub)]
    public class GitHubSignInForm : SignInForm
    {
        public GitHubSignInForm(DriverUser driver, object originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Name("login");
        protected override By PasswordFindBy => By.Name("password");
        protected override By SubmitFindBy => By.Name("commit");

        public override UserMainPage QuickSubmit(string email, string password)
        {
            try
            {
                Email = email;
                Password = password;
                Submit();
            }
            catch
            {
                //Ignored
            }
            SwitchToOriginalWindow();
            return new UserMainPage(this);
        }
    }
}
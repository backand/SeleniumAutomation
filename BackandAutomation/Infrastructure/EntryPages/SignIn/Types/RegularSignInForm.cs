using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignFormType.None)]
    public class RegularSignInForm : SignInForm
    {
        public RegularSignInForm(DriverUser driver, object originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => Selectors.Login.Email;
        protected override By PasswordFindBy => Selectors.Login.Password;
        protected override By SubmitFindBy => Selectors.Login.Submit;

        public override UserMainPage QuickSubmit(string email, string password)
        {
            Email = email;
            Password = password;
            Submit();
            return new UserMainPage(this);
        }
    }
}
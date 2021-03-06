using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignFormType.Google)]
    public class GoogleSignInForm : SignInForm
    {
        public GoogleSignInForm(DriverUser driverUser, object originalWindowHandle)
            : base(driverUser, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => new OrCondition(By.Id("Email"), By.Id("next"));
        protected override By PasswordFindBy => By.Id("Passwd");
        protected override By SubmitFindBy => By.Id("signIn");

        protected override string Email
        {
            set
            {
                EmailElement.SendKeys(value);
                Driver.TryFindElement(By.Id("next"))?.Click();
            }
        }

        public override UserMainPage QuickSubmit(string email, string password)
        {
            try
            {
                Email = email;
                //SubmitElement.Click();
                Password = password;
                SubmitElement.Click();
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
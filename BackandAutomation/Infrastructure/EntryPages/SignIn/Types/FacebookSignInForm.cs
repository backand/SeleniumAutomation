using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignFormType.Facebook)]
    public class FacebookSignInForm : SignInForm
    {
        public FacebookSignInForm(DriverUser driver, object originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Id("email");
        protected override By PasswordFindBy => By.Id("pass");
        protected override By SubmitFindBy => By.Id("loginbutton");

        public override UserMainPage QuickSubmit(string email, string password)
        {
            try
            {
                Email = email;
                Password = password;
                SubmitElement.Click();
                WaitUntil.UntilElementExists(By.Name("__CONFIRM__"), 3);
                (Driver as IJavaScriptExecutor)?.ExecuteScript("document.getElementByName('__CONFIRM__')[0].click()");
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
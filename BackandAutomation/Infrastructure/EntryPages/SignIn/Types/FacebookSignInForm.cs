using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn.Types
{
    [SignInFormType(SignInFormType.Facebook)]
    public class FacebookSignInForm : SignInForm
    {
        public FacebookSignInForm(IWebDriver driver, string originalWindowHandle) : base(driver, originalWindowHandle)
        {
        }

        protected override By EmailFindBy => By.Id("email");
        protected override By PasswordFindBy => By.Id("pass");
        protected override By SubmitFindBy => By.Id("u_0_2");

        protected override void CompleteFormLogic()
        {
            try
            {
                WaitUntil.UntilElementExists(By.Name("__CONFIRM__"), 3);
                (Driver as IJavaScriptExecutor)?.ExecuteScript("document.getElementByName('__CONFIRM__')[0].click()");
            }
            catch (WebDriverTimeoutException)
            {

            }
        }
    }
}
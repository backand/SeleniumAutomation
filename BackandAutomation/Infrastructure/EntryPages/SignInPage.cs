using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignInPage : LoginPage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }

        public UserMainPage QuickSignIn(SignInFormType signInFormType, string email, string password)
        {
            SignInForm form = SignIn(signInFormType);
            try
            {
                form.Email = email;
            }
            catch (NoSuchWindowException)
            {
                // That's an exception that been thrown when the form has already been filled.
                Driver.SwitchTo().Window(OriginalHandle);
                return new UserMainPage(Driver);
            }
            form.Password = password;
            return form.Submit();
        }
    }
}
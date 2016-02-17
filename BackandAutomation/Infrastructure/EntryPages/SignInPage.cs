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
            UserMainPage quickSignIn;
            if (HandleNoSignInForm(signInFormType, email, password, out quickSignIn)) return quickSignIn;
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

        private bool HandleNoSignInForm(SignInFormType signInFormType, string email, string password,
            out UserMainPage quickSignIn)
        {
            quickSignIn = null;
            if (signInFormType != SignInFormType.None) return false;
            Email = email;
            Password = password;
            {
                quickSignIn = Submit();
                return true;
            }
        }
    }
}
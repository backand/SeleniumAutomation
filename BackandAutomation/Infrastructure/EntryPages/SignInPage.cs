using Core;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignInPage : LoginPage
    {
        public SignInPage(DriverUser driverUser) : base(driverUser)
        {
            SignInFactory = new SignInFormsFactory(Driver);
        }

        public SignInFormsFactory SignInFactory { get; set; }

        public SignInForm SpecifySignForm(SignFormType signFormType)
        {
            OpenSignForm(signFormType);
            return SignInFactory.Create(signFormType, OriginalHandle);
        }

        public UserMainPage QuickSignIn(SignFormType signFormType, string email, string password)
        {
            UserMainPage quickSignIn;
            if (HandleNoSignInForm(signFormType, email, password, out quickSignIn)) return quickSignIn;
            SignInForm form = SpecifySignForm(signFormType);
            try
            {
                form.Email = email;
            }
            catch (NoSuchWindowException)
            {
                // That's an exception that been thrown when the form has already been filled.
                Driver.SwitchTo().Window(OriginalHandle);
                return new UserMainPage(this);
            }
            form.Password = password;
            return form.Submit();
        }

        private bool HandleNoSignInForm(SignFormType signFormType, string email, string password,
            out UserMainPage quickSignIn)
        {
            quickSignIn = null;
            if (signFormType != SignFormType.None) return false;
            Email = email;
            Password = password;

            quickSignIn = Submit();
            return true;
        }
    }
}
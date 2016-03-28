using Core;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignUpPage : LoginPage
    {
        public SignUpPage(DriverUser driverUser) : base(driverUser)
        {
            SignInFactory = new SignInFormsFactory(this);
        }

        public SignInFormsFactory SignInFactory { get; set; }

        private bool HandleNoSignUpForm(SignFormType signFormType, string userName, string email, string password,
            out UserMainPage quickSignUp)
        {
            quickSignUp = null;
            if (signFormType != SignFormType.None) return false;
            FullName = userName;
            Email = email;
            Password = password;

            quickSignUp = Submit();
            return true;
        }

        private IWebElement SignUpElement => Driver.FindElement(Selectors.Login.SignUp);
        private IWebElement FullNameElement => Driver.FindElement(Selectors.Login.FullName);
        private IWebElement ConfirmPasswordElement => Driver.FindElement(Selectors.Login.ConfirmPassword);

        public string FullName
        {
            get { return FullNameElement.Text; }
            set { FullNameElement.SendKeys(value); }
        }

        public override string Password
        {
            get { return PasswordElement.Text; }
            set
            {
                PasswordElement.SendKeys(value);
                ConfirmPasswordElement.SendKeys(value);
            }
        }


        public SignInForm SpecifySignForm(SignFormType signFormType)
        {
            OpenSignForm(signFormType);
            return SignInFactory.Create(signFormType, OriginalHandle);
        }

        public UserMainPage QuickSignUp(SignFormType signFormType, string userName, string email, string password)
        {
            //SignUpElement.Click();
            UserMainPage signInForm;
            if (HandleNoSignUpForm(signFormType, userName, email, password, out signInForm)) return signInForm;
            var form = SpecifySignForm(signFormType);
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
    }

    public class SignUpForm : SignForm
    {
        public SignUpForm(DriverUser driverUser) : base(driverUser)
        {
        }
    }
}
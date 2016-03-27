using System;
using Infrastructure.EntryPages.SignIn;
using Infrastructure.EntryPages.SignIn.Types;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignFormsFactory : BasicFactory<SignInForm>
    {
        private IWebDriver driver;

        public SignFormsFactory(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public SignUpForm CreateSignUp(SignFormType signFormType, string originalHandle)
        {
            throw new NotImplementedException();
        }

        public SignInForm CreateSignIn(SignFormType signFormType, string originalHandle)
        {
            throw new NotImplementedException();
        }

        protected override void InitClasses()
        {
            RegisterClass(typeof(GitHubForm));
            RegisterClass(typeof(GoogleSignInForm));
            RegisterClass(typeof(FacebookSignInForm));
        }
    }
}
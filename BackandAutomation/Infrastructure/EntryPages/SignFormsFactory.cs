using System;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignFormsFactory
    {
        private IWebDriver driver;

        public SignFormsFactory(IWebDriver driver)
        {
            this.driver = driver;
        }

        public SignUpForm CreateSignUp(SignFormType signFormType, string originalHandle)
        {
            throw new NotImplementedException();
        }
    }
}
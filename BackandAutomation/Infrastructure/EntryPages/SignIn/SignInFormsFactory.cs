using System;
using System.Collections;
using System.Linq;
using Core;
using Infrastructure.EntryPages.SignIn.Types;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public class SignInFormsFactory : DriverUser
    {
        public SignInFormsFactory(IWebDriver driver) : base(driver)
        {
            RegisteredImplementations = new ArrayList();
            RegisterClass(typeof (GitHubForm));
            RegisterClass(typeof (GoogleSignInForm));
            RegisterClass(typeof (FacebookSignInForm));
        }

        private ArrayList RegisteredImplementations { get; }

        private void RegisterClass(Type requestStrategyImpl)
        {
            if (!requestStrategyImpl.IsSubclassOf(typeof (SignInForm)))
                throw new Exception("LoginPage must inherit from class AbstractLoginPage");

            RegisteredImplementations.Add(requestStrategyImpl);
        }

        public SignInForm Create(SignInFormType signInFormType, string originalWindowHandle)
        {
            foreach (var impl in from Type impl in RegisteredImplementations
                let attrlist = impl.GetCustomAttributes(true)
                where attrlist.OfType<SignInFormTypeAttribute>().Any(attr => attr.SignInFormType.Equals(signInFormType))
                select impl)
            {
                return (SignInForm) Activator.CreateInstance(impl, Driver, originalWindowHandle);
            }
            throw new Exception("Could not find a SignInForm implementation for this SignInFormType");
        }
    }
}
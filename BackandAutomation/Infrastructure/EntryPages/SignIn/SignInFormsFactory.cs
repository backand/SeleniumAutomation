using System;
using System.Collections;
using System.Linq;
using Core;
using Infrastructure.EntryPages.SignIn.Types;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public class SignInFormsFactory : BasicFactory
    {
        public SignInFormsFactory(IWebDriver driver) : base(driver)
        {
        }

        public SignInFormsFactory(DriverUser driverUser) : base(driverUser)
        {
            
        }

        protected override void InitClasses()
        {
            RegisterClass(typeof(GitHubForm));
            RegisterClass(typeof(GoogleSignInForm));
            RegisterClass(typeof(FacebookSignInForm));
        }

        public SignInForm Create(SignFormType signFormType, string originalWindowHandle)
        {
            foreach (var impl in from Type impl in RegisteredImplementations
                                 let attrlist = impl.GetCustomAttributes(true)
                                 where attrlist.OfType<SignInFormTypeAttribute>().Any(attr => attr.SignFormType.Equals(signFormType))
                                 select impl)
            {
                return (SignInForm)Activator.CreateInstance(impl, Driver, originalWindowHandle);
            }
            throw new Exception("Could not find a SignInForm implementation for this SignInFormType");
        }
    }
}
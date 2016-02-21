using System;
using System.Collections;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public abstract class BasicFactory : DriverUser
    {
        protected BasicFactory(IWebDriver driver) : base(driver)
        {
            RegisteredImplementations = new ArrayList();
            InitClasses();
        }

        protected abstract void InitClasses();

        protected ArrayList RegisteredImplementations { get; }

        protected void RegisterClass(Type requestStrategyImpl)
        {
            if (!requestStrategyImpl.IsSubclassOf(typeof(SignInForm)))
                throw new Exception("Class must inherit from the abstract Class");

            RegisteredImplementations.Add(requestStrategyImpl);
        }
    }
}
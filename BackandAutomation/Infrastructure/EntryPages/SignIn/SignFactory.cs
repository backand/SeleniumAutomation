using System;
using System.Collections;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public abstract class BasicFactory<T> : DriverUser 
        where T : class
    {
        protected BasicFactory(IWebDriver driver) : base(driver)
        {
            RegisteredImplementations = new ArrayList();
            InitClasses();
        }

        protected BasicFactory(DriverUser driverUser) : this(driverUser.Driver)
        {
        }

        protected abstract void InitClasses();

        protected ArrayList RegisteredImplementations { get; }

        protected void RegisterClass(Type requestStrategyImpl) 
        {
            if (!requestStrategyImpl.IsSubclassOf(typeof(T)))
                throw new Exception("Class must inherit from the abstract Class");

            RegisteredImplementations.Add(requestStrategyImpl);
        }
    }
}
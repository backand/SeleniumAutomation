using System;
using System.Collections.Generic;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages.SignIn
{
    public abstract class BasicFactory<T> : DriverUser 
        where T : class
    {
        protected BasicFactory(IWebDriver driver) : base(driver)
        {
            InitClasses();
        }

        protected BasicFactory(DriverUser driverUser) : this(driverUser.Driver)
        {
        }

        protected abstract void InitClasses();

        protected List<Type> RegisteredImplementations { get; } = new List<Type>();

        protected void RegisterClass(Type requestStrategyImpl) 
        {
            if (!requestStrategyImpl.IsSubclassOf(typeof(T)))
                throw new Exception("Class must inherit from the abstract Class");

            RegisteredImplementations.Add(requestStrategyImpl);
        }
    }
}
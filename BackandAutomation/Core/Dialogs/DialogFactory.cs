using System;
using OpenQA.Selenium;

namespace Core.Dialogs
{
    public class DialogFactory : DriverUser
    {
        public DialogFactory(IWebDriver driver) : base(driver)
        {
        }

        public DialogFactory(DriverUser driver) : base(driver)
        {
        }

        public T Create<T>() where T : ModalDialog
        {
            return Activator.CreateInstance(typeof(T), Driver) as T;
        }
    }
}
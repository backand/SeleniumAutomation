using System;
using System.Reflection;
using Core.Dialogs;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Core
{
    public class WaitUntil
    {
        private const int TimeOut = 20;
        private IWebDriver Driver { get; }

        public WaitUntil(IWebDriver driver)
        {
            Driver = driver;
        }

        public IWebElement UntilElementExists(By findBy, int timeOut = TimeOut, params Type[] exceptionTypes)
        {
            IWebElement element = null;
            UntilActionFinishes(driver => driver.TryFindElement(findBy, out element), timeOut, exceptionTypes);
            return element;
        }

        public IWebElement UntilElementDoesntExist(By findBy, int timeOut = TimeOut, params Type[] exceptionTypes)
        {
            IWebElement element = null;
            UntilActionFinishes(driver => !driver.TryFindElement(findBy, out element), timeOut, exceptionTypes);
            return element;
        }

        public void ElementIsClickable(IWebElement element, int timeOut = TimeOut)
        {
            UntilActionFinishes(driver =>
            {
                try
                {
                    element.Click();
                    return true;
                }
                catch (Exception ex)
                {
                    if (ex is ElementNotVisibleException || ex is StaleElementReferenceException)
                        return false;
                    else
                        throw ex;
                }
            }, timeOut, typeof(ElementNotVisibleException), typeof(StaleElementReferenceException));
        }

        public IAlert UntilAlertPoppesUp(int timeOut = TimeOut)
        {
            return UntilActionFinishes(driver => driver.SwitchTo().Alert(), timeOut, typeof(NoAlertPresentException));
        }

        private T UntilActionFinishes<T>(Func<IWebDriver, T> funcToWait, int timeOut = TimeOut, params Type[] exceptionTypes)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOut));
            wait.IgnoreExceptionTypes(exceptionTypes);
            T result = wait.Until(funcToWait);
            return result;
        }
        
        public TDialog UntilDialogPopUp<TDialog>() where TDialog : ModalDialog
        {
            DialogFactory factory = new DialogFactory(Driver);
            UntilActionFinishes(driver => driver.FindElement(Selectors.ModalDialog.MainElement),
                exceptionTypes: typeof (NoSuchWindowException));
            return factory.Create<TDialog>();
        }
    }
}
using Core;
using Infrastructure.EntryPages;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;
using Protractor;

namespace Infrastructure
{
    public class BackandPage : BasePage
    {
        public BackandPage(IWebDriver driver) : base(driver)
        {
            CurrentDriver = (driver as NgWebDriver)?.WrappedDriver;
        }

        public IWebDriver CurrentDriver { get; set; }

        private IWebElement SignInElement => CurrentDriver.FindElement(By.CssSelector(".navbar-nav .login"));

        public SignInPage SignIn()
        {
            SignInElement.Click();
            return new SignInPage(Driver);
        }

        public UserMainPage QuickSignIn(SignInFormType signInFormType, string email, string password)
        {
            UserMainPage mainPage = SignIn().QuickSignIn(signInFormType, email, password);
            return mainPage;
        }
    }
}
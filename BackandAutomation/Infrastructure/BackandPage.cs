using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;
using Protractor;

namespace Infrastructure
{
    public class BackandPage : BasePage
    {
        public BackandPage(IWebDriver driverUser) : base(driverUser)
        {
            CurrentDriver = (Driver as NgWebDriver)?.WrappedDriver;
        }

        private IWebDriver CurrentDriver { get; }

        private SignInPage SignIn()
        {
            CurrentDriver.JavascriptClick(Selectors.LoginPageButtons.SignInSelector);
            return new SignInPage(this);
        }

        private SignUpPage SignUp()
        {
            CurrentDriver.JavascriptClick(Selectors.LoginPageButtons.SignUpSelector);
            return new SignUpPage(this);
        }

        public UserMainPage QuickSignIn<T>(string email, string password) where T : SignInForm
        {
            var mainPage = SignIn().QuickSignIn<T>(email, password);
            return mainPage;
        }
    }
}
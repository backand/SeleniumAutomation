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
        public BackandPage(IWebDriver driver) : base(driver)
        {
            CurrentDriver = (driver as NgWebDriver)?.WrappedDriver;
        }

        public BackandPage(DriverUser driverUser) : this(driverUser.Driver)
        {
        }

        private IWebDriver CurrentDriver { get; }

        private SignInPage SignIn()
        {
            CurrentDriver.JavascriptClick(Selectors.LoginPageButtons.SignInSelector);
            return new SignInPage(Driver);
        }

        private SignUpPage SignUp()
        {
            CurrentDriver.JavascriptClick(Selectors.LoginPageButtons.SignUpSelector);
            return new SignUpPage(Driver);
        }

        public UserMainPage QuickSignIn(SignFormType signFormType, string email, string password)
        {
            UserMainPage mainPage = SignIn().QuickSignIn(signFormType, email, password);
            return mainPage;
        }

        public UserMainPage QuickSignUp(SignFormType signFormType, string fullName, string email, string password)
        {
            UserMainPage mainPage = SignUp().QuickSignUp(signFormType, fullName, email, password);
            return mainPage;
        }
    }
}
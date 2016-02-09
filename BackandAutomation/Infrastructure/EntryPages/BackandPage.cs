using OpenQA.Selenium;
using Protractor;

namespace Infrastructure.EntryPages
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

        //public UserMainPage SignIn(SignInFormType signInFormType, string email, string password)
        //{
        //    SignInForm page = SignIn(signInFormType);
        //    page.Email = email;
        //    page.Password = password;
        //    page.Submit();
        //    return new UserMainPage(Driver);
        //}
    }
}
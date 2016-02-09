using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class SignInPage : LoginPage
    {
        public SignInPage(IWebDriver driver) : base(driver)
        {
        }
    }
}
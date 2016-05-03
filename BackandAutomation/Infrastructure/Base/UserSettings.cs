using Core;
using Infrastructure.EntryPages;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class UserSettings : DriverUser
    {
        public UserSettings(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
            ToggleDropDown();
        }

        private IWebElement MainElement { get; }
        private IWebElement ToggleElement => MainElement.FindElement(By.ClassName("dropdown-toggle"));
        private IWebElement EmailElement => MainElement.FindElement(By.ClassName("bknd-username"));
        private IWebElement LogOutElement => MainElement.FindElement(By.ClassName("ti-export"));
        private IWebElement ChangePasswordElement => MainElement.FindElement(By.ClassName("ti-lock")).GetParent();

        public string LoginEmail => EmailElement?.Text;

        public void ToggleDropDown()
        {
            ToggleElement.Click();
        }

        public SignInPage LogOut()
        {
            LogOutElement.Click();
            return new SignInPage(this);
        }
    }
}
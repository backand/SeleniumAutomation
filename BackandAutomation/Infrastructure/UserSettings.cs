using Core;
using Infrastructure.EntryPages;
using OpenQA.Selenium;

namespace Infrastructure
{
    public class UserSettings : DriverUser
    {
        public UserSettings(IWebDriver driver, IWebElement mainElement) : base(driver)
        {
            MainElement = mainElement;
            ToggleDropDown();
        }

        public void ToggleDropDown()
        {
            ToggleElement.Click();
        }

        private IWebElement MainElement { get; set; }
        private IWebElement ToggleElement => MainElement.FindElement(By.ClassName("dropdown-toggle"));
        private IWebElement EmailElement => MainElement.FindElement(By.ClassName("bknd-username"));
        private IWebElement LogOutElement => MainElement.FindElement(By.ClassName("ti-export"));
        private IWebElement ChangePasswordElement => MainElement.FindElement(By.ClassName("ti-lock")).GetParent();

        public string LoginEmail => EmailElement?.Text;

        public SignInPage LogOut()
        {
            LogOutElement.Click();
            return new SignInPage(Driver);
        }
    }
}
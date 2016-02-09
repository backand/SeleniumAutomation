using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class EntryPage : SignInForm
    {
        public EntryPage(IWebDriver driver) : base(driver)
        {
        }

        protected override By EmailFindBy => By.CssSelector("[type=email]");
        protected override By PasswordFindBy => By.CssSelector("[type=password]");
        protected override By SubmitFindBy => By.CssSelector("[type=submit]");
    }
}
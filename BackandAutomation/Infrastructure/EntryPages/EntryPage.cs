using Core;
using Infrastructure.EntryPages.SignIn;
using OpenQA.Selenium;

namespace Infrastructure.EntryPages
{
    public class EntryPage : SignInForm
    {
        public EntryPage(DriverUser driverUser) : base(driverUser)
        {
        }

        protected override By EmailFindBy => By.CssSelector("[type=email]");
        protected override By PasswordFindBy => By.CssSelector("[type=password]");
        protected override By SubmitFindBy => By.CssSelector("[type=submit]");
    }
}
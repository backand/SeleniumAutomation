using Core;
using Infrastructure;
using Infrastructure.Base;
using Infrastructure.EntryPages;
using Infrastructure.EntryPages.SignIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;

namespace Tests
{
    [TestClass]
    public class SignIn : BackandTestClassBase
    {
        [TestMethod]
        public void SignInFromFacebook()
        {
            SignInFromExternalAccount(SignInFormType.Facebook);
        }

        [TestMethod]
        public void SignInFromGitHub()
        {
            SignInFromExternalAccount(SignInFormType.GitHub);
        }

        [TestMethod]
        public void SignInFromGoogle()
        {
            SignInFromExternalAccount(SignInFormType.Google);
        }

        private void SignInFromExternalAccount(SignInFormType facebook)
        {
            string email = Configuration.Instance.LoginCredentials.Email;
            string password = Configuration.Instance.LoginCredentials.Password;

            UserMainPage mainPage = Page.QuickSignIn(facebook, email, password);
            UserSettings settings = mainPage.Settings;
            Assert.AreEqual(email, settings.LoginEmail);

            SignInPage signInPage = settings.LogOut();
            mainPage = signInPage.QuickSignIn(facebook, email, password);
            settings = mainPage.Settings;
            Assert.AreEqual(email, settings.LoginEmail);
        }
    }
}
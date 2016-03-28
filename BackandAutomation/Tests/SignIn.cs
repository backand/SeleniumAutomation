using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages;
using Infrastructure.EntryPages.SignIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [TestClass]
    [DecomposedLogin]
    public class SignIn : BackandTestClassBase
    {
        [TestMethod]
        public void SignInRegular()
        {
            SignInFromExternalAccount(SignFormType.None);
        }

        [TestMethod]
        public void SignInFromFacebook()
        {
            SignInFromExternalAccount(SignFormType.Facebook);
        }

        [TestMethod]
        public void SignInFromGitHub()
        {
            SignInFromExternalAccount(SignFormType.GitHub);
        }

        [TestMethod]
        public void SignInFromGoogle()
        {
            SignInFromExternalAccount(SignFormType.Google);
        }

        private void SignInFromExternalAccount(SignFormType signFormType)
        {
            string email = Configuration.Instance.LoginCredentials.Email;
            string password = Configuration.Instance.LoginCredentials.Password;

            Page = EnterancePage.QuickSignIn(signFormType, email, password);
            UserSettings settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);

            SignInPage signInPage = settings.LogOut();
            Page = signInPage.QuickSignIn(signFormType, email, password);
            settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);
        }
    }
}
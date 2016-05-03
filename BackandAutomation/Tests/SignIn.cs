using Core;
using Infrastructure.EntryPages.SignIn;
using Infrastructure.EntryPages.SignIn.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [TestClass]
    [DecomposedLogin]
    public class SignIn : BackandTestClassBase
    {
        [TestMethod, Timeout(360000)]
        public void SignInRegular()
        {
            SignInFromExternalAccount<RegularSignInForm>();
        }

        [TestMethod, Timeout(360000)]
        public void SignInFromFacebook()
        {
            SignInFromExternalAccount<FacebookSignInForm>();
            ;
        }

        [TestMethod, Timeout(360000)]
        public void SignInFromGitHub()
        {
            SignInFromExternalAccount<GitHubSignInForm>();
        }

        [TestMethod, Timeout(360000)]
        public void SignInFromGoogle()
        {
            SignInFromExternalAccount<GoogleSignInForm>();
        }

        [TestMethod, Timeout(360000), Ignore]
        public void SignInFromTwitter()
        {
            SignInFromExternalAccount<TwitterSignInForm>();
        }

        private void SignInFromExternalAccount<T>() where T : SignInForm
        {
            var email = Configuration.Instance.LoginCredentials.Email;
            var password = Configuration.Instance.LoginCredentials.Password;

            Page = EnterancePage.QuickSignIn<T>(email, password);
            var settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);

            var signInPage = settings.LogOut();
            Page = signInPage.QuickSignIn<T>(email, password);
            settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);
        }
    }
}
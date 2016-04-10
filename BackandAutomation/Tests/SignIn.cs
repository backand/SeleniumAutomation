using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages;
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
        [TestMethod]
        public void SignInRegular()
        {
            SignInFromExternalAccount<RegularSignInForm>();
        }

        [TestMethod]
        public void SignInFromFacebook()
        {
            SignInFromExternalAccount<FacebookSignInForm>(); ;
        }

        [TestMethod]
        public void SignInFromGitHub()
        {
            SignInFromExternalAccount<GitHubSignInForm>();
        }

        [TestMethod]
        public void SignInFromGoogle()
        {
            SignInFromExternalAccount<GoogleSignInForm>();
        }

        [TestMethod, Ignore]
        public void SignInFromTwitter()
        {
            SignInFromExternalAccount<TwitterSignInForm>();
        }

        private void SignInFromExternalAccount<T>() where T : SignInForm
        {
            string email = Configuration.Instance.LoginCredentials.Email;
            string password = Configuration.Instance.LoginCredentials.Password;

            Page = EnterancePage.QuickSignIn<T>(email, password);
            UserSettings settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);

            SignInPage signInPage = settings.LogOut();
            Page = signInPage.QuickSignIn<T>(email, password);
            settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);
        }
    }
}
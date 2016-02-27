using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages;
using Infrastructure.EntryPages.SignIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;
using Tests.Utils;

namespace Tests
{
    [TestClass]
    public class SignUp : BackandTestClassBase
    {
        [TestMethod]
        public void SignUpRegular()
        {
            SignInFromExternalAccount(SignFormType.None);
        }

        [TestMethod]
        public void SignUpFromGoogle()
        {
            SignInFromExternalAccount(SignFormType.Google);
        }

        private void SignInFromExternalAccount(SignFormType signFormType)
        {
            string email = Utilities.GenerateEmail();
            string password = Configuration.Instance.LoginCredentials.Password;
            string fullName = Configuration.Instance.LoginCredentials.FullName;

            Page = EnterancePage.QuickSignUp(signFormType, fullName, email, password);
            UserSettings settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);

            SignInPage signInPage = settings.LogOut();
            Page = signInPage.QuickSignIn(signFormType, email, password);
            settings = Page.Settings;
            Assert.AreEqual(email, settings.LoginEmail);
        }
    }
}
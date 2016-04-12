using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [TestClass]
    [DecomposedLogin]
    public class SignUp : BackandTestClassBase
    {
        //[TestMethod, Timeout(360)]
        //public void SignUpRegular()
        //{
        //    SignInFromExternalAccount(SignFormType.None);
        //}

        //[TestMethod, Timeout(360), Ignore]
        //public void SignUpFromGoogle()
        //{
        //    SignInFromExternalAccount(SignFormType.Google);
        //}

        //private void SignInFromExternalAccount(SignFormType signFormType)
        //{
        //    string email = Utilities.GenerateEmail();
        //    string password = Configuration.Instance.LoginCredentials.Password;
        //    string fullName = Configuration.Instance.LoginCredentials.FullName;

        //    Page = EnterancePage.QuickSignUp(signFormType, fullName, email, password);
        //    UserSettings settings = Page.Settings;
        //    Assert.AreEqual(email, settings.LoginEmail);

        //    SignInPage signInPage = settings.LogOut();
        //    Page = signInPage.QuickSignIn(signFormType, email, password);
        //    settings = Page.Settings;
        //    Assert.AreEqual(email, settings.LoginEmail);
        //}
    }
}
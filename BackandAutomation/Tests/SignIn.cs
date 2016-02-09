using Core;
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
            //Page.SignIn(SignInFormType.GitHub, Utilities.LoginEmail, Utilities.LoginPassword);
            SignInForm form = Page.SignIn().SignIn(SignInFormType.Facebook);
            form.Email = Configuration.Instance.LoginCredentials.Email;
            form.Password = Configuration.Instance.LoginCredentials.Password;
            form.Submit();
        }
    }
}
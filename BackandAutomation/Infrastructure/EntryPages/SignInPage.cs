using Core;
using Infrastructure.EntryPages.SignIn;

namespace Infrastructure.EntryPages
{
    public class SignInPage : LoginPage
    {
        public SignInPage(DriverUser driverUser) : base(driverUser)
        {
            SignInFactory = new SignInFormsFactory(this);
        }

        private SignInFormsFactory SignInFactory { get; }

        public UserMainPage QuickSignIn<T>(string email, string password) where T : SignInForm
        {
            OpenSignForm<T>();
            T form = SignInFactory.Create<T>(OriginalHandle);
            return form.QuickSubmit(email, password);
        }
    }
}
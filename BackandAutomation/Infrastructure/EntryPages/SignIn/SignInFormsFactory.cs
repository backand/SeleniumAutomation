using Core;
using Infrastructure.Base;
using Infrastructure.EntryPages.SignIn.Types;

namespace Infrastructure.EntryPages.SignIn
{
    public class SignInFormsFactory : BasicFactory<SignInForm>
    {
        public SignInFormsFactory(DriverUser driverUser) : base(driverUser)
        {
            
        }

        protected override void InitClasses()
        {
            RegisterClass(typeof(GitHubSignInForm));
            RegisterClass(typeof(GoogleSignInForm));
            RegisterClass(typeof(FacebookSignInForm));
            RegisterClass(typeof(RegularSignInForm));
        }
        
        public T Create<T>(string originalWindowHandle) where T : SignInForm
        {
            return base.Create<T>(originalWindowHandle);
        }
    }
}
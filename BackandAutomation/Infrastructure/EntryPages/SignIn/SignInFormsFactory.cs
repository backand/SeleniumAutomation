using Core;
using Infrastructure.Base;

namespace Infrastructure.EntryPages.SignIn
{
    public class SignInFormsFactory : BasicFactory<SignInForm>
    {
        public SignInFormsFactory(DriverUser driverUser) : base(driverUser)
        {
        }

        public T Create<T>(string originalWindowHandle) where T : SignInForm
        {
            return base.Create<T>(originalWindowHandle);
        }
    }
}
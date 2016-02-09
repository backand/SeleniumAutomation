using System;

namespace Infrastructure.EntryPages.SignIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SignInFormTypeAttribute : Attribute
    {
        public SignInFormTypeAttribute(SignInFormType signInFormType)
        {
            SignInFormType = signInFormType;
        }

        public SignInFormType SignInFormType { get; }
    }
}
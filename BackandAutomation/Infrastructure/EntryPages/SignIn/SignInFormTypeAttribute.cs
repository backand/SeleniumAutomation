using System;

namespace Infrastructure.EntryPages.SignIn
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SignInFormTypeAttribute : Attribute
    {
        public SignInFormTypeAttribute(SignFormType signFormType)
        {
            SignFormType = signFormType;
        }

        public SignFormType SignFormType { get; }
    }
}
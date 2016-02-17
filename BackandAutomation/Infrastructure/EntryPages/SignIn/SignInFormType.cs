using Core;

namespace Infrastructure.EntryPages.SignIn
{
    public enum SignInFormType
    {
        [EnumText("github")] GitHub,
        [EnumText("google-plus")] Google,
        [EnumText("facebook")] Facebook,
        [EnumText("")] None
    }
}
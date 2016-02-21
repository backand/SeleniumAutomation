using Core;

namespace Infrastructure.EntryPages.SignIn
{
    public enum SignFormType
    {
        [EnumText("github")] GitHub,
        [EnumText("google-plus")] Google,
        [EnumText("facebook")] Facebook,
        [EnumText("")] None
    }
}
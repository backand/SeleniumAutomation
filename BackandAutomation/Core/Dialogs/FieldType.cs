namespace Core.Dialogs
{
    public enum FieldType
    {
        [EnumText("0")]
        String,
        [EnumText("1")]
        Text,
        [EnumText("2")]
        Point,
        [EnumText("3")]
        DateTime,
        [EnumText("4")]
        Float,
        [EnumText("5")]
        Boolean,
        [EnumText("6")]
        Collection,
        [EnumText("7")]
        Object
    }
}
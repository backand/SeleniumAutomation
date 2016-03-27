using System;

namespace Core
{
    public class EnumTextAttribute : Attribute
    {
        public EnumTextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; private set; }
    }
}

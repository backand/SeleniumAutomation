using System;

namespace Core
{
    public class MenuOptionAttribute : Attribute
    {
        public MenuOptionAttribute(string selector, string name, bool expandable)
        {
            Selector = selector;
            Name = name;
            Expandable = expandable;
        }

        public bool Expandable { get; private set; }
        public string Selector { get; private set; }
        public string Name { get; private set; }
    }
}
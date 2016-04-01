using System;

namespace Core
{
    public class MenuOptionAttribute : Attribute
    {
        public MenuOptionAttribute(string selector, string name, bool expandable, bool isDynamic)
        {
            Selector = selector;
            Name = name;
            Expandable = expandable;
            IsDynamic = isDynamic;
        }

        public MenuOptionAttribute(string selector, bool expandable, bool isDynamic)
        {
            Selector = selector;
            Expandable = expandable;
            IsDynamic = isDynamic;
        }

        public bool Expandable { get; private set; }
        public string Selector { get; private set; }
        public string Name { get; private set; }
        public bool IsDynamic { get; set; }
    }
}
using System;
using Core;

namespace Tests.Attributes
{
    public class CreateAppAttribute : Attribute
    {
        public CreateAppAttribute()
        {
            
        }

        public CreateAppAttribute(string name, string title)
        {
            Name = name;
            Title = title;
        }

        public string Name { get; set; } = "TestName".GenerateString();
        public string Title { get; set; } = "TestTitle".GenerateString();
    }
}
using System;

namespace Infrastructure.Base
{
    public class BackandPageTypeAttribute : Attribute
    {
        public BackandPageTypeAttribute(params LeftMenuOption[] option)
        {
            ConcatedOptions = option;
        }
        
        public LeftMenuOption[] ConcatedOptions { get; set; }
    }
}
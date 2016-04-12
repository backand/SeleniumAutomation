using System;
using OpenQA.Selenium;

namespace Core
{
    public class EnumFindByAttribute : Attribute
    {
        public EnumFindByAttribute(By findBy)
        {
            FindBy = findBy;
        }

        public By FindBy { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OpenQA.Selenium;
using Protractor;

namespace Core
{
    public static class Extensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            return type.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }

        public static string ToText(this Enum enumValue)
        {
            EnumTextAttribute enumTextAttribute = enumValue.GetAttribute<EnumTextAttribute>();
            return enumTextAttribute?.Text;
        }

        public static T ToEnum<T>(this string stringValue) where T : struct
        {
            IEnumerable<T> array = Enum.GetValues(typeof(T)).Cast<T>();
            return array.FirstOrDefault(value => (value as Enum).ToText() == stringValue);
        }

        public static By GetFindBy(this Enum enumValue)
        {
            EnumFindByAttribute enumTextAttribute = enumValue.GetAttribute<EnumFindByAttribute>();
            return enumTextAttribute?.FindBy;
        }

        //public static T ToEnum<T>(this By findBy) where T : struct
        //{
        //    IEnumerable<T> array = Enum.GetValues(typeof(T)).Cast<T>();
        //    return array.FirstOrDefault(value => (value as Enum).GetFindBy() == findBy);
        //}

        public static bool TryFindElement(this ISearchContext searcher, By findBy, out IWebElement element)
        {
            var elements = searcher.FindElements(findBy);
            element = elements.FirstOrDefault();
            return element != null;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
        
        public static bool TryFindElement(this ISearchContext searcher, By findBy, out IWebElement element)
        {
            var elements = searcher.FindElements(findBy);
            element = elements.FirstOrDefault();
            return element != null;
        }

        public static IWebElement Hover(this IWebDriver driver, IWebElement element)
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
            return element;
        }

        public static IWebElement GetParent(this ISearchContext element)
        {
            return element.FindElement(By.XPath(".."));
        }

        public static ReadOnlyCollection<IWebElement> GetChildren(this ISearchContext element)
        {
            return element.FindElements(By.XPath("*"));
        }

        public static string GetClass(this IWebElement element)
        {
            return element.GetAttribute("class");
        }

        public static string GetId(this IWebElement element)
        {
            return element.GetAttribute("id");
        }

        public static string GetLink(this IWebElement element)
        {
            return element.GetAttribute("href");
        }

        public static string[] GetClasses(this IWebElement element)
        {
            return element.GetClass().Split(' ');
        }
    }
}
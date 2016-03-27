using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

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
            element = searcher.TryFindElement(findBy);
            return element != null;
        }

        public static IWebElement TryFindElement(this ISearchContext searcher, By findBy)
        {
            ReadOnlyCollection<IWebElement> elements = searcher.FindElements(findBy);
            return elements.FirstOrDefault();
        }

        public static void JavascriptClick(this IWebDriver driver, string cssSelector)
        {
            var jsExecuter = (driver as IJavaScriptExecutor);
            jsExecuter.ExecuteScript($"$('{cssSelector}')[0].click()");
        }

        public static IWebElement Hover(this IWebDriver driver, IWebElement element)
        {
            //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //element = wait.Until(ExpectedConditions.ElementIsVisible(element));

            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
            return element;
        }

        public static IWebElement Hover(this IWebDriver driver, By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(locator));

                Actions action = new Actions(driver);

                action.MoveToElement(element).Perform();
                return element;
            }
            catch (WebDriverTimeoutException)
            {
                throw;
            }
        }

        public static void TryClick(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception ex)
            {

            }
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

        public static IEnumerable<string> GetClasses(this IWebElement element)
        {
            return element.GetClass().Split(' ');
        }

        public static string GenerateString(this string str)
        {
            return string.Concat(str, Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10));
        }
    }
}
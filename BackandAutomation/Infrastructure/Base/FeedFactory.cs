using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using OpenQA.Selenium;

namespace Infrastructure.Base
{
    public class FeedFactory : BasicFactory<BackandApplicationsBasePage>
    {
        private string _pageName;

        public FeedFactory(DriverUser driverUser) : base(driverUser)
        {
        }

        public T Create<T>(string pageName) where T : BackandApplicationsBasePage
        {
            _pageName = pageName;
            return base.Create<T>();
        }

        protected override void CreationExtraLogic(Type type)
        {
            var attribute = type.GetCustomAttribute<BackandPageTypeAttribute>();
            var leftMenuOptions = attribute.ConcatedOptions;
            CallOptions(leftMenuOptions);
        }

        private void CallOptions(IEnumerable<LeftMenuOption> options)
        {
            foreach (var option in options)
            {
                var optionAttribute = (option as Enum).GetAttribute<MenuOptionAttribute>();
                var cssSelector = optionAttribute.Selector;
                var isDynamic = optionAttribute.IsDynamic;
                var name = isDynamic ? _pageName : optionAttribute.Name;

                var webElements =
                    Driver.FindElements(By.CssSelector(cssSelector)).Select(element => element.GetParent());
                var optionElement = webElements.FirstOrDefault(element => element.Text == name);

                if (optionAttribute.Expandable)
                {
                    var listItem = optionElement.GetParent();
                    if (!listItem.IsOpen() || (name == "Objects" && listItem.IsOpen()))
                        optionElement?.Click();
                }
                else
                    optionElement?.Click();
            }
        }
    }
}
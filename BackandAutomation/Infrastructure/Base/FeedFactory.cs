using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using Infrastructure.Apps;
using Infrastructure.Object;
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
            BackandPageTypeAttribute attribute = type.GetCustomAttribute<BackandPageTypeAttribute>();
            LeftMenuOption[] leftMenuOptions = attribute.ConcatedOptions;
            CallOptions(leftMenuOptions);
        }

        private void CallOptions(IEnumerable<LeftMenuOption> options)
        {
            foreach (LeftMenuOption option in options)
            {
                MenuOptionAttribute optionAttribute = (option as Enum).GetAttribute<MenuOptionAttribute>();
                string cssSelector = optionAttribute.Selector;
                bool isDynamic = optionAttribute.IsDynamic;
                string name = isDynamic ? _pageName : optionAttribute.Name;

                IEnumerable<IWebElement> webElements =
                    Driver.FindElements(By.CssSelector(cssSelector)).Select(element => element.GetParent());
                IWebElement optionElement = webElements.FirstOrDefault(element => element.Text == name);

                if (optionAttribute.Expandable)
                {
                    IWebElement listItem = optionElement.GetParent();
                    if (!listItem.IsOpen() || (name == "Objects" && listItem.IsOpen()))
                        optionElement?.Click();
                }
                else
                    optionElement?.Click();
            }
        }
    }
}
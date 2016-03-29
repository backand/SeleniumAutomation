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
        public FeedFactory(DriverUser driverUser) : base(driverUser)
        {

        }

        protected override void InitClasses()
        {
            RegisterClass(typeof(DashbordPage));
            RegisterClass(typeof(ObjectsItemsPage));
            RegisterClass(typeof(AppSettingsPage));
        }
        
        public T Create<T>() where T : BackandApplicationsBasePage
        {
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
                string name = optionAttribute.Name;

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
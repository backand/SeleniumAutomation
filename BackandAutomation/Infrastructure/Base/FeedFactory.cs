using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core;
using Infrastructure.Apps;
using Infrastructure.EntryPages.SignIn;
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

        public BackandApplicationsBasePage Create(params LeftMenuOption[] leftMenuOption)
        {
            foreach (Type impl in from Type impl in RegisteredImplementations
                                  let attrlist = impl.GetCustomAttributes(true)
                                  where
                                      attrlist.OfType<BackandPageTypeAttribute>()
                                          .Any(attr => OptionEquals(attr.ConcatedOptions, leftMenuOption))
                                  select impl)
            {
                BackandPageTypeAttribute currentType = impl.GetCustomAttribute<BackandPageTypeAttribute>();
                LeftMenuOption[] options = currentType.ConcatedOptions;
                CallOptions(options);
                return (BackandApplicationsBasePage)Activator.CreateInstance(impl, this);
            }
            throw new Exception("Could not find a BackandApplicationsPage implementation for this SignInFormType");
        }

        public T Create<T>() where T : BackandApplicationsBasePage
        {
            Type type = RegisteredImplementations.FirstOrDefault(impl => impl == typeof(T));
            if (type == null)
                throw new Exception("Could not find a BackandApplicationsPage implementation for this type");
            BackandPageTypeAttribute attribute = type.GetCustomAttribute<BackandPageTypeAttribute>();
            LeftMenuOption[] leftMenuOptions = attribute.ConcatedOptions;
            CallOptions(leftMenuOptions);
            return (T)Activator.CreateInstance(type, this);
        }

        private void CallOptions(IEnumerable<LeftMenuOption> options)
        {
            foreach (LeftMenuOption option in options)
            {
                MenuOptionAttribute optionAttribute = (option as Enum).GetAttribute<MenuOptionAttribute>();
                string cssSelector = optionAttribute.Selector;
                string name = optionAttribute.Name;

                List<IWebElement> webElements =
                    Driver.FindElements(By.CssSelector(cssSelector)).Select(element => element.GetParent()).ToList();
                IWebElement optionElement = webElements.FirstOrDefault(element => element.Text == name);

                if (optionAttribute.Expandable)
                {
                    IWebElement listItem = optionElement.GetParent();
                    if (!listItem.IsOpen() || (name == "Objects" && listItem.IsOpen()))
                    {
                        optionElement?.Click();
                    }
                }
                else
                {
                    optionElement?.Click();
                }
            }
        }

        private bool OptionEquals(IEnumerable<LeftMenuOption> currentOptions,
            IEnumerable<LeftMenuOption> expectedOptions)
        {
            return currentOptions.SequenceEqual(expectedOptions);
        }
    }
}
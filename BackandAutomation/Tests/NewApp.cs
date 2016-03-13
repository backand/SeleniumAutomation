using System.Collections.Generic;
using System.Linq;
using Infrastructure.Apps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;
using Tests.Utils;

namespace Tests
{
    [TestClass, InstantLogin]
    public class NewApp : BackandTestClassBase
    {
        [TestMethod]
        public void NewAppValidation()
        {
            string appName = Utilities.GenerateString("TestName");
            string appTitle = Utilities.GenerateString("TestTitle");

            KickstartPage kickstartPage = CreateApp(appName, appTitle);
            string current = kickstartPage.CurrentAppComponent.Name;
            Assert.AreEqual(appName.ToLower(), current);

            Page = kickstartPage.GoToHomePage();
            AppsFeed feed = Page.AppsFeed;
            Assert.IsTrue(feed.AppsPannels.Any(app => app.Name == appName.ToUpper()));
        }

        [TestMethod]
        public void DeleteApp()
        {
            AppsFeed feed = Page.AppsFeed;
            string appName;
            List<BackandAppPannel> backandAppPannels = feed.AppsPannels.ToList();
            BackandAppPannel appPannel = backandAppPannels.FirstOrDefault(app => app.RibbonType == RibbonType.Connected);
            if (appPannel == null)
            {
                appName = Utilities.GenerateString("TestName");
                string appTitle = Utilities.GenerateString("TestTitle");
                var kickstarterPAge = CreateApp(appName, appTitle);
                Page = kickstarterPAge.GoToHomePage();
                appPannel = backandAppPannels.FirstOrDefault(app => app.Name == appName);
            }
            else
            {
                appName = appPannel.Name;
            }
            Assert.IsNotNull(appPannel, "appPannel != null");
            AppSettingsPage appSettings = appPannel.MoveToAppSettingsPage();
            Page = appSettings.Delete();
            feed = Page.AppsFeed;
            backandAppPannels = feed.AppsPannels.ToList();
            appPannel = backandAppPannels.FirstOrDefault(app => app.Name == appName);
            Assert.IsNull(appPannel);
        }

        private KickstartPage CreateApp(string name, string title)
        {
            AppsFeed feed = Page.AppsFeed;
            NewAppForm newAppForm = feed.New();
            newAppForm.Name = name;
            newAppForm.Title = title;
            return newAppForm.Submit();
        }
    }
}
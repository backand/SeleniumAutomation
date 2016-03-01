using System.Collections.Generic;
using System.Linq;
using Infrastructure;
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

            AppsFeed feed = Page.AppsFeed;
            NewAppForm newAppForm = feed.New();
            newAppForm.Name = appName;
            newAppForm.Title = appTitle;
            KickstartPage kickstartPage = newAppForm.Submit();
            string current = kickstartPage.CurrentAppComponent.Name;
            Assert.AreEqual(appName.ToLower(), current);

            Page = kickstartPage.GoToHomePage();
            feed = Page.AppsFeed;
            Assert.IsTrue(feed.AppsPannels.Any(app => app.Name == appName.ToUpper()));
        }

        [TestMethod]
        public void DeleteApp()
        {
            AppsFeed feed = Page.AppsFeed;
            string appName;
            List<BackandAppPannel> backandAppPannels = feed.AppsPannels.ToList();
            BackandAppPannel appPannel = backandAppPannels.FirstOrDefault(app => app.RibbonType == RibbonType.Connected);
            AppSettingsPage appSettings;
            if (appPannel == null)
            {
                appName = Utilities.GenerateString("TestName");
                string appTitle = Utilities.GenerateString("TestTitle");
                appSettings = CreateApp(appName, appTitle);
                
            }
            else
            {
                appName = appPannel.Name;
                appSettings = appPannel.MoveToAppSettingsPage();
            }
            Page = appSettings.Delete();
            feed = Page.AppsFeed;
            backandAppPannels = feed.AppsPannels.ToList();
            appPannel = backandAppPannels.FirstOrDefault(app => app.Name == appName);
            Assert.IsNull(appPannel);
        }

        private AppSettingsPage CreateApp(string name, string title)
        {
            throw new System.NotImplementedException();
        }
    }
}
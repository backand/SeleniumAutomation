using System.Linq;
using Infrastructure.Apps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Attributes;
using Tests.Base;

namespace Tests
{
    [TestClass]
    [CreateApp]
    public class NewApp : BackandTestClassBase
    {
        [TestMethod, Timeout(360)]
        public void NewAppValidation()
        {
            string current = ApplicationsPage.CurrentAppComponent.Name;
            Assert.AreEqual(CreateAppDetails.Name.ToLower(), current);

            Page = ApplicationsPage.GoToHomePage();
            AppsFeed feed = Page.AppsFeed;
            Assert.IsTrue(feed.AppsPannels.Any(app => app.Name == CreateAppDetails.Name.ToUpper()));
        }

        [TestMethod, Timeout(360)]
        [DontDeleteApp]
        public void DeleteApp()
        {
            AppSettingsPage settingsPage = ApplicationsPage.LeftMenu.FetchPage<AppSettingsPage>();

            Page = settingsPage.Delete();

            AppsFeed feed = Page.AppsFeed;
            bool result = feed.AppsPannels.Any(
                app => app.Name == CreateAppDetails.Name.ToUpper() && app.Title == CreateAppDetails.Title);

            Assert.IsFalse(result);
        }

        //[TestMethod, Timeout(360), Ignore]
        public void DeleteAll()
        {
            BackandAppPannel appPannel;
            while ((appPannel = Page.AppsFeed.AppsPannels.FirstOrDefault(app => app.RibbonType == RibbonType.Connected)) !=
                   null)
            {
                appPannel.MoveToAppSettingsPage().Delete();
            }
        }
    }
}
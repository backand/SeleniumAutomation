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
        [TestMethod, Timeout(360000)]
        public void NewAppValidation()
        {
            var current = ApplicationsPage.CurrentAppComponent.Name;
            Assert.AreEqual(CreateAppDetails.Name.ToLower(), current);

            Page = ApplicationsPage.GoToHomePage();
            var feed = Page.AppsFeed;
            Assert.IsTrue(feed.AppsPannels.Any(app => app.Name == CreateAppDetails.Name.ToUpper()));
        }

        [TestMethod, Timeout(360000)]
        [DontDeleteApp]
        public void DeleteApp()
        {
            var settingsPage = ApplicationsPage.LeftMenu.FetchPage<AppSettingsPage>();

            Page = settingsPage.Delete();

            var feed = Page.AppsFeed;
            var result = feed.AppsPannels.Any(
                app => app.Name == CreateAppDetails.Name.ToUpper() && app.Title == CreateAppDetails.Title);

            Assert.IsFalse(result);
        }

        //[TestMethod, Timeout(360000), Ignore]
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
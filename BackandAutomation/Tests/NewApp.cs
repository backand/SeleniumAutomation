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
    }
}
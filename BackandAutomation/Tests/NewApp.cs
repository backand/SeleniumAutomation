using System.Linq;
using Infrastructure.Apps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Base;

namespace Tests
{
    [TestClass, InstantLogin]
    public class NewApp : BackandTestClassBase
    {
        [TestMethod]
        public void NewAppValidation()
        {
            AppsFeed feed = Page.AppsFeed;
            NewAppForm newAppForm = feed.New();
            newAppForm.Name = "test name";
            newAppForm.Title = "test title";
            newAppForm.Submit();
            Assert.IsTrue(feed.AppsPannels.Any(app => app.Name == "test name"));
        }
    }
}
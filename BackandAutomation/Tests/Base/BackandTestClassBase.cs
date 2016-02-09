using Core;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Tests.Base
{
    [TestClass]
    public abstract class BackandTestClassBase
    {
        protected BackandPage Page { get; set; }

        protected static TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            TestContext = testContext;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Page = new BackandPage(GetDriver());
            TestInitializeExtension();
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            Driver.Dispose();
        }

        protected IWebDriver GetDriver()
        {
            Driver = DriversPool.GetWebDriver(false);
            return Driver;
        }

        public IWebDriver Driver { get; set; }

        public void TestInitializeExtension()
        {
        }

        public void TestCleanupExtension()
        {
        }
    }
}
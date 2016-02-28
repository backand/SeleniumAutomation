using System;
using System.IO;
using System.Reflection;
using Core;
using Infrastructure;
using Infrastructure.EntryPages.SignIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Tests.Base
{
    [TestClass]
    public class BackandTestClassBase
    {
        protected BackandPage EnterancePage { get; set; }
        protected UserMainPage Page { get; set; }

        protected static TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void SetupTests(TestContext testContext)
        {
            TestContext = testContext;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                EnterancePage = new BackandPage(GetDriver());
                TestInitializeExtension();
            }
            catch (Exception)
            {
                ClassCleanup();
                throw;
            }
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            TestCleanupExtension();
            Driver.Close();
        }

        protected IWebDriver GetDriver()
        {
            Driver = DriversPool.GetWebDriver(false);
            return Driver;
        }

        public IWebDriver Driver { get; set; }

        public void TestInitializeExtension()
        {
            Attribute fastLoginAttribute = GetType().GetCustomAttribute(typeof(InstantLoginAttribute));
            if (fastLoginAttribute != null)
            {
                Page = EnterancePage.QuickSignIn(SignFormType.None, Configuration.Instance.LoginCredentials.Email,
                    Configuration.Instance.LoginCredentials.Password);
            }
        }

        public void TestCleanupExtension()
        {
            //if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed ||
            //    TestContext.CurrentTestOutcome == UnitTestOutcome.Aborted ||
            //    TestContext.CurrentTestOutcome == UnitTestOutcome.Error)
            {
                DirectoryInfo screenshots = Page.ScreenshotsContainer.GetScreenshotsFolder();
            }
        }
    }
}
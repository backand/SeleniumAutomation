using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Core;
using Infrastructure;
using Infrastructure.Apps;
using Infrastructure.EntryPages.SignIn.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Tests.Attributes;

namespace Tests.Base
{
    [TestClass]
    public class BackandTestClassBase
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public TestContext TestContext { get; set; }

        protected BackandPage EnterancePage { get; private set; }
        protected UserMainPage Page { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                FolderPathProvider provider = new FolderPathProvider();
                EnterancePage = new BackandPage(GetDriver());
                TestInitializeExtension();
            }
            catch (Exception ex)
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

        private IWebDriver GetDriver()
        {
            Driver = DriversPool.GetWebDriver(false);
            return Driver;
        }

        private IWebDriver Driver { get; set; }

        protected virtual void TestInitializeExtension()
        {
            AttributesHandler();
        }

        private void AttributesHandler()
        {
            GetAllAttributes();
            if (!TestAttributes.OfType<DecomposedLoginAttribute>().Any())
            {
                Page = EnterancePage.QuickSignIn<RegularSignInForm>(Configuration.Instance.LoginCredentials.Email,
                    Configuration.Instance.LoginCredentials.Password);
            }

            CreateAppDetails = TestAttributes.OfType<CreateAppAttribute>().FirstOrDefault();
            if (CreateAppDetails != null)
            {
                AppsFeed feed = Page.AppsFeed;
                NewAppForm newAppForm = feed.New();
                newAppForm.Name = CreateAppDetails.Name;
                newAppForm.Title = CreateAppDetails.Title;
                ApplicationsPage = newAppForm.Submit();
            }
        }

        protected CreateAppAttribute CreateAppDetails { get; private set; }

        private void GetAllAttributes()
        {
            IEnumerable<Attribute> classAttributes = GetType().GetCustomAttributes();
            IEnumerable<Attribute> testAttributes = GetType().GetMethod(TestContext.TestName).GetCustomAttributes();

            IEnumerable<Attribute> attributes = classAttributes.Union(testAttributes);

            TestAttributes = attributes as Attribute[] ?? attributes.ToArray();
        }

        private Attribute[] TestAttributes { get; set; }

        protected KickstartPage ApplicationsPage { get; private set; }

        public void TestCleanupExtension()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Aborted ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Error)
            {
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(FolderPathProvider.FullPath);

                foreach (FileInfo file in directory.GetFiles()) file.Delete();
                foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);

                directory.Delete();
            }
            if (!TestAttributes.OfType<DontDeleteAppAttribute>().Any() && CreateAppDetails != null)
            {
                try
                {
                    AppsFeed appsFeed = Page.GoToHomePage().AppsFeed;
                    BackandAppPannel appPannel =
                        appsFeed.AppsPannels.FirstOrDefault(app => app.Name == CreateAppDetails.Name.ToUpper());
                    appPannel?.MoveToAppSettingsPage().Delete();
                }
                catch
                {
                    // ignored
                }
            }
        }
    }
}
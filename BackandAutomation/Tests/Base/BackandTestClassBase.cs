using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
            StartRunTimoutWatch();
            try
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ScreenshotsProvider(TestContext.TestName);
                EnterancePage = new BackandPage(GetDriver());
                TestInitializeExtension();
            }
            catch (Exception ex)
            {
                ClassCleanup();
                throw;
            }
        }

        private void StartRunTimoutWatch()
        {
            TimeoutTask = Task.Factory.StartNew(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.Elapsed < TimeSpan.FromSeconds(Configuration.Instance.App.TestTimeOut))
                {
                }
                Assert.Fail("Test aborted due to timeout.");
            });
            //TimeoutTask.Start();
        }

        private Task TimeoutTask { get; set; }

        [TestCleanup]
        public void ClassCleanup()
        {
            TestCleanupExtension();
            Driver.Close();
            if(!TimeoutTask.IsCompleted)
                TimeoutTask.Dispose();
        }

        private IWebDriver GetDriver()
        {
            Driver = DriversPool.GetWebDriver();
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
                // Add log
            }

            CreateAppDetails = TestAttributes.OfType<CreateAppAttribute>().FirstOrDefault();
            if (CreateAppDetails != null)
            {
                AppsFeed feed = Page.AppsFeed;
                NewAppForm newAppForm = feed.New();
                newAppForm.Name = CreateAppDetails.Name;
                newAppForm.Title = CreateAppDetails.Title;
                ApplicationsPage = newAppForm.Submit();
                // Add log
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

        private void TestCleanupExtension()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Aborted ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Error)
            {
            }
            else
            {
                DirectoryInfo directory = new DirectoryInfo(ScreenshotsProvider.FolderFullPath);

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
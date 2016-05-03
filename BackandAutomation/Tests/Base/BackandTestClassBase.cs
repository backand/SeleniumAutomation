using Core;
using Infrastructure;
using Infrastructure.Apps;
using Infrastructure.EntryPages.SignIn.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using Tests.Attributes;
//using System.Windows.Forms;

namespace Tests.Base
{
    [TestClass]
    public class BackandTestClassBase
    {
        private Timer Timer;

        // ReSharper disable once MemberCanBePrivate.Global
        public TestContext TestContext { get; set; }

        protected BackandPage EnterancePage { get; private set; }
        protected UserMainPage Page { get; set; }

        private IWebDriver Driver { get; set; }

        protected CreateAppAttribute CreateAppDetails { get; private set; }

        private Attribute[] TestAttributes { get; set; }

        protected KickstartPage ApplicationsPage { get; private set; }

        [TestInitialize]
        public void TestInitialize()
        {
            try
            {
                StartRunTimoutWatch();
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
            int t = Configuration.Instance.App.TestTimeOut;
            Timer = new Timer(TimeSpan.FromSeconds(t).TotalMilliseconds);
            Timer.Elapsed += TimerOnElapsed;
            Timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            Timer.Dispose();
            ClassCleanup();

            throw new AssertFailedException("Test aborted due to timeout.");

            throw new TimeoutException("Test aborted due to timeout.");
        }

        [TestCleanup]
        public void ClassCleanup()
        {
            TestCleanupExtension();

            if (Driver != null)
            {
                Driver.Close();
                Driver.Dispose();

            }

        }

        private IWebDriver GetDriver()
        {
            Driver = DriversPool.GetWebDriver();
            return Driver;
        }

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
                var feed = Page.AppsFeed;
                var newAppForm = feed.New();
                newAppForm.Name = CreateAppDetails.Name;
                newAppForm.Title = CreateAppDetails.Title;
                ApplicationsPage = newAppForm.Submit();
                // Add log
            }
        }

        private void GetAllAttributes()
        {
            var classAttributes = GetType().GetCustomAttributes();
            var testAttributes = GetType().GetMethod(TestContext.TestName).GetCustomAttributes();

            var attributes = classAttributes.Union(testAttributes);

            TestAttributes = attributes as Attribute[] ?? attributes.ToArray();
        }

        private void TestCleanupExtension()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Aborted ||
                TestContext.CurrentTestOutcome == UnitTestOutcome.Error)
            {
            }
            else
            {
                var directory = new DirectoryInfo(ScreenshotsProvider.FolderFullPath);

                foreach (var file in directory.GetFiles()) file.Delete();
                foreach (var subDirectory in directory.GetDirectories()) subDirectory.Delete(true);

                directory.Delete();
            }
            if (!TestAttributes.OfType<DontDeleteAppAttribute>().Any() && CreateAppDetails != null)
            {
                try
                {
                    var appsFeed = Page.GoToHomePage().AppsFeed;
                    var appPannel =
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
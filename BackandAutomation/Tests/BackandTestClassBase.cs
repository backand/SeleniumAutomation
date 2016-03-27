using Core;
using Infrastructure;
using Infrastructure.EntryPages.SignIn;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Infrastructure.Apps;
using Tests.Base;
using Tests.Utils;

namespace Tests
{
    [TestClass]
    public class BackandTestClassBase
    {
        private TestContext testContextInstance;

        protected TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
            //TestCleanupExtension();
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
            Attribute fastLoginAttribute = GetType().GetCustomAttribute(typeof(InstantLoginAttribute));
            if (fastLoginAttribute != null)
            {
                Page = EnterancePage.QuickSignIn(SignFormType.None, Configuration.Instance.LoginCredentials.Email,
                    Configuration.Instance.LoginCredentials.Password);
            }
        }

        private void AttributesHandler()
        {
            Attribute[] attributes = GetAllAttributes();
            if (!attributes.OfType<DecomposedLoginAttribute>().Any())
            {
                Page = EnterancePage.QuickSignIn(SignFormType.None, Configuration.Instance.LoginCredentials.Email,
                    Configuration.Instance.LoginCredentials.Password);
            }

            CreateAppAttribute createAppAttribute = attributes.OfType<CreateAppAttribute>().FirstOrDefault();
            if (createAppAttribute != null)
            {
                AppsFeed feed = Page.AppsFeed;
                NewAppForm newAppForm = feed.New();
                newAppForm.Name = createAppAttribute.Name;
                newAppForm.Title = createAppAttribute.Title;
                ApplicationsPage = newAppForm.Submit();
            }
        }

        private Attribute[] GetAllAttributes()
        {
            IEnumerable<Attribute> classAttributes = GetType().GetCustomAttributes();
            IEnumerable<Attribute> testAttributes = GetType().GetMethod(TestContext.TestName).GetCustomAttributes();

            IEnumerable<Attribute> attributes = classAttributes.Union(testAttributes);

            Attribute[] enumerable = attributes as Attribute[] ?? attributes.ToArray();
            return enumerable;
        }

        public KickstartPage ApplicationsPage { get; set; }

        protected virtual void TestCleanupExtension()
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
        }
    }
}
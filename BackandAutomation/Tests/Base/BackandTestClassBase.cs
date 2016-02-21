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

        public static TestContext TestContext { get; set; }

        [DeploymentItem("Tests/Utils/BackandConfiguration.xml", "Helpers")]
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            string fileContent = File.ReadAllText(@"Helpers\BackandConfiguration.xml");

        }

        [TestInitialize]
        public void TestInitialize()
        {
            EnterancePage = new BackandPage(GetDriver());
            TestInitializeExtension();
        }

        [TestCleanup]
        public void ClassCleanup()
        {
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
                Page = EnterancePage.QuickSignIn(SignInFormType.None, Configuration.Instance.LoginCredentials.Email,
                    Configuration.Instance.LoginCredentials.Password);
            }
        }

        public void TestCleanupExtension()
        {
        }
    }
}
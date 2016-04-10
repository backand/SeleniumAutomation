using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;
using Protractor;

namespace Core
{
    public class ScreenshotsContainer
    {
        public ScreenshotsContainer(IWebDriver driver)
        {
            Driver = driver;
            ScreenshotsDir = Path.Combine(Configuration.Instance.ScreenshotsFolder, ScreenshotsProvider.FolderFullPath);
            ScreenshotsFolder = Directory.CreateDirectory(ScreenshotsDir);
        }

        private string ScreenshotsDir { get; }
        private DirectoryInfo ScreenshotsFolder { get; }
        private IWebDriver Driver { get; }

        public void AddScreenshot()
        {
            try
            {
                NgWebDriver ngWebDriver = Driver as NgWebDriver;
                if (ngWebDriver != null)
                {
                    IWebDriver driver = ngWebDriver.WrappedDriver;
                    ITakesScreenshot screenshotTake = driver as ITakesScreenshot;
                    Screenshot screenshot = screenshotTake?.GetScreenshot();

                    string fileName = ScreenshotsProvider.TestName + DateTime.Now.ToLongTimeString().Replace(':', '-');
                    string filePath = $"{Path.Combine(ScreenshotsDir, fileName)}.bmp";

                    screenshot?.SaveAsFile(filePath, ImageFormat.Bmp);
                }
            }
            catch
            {
                //Ignored
            }
        }
    }

    public class ScreenshotsProvider
    {
        public ScreenshotsProvider(string testName)
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            FolderFullPath = Path.Combine(Configuration.Instance.ScreenshotsFolder, $"Results - {date} {time}");
            TestName = testName;
        }

        public static string FolderFullPath { get; private set; }
        public static string TestName { get; private set; }
    }
}
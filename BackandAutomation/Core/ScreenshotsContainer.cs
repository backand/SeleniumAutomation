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
            ScreenshotsDir = Path.Combine(Configuration.Instance.ScreenshotsFolder, FolderPathProvider.FullPath);
            ScreenshotsFolder = Directory.CreateDirectory(ScreenshotsDir);
        }

        private string MakeFolderPath()
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            string folderName = $"Results - {date} {time}";
            return folderName;
        }

        public string ScreenshotsDir { get; set; }
        public DirectoryInfo ScreenshotsFolder { get; set; }
        private IWebDriver Driver { get; }

        public void AddScreenshot()
        {
            IWebDriver driver = (Driver as NgWebDriver).WrappedDriver;
            ITakesScreenshot screenshotTake = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotTake.GetScreenshot();

            string fileName = DateTime.Now.ToLongTimeString().Replace(':', '-');
            string filePath = Path.Combine(ScreenshotsDir, fileName) + ".bmp";

            screenshot.SaveAsFile(filePath, ImageFormat.Bmp);
        }

        public DirectoryInfo GetScreenshotsFolder()
        {
            return ScreenshotsFolder;
        }
    }

    public class FolderPathProvider
    {
        public FolderPathProvider()
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            FullPath = Path.Combine(Configuration.Instance.ScreenshotsFolder, $"Results - {date} {time}");
        }

        public static string FullPath { get; set; }
    }
}
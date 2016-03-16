using System;
using System.Drawing.Imaging;
using System.IO;
using OpenQA.Selenium;

namespace Core
{
    public class ScreenshotsContainer
    {
        public ScreenshotsContainer(IWebDriver driver)
        {
            Driver = driver;
            FolderPath = MakeFolderPath();

            string screenshotsDir = Path.Combine(Configuration.Instance.ScreenshotsFolder, FolderPath);

            ScreenshotsFolder = Directory.CreateDirectory(screenshotsDir);
        }

        private string MakeFolderPath()
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            string folderName = $"Results - {date} {time}";
            return folderName;
        }

        public string FolderPath { get; set; }
        public DirectoryInfo ScreenshotsFolder { get; set; }
        private IWebDriver Driver { get; }

        public void AddScreenshot()
        {
            ITakesScreenshot screenshotTaker = Driver as ITakesScreenshot;
            Screenshot screenshot = screenshotTaker?.GetScreenshot();
            screenshot?.SaveAsFile(Path.Combine(FolderPath, Driver.Url), ImageFormat.Bmp);
        }

        public DirectoryInfo GetScreenshotsFolder()
        {
            return ScreenshotsFolder;
        }
    }
}
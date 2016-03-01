using System;
using System.Collections.Generic;
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
            FolderPath = DateTime.Now.ToShortDateString();

            ScreenshotsFolder =
                Directory.CreateDirectory(Path.Combine(Configuration.Instance.ScreenshotsFolder, FolderPath));
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
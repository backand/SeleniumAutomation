using Core;
using System;
using System.IO;

namespace Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string FolderPath = MakeFolderPath();

            string screenshotsDir = Path.Combine(Configuration.Instance.ScreenshotsFolder, FolderPath);

            Directory.CreateDirectory(screenshotsDir);
        }

        private static string MakeFolderPath()
        {
            DateTime datetimeNow = DateTime.Now;
            string time = datetimeNow.ToLongTimeString().Replace(':', '-');
            string date = datetimeNow.ToShortDateString().Replace('/', '.');
            string folderName = $"Results - {date} {time}";
            return folderName;
        }
    }
}
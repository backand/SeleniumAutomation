using Core;
using System;
using System.IO;

namespace Playground
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string fileName = "https://www.backand.com/apps/#/";
            //string fileName = Path.Combine(@"C:\Screenshots\Results - 20.03.2016 02-07-02", "https://www.backand.com/apps/#/");
            int startIndex = "https://www.backand.com/".Length;
            int endIndex = fileName.IndexOf('/', startIndex);
            fileName= fileName.Substring(startIndex, endIndex - startIndex);

            string filePath = Path.Combine(@"C:\Screenshots\Results - 20.03.2016 02-07-02", fileName);

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
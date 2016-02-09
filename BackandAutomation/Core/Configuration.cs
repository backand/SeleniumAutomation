using System;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using Core.ConfigurationElements;

namespace Core
{
    public class Configuration
    {
        public static readonly object LockObj = new object();
        private static BackandConfiguration _configuration;
        private static Configuration _configuratonInstance;

        private Configuration()
        {
            var serializer = new XmlSerializer(typeof (BackandConfiguration));
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..", _filePath);
            using (Stream stream = new FileStream(path, FileMode.Open))
            {
                var obj = serializer.Deserialize(stream);
                _configuration = obj as BackandConfiguration;
            }
        }

        private string _filePath => ConfigurationManager.AppSettings["configurationExtensionPath"];
        
        public static BackandConfiguration Instance
        {
            get
            {
                if (_configuration != null) return _configuration;
                lock (LockObj)
                {
                    if (_configuration == null)
                    {
                        _configuratonInstance = new Configuration();
                    }
                }
                return _configuration;
            }
        }

        //public string AppUrl => _configuration.App.Url;
        //public bool IsLocal => _configuration.Selenium.IsLocal;

        //public string DriverPath
        //    =>
        //        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..",
        //            _configuration.Selenium.ChromeDriverPath);

        //public TimeSpan ProtractorTimeOut => TimeSpan.FromSeconds(_configuration.Selenium.ProtractorTimeOut);
    }
}
using Core.ConfigurationElements;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace Core
{
    public class Configuration
    {
        private static readonly object LockObj = new object();
        private static BackandConfiguration _configuration;

        private Configuration()
        {
            _configuration = JsonConvert.DeserializeObject<BackandConfiguration>(File.ReadAllText(_filePath));
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
                        // ReSharper disable once ObjectCreationAsStatement
                        new Configuration();
                    }
                }
                return _configuration;
            }
        }
    }
}
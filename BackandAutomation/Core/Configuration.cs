using System.Configuration;
using System.Xml.Serialization;
using Core.ConfigurationElements;

namespace Core
{
    public class Configuration
    {
        private static readonly object LockObj = new object();
        private static BackandConfiguration _configuration;

        private Configuration()
        {
            var serializer = new XmlSerializer(typeof (BackandConfiguration));

            using (var stream = ResourcesHandler.GetStream(_filePath))
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
                        // ReSharper disable once ObjectCreationAsStatement
                        new Configuration();
                    }
                }
                return _configuration;
            }
        }
    }
}
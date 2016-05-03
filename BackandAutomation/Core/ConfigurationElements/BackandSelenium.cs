using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Core.ConfigurationElements
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class BackandSelenium
    {
        [XmlElement("isLocal")]
        public bool IsLocal { get; set; }

        [XmlElement("chromeDriverPath")]
        public string ChromeDriverPath { get; set; }

        [XmlElement("protractorTimeOut", typeof (int))]
        public int ProtractorTimeOut { get; set; }

        [XmlElement("remoteGridHub")]
        public string RemoteGridHub { get; set; }
    }
}
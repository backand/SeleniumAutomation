using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Core.ConfigurationElements
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class BackandApp
    {
        [XmlElement("url")]
        public string Url { get; set; }

        [XmlElement("testTimeOut")]
        public int TestTimeOut { get; set; }
    }
}
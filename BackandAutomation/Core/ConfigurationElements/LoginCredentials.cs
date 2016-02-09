using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Core.ConfigurationElements
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class LoginCredentials
    {
        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }
    }
}
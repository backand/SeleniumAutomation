using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Core.ConfigurationElements
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot("backandConfiguration")]
    public class BackandConfiguration
    {
        [XmlElement("app")]
        public BackandApp App { get; set; }

        [XmlElement("selenium")]
        public BackandSelenium Selenium { get; set; }

        [XmlElement("loginCredentials")]
        public LoginCredentials LoginCredentials { get; set; }

        [XmlElement("screenshotsFolder")]
        public string ScreenshotsFolder { get; set; }
    }
}


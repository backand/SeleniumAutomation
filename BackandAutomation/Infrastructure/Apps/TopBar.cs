using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class TopBar : DriverUser
    {
        public TopBar(DriverUser driverUser) : base(driverUser)
        {

        }

        protected virtual IWebElement MainElement => Driver.FindElement(By.ClassName("top-header"));
    }
}
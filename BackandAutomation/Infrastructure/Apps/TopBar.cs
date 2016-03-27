using Core;
using OpenQA.Selenium;

namespace Infrastructure.Apps
{
    public class TopBar : DriverUser
    {
        public TopBar(DriverUser driver) : base(driver)
        {

        }

        protected virtual IWebElement MainElement => Driver.FindElement(By.ClassName("top-header"));
    }
}
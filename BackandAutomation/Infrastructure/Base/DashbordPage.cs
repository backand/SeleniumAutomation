using Core;

namespace Infrastructure.Base
{
    [BackandPageType(LeftMenuOption.Dashboard)]
    public class DashbordPage : BackandApplicationsBasePage
    {
        public DashbordPage(DriverUser driver) : base(driver)
        {
        }
    }
}
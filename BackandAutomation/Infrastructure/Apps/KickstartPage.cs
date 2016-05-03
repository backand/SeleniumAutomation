using Core;
using Infrastructure.Base;

namespace Infrastructure.Apps
{
    public class KickstartPage : BackandApplicationsBasePage
    {
        public KickstartPage(DriverUser driver) : base(driver)
        {
        }

        public DatabaseTopBar TopBar => new DatabaseTopBar(this);
    }
}
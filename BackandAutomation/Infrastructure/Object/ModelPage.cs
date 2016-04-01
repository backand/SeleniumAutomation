using Core;
using Infrastructure.Base;

namespace Infrastructure.Object
{
    [BackandPageType(LeftMenuOption.Objects, LeftMenuOption.DynamicObject)]
    public class ModelPage : BackandApplicationsBasePage
    {
        public ModelPage(DriverUser driver) : base(driver)
        {
        }
    }
}
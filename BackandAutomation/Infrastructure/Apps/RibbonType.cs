using Core;

namespace Infrastructure.Apps
{
    public enum RibbonType
    {
        [EnumText("primary")]
        New,
        [EnumText("success")]
        Connected,
        [EnumText("warning")]
        Example
    }
}
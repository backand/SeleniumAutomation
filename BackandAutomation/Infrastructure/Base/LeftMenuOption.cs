using Core;

namespace Infrastructure.Base
{
    public enum LeftMenuOption
    {
        [MenuOption(".ba-icon-dashboard", "Dashboard", ExpandableFlag.Yes, DynamicFlag.No)] Dashboard,
        [MenuOption(".ba-icon-objects", "Objects", ExpandableFlag.Yes, DynamicFlag.No)] Objects,
        [MenuOption(".ba-icon-settings", "Settings", ExpandableFlag.Yes, DynamicFlag.No)] Settings,
        [MenuOption(".ba-icon-general", "General", ExpandableFlag.No, DynamicFlag.No)] General,
        [MenuOption(".ba-icon-todo", ExpandableFlag.No, DynamicFlag.Yes)] DynamicObject,
        [MenuOption(".ba-icon-model", "Model", ExpandableFlag.No, DynamicFlag.No)] Model,
        [MenuOption(".ba-icon-new-query", "New Object", ExpandableFlag.No, DynamicFlag.No)] NewObject
    }

    public static class ExpandableFlag
    {
        public const bool Yes = true;
        public const bool No = false;
    }

    public static class DynamicFlag
    {
        public const bool Yes = true;
        public const bool No = false;
    }
}
using Core;

namespace Infrastructure.Base
{
    public enum LeftMenuOption
    {
        [MenuOption(".ba-icon-dashboard", "Dashboard", false)]
        Dashboard,
        [MenuOption(".ba-icon-objects", "Objects", true)]
        Objects,
        [MenuOption(".ba-icon-todo", "items", false)]
        Items,
        [MenuOption(".ba-icon-settings", "Settings", true)]
        Settings,
        [MenuOption(".ba-icon-general", "General", false)]
        General
    }
}
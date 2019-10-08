using System;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI
{
    public class ThemeDirtyEventArgs : EventArgs
    {
        public ThemeCategory category = ThemeCategory.None;
        public ThemePart part = ThemePart.None;
        public IMixable loadable = null;

        public ThemeDirtyEventArgs(ThemeCategory category, ThemePart part, IMixable loadable = null) {
            this.loadable = loadable;
            this.category = category;
            this.part = part;
        }
    }

    public class UIDirtyEventArgs : EventArgs
    {
        public ThemeMix mix;

        public UIDirtyEventArgs(ThemeMix mix) {
            this.mix = mix;
        }
    }

    public class ThemeSelectedEventArgs : EventArgs
    {
        public string packageID;
        public ThemeCategory category = ThemeCategory.None;
        public ThemePart part = ThemePart.None;

        public ThemeSelectedEventArgs(string packageID, ThemeCategory category, ThemePart part) {
            this.packageID = packageID;
            this.category = category;
            this.part = part;
        }
    }

    public class ThemesPanelClosingEventArgs : EventArgs
    {
        public ThemeCategory category = ThemeCategory.None;
        public ThemePart part = ThemePart.None;
        public ThemesPanelClosingEventArgs(ThemeCategory category, ThemePart part) {
            this.category = category;
            this.part = part;
        }
    }
}

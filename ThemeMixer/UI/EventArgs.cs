﻿using System;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI.Color;

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
        public string themeID;
        public ThemeCategory category = ThemeCategory.None;
        public ThemePart part = ThemePart.None;

        public ThemeSelectedEventArgs(string themeID, ThemeCategory category, ThemePart part) {
            this.themeID = themeID;
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

    public class ColorPanelVisibilityChangedEventArgs : EventArgs
    {
        public bool visible;
        public ColorPanel panel;

        public ColorPanelVisibilityChangedEventArgs(ColorPanel panel, bool visible) {
            this.visible = visible;
            this.panel = panel;
        }
    }
}

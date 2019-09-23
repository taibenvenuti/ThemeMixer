using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI
{
    public static class UIExtensions
    {
        public static void Setup(this PanelBase panel, Vector2 size, int spacing = UIUtils.DEFAULT_SPACING, bool autoLayout = false, LayoutDirection autoLayoutDirection = LayoutDirection.Horizontal, LayoutStart autoLayoutStart = LayoutStart.TopLeft, string backgroundSprite = "") {
            panel.size = size;
            panel.autoLayout = autoLayout;
            panel.autoLayoutDirection = autoLayoutDirection;

            switch (autoLayoutDirection) {
                case LayoutDirection.Horizontal: panel.autoFitChildrenHorizontally = true; break;
                case LayoutDirection.Vertical: panel.autoFitChildrenVertically = true; break;
                default: break;
            }
            panel.autoLayoutStart = autoLayoutStart;
            panel.atlas = Sprites.DefaultAtlas;
            panel.backgroundSprite = backgroundSprite;
            switch (autoLayoutStart) {
                case LayoutStart.TopLeft:
                    panel.padding = new RectOffset(spacing, 0, spacing, 0);
                    panel.autoLayoutPadding = new RectOffset(0, spacing, 0, spacing);
                    break;

                case LayoutStart.BottomLeft:
                    panel.padding = new RectOffset(spacing, 0, 0, spacing);
                    panel.autoLayoutPadding = new RectOffset(0, spacing, spacing, 0);
                    break;

                case LayoutStart.TopRight:
                    panel.padding = new RectOffset(0, spacing, 0, spacing);
                    panel.autoLayoutPadding = new RectOffset(spacing, 0, spacing, 0);
                    break;

                case LayoutStart.BottomRight:
                    panel.padding = new RectOffset(spacing, 0, 0, spacing);
                    panel.autoLayoutPadding = new RectOffset(0, spacing, spacing, 0);
                    break;
            }
        }
    }
}

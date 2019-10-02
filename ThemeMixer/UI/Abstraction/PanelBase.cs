using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.UI.Abstraction
{
    public class PanelBase : UIPanel {
        protected UIController Controller => UIController.Instance;
        protected SerializationService Data => SerializationService.Instance;
        protected static Color32 UIColor { get; set; } = new Color32(128, 128, 128, 255);
        public ThemeCategory ThemePart { get; private set; }

        public UIButton CreateButton(Vector2 size, string text = "", string tooltip = "", string foregroundSprite = "", string backgroundSprite = "ButtonSmall", bool isFocusable = false, UITextureAtlas atlas = null) {
            UIButton button = AddUIComponent<UIButton>();
            button.size = size;
            button.text = text;
            button.tooltip = tooltip;
            button.textPadding = new RectOffset(0, 0, 3, 0);
            button.disabledTextColor = new Color32(128, 128, 128, 255);
            button.normalBgSprite = backgroundSprite;
            button.hoveredBgSprite = string.Concat(backgroundSprite, "Hovered");
            button.pressedBgSprite = string.Concat(backgroundSprite, "Pressed");
            button.disabledBgSprite = string.Concat(backgroundSprite, "Disabled");
            button.focusedBgSprite = string.Concat(backgroundSprite, isFocusable ? "Focused" : "");
            button.normalFgSprite = foregroundSprite;
            button.hoveredFgSprite = string.Concat(foregroundSprite, "Hovered");
            button.pressedFgSprite = string.Concat(foregroundSprite, "Pressed");
            button.disabledFgSprite = string.Concat(foregroundSprite, "Disabled");
            button.focusedFgSprite = string.Concat(foregroundSprite, isFocusable ? "Focused" : "");
            button.atlas = UISprites.DefaultAtlas;
            if (atlas != null) button.atlas = atlas;
            return button;
        }
        public void Setup(Vector2 size, int spacing = UIUtils.DEFAULT_SPACING, bool autoLayout = false, LayoutDirection autoLayoutDirection = LayoutDirection.Horizontal, LayoutStart autoLayoutStart = LayoutStart.TopLeft, string backgroundSprite = "", ThemeCategory themePart = ThemeCategory.None) {
            this.size = size;
            this.autoLayout = autoLayout;
            this.autoLayoutDirection = autoLayoutDirection;
            switch (autoLayoutDirection) {
                case LayoutDirection.Horizontal: autoFitChildrenHorizontally = true; break;
                case LayoutDirection.Vertical: autoFitChildrenVertically = true; break;
                default: break;
            }
            this.autoLayoutStart = autoLayoutStart;
            atlas = UISprites.DefaultAtlas;
            this.backgroundSprite = backgroundSprite;
            switch (autoLayoutStart) {
                case LayoutStart.TopLeft:
                    padding = new RectOffset(spacing, 0, spacing, 0);
                    autoLayoutPadding = new RectOffset(0, spacing, 0, spacing);
                    break;

                case LayoutStart.BottomLeft:
                    padding = new RectOffset(spacing, 0, 0, spacing);
                    autoLayoutPadding = new RectOffset(0, spacing, spacing, 0);
                    break;

                case LayoutStart.TopRight:
                    padding = new RectOffset(0, spacing, 0, spacing);
                    autoLayoutPadding = new RectOffset(spacing, 0, spacing, 0);
                    break;

                case LayoutStart.BottomRight:
                    padding = new RectOffset(spacing, 0, 0, spacing);
                    autoLayoutPadding = new RectOffset(0, spacing, spacing, 0);
                    break;
            }
            ThemePart = themePart;
            builtinKeyNavigation = true;
        }

        protected virtual void Refresh(ILoadable themePart) { }
    }
}

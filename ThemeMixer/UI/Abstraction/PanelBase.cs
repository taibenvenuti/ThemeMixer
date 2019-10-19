using ColossalFramework.UI;
using System;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes.Enums;
using UnityEngine;

namespace ThemeMixer.UI.Abstraction
{
    public class PanelBase : UIPanel
    {
        public static event EventHandler<ThemeDirtyEventArgs> EventThemeDirty;
        protected UIController Controller => UIController.Instance;
        protected SerializationService Data => SerializationService.Instance;
        protected static Color32 UIColor { get; set; } = new Color32(100, 100, 100, 255);
        protected static Color32 UIColorDark { get; set; } = new Color32(40, 40, 40, 255);
        protected static Color32 UIColorLight { get; set; } = new Color32(128, 128, 128, 255);
        protected static Color32 UIColorGrey { get; set; } = new Color32(55, 58, 60, 255);
        public ThemeCategory Category { get; set; } = ThemeCategory.None;

        public override void Awake() {
            base.Awake();
            Controller.EventUIDirty += OnRefreshUI;
        }

        public override void OnDestroy() {
            Controller.EventUIDirty -= OnRefreshUI;
            base.OnDestroy();
        }

        public virtual void Setup(string name, float width, float height, int spacing = UIUtils.DEFAULT_SPACING, bool autoLayout = false, LayoutDirection autoLayoutDirection = LayoutDirection.Horizontal, LayoutStart autoLayoutStart = LayoutStart.TopLeft, string backgroundSprite = "") {
            this.name = name;
            this.width = width;
            this.height = height;
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
            builtinKeyNavigation = true;
            color = UIColor;
        }

        protected virtual void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) { }

        protected virtual void OnRefreshTheme(object sender, ThemeDirtyEventArgs eventArgs) {
            EventThemeDirty?.Invoke(sender, eventArgs);
        }
    }
}

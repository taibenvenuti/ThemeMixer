using ColossalFramework.UI;
using System;
using System.Reflection;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI.Parts;
using UnityEngine;

namespace ThemeMixer.UI.Abstraction
{
    public class PanelBase : UIPanel
    {
        public static event EventHandler<ThemeDirtyEventArgs> EventThemeDirty;
        protected UIController Controller => UIController.Instance;
        protected SerializationService Data => SerializationService.Instance;
        protected static Color32 UIColor { get; set; } = new Color32(128, 128, 128, 255);
        public ThemeCategory Category { get; set; } = ThemeCategory.None;

        public override void Awake() {
            base.Awake();

            object[] attrsB = GetType().GetCustomAttributes(typeof(UICategoryAttribute), true);
            if (attrsB != null && attrsB.Length > 0 && attrsB[0] is UICategoryAttribute b)
                Category = b.Category;

            object[] attrsA = GetType().GetCustomAttributes(typeof(UIProperties), true);
            if (attrsA != null && attrsA.Length > 0 && attrsA[0] is UIProperties a)
                Setup(a.Name, a.Size, a.Spacing, a.AutoLayout, a.LayoutDirection, a.LayoutStart, a.BackgroundSprite);
        }

        public override void Start() {
            base.Start();
            FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (FieldInfo field in fields) {
                if (field == null || (!typeof(PanelBase).IsAssignableFrom(field.FieldType))) continue;
                PanelBase panelBase = field.GetValue(this) as PanelBase;
                object[] attrsD = field.GetCustomAttributes(typeof(UICategoryAttribute), true);
                if (attrsD?.Length > 0 && attrsD[0] is UICategoryAttribute d)
                    panelBase.Category = d.Category;

                object[] attrsC = field.GetCustomAttributes(typeof(UIProperties), true);
                if (attrsC?.Length > 0 && attrsC[0] is UIProperties c)
                    panelBase.Setup(c.Name, c.Size, c.Spacing, c.AutoLayout, c.LayoutDirection, c.LayoutStart, c.BackgroundSprite);

                if (panelBase is TexturePanel texturePanel) {
                    object[] attrsA = field.GetCustomAttributes(typeof(UITextureIDAttribute), true);
                    if (attrsA?.Length > 0 && attrsA[0] is UITextureIDAttribute a)
                        texturePanel.textureID = a.TextureID;
                }
            }
            Controller.EventUIDirty += OnRefreshUI;
        }

        public UIButton CreateButton(Vector2 size, string text = "", string tooltip = "", string foregroundSprite = "", string backgroundSprite = "ButtonSmall", bool isFocusable = false, UITextureAtlas atlas = null) {
            UIButton button = AddUIComponent<UIButton>();
            button.size = size;
            button.text = text;
            button.tooltip = tooltip;
            button.textPadding = new RectOffset(8, 8, 8, 5);
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

        public virtual void Setup(string name, Vector2 size, int spacing = UIUtils.DEFAULT_SPACING, bool autoLayout = false, LayoutDirection autoLayoutDirection = LayoutDirection.Horizontal, LayoutStart autoLayoutStart = LayoutStart.TopLeft, string backgroundSprite = "") {
            this.name = name;
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
            builtinKeyNavigation = true;
            color = UIColor;
        }

        protected virtual void OnRefreshUI(object sender, UIDirtyEventArgs e) { }

        protected virtual void OnRefreshTheme(object sender, ThemeDirtyEventArgs eventArgs) {
            EventThemeDirty?.Invoke(sender, eventArgs);
        }
    }
}

using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    [UIProperties("Texture Panel Container", 300.0f, 0.0f, UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
    public class TexturePanel : PanelBase
    {
        public TextureID textureID;
        [UIProperties("Texture Panel", 290.0f, 66.0f)]
        protected PanelBase panelTop;
        protected UIButton thumbBackground;
        protected UIPanel thumbMiddleground;
        protected UISprite thumb;
        protected UILabel label;
        [UIProperties("Texture Panel Space", 290.0f, 0.01f)]
        protected PanelBase panelBottom;
        protected UISlider slider;

        public override void Awake() {
            base.Awake();
            object[] attrs = GetType().GetCustomAttributes(typeof(UITextureIDAttribute), true);
            if (attrs != null && attrs.Length > 0 && attrs[0] is UITextureIDAttribute attr)
                textureID = attr.TextureID;

            panelTop = AddUIComponent<PanelBase>();
            panelBottom = AddUIComponent<PanelBase>();
            thumbBackground = panelTop.AddUIComponent<UIButton>();
            thumbMiddleground = panelTop.AddUIComponent<UIPanel>();
            thumb = panelTop.AddUIComponent<UISprite>();
            label = panelTop.AddUIComponent<UILabel>();
            slider = panelTop.AddUIComponent<UISlider>();
        }

        public override void Start() {
            base.Start();

            thumbBackground.normalBgSprite = "WhiteRect";
            thumbBackground.relativePosition = new Vector2(0.0f, 0.0f);
            thumbBackground.size = new Vector2(66.0f, 66.0f);
            thumbBackground.color = UIColor;
            thumbBackground.focusedColor = UIColor;
            thumbBackground.hoveredColor = new Color32(20, 155, 215, 255);
            thumbBackground.pressedColor = new Color32(20, 155, 215, 255);
            thumbBackground.eventClicked += OnTextureClicked;
            thumbBackground.tooltip = Translation.Instance.GetTranslation(TranslationID.LABEL_SELECT);

            thumbMiddleground.backgroundSprite = "WhiteRect";
            thumbMiddleground.relativePosition = new Vector2(2.0f, 2.0f);
            thumbMiddleground.size = new Vector2(62.0f, 62.0f);
            thumbMiddleground.isInteractive = false;

            thumb.size = new Vector2(62.0f, 62.0f);
            thumb.atlas = ThemeSprites.Atlas;
            thumb.spriteName = UIUtils.GetTextureSpriteName(textureID);
            thumb.relativePosition = new Vector2(2.0f, 2.0f);
            thumb.isInteractive = false;

            label.relativePosition = new Vector2(71.0f, 0.0f);
            label.autoSize = false;
            label.autoHeight = true;
            label.size = new Vector2(219.0f, 32.0f);
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.TextureToTranslationID(textureID));

            slider.size = new Vector2(219.0f, 10.0f);
            color = new Color32(57, 67, 70, 255);
        }

        private void OnTextureClicked(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(Category, textureID);
        }
    }
}

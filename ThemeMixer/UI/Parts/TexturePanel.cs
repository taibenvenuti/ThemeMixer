using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    [UIProperties("Texture Panel Container", 300.0f, 0.0f, UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
    public class TexturePanel : PanelBase
    {
        public event LoadTextureClickedEventHandler EventLoadTextureClicked;
        public event TextureTilingchangedEventHandler EventTextureTilingChanged;

        public TextureID textureID;
        [UIProperties("Texture Panel", 290.0f, 66.0f)]
        protected PanelBase panelTop;
        protected UIPanel thumbBackground;
        protected UISprite thumb;
        protected UILabel label;
        protected UIButton button;
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
            thumbBackground = panelTop.AddUIComponent<UIPanel>();
            thumb = panelTop.AddUIComponent<UISprite>();
            label = panelTop.AddUIComponent<UILabel>();
            button = panelTop.AddUIComponent<UIButton>();
            slider = panelTop.AddUIComponent<UISlider>();
        }

        public override void Start() {
            base.Start();

            thumbBackground.backgroundSprite = "WhiteRect";
            thumbBackground.relativePosition = new Vector2(0.0f, 0.0f);
            thumbBackground.size = new Vector2(66.0f, 66.0f);

            thumb.size = new Vector2(64.0f, 64.0f);
            thumb.atlas = ThemeSprites.Atlas;
            thumb.spriteName = Controller.GetTextureSpriteName(textureID);
            thumb.relativePosition = new Vector2(1.0f, 1.0f);
            thumb.eventClicked += OnTextureClicked;
            thumb.tooltip = Translation.Instance.GetTranslation(TranslationID.LABEL_SELECT);

            label.relativePosition = new Vector2(71.0f, 0.0f);
            label.autoSize = false;
            label.autoHeight = true;
            label.size = new Vector2(219.0f, 32.0f);
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.TextureToTranslationID(textureID));

            slider.size = new Vector2(219.0f, 10.0f);
        }

        private void OnTextureClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventLoadTextureClicked?.Invoke(null, textureID);
        }
    }
}

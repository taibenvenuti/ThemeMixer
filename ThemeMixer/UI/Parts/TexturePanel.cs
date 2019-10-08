using ColossalFramework.UI;
using System;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    [UIProperties("Texture Panel Container", 350.0f, 0.0f, UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
    public class TexturePanel : PanelBase
    {
        public TextureID textureID;
        [UIProperties("Texture Panel", 340.0f, 66.0f)]
        protected PanelBase container;
        protected UIButton thumbBackground;
        protected UIPanel thumbMiddleground;
        protected UISprite thumb;
        protected UILabel label;
        [UIProperties("Texture Panel Space", 340.0f, 0.01f)]
        protected PanelBase panelBottom;
        protected UISlider slider;
        protected UITextField textfield;

        public override void Awake() {
            base.Awake();
            object[] attrs = GetType().GetCustomAttributes(typeof(UITextureIDAttribute), true);
            if (attrs != null && attrs.Length > 0 && attrs[0] is UITextureIDAttribute attr)
                textureID = attr.TextureID;

            CreateUIElements();
        }

        private void CreateUIElements() {
            container = AddUIComponent<PanelBase>();
            panelBottom = AddUIComponent<PanelBase>();
            thumbBackground = container.AddUIComponent<UIButton>();
            thumbMiddleground = container.AddUIComponent<UIPanel>();
            thumb = container.AddUIComponent<UISprite>();
            label = container.AddUIComponent<UILabel>();
            slider = UIUtils.CreateSlider(container, 165.0f, 1.0f, 2000.0f, 1.0f);
            textfield = container.AddUIComponent<UITextField>();
            color = UIColor;
        }

        public override void Start() {
            base.Start();
            SetupThumbnail();
            SetupLabel();
            SetupSlider();
            SetupSliderTextfield();
        }

        private void SetupSliderTextfield() {
            textfield.atlas = UISprites.DefaultAtlas;
            textfield.size = new Vector2(70.0f, 21.0f);
            textfield.padding = new RectOffset(2, 2, 4, 4);
            textfield.builtinKeyNavigation = true;
            textfield.isInteractive = true;
            textfield.readOnly = false;
            textfield.selectOnFocus = true;
            textfield.horizontalAlignment = UIHorizontalAlignment.Center;
            textfield.selectionSprite = "EmptySprite";
            textfield.selectionBackgroundColor = new Color32(0, 172, 234, 255);
            textfield.normalBgSprite = "TextFieldPanelHovered";
            textfield.textColor = new Color32(0, 0, 0, 255);
            textfield.textScale = 0.85f;
            textfield.color = new Color32(255, 255, 255, 255);
            textfield.relativePosition = new Vector2(261.0f, 35.0f);
            textfield.eventTextSubmitted += OnTextfieldTextSubmitted;
            textfield.eventKeyPress += OnTextfieldKeyPress;
            textfield.eventLostFocus += OnTextfieldLostFocus;
            textfield.text = string.Concat(Math.Round(ThemeUtils.GetTilingValue(textureID), 4, MidpointRounding.AwayFromZero));
            textfield.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_TILING);
        }

        private void OnTextfieldTextSubmitted(UIComponent component, string value) {
            if (float.TryParse(textfield.text.Replace(',', '.'), out float f)) {
                float finalValue = Mathf.Clamp(f, 0.0001f, 0.2f);
                textfield.text = finalValue.ToString();
                slider.value = finalValue * 10000.0f;
            }
        }

        private void OnTextfieldLostFocus(UIComponent component, UIFocusEventParameter eventParam) {
            OnTextfieldTextSubmitted(textfield, textfield.text);
        }

        private void OnTextfieldKeyPress(UIComponent component, UIKeyEventParameter eventParam) {
            char ch = eventParam.character;
            if (!char.IsControl(ch) && !char.IsDigit(ch) && (ch != '.' || (ch == '.' && textfield.text.Contains("."))) && (ch != ',' || (ch == ',' && textfield.text.Contains(",")))) {
                eventParam.Use();
            }
            if (eventParam.keycode == KeyCode.Escape) {
                textfield.Unfocus();
                eventParam.Use();
            }
        }

        private void SetupSlider() {
            slider.relativePosition = new Vector2(80.0f, 40.0f);
            slider.value = ThemeUtils.GetTilingValue(textureID) * 10000.0f;
            slider.eventValueChanged += OnSliderValueChanged;
            slider.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_TILING);
        }

        private void SetupLabel() {
            label.relativePosition = new Vector2(71.0f, 5.0f);
            label.autoSize = false;
            label.autoHeight = true;
            label.size = new Vector2(219.0f, 32.0f);
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.TextureToTranslationID(textureID));
        }

        private void SetupThumbnail() {
            thumbBackground.normalBgSprite = "WhiteRect";
            thumbBackground.relativePosition = new Vector2(0.0f, 0.0f);
            thumbBackground.size = new Vector2(66.0f, 66.0f);
            thumbBackground.color = UIColorLight;
            thumbBackground.focusedColor = UIColorLight;
            thumbBackground.hoveredColor = new Color32(20, 155, 215, 255);
            thumbBackground.pressedColor = new Color32(20, 155, 215, 255);
            thumbBackground.eventClicked += OnTextureClicked;
            string select = Translation.Instance.GetTranslation(TranslationID.LABEL_SELECT);
            string texture = Translation.Instance.GetTranslation(TranslationID.LABEL_TEXTURE);
            thumbBackground.tooltip = string.Concat(select, " ", texture);

            thumbMiddleground.backgroundSprite = "WhiteRect";
            thumbMiddleground.relativePosition = new Vector2(2.0f, 2.0f);
            thumbMiddleground.size = new Vector2(62.0f, 62.0f);
            thumbMiddleground.isInteractive = false;

            thumb.size = new Vector2(62.0f, 62.0f);
            thumb.atlas = ThemeSprites.Atlas;
            thumb.spriteName = UIUtils.GetTextureSpriteName(textureID);
            thumb.relativePosition = new Vector2(2.0f, 2.0f);
            thumb.isInteractive = false;
        }

        private void OnTextureClicked(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(Category, textureID);
        }

        private void OnSliderValueChanged(UIComponent component, float value) {
            float finalValue = value / 10000.0f;
            Controller.OnTilingChanged(textureID, finalValue);
            textfield.text = string.Concat(Math.Round(finalValue, 4, MidpointRounding.AwayFromZero));
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) {
            base.OnRefreshUI(sender, eventArgs);
            thumb.spriteName = UIUtils.GetTextureSpriteName(textureID);
            label.text = Translation.Instance.GetTranslation(TranslationID.TextureToTranslationID(textureID));
            float tiling = ThemeUtils.GetTilingValue(textureID);
            slider.eventValueChanged -= OnSliderValueChanged;
            slider.value = tiling * 10000.0f;
            slider.eventValueChanged += OnSliderValueChanged;
            textfield.text = string.Concat(Math.Round(tiling, 4, MidpointRounding.AwayFromZero));
        }
    }
}

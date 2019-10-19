using ColossalFramework.UI;
using System;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    public abstract class ValuePanel : PanelBase
    {
        public ValueID ValueID;
        protected PanelBase containerTitle;
        protected PanelBase containerSlider;
        protected UILabel labelTitle;
        protected UISlider slider;
        protected UITextField textfield;
        protected UIButton resetButton;
        protected float defaultValue;
        private bool ignoreEvents = false;

        public override void Awake() {
            base.Awake();
            Setup("Value Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect");
            CreateUIElements();
            CacheDefaultValue();
            SetupLabels();
            SetupButtons();
            SetupSlider();
            SetupTextfield();
        }

        private void CreateUIElements() {
            containerTitle = AddUIComponent<PanelBase>();
            containerTitle.size = new Vector2(340.0f, 22.0f);
            containerTitle.padding = new RectOffset(5, 0, 5, 0);
            containerSlider = AddUIComponent<PanelBase>();
            containerSlider.size = new Vector2(340.0f, 22.0f);
            containerSlider.padding = new RectOffset(5, 0, 5, 0);

            labelTitle = containerTitle.AddUIComponent<UILabel>();
            string resetTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_RESET);
            resetButton = UIUtils.CreateButton(containerTitle, new Vector2(22.0f, 22.0f), tooltip: resetTooltip, backgroundSprite: "", foregroundSprite: "UndoIcon", atlas: UISprites.Atlas);

            slider = UIUtils.CreateSlider(containerSlider, 240.0f, 0.0f, 1.0f, 1.0f);
            textfield = containerSlider.AddUIComponent<UITextField>();

            this.CreateSpace(0.0f, 0.01f);

            color = UIColorGrey;
        }

        private void OnResetClicked(UIComponent component, UIMouseEventParameter eventParam) {
            ignoreEvents = true;
            slider.value = defaultValue;
            textfield.text = defaultValue.ToString("0.####");
            ignoreEvents = false;
            SetValue(defaultValue);
        }

        private void OnLoadClicked(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(Category, ValueID);
        }

        private void SetupLabels() {
            string title = Translation.Instance.GetTranslation(TranslationID.ValueToTranslationID(ValueID));
            SetupLabel(labelTitle, title, new Vector2(0.0f, 0.0f), new Vector2(340.0f, 22.0f));
        }

        private void SetupLabel(UILabel label, string text, Vector2 position, Vector2 size) {
            label.autoSize = false;
            label.autoHeight = true;
            label.size = size;
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.text = text;
            label.relativePosition = position;
        }

        private void SetupButtons() {
            resetButton.eventClicked += OnResetClicked;
            resetButton.relativePosition = new Vector2(308.0f, 0.0f);
        }

        private void SetupSlider() {
            slider.eventValueChanged += OnSliderValueChanged;
            slider.relativePosition = new Vector2(5.0f, 6.0f);
            slider.tooltip = Translation.Instance.GetTranslation(TranslationID.GetValueTooltipID(ValueID));
            GetSliderMinMaxAndStep(out float min, out float max, out float step);
            slider.minValue = min;
            slider.maxValue = max;
            slider.stepSize = step;
            slider.scrollWheelAmount = step;
            slider.value = defaultValue;
        }

        private void SetupTextfield() {
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
            textfield.text = defaultValue.ToString("0.####");
            textfield.tooltip = Translation.Instance.GetTranslation(TranslationID.GetValueTooltipID(ValueID));
            textfield.eventTextSubmitted += OnTextfieldTextSubmitted;
            textfield.eventKeyPress += OnTextfieldKeyPress;
            textfield.eventLostFocus += OnTextfieldLostFocus;
            textfield.relativePosition = new Vector2(261.0f, 0.0f);
        }

        private void OnTextfieldTextSubmitted(UIComponent component, string value) {
            if (ignoreEvents) return;
            UITextField textfield = component as UITextField;
            if (float.TryParse(textfield.text.Replace(',', '.'), out float f)) {
                textfield.text = f.ToString("0.####");
                slider.value = f;
            }
        }

        private void OnTextfieldLostFocus(UIComponent component, UIFocusEventParameter eventParam) {
            if (ignoreEvents) return;
            UITextField textfield = component as UITextField;
            OnTextfieldTextSubmitted(component, textfield.text);
        }

        private void OnTextfieldKeyPress(UIComponent component, UIKeyEventParameter eventParam) {
            if (ignoreEvents) return;
            UITextField textfield = component as UITextField;
            char ch = eventParam.character;
            if (!char.IsControl(ch) && !char.IsDigit(ch) &&
                (ch != '.' || (ch == '.' && textfield.text.Contains(".") || !CanHaveDecimals())) &&
                (ch != ',' || (ch == ',' && textfield.text.Contains(",") || !CanHaveDecimals())) &&
                (ch != '-' || (ch == '-' && textfield.text.Contains("-") || !CanBeNegative()))) {
                eventParam.Use();
            }
            if (eventParam.keycode == KeyCode.Escape) {
                textfield.Unfocus();
                eventParam.Use();
            }
        }

        private void OnSliderValueChanged(UIComponent component, float value) {
            if (ignoreEvents) return;
            string valueString = value.ToString("0.####");
            textfield.text = valueString;
            SetValue(value);
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) {
            base.OnRefreshUI(sender, eventArgs);
            try {
                labelTitle.text = Translation.Instance.GetTranslation(TranslationID.ValueToTranslationID(ValueID));
                CacheDefaultValue();
                ignoreEvents = true;
                slider.value = (float)defaultValue;
                textfield.text = ((float)defaultValue).ToString("0.####");
                ignoreEvents = false;
            } catch (Exception) {
                Debug.LogError("Exception caught in TexturePanel.OnRefreshUI");
            }
        }
        private void CacheDefaultValue() {
            switch (ValueID) {
                case ValueID.Longitude:
                case ValueID.Latitude:
                case ValueID.SunSize:
                case ValueID.SunAnisotropy:
                case ValueID.MoonSize:
                case ValueID.Rayleigh:
                case ValueID.Mie:
                case ValueID.Exposure:
                case ValueID.StarsIntensity:
                case ValueID.OuterSpaceIntensity:
                case ValueID.MinTemperatureDay:
                case ValueID.MaxTemperatureDay:
                case ValueID.MinTemperatureNight:
                case ValueID.MaxTemperatureNight:
                case ValueID.MinTemperatureRain:
                case ValueID.MaxTemperatureRain:
                case ValueID.MinTemperatureFog:
                case ValueID.MaxTemperatureFog:
                    defaultValue = Controller.GetValue<float>(ValueID);
                    break;
                case ValueID.RainProbabilityDay:
                case ValueID.RainProbabilityNight:
                case ValueID.FogProbabilityDay:
                case ValueID.FogProbabilityNight:
                case ValueID.NorthernLightsProbability:
                    defaultValue = Controller.GetValue<int>(ValueID);
                    break;
            }
        }

        private void SetValue(float value) {
            switch (ValueID) {
                case ValueID.Longitude:
                case ValueID.Latitude:
                case ValueID.SunSize:
                case ValueID.SunAnisotropy:
                case ValueID.MoonSize:
                case ValueID.Rayleigh:
                case ValueID.Mie:
                case ValueID.Exposure:
                case ValueID.StarsIntensity:
                case ValueID.OuterSpaceIntensity:
                case ValueID.MinTemperatureDay:
                case ValueID.MaxTemperatureDay:
                case ValueID.MinTemperatureNight:
                case ValueID.MaxTemperatureNight:
                case ValueID.MinTemperatureRain:
                case ValueID.MaxTemperatureRain:
                case ValueID.MinTemperatureFog:
                case ValueID.MaxTemperatureFog:
                    Controller.OnValueChanged(ValueID, value);
                    break;
                case ValueID.RainProbabilityDay:
                case ValueID.RainProbabilityNight:
                case ValueID.FogProbabilityDay:
                case ValueID.FogProbabilityNight:
                case ValueID.NorthernLightsProbability:
                    Controller.OnValueChanged(ValueID, (int)value);
                    break;
            }
        }

        private bool CanBeNegative() {
            switch (ValueID) {
                case ValueID.Longitude:
                case ValueID.Latitude:
                case ValueID.MinTemperatureDay:
                case ValueID.MaxTemperatureDay:
                case ValueID.MinTemperatureNight:
                case ValueID.MaxTemperatureNight:
                case ValueID.MinTemperatureRain:
                case ValueID.MaxTemperatureRain:
                case ValueID.MinTemperatureFog:
                case ValueID.MaxTemperatureFog:
                    return true;
                default: return false;
            }
        }

        private bool CanHaveDecimals() {
            switch (ValueID) {
                case ValueID.Longitude:
                case ValueID.Latitude:
                case ValueID.SunSize:
                case ValueID.SunAnisotropy:
                case ValueID.MoonSize:
                case ValueID.Rayleigh:
                case ValueID.Mie:
                case ValueID.Exposure:
                case ValueID.StarsIntensity:
                case ValueID.OuterSpaceIntensity:
                case ValueID.MinTemperatureDay:
                case ValueID.MaxTemperatureDay:
                case ValueID.MinTemperatureNight:
                case ValueID.MaxTemperatureNight:
                case ValueID.MinTemperatureRain:
                case ValueID.MaxTemperatureRain:
                case ValueID.MinTemperatureFog:
                case ValueID.MaxTemperatureFog:
                    return true;
                case ValueID.RainProbabilityDay:
                case ValueID.RainProbabilityNight:
                case ValueID.FogProbabilityDay:
                case ValueID.FogProbabilityNight:
                case ValueID.NorthernLightsProbability:
                    return false;
            }
            return false;
        }

        private void GetSliderMinMaxAndStep(out float minValue, out float maxValue, out float step) {
            minValue = 0.0f;
            maxValue = 1.0f;
            step = 1.0f;
            switch (ValueID) {
                case ValueID.Longitude:
                    minValue = -180.0f;
                    maxValue = 180.0f;
                    step = 1.0f;
                    break;
                case ValueID.Latitude:
                    minValue = -80.0f;
                    maxValue = 80.0f;
                    step = 1.0f;
                    break;
                case ValueID.SunSize:
                    minValue = 0.001f;
                    maxValue = 10.0f;
                    step = 0.001f;
                    break;
                case ValueID.SunAnisotropy:
                case ValueID.MoonSize:
                    minValue = 0.0f;
                    maxValue = 1.0f;
                    step = 0.001f;
                    break;
                case ValueID.Rayleigh:
                case ValueID.Mie:
                case ValueID.StarsIntensity:
                case ValueID.Exposure:
                case ValueID.OuterSpaceIntensity:
                    minValue = 0.0f;
                    maxValue = 5.0f;
                    step = 0.001f;
                    break;
                case ValueID.MinTemperatureDay:
                case ValueID.MaxTemperatureDay:
                case ValueID.MinTemperatureNight:
                case ValueID.MaxTemperatureNight:
                case ValueID.MinTemperatureRain:
                case ValueID.MaxTemperatureRain:
                case ValueID.MinTemperatureFog:
                case ValueID.MaxTemperatureFog:
                    minValue = -50.0f;
                    maxValue = 50.0f;
                    step = 0.1f;
                    break;
                case ValueID.RainProbabilityDay:
                case ValueID.RainProbabilityNight:
                case ValueID.FogProbabilityDay:
                case ValueID.FogProbabilityNight:
                case ValueID.NorthernLightsProbability:
                    minValue = 0.0f;
                    maxValue = 100.0f;
                    step = 1.0f;
                    break;
            }
        }
    }
}

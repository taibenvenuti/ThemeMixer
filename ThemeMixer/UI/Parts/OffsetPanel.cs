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
    [UICategory(ThemeCategory.Terrain)]
    [UIProperties("Offset Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect")]
    public class OffsetPanel : PanelBase
    {
        public OffsetID offsetID;
        [UIProperties("Offset Panel Title Container", 340.0f, 32.0f, 5)]
        protected PanelBase containerTitle;
        [UIProperties("Offset Panel Red Container", 340.0f, 22.0f, 5)]
        protected PanelBase containerR;
        [UIProperties("Offset Panel Green Container", 340.0f, 22.0f, 5)]
        protected PanelBase containerG;
        [UIProperties("Offset Panel Blue Container", 340.0f, 22.0f, 5)]
        protected PanelBase containerB;
        protected UILabel labelTitle;
        protected UILabel labelR;
        protected UILabel labelG;
        protected UILabel labelB;
        protected UISlider sliderR;
        protected UISlider sliderG;
        protected UISlider sliderB;
        protected UITextField textfieldR;
        protected UITextField textfieldG;
        protected UITextField textfieldB;

        public override void Awake() {
            base.Awake();
            CreateUIElements();
        }

        private void CreateUIElements() {
            containerTitle = AddUIComponent<PanelBase>();
            labelTitle = containerTitle.AddUIComponent<UILabel>();

            containerR = AddUIComponent<PanelBase>();
            labelR = containerR.AddUIComponent<UILabel>();
            sliderR = UIUtils.CreateSlider(containerR, 218.0f, -1.0f, 1.0f, 0.0001f);
            textfieldR = containerR.AddUIComponent<UITextField>();

            containerG = AddUIComponent<PanelBase>();
            labelG = containerG.AddUIComponent<UILabel>();
            sliderG = UIUtils.CreateSlider(containerG, 218.0f, -1.0f, 1.0f, 0.0001f);
            textfieldG = containerG.AddUIComponent<UITextField>();

            containerB = AddUIComponent<PanelBase>();
            labelB = containerB.AddUIComponent<UILabel>();
            sliderB = UIUtils.CreateSlider(containerB, 218.0f, -1.0f, 1.0f, 0.0001f);
            textfieldB = containerB.AddUIComponent<UITextField>();

            this.CreateSpace(0.0f, 5.0f);

            color = UIColor;
        }

        public override void Start() {
            base.Start();
            SetupLabels();
            SetupSliders();
            SetupTextfields();
        }

        private void SetupLabels() {
            string title = Translation.Instance.GetTranslation(TranslationID.OffsetToTranslationID(offsetID));
            SetupLabel(labelTitle, title, new Vector2(0.0f, 5.0f), new Vector2(340.0f, 22.0f));
            SetupLabel(labelR, "R", new Vector2(0.0f, 0.0f), new Vector2(22.0f, 22.0f));
            SetupLabel(labelG, "G", new Vector2(0.0f, 0.0f), new Vector2(22.0f, 22.0f));
            SetupLabel(labelB, "B", new Vector2(0.0f, 0.0f), new Vector2(22.0f, 22.0f));

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

        private void SetupSliders() {
            SetupSlider(sliderR, new Vector2(27.0f, 6.0f));
            SetupSlider(sliderG, new Vector2(27.0f, 6.0f));
            SetupSlider(sliderB, new Vector2(27.0f, 6.0f));
        }

        private void SetupSlider(UISlider slider, Vector2 position) {
            Vector3 offset = ThemeUtils.GetOffsetValue(offsetID);
            float value = ReferenceEquals(slider, sliderR) ? offset.x : ReferenceEquals(slider, sliderG) ? offset.y : offset.z;
            slider.value = value;
            slider.eventValueChanged += OnSliderValueChanged;
            slider.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_OFFSET);
            slider.relativePosition = position;
        }

        private void SetupTextfields() {
            SetupTextfield(textfieldR, new Vector2(261.0f, 0.0f));
            SetupTextfield(textfieldG, new Vector2(261.0f, 0.0f));
            SetupTextfield(textfieldB, new Vector2(261.0f, 0.0f));
        }

        private void SetupTextfield(UITextField textfield, Vector2 position) {
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
            Vector3 offset = ThemeUtils.GetOffsetValue(offsetID);
            float value = ReferenceEquals(textfield, textfieldR) ? offset.x : ReferenceEquals(textfield, textfieldG) ? offset.y : offset.z; 
            textfield.text = value.ToString("F4");
            textfield.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_OFFSET);
            textfield.eventTextSubmitted += OnTextfieldTextSubmitted;
            textfield.eventKeyPress += OnTextfieldKeyPress;
            textfield.eventLostFocus += OnTextfieldLostFocus;
            textfield.relativePosition = position;
        }

        private void OnTextfieldTextSubmitted(UIComponent component, string value) {
            UITextField textfield = component as UITextField;
            if (float.TryParse(textfield.text.Replace(',', '.'), out float f)) {
                float finalValue = Mathf.Clamp(f, -1.0f, 1.0f);
                textfield.text = finalValue.ToString("F4");
                UISlider slider = ReferenceEquals(textfield, textfieldR) ? sliderR : ReferenceEquals(textfield, textfieldG) ? sliderG : sliderB;
                slider.value = finalValue;
            }
        }

        private void OnTextfieldLostFocus(UIComponent component, UIFocusEventParameter eventParam) {
            UITextField textfield = component as UITextField;
            OnTextfieldTextSubmitted(component, textfield.text);
        }

        private void OnTextfieldKeyPress(UIComponent component, UIKeyEventParameter eventParam) {
            UITextField textfield = component as UITextField;
            char ch = eventParam.character;
            if (!char.IsControl(ch) && !char.IsDigit(ch) && 
                (ch != '.' || (ch == '.' && textfield.text.Contains("."))) && 
                (ch != ',' || (ch == ',' && textfield.text.Contains(","))) && 
                (ch != '-' || (ch == '-' && textfield.text.Contains("-")))) {
                eventParam.Use();
            }
            if (eventParam.keycode == KeyCode.Escape) {
                textfield.Unfocus();
                eventParam.Use();
            }
        }

        private void OnSliderValueChanged(UIComponent component, float value) {
            float rValue = sliderR.value;
            float gValue = sliderG.value;
            float bValue = sliderB.value;

            UISlider slider = component as UISlider;
            float finalValue = value;
            string valueString = finalValue.ToString("F4");

            if (ReferenceEquals(slider, sliderR)) {
                textfieldR.text = valueString;
                Controller.OnOffsetChanged(offsetID, new Vector3(finalValue, gValue, bValue));
            }
            if (ReferenceEquals(slider, sliderG)) {
                textfieldG.text = valueString;
                Controller.OnOffsetChanged(offsetID, new Vector3(rValue, finalValue, bValue));
            }
            if (ReferenceEquals(slider, sliderB)) {
                textfieldB.text = valueString;
                Controller.OnOffsetChanged(offsetID, new Vector3(rValue, gValue, finalValue));
            }
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) {
            base.OnRefreshUI(sender, eventArgs);
            try {
                labelTitle.text = Translation.Instance.GetTranslation(TranslationID.OffsetToTranslationID(offsetID));
                Vector3 offset = ThemeUtils.GetOffsetValue(offsetID);
                sliderR.eventValueChanged -= OnSliderValueChanged;
                sliderG.eventValueChanged -= OnSliderValueChanged;
                sliderB.eventValueChanged -= OnSliderValueChanged;

                sliderR.value = offset.x;
                sliderG.value = offset.y;
                sliderB.value = offset.z;

                sliderR.eventValueChanged += OnSliderValueChanged;
                sliderG.eventValueChanged += OnSliderValueChanged;
                sliderB.eventValueChanged += OnSliderValueChanged;

                textfieldR.text = offset.x.ToString("F4");
                textfieldG.text = offset.y.ToString("F4");
                textfieldB.text = offset.z.ToString("F4");
            } catch (Exception) {
                Debug.LogWarning("Exception caught in TexturePanel.OnRefreshUI");
            }
        }
    }
}

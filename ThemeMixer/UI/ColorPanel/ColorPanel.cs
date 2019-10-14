using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Color
{
    public class ColorPanel : PanelBase
    {
        private UIColorPicker colorPicker;

        [UIProperties("RGB Panel", 0.0f, 35.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        private PanelBase rgbPanel;

        [UIProperties("Buttons Panel", 0.0f, 34.0f, 10, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        private PanelBase buttonsPanel;

        [UIProperties("Reset Button", 0.0f, 40.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        private ButtonPanel resetButton;

        [UIProperties("Save Button", 0.0f, 40.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        private ButtonPanel saveButton;

        private UIPanel colorPanel;
        private UITextField redTextField;
        private UITextField greenTextField;
        private UITextField blueTextField;
        private PanelBase savedSwatchesPanel;
        private Color32 currentColor;
        private bool updateNeeded;
        private List<SavedSwatch> savedSwatches;
        private const int MAX_SAVED_SWATCHES = 10;

        public ColorID ColorID;
        private Color32 defaultValue;

        public override void Awake() {
            base.Awake();
            savedSwatches = Data.GetSavedSwatches();
            if (Mod.InGame) {
                UIColorField field = UITemplateManager.Get<UIPanel>("LineTemplate").Find<UIColorField>("LineColor");
                field = Instantiate(field);
                colorPicker = Instantiate(field.colorPicker);
            }
            rgbPanel = AddUIComponent<PanelBase>();
            buttonsPanel = AddUIComponent<PanelBase>();
            resetButton = buttonsPanel.AddUIComponent<ButtonPanel>();
            saveButton = buttonsPanel.AddUIComponent<ButtonPanel>();


            RefreshSavedSwatchesPanel();
            this.CreateSpace(255.0f, 11.0f);
            RefreshColors();
            color = UIColor;
            padding = new RectOffset(1, 0, 0, 0);
            autoFitChildrenHorizontally = true;
        }

        public override void Start() {
            base.Start();
            defaultValue = Controller.GetCurrentColor(ColorID);
            if (Mod.InGame) SetupColorPicker();
            SetupRGBPanel();
            SetupButtonsPanel();
        }

        public override void OnDestroy() {
            redTextField.eventGotFocus -= OnGotFocus;
            redTextField.eventKeyPress -= OnKeyPress;
            redTextField.eventTextChanged -= OnTextChanged;
            redTextField.eventTextSubmitted -= OnTextSubmitted;
            greenTextField.eventGotFocus -= OnGotFocus;
            greenTextField.eventKeyPress -= OnKeyPress;
            greenTextField.eventTextChanged -= OnTextChanged;
            greenTextField.eventTextSubmitted -= OnTextSubmitted;
            blueTextField.eventGotFocus -= OnGotFocus;
            blueTextField.eventKeyPress -= OnKeyPress;
            blueTextField.eventTextChanged -= OnTextChanged;
            blueTextField.eventTextSubmitted -= OnTextSubmitted;
            if (colorPicker != null) colorPicker.eventColorUpdated -= OnColorUpdated;
            base.OnDestroy();
        }

        public override void Update() {
            base.Update();
            if (updateNeeded && Input.GetMouseButtonUp(0)) {
                ColorChanged();
                updateNeeded = false;
            }
            if (savedSwatches.Count == MAX_SAVED_SWATCHES || savedSwatches.Find(s => s.Color == Controller.GetCurrentColor(ColorID)) != null) {
                if (saveButton.isEnabled) saveButton.Disable();
                if (savedSwatches.Count == MAX_SAVED_SWATCHES) {
                    saveButton.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_BUTTON_SAVE_MAXREACHED);
                } else if (savedSwatches.Find(s => s.Color == Controller.GetCurrentColor(ColorID)) != null) {
                    saveButton.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_BUTTON_SAVE_COLOREXISTS);
                }
            } else if (savedSwatches.Count < MAX_SAVED_SWATCHES) {
                if (!saveButton.isEnabled) saveButton.Enable();
                saveButton.tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_BUTTON_SAVE);
            }
        }

        private void SetupColorPicker() {
            colorPicker.eventColorUpdated += OnColorUpdated;
            colorPicker.color = Controller.GetCurrentColor(ColorID);
            colorPicker.component.color = UIColor;
            UIPanel pickerPanel = colorPicker.component as UIPanel;
            pickerPanel.backgroundSprite = "";
            colorPicker.component.size = new Vector2(254f, 217f);
            AttachUIComponent(colorPicker.gameObject);
        }

        private void SetupRGBPanel() {
            rgbPanel.padding = new RectOffset(10, 0, 5, 0);
            colorPanel = rgbPanel.AddUIComponent<UIPanel>();
            colorPanel.backgroundSprite = "WhiteRect";
            colorPanel.size = new Vector2(28.0f, 25.0f);
            colorPanel.color = Controller.GetCurrentColor(ColorID);
            colorPanel.atlas = UISprites.DefaultAtlas;

            Color32 color32 = colorPanel.color;
            CreateLabel(Translation.Instance.GetTranslation(TranslationID.LABEL_RED));
            redTextField = CreateTextfield(color32.r.ToString());
            CreateLabel(Translation.Instance.GetTranslation(TranslationID.LABEL_GREEN));
            greenTextField = CreateTextfield(color32.g.ToString());
            CreateLabel(Translation.Instance.GetTranslation(TranslationID.LABEL_BLUE));
            blueTextField = CreateTextfield(color32.b.ToString());
        }

        private UILabel CreateLabel(string text) {
            UILabel label = rgbPanel.AddUIComponent<UILabel>();
            label.font = UIUtils.Font;
            label.text = text;
            label.autoSize = false;
            label.autoHeight = false;
            label.size = new Vector2(15.0f, 25.0f);
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.textAlignment = UIHorizontalAlignment.Right;
            label.padding = new RectOffset(0, 0, 5, 0);
            label.atlas = UISprites.DefaultAtlas;
            return label;
        }

        private UITextField CreateTextfield(string text) {
            UITextField textField = rgbPanel.AddUIComponent<UITextField>();
            textField.size = new Vector2(44.0f, 25.0f);
            textField.padding = new RectOffset(6, 6, 6, 6);
            textField.builtinKeyNavigation = true;
            textField.isInteractive = true;
            textField.readOnly = false;
            textField.horizontalAlignment = UIHorizontalAlignment.Center;
            textField.selectionSprite = "EmptySprite";
            textField.selectionBackgroundColor = new Color32(0, 172, 234, 255);
            textField.normalBgSprite = "TextFieldPanelHovered";
            textField.disabledBgSprite = "TextFieldPanelHovered";
            textField.textColor = new Color32(0, 0, 0, 255);
            textField.disabledTextColor = new Color32(80, 80, 80, 128);
            textField.color = new Color32(255, 255, 255, 255);
            textField.eventGotFocus += OnGotFocus;
            textField.eventKeyPress += OnKeyPress;
            textField.eventTextChanged += OnTextChanged;
            textField.eventTextSubmitted += OnTextSubmitted;
            textField.text = text;
            textField.atlas = UISprites.DefaultAtlas;
            return textField;
        }

        private void SetupButtonsPanel() {
            buttonsPanel.padding = new RectOffset(10, 0, 5, 0);
            SetupResetButton();
            SetupSaveButton();
        }

        private void SetupResetButton() {
            resetButton.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            resetButton.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_RESET));
            resetButton.EventButtonClicked += OnResetClicked;
        }

        private void SetupSaveButton() {
            saveButton.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            saveButton.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_SAVE));
            saveButton.EventButtonClicked += OnSaveClicked;
            if (savedSwatches.Count == MAX_SAVED_SWATCHES) saveButton.Disable();
        }

        private void RefreshSavedSwatchesPanel() {
            if (savedSwatchesPanel != null) Destroy(savedSwatchesPanel.gameObject);
            savedSwatchesPanel = AddUIComponent<PanelBase>();
            savedSwatchesPanel.Setup("Saved Swatches Panel", new Vector2(256.0f, 0.0f), 0,  true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            savedSwatchesPanel.padding = new RectOffset(5, 0, 5, 0);
            foreach (var savedSwatch in savedSwatches) {
                AddSavedSwatch(savedSwatch);
            }
        }

        private void AddSavedSwatch(SavedSwatch savedSwatch) {
            SavedSwatchPanel savedSwatchPanel = savedSwatchesPanel.AddUIComponent<SavedSwatchPanel>();
            savedSwatchPanel.Setup("Saved Swatch", new Vector2(256.0f, 24.0f), 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft, ColorID);
            savedSwatchPanel.savedSwatch = savedSwatch;
            savedSwatchPanel.autoLayoutPadding = new RectOffset(5, 0, 5, 0);
            savedSwatchPanel.EventSwatchClicked += OnSavedSwatchClicked;
            savedSwatchPanel.EventRemoveSwatch += OnSavedSwatchRemoved;
            savedSwatchPanel.EventSwatchRenamed += OnSavedSwatchRenamed;
        }

        private void OnSavedSwatchRenamed(SavedSwatch savedSwatch) {
            //
        }

        private void OnSavedSwatchRemoved(SavedSwatchPanel savedSwatchPanel) {
            if (savedSwatchPanel != null) {
                savedSwatches.Remove(savedSwatchPanel.savedSwatch);
                Data.UpdateSavedSwatches(savedSwatches);
                Destroy(savedSwatchPanel.gameObject);
            }
        }

        private void OnSavedSwatchClicked(Color32 color) {
            UpdateColor(color);
        }

        private void OnSaveClicked() {
            if (savedSwatches.Find(s => s.Color.r == currentColor.r && s.Color.g == currentColor.g && s.Color.b == currentColor.b) == null) {
                SavedSwatch newSwatch = new SavedSwatch() { Name = Translation.Instance.GetTranslation(TranslationID.LABEL_NEW_SWATCH), Color = currentColor };
                AddSavedSwatch(newSwatch);
                savedSwatches.Add(newSwatch);
                Data.UpdateSavedSwatches(savedSwatches);
            }
        }

        private void OnResetClicked() {
            Controller.OnColorChanged(ColorID, defaultValue);
            RefreshColors();
        }

        private void OnGotFocus(UIComponent component, UIFocusEventParameter eventParam) {
            UITextField textField = component as UITextField;
            textField.SelectAll();
        }

        private void OnTextChanged(UIComponent component, string value) {
            UITextField textField = component as UITextField;
            textField.eventTextChanged -= OnTextChanged;
            textField.text = GetClampedString(value);
            textField.eventTextChanged += OnTextChanged;
        }

        private string GetClampedString(string value) {
            return value == "" ? value : GetClampedFloat(value).ToString("F0");
        }

        private float GetClampedFloat(string value) {
            if (!float.TryParse(value, out float number)) {
                return 0.0f;
            }
            return Mathf.Clamp(number, 0, 255);
        }

        private void OnKeyPress(UIComponent component, UIKeyEventParameter parameter) {
            char ch = parameter.character;
            if (!char.IsControl(ch) && !char.IsDigit(ch)) {
                parameter.Use();
            }
        }

        private void OnTextSubmitted(UIComponent component, string value) {
            UITextField textField = component as UITextField;
            Color32 color = currentColor;
            if (textField == redTextField) {
                color = new Color32((byte)GetClampedFloat(value), color.g, color.b, 255);
            } else if (textField == greenTextField) {
                color = new Color32(color.r, (byte)GetClampedFloat(value), color.b, 255);
            } else if (textField == blueTextField) {
                color = new Color32(color.r, color.g, (byte)GetClampedFloat(value), 255);
            }

            UpdateColor(color);
        }

        private void ColorChanged() {
            Controller.OnColorChanged(ColorID, currentColor);
        }

        private void UpdateColor(UnityEngine.Color value) {
            if (colorPicker != null) {
                colorPicker.eventColorUpdated -= OnColorUpdated;
                colorPicker.color = value;
                colorPicker.eventColorUpdated += OnColorUpdated;
            }
            OnColorUpdated(value);
        }

        private void OnColorUpdated(UnityEngine.Color value) {
            currentColor = value;
            if (colorPanel != null) colorPanel.color = value;
            UpdateTextfields();
            updateNeeded = true;
        }

        private void UpdateTextfields() {
            if (redTextField != null) {
                redTextField.eventTextChanged -= OnTextChanged;
                redTextField.text = currentColor.r.ToString();
                redTextField.eventTextChanged += OnTextChanged;
            }
            if (greenTextField != null) {
                greenTextField.eventTextChanged -= OnTextChanged;
                greenTextField.text = currentColor.g.ToString();
                greenTextField.eventTextChanged += OnTextChanged;
            }
            if (blueTextField != null) {
                blueTextField.eventTextChanged -= OnTextChanged;
                blueTextField.text = currentColor.b.ToString();
                blueTextField.eventTextChanged += OnTextChanged;
            }
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) {
            base.OnRefreshUI(sender, eventArgs);
            RefreshColors();
        }

        private void RefreshColors() {
            currentColor = colorPanel.color = Controller.GetCurrentColor(ColorID);
            if (colorPicker != null) {
                colorPicker.eventColorUpdated -= OnColorUpdated;
                colorPicker.color = Controller.GetCurrentColor(ColorID);
                colorPicker.eventColorUpdated += OnColorUpdated;
            }
            UpdateTextfields();
        }
    }
}

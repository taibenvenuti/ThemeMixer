using ColossalFramework.UI;
using System;
using System.Collections.Generic;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Color
{
    public abstract class ColorPanel : PanelBase
    {
        public event EventHandler<ColorPanelVisibilityChangedEventArgs> EventVisibilityChanged;

        private UIPanel topPanel;
        private UIButton colorButton;
        private UILabel colorLabel;
        private UIButton loadButton;
        private UIButton resetButton;

        private UIColorPicker colorPicker;

        private PanelBase rgbPanel;
        private PanelBase buttonsPanel;
        private ButtonPanel closeButton;
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
        private bool visible = false;
        private bool ignoreEvents = false;

        public override void Awake() {
            base.Awake();
            Setup("Color Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect");
            CreateUIElements();
            defaultValue = Controller.GetCurrentColor(ColorID);
            SetupTopPanel();
            if (Mod.InGame) SetupColorPicker();
            SetupRGBPanel();
            SetupButtonsPanel();
            RefreshColors();
            OnCloseClicked();
            color = UIColorGrey;
        }

        private void CreateUIElements() {
            topPanel = AddUIComponent<UIPanel>();
            string colorButtonTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_OPENCOLORPICKER);
            colorButton = UIUtils.CreateButton(topPanel, new Vector2(22.0f, 22.0f), tooltip: colorButtonTooltip, backgroundSprite: "WhiteRect", atlas: UISprites.DefaultAtlas);
            colorLabel = topPanel.AddUIComponent<UILabel>();
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(topPanel, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            string resetTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_RESET);
            resetButton = UIUtils.CreateButton(topPanel, new Vector2(22.0f, 22.0f), tooltip: resetTooltip, backgroundSprite: "", foregroundSprite: "UndoIcon", atlas: UISprites.Atlas);
            savedSwatches = Data.GetSavedSwatches(ColorID);
            if (Mod.InGame) {
                UIColorField field = UITemplateManager.Get<UIPanel>("LineTemplate").Find<UIColorField>("LineColor");
                field = Instantiate(field);
                colorPicker = Instantiate(field.colorPicker);
                AttachUIComponent(colorPicker.gameObject);
                UITextureSprite hsb = colorPicker.component.Find<UITextureSprite>("HSBField");
                UISlider hue = colorPicker.component.Find<UISlider>("HueSlider");
                hsb.relativePosition = new Vector3(55.0f, 7.0f);
                hue.relativePosition = new Vector3(267.0f, 7.0f);
            }
            rgbPanel = AddUIComponent<PanelBase>();
            buttonsPanel = AddUIComponent<PanelBase>();
            closeButton = buttonsPanel.AddUIComponent<ButtonPanel>();
            saveButton = buttonsPanel.AddUIComponent<ButtonPanel>();

            RefreshSavedSwatchesPanel();
            this.CreateSpace(1.0f, 0.1f);

            color = UIColor;
            autoFitChildrenHorizontally = true;
        }

        private void RefreshSavedSwatchesPanel() {
            if (savedSwatchesPanel != null) Destroy(savedSwatchesPanel.gameObject);
            savedSwatchesPanel = AddUIComponent<PanelBase>();
            savedSwatchesPanel.Setup("Saved Swatches Panel", 256.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            savedSwatchesPanel.padding = new RectOffset(0, 0, 0, 5);
            foreach (var savedSwatch in savedSwatches) AddSavedSwatch(savedSwatch);
            savedSwatchesPanel.isVisible = savedSwatches.Count != 0;
        }

        private void SetupTopPanel() {
            topPanel.size = new Vector2(345, 22.0f);
            colorButton.relativePosition = new Vector3(0.0f, 0.0f);
            colorButton.eventClicked += OnColorButtonClicked; 
            colorLabel.height = 22.0f;
            colorLabel.font = UIUtils.Font;
            colorLabel.textScale = 1.0f;
            colorLabel.padding = new RectOffset(4, 0, 4, 0);
            colorLabel.text = UIUtils.GetColorName(ColorID);
            colorLabel.relativePosition = new Vector3(32.0f, 0.0f);
            loadButton.relativePosition = new Vector3(291.0f, 0.0f);
            loadButton.eventClicked += OnLoadClicked;
            resetButton.relativePosition = new Vector3(318.0f, 0.0f);
            resetButton.eventClicked += OnResetClicked;
        }

        private void OnLoadClicked(UIComponent component, UIMouseEventParameter eventParam) {
            if (ignoreEvents) return;
            Controller.OnLoadFromTheme(Category, ColorID);
        }

        private void OnResetClicked(UIComponent component, UIMouseEventParameter eventParam) {
            if (ignoreEvents) return;
            Controller.OnColorChanged(ColorID, defaultValue);
            RefreshColors();
        }

        private void OnColorButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            if (ignoreEvents) return;
            visible = !visible;
            if (colorPicker != null) colorPicker.component.isVisible = visible;
            rgbPanel.isVisible = visible;
            buttonsPanel.isVisible = visible;
            savedSwatchesPanel.isVisible = visible;
            EventVisibilityChanged?.Invoke(this, new ColorPanelVisibilityChangedEventArgs(this, visible));
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
            pickerPanel.size = new Vector2(340f, 212f);
        }

        private void SetupRGBPanel() {
            rgbPanel.Setup("RGB Panel", 0.0f, 25.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            rgbPanel.padding = new RectOffset(55, 0, 0, 5);
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
            buttonsPanel.Setup("Buttons Panel", 0.0f, 30.0f, 10, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            buttonsPanel.padding = new RectOffset(55, 0, 0, 0);
            SetupCloseButton();
            SetupSaveButton();
        }

        private void SetupCloseButton() {
            closeButton.Setup("Close Button", 0.0f, 30.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            closeButton.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            closeButton.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_CLOSE));
            closeButton.EventButtonClicked += OnCloseClicked;
        }

        private void SetupSaveButton() {
            saveButton.Setup("Save Button", 0.0f, 30.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            saveButton.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            saveButton.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_SAVE));
            saveButton.EventButtonClicked += OnSaveClicked;
            if (savedSwatches.Count == MAX_SAVED_SWATCHES) saveButton.Disable();
        }

        private void AddSavedSwatch(SavedSwatch savedSwatch) {
            SavedSwatchPanel savedSwatchPanel = savedSwatchesPanel.AddUIComponent<SavedSwatchPanel>();
            savedSwatchPanel.Setup("Saved Swatch", 256.0f, 24.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft, ColorID, savedSwatch);
            savedSwatchPanel.savedSwatch = savedSwatch;
            savedSwatchPanel.autoLayoutPadding = new RectOffset(0, 5, 5, 5);
            savedSwatchPanel.EventSwatchClicked += OnSavedSwatchClicked;
            savedSwatchPanel.EventRemoveSwatch += OnSavedSwatchRemoved;
            savedSwatchPanel.EventSwatchRenamed += OnSavedSwatchRenamed;
        }

        private void OnSavedSwatchRenamed(SavedSwatch savedSwatch) {
            if (ignoreEvents) return;
            //
        }

        private void OnSavedSwatchRemoved(SavedSwatchPanel savedSwatchPanel) {
            if (ignoreEvents) return;
            if (savedSwatchPanel != null) {
                savedSwatches.Remove(savedSwatchPanel.savedSwatch);
                Data.UpdateSavedSwatches(savedSwatches, ColorID);
                Destroy(savedSwatchPanel.gameObject);
            }
        }

        private void OnSavedSwatchClicked(Color32 color) {
            UpdateColor(color);
        }

        private void OnSaveClicked() {
            if (ignoreEvents) return;
            if (savedSwatches.Find(s => s.Color.r == currentColor.r && s.Color.g == currentColor.g && s.Color.b == currentColor.b) == null) {
                SavedSwatch newSwatch = new SavedSwatch() { Name = Translation.Instance.GetTranslation(TranslationID.LABEL_NEW_SWATCH), Color = currentColor };
                AddSavedSwatch(newSwatch);
                savedSwatches.Add(newSwatch);
                Data.UpdateSavedSwatches(savedSwatches, ColorID);
            }
        }

        private void OnCloseClicked() {
            if (ignoreEvents) return;
            visible = false;
            if (colorPicker != null) colorPicker.component.isVisible = visible;
            rgbPanel.isVisible = visible;
            buttonsPanel.isVisible = visible;
            savedSwatchesPanel.isVisible = visible;
            EventVisibilityChanged?.Invoke(this, new ColorPanelVisibilityChangedEventArgs(this, visible));
        }

        private void OnGotFocus(UIComponent component, UIFocusEventParameter eventParam) {
            if (ignoreEvents) return;
            UITextField textField = component as UITextField;
            textField.SelectAll();
        }

        private void OnTextChanged(UIComponent component, string value) {
            if (ignoreEvents) return;
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
            if (ignoreEvents) return;
            char ch = parameter.character;
            if (!char.IsControl(ch) && !char.IsDigit(ch)) {
                parameter.Use();
            }
        }

        private void OnTextSubmitted(UIComponent component, string value) {
            if (ignoreEvents) return;
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
            if (ignoreEvents) return;
            currentColor = value;
            if (colorPanel != null) colorPanel.color = value;
            colorButton.color = colorButton.hoveredColor = colorButton.pressedColor = colorButton.focusedColor = value;
            UpdateTextfields();
            updateNeeded = true;
        }

        private void UpdateTextfields() {
            if (redTextField != null) {
                redTextField.text = currentColor.r.ToString();
            }
            if (greenTextField != null) {
                greenTextField.text = currentColor.g.ToString();
            }
            if (blueTextField != null) {
                blueTextField.text = currentColor.b.ToString();
            }
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs eventArgs) {
            base.OnRefreshUI(sender, eventArgs);
            RefreshColors();
        }

        private void RefreshColors() {
            ignoreEvents = true;
            currentColor = colorPanel.color = Controller.GetCurrentColor(ColorID);
            if (colorPicker != null) {
                colorPicker.color = Controller.GetCurrentColor(ColorID);
            }
            UpdateTextfields();
            ignoreEvents = false;
        }
    }
}

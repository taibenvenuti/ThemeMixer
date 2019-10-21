using ColossalFramework.UI;
using System;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.CategoryPanels
{
    public class MixesPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;

        protected PanelBase selectMixPanel;
        protected UILabel selectMixLabel;
        protected UIDropDown selectMixDropDown;
        protected CheckboxPanel useAsDefaultCheckbox;
        protected ButtonPanel loadButtonPanel;

        protected PanelBase saveMixPanel;
        protected UILabel saveMixLabel;
        protected PanelBase textFieldPanel;
        protected UITextField saveMixTextField;
        protected ButtonPanel saveButtonPanel;

        private string saveName;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Mixes;
            Setup("Mixes Panel", 360.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            CreateTitleLabel();
            CreatePanels();
            this.CreateSpace(0.0f, 0.1f);
        }

        private void CreateTitleLabel() {
            labelPanel = AddUIComponent<UIPanel>();
            labelPanel.size = new Vector2(width, 22.0f);
            label = labelPanel.AddUIComponent<UILabel>();
            label.font = UIUtils.BoldFont;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_MIXES);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
        }

        private void CreatePanels() {
            CreateSelectMixPanel();
            CreateSaveMixPanel();
        }

        private void CreateSaveMixPanel() {
            saveMixPanel = AddUIComponent<PanelBase>();
            saveMixPanel.Setup("Save Mix Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect");
            saveMixPanel.color = UIColorGrey;

            saveMixPanel.CreateSpace(0.0f, 0.01f);
            CreateLabel(saveMixPanel, Translation.Instance.GetTranslation(TranslationID.LABEL_SAVEMIX));
            saveMixPanel.CreateSpace(0.0f, 0.01f);
            CreateTextField();
            saveMixPanel.CreateSpace(0.0f, 0.01f);
            CreateSaveButton();
            saveMixPanel.CreateSpace(0.0f, 5.0f);
        }

        private void CreateTextField() {
            textFieldPanel = saveMixPanel.AddUIComponent<PanelBase>();
            textFieldPanel.Setup("Name Text Field", 340.0f, 30.0f);
            UILabel nameLabel = textFieldPanel.AddUIComponent<UILabel>();
            nameLabel.pivot = UIPivotPoint.MiddleRight;
            nameLabel.font = UIUtils.Font;
            nameLabel.padding = new RectOffset(0, 0, 8, 4);
            nameLabel.text = Translation.Instance.GetTranslation(TranslationID.LABEL_NAME);
            nameLabel.relativePosition = new Vector3(110.0f - nameLabel.width, 0.0f);
            saveMixTextField = textFieldPanel.AddUIComponent<UITextField>();
            saveMixTextField.atlas = UISprites.DefaultAtlas;
            saveMixTextField.size = new Vector2(220.0f, 30.0f);
            saveMixTextField.padding = new RectOffset(4, 4, 6, 6);
            saveMixTextField.builtinKeyNavigation = true;
            saveMixTextField.isInteractive = true;
            saveMixTextField.readOnly = false;
            saveMixTextField.selectOnFocus = true;
            saveMixTextField.horizontalAlignment = UIHorizontalAlignment.Center;
            saveMixTextField.selectionSprite = "EmptySprite";
            saveMixTextField.selectionBackgroundColor = new Color32(0, 172, 234, 255);
            saveMixTextField.normalBgSprite = "TextFieldPanelHovered";
            saveMixTextField.textColor = new Color32(0, 0, 0, 255);
            saveMixTextField.textScale = 0.85f;
            saveMixTextField.color = new Color32(255, 255, 255, 255);
            saveMixTextField.relativePosition = new Vector3(120.0f, 0.0f);
            saveMixTextField.eventTextSubmitted += OnTextfieldTextSubmitted;
            saveMixTextField.eventKeyPress += OnTextfieldKeyPress; ;
            saveMixTextField.eventLostFocus += OnTextfieldLostFocus;
            saveMixTextField.eventTextChanged += OnTextFieldTextChanged;
        }

        private void OnTextFieldTextChanged(UIComponent component, string value) {
            UITextField textfield = component as UITextField;
            if (textfield.text.Length > 0) saveButtonPanel.EnableButton(Translation.Instance.GetTranslation(TranslationID.BUTTON_SAVE));
            else saveButtonPanel.DisableButton();
        }

        private void OnTextfieldTextSubmitted(UIComponent component, string value) {
            saveName = value;
        }

        private void OnTextfieldLostFocus(UIComponent component, UIFocusEventParameter eventParam) {
            UITextField textfield = component as UITextField;
            OnTextfieldTextSubmitted(component, textfield.text);
        }

        private void OnTextfieldKeyPress(UIComponent component, UIKeyEventParameter eventParam) {
            UITextField textfield = component as UITextField;
            char ch = eventParam.character;
            if (!char.IsControl(ch) && !char.IsLetterOrDigit(ch) && !char.IsWhiteSpace(ch)) {
                eventParam.Use();
            }
            if (eventParam.keycode == KeyCode.Escape) {
                textfield.Unfocus();
                eventParam.Use();
            }
        }

        private void CreateSaveButton() {
            saveButtonPanel = saveMixPanel.AddUIComponent<ButtonPanel>();
            saveButtonPanel.Setup("Save Button", 340.0f, 30.0f);
            saveButtonPanel.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            saveButtonPanel.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_SAVE));
            saveButtonPanel.AlignRight();
            saveButtonPanel.DisableButton();
            saveButtonPanel.EventButtonClicked += OnSaveClicked;
        }

        private void OnSaveClicked() {
            Controller.SaveMix(saveName);
            saveButtonPanel.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_SAVED));
            saveButtonPanel.DisableButton();
        }

        private void CreateSelectMixPanel() {
            selectMixPanel = AddUIComponent<PanelBase>();
            selectMixPanel.Setup("Select Mix Panel", 350.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "WhiteRect");
            selectMixPanel.color = UIColorGrey;
            selectMixPanel.CreateSpace(0.0f, 0.01f);
            CreateLabel(selectMixPanel, Translation.Instance.GetTranslation(TranslationID.LABEL_SELECTMIX));
            selectMixPanel.CreateSpace(0.0f, 0.01f);
            CreateDropDown();
            selectMixPanel.CreateSpace(0.0f, 0.01f);
            CreateCheckBox();
            selectMixPanel.CreateSpace(0.0f, 0.01f);
            CreateLoadButton();
            selectMixPanel.CreateSpace(0.0f, 5.0f);
        }
        private void CreateLoadButton() {
            loadButtonPanel = selectMixPanel.AddUIComponent<ButtonPanel>();
            loadButtonPanel.Setup("Load Button", 340.0f, 30.0f);
            loadButtonPanel.SetAnchor(UIAnchorStyle.Left | UIAnchorStyle.CenterVertical);
            loadButtonPanel.SetText(Translation.Instance.GetTranslation(TranslationID.BUTTON_LOAD));
            loadButtonPanel.AlignRight();
            loadButtonPanel.EventButtonClicked += OnLoadClicked;
        }

        private void OnLoadClicked() {
            ThemeMix mix = Data.GetMixByIndex(selectMixDropDown.selectedIndex);
            if (mix != null) Controller.LoadMix(mix);
        }

        private void CreateCheckBox() {
            useAsDefaultCheckbox = selectMixPanel.AddUIComponent<CheckboxPanel>();
            bool state = false;//Controller.GetDefaultMix();
            string label = Translation.Instance.GetTranslation(TranslationID.LABEL_USEASDEFAULT);
            string tooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_USEASDEFAULT);
            useAsDefaultCheckbox.Initialize(state, label, tooltip);
            useAsDefaultCheckbox.MakeSmallVersion();
            useAsDefaultCheckbox.EventCheckboxStateChanged += OnUseAsDefaultCheckChanged;
        }

        private void OnUseAsDefaultCheckChanged(UIComponent component, bool value) {
            //Controller.SetDefaultMix();
        }
            
        private void CreateLabel(UIComponent parent, string text) {
            UILabel label = parent.AddUIComponent<UILabel>();
            label.autoSize = false;
            label.autoHeight = true;
            label.width = 340.0f;
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Left;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 4, 0);
            label.text = text;
        }

        private void CreateDropDown() {
            UIPanel panel = selectMixPanel.AddUIComponent<UIPanel>();
            panel.size = new Vector2(340.0f, 30.0f);
            selectMixDropDown = panel.AddUIComponent<UIDropDown>();
            selectMixDropDown.relativePosition = Vector3.zero;
            selectMixDropDown.atlas = UISprites.DefaultAtlas;
            selectMixDropDown.size = new Vector2(340f, 30f);
            selectMixDropDown.listBackground = "StylesDropboxListbox";
            selectMixDropDown.itemHeight = 30;
            selectMixDropDown.itemHover = "ListItemHover";
            selectMixDropDown.itemHighlight = "ListItemHighlight";
            selectMixDropDown.normalBgSprite = "CMStylesDropbox";
            selectMixDropDown.hoveredBgSprite = "CMStylesDropboxHovered";
            selectMixDropDown.listWidth = 300;
            selectMixDropDown.listHeight = 500;
            selectMixDropDown.listPosition = UIDropDown.PopupListPosition.Automatic;
            selectMixDropDown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            selectMixDropDown.popupColor = UnityEngine.Color.white;
            selectMixDropDown.popupTextColor = new Color32(170, 170, 170, 255);
            selectMixDropDown.textScale = 0.8f;
            selectMixDropDown.verticalAlignment = UIVerticalAlignment.Middle;
            selectMixDropDown.horizontalAlignment = UIHorizontalAlignment.Left;
            selectMixDropDown.textFieldPadding = new RectOffset(8, 0, 8, 0);
            selectMixDropDown.itemPadding = new RectOffset(10, 0, 8, 0);
            selectMixDropDown.triggerButton = selectMixDropDown;
            selectMixDropDown.eventDropdownOpen += OnDropDownOpen;
            selectMixDropDown.eventDropdownClose += OnDropDownClose;
            selectMixDropDown.items = Data.MixNames;
            if (selectMixDropDown.items.Length > 0)
                selectMixDropDown.selectedIndex = 0;
        }

        private void OnSelectIndexChanged(UIComponent component, int value) {

        }

        private void OnDropDownClose(UIDropDown dropdown, UIListBox popup, ref bool overridden) {
            selectMixDropDown.triggerButton.isInteractive = true;
        }

        private void OnDropDownOpen(UIDropDown dropdown, UIListBox popup, ref bool overridden) {
            selectMixDropDown.triggerButton.isInteractive = false;
        }
    }
}

using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class CheckboxPanel : PanelBase
    {
        public event PropertyChangedEventHandler<bool> EventCheckboxStateChanged;

        private UICheckBox checkbox;
        private UILabel label;

        public override void OnDestroy() {
            checkbox.eventCheckChanged -= OnCheckboxStateChanged;
            base.OnDestroy();
        }

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Terrain;
            Setup("Sprite Detail Checkbox", 0.0f, 22.0f, 0, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            CreateCheckbox();
            CreateLabel();
        }

        private void CreateCheckbox() {
            UIPanel checkboxPanel = AddUIComponent<UIPanel>();
            checkboxPanel.size = new Vector2(25.0f, 22.0f);
            checkbox = checkboxPanel.AddUIComponent<UICheckBox>();
            checkbox.size = new Vector2(15.0f, 15.0f);
            checkbox.relativePosition = new Vector2(5.0f, 3.5f);
            var sprite = checkbox.AddUIComponent<UISprite>();
            sprite.spriteName = "check-unchecked";
            sprite.atlas = UISprites.DefaultAtlas;
            sprite.size = checkbox.size;
            sprite.transform.parent = checkbox.transform;
            sprite.transform.localPosition = Vector3.zero;
            var checkedBoxObj = sprite.AddUIComponent<UISprite>();
            checkedBoxObj.spriteName = "check-checked";
            checkedBoxObj.atlas = UISprites.DefaultAtlas;
            checkedBoxObj.size = checkbox.size;
            checkedBoxObj.relativePosition = Vector3.zero;
            checkbox.checkedBoxObject = checkedBoxObj;
            checkbox.eventCheckChanged += OnCheckboxStateChanged;
            checkbox.anchor = UIAnchorStyle.CenterVertical | UIAnchorStyle.Left;
        }

        private void CreateLabel() {
            label = AddUIComponent<UILabel>();
            label.textAlignment = UIHorizontalAlignment.Left;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.font = UIUtils.Font;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.anchor = UIAnchorStyle.CenterVertical | UIAnchorStyle.Left;
            label.relativePosition = new Vector2(25.0f, 0.0f);
        }

        public void Initialize(bool state, string text, string tooltip) {
            SetState(state);
            label.text = text;
            label.tooltip = checkbox.tooltip = tooltip;
            autoFitChildrenHorizontally = true;
        }

        public void SetState(bool state) {
            checkbox.isChecked = state;
        }

        private void OnCheckboxStateChanged(UIComponent component, bool value) {
            EventCheckboxStateChanged?.Invoke(this, value);
        }
    }
}

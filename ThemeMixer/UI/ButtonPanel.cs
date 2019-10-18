using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Color
{
    public class ButtonPanel : PanelBase
    {
        public delegate void ButtonClickedEventHandler();
        public event ButtonClickedEventHandler EventButtonClicked;

        private UIButton button;
        public override void Awake() {
            base.Awake();
            CreateButton();
            this.CreateSpace(0.0f, 5.0f);
        }

        public override void OnDestroy() {
            EventButtonClicked = null;
            button.eventClicked -= OnButtonClicked;
            base.OnDestroy();
        }

        private void CreateButton() {
            button = UIUtils.CreateButton(this, new Vector2(112.5f, 30.0f));
            button.eventClicked += OnButtonClicked;
        }

        public void SetAnchor(UIAnchorStyle anchors) {
            button.anchor = anchors;
        }

        public void SetText(string text, string tooltip = "") {
            button.text = text;
            button.tooltip = tooltip;
        }

        private void OnButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventButtonClicked?.Invoke();
        }
    }
}

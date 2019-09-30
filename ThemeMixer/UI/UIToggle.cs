using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Serialization;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class UIToggle : UIButton
    {
        public delegate void UIToggleClickedEventHandler();
        public event UIToggleClickedEventHandler EventUIToggleClicked;

        private bool toggled;
        private Vector3 deltaPos { get; set; }

        public override void Start() {
            base.Start();
            name = "Theme Mixer Toggle";
            atlas = UISprites.Atlas;
            normalBgSprite = UISprites.UIToggleIcon;
            hoveredBgSprite = UISprites.UIToggleIconHovered;
            pressedBgSprite = UISprites.UIToggleIconPressed;
            absolutePosition = SerializationService.Instance.GetUITogglePosition() ?? GetDefaultPosition();
        }

        private Vector2 GetDefaultPosition() {
            UIComponent referenceComponent = GetUIView().FindUIComponent<UIComponent>("Esc");
            Vector2 pos = new Vector2(referenceComponent.absolutePosition.x + referenceComponent.width - width, referenceComponent.absolutePosition.y + referenceComponent.height + 5.0f);
            return pos;
        }

        protected override void OnClick(UIMouseEventParameter p) {
            if (!p.buttons.IsFlagSet(UIMouseButton.Left)) return;
            toggled = !toggled;
            EventUIToggleClicked?.Invoke();
            normalBgSprite = toggled ? UISprites.UIToggleIconFocused : UISprites.UIToggleIcon;
        }

        protected override void OnMouseDown(UIMouseEventParameter p) {
            if (p.buttons.IsFlagSet(UIMouseButton.Right)) {
                Vector3 mousePos = Input.mousePosition;
                mousePos.y = m_OwnerView.fixedHeight - mousePos.y;

                deltaPos = absolutePosition - mousePos;
                BringToFront();
            }
        }

        protected override void OnMouseMove(UIMouseEventParameter p) {
            if (p.buttons.IsFlagSet(UIMouseButton.Right)) {
                Vector3 mousePos = Input.mousePosition;
                mousePos.y = m_OwnerView.fixedHeight - mousePos.y;
                absolutePosition = mousePos + deltaPos;
                SerializationService.Instance.SetUITogglePosition(new Vector2(absolutePosition.x, absolutePosition.y));
            }
        }

        protected override void OnMouseUp(UIMouseEventParameter p) {
            base.OnMouseUp(p);
            if (p.buttons.IsFlagSet(UIMouseButton.Right)) {
                SerializationService.Instance.SaveData();
            }
        }
    }
}

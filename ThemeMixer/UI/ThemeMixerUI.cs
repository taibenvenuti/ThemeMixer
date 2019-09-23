using ColossalFramework.UI;
using ThemeMixer.Serialization;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.Parts;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class ThemeMixerUI : PanelBase
    {
        private PanelBase currentPanel;
        private ToolBar toolBar;
        private UIPanel space;

        public override void Start() {
            base.Start();

            size = new Vector2(0.0f, 234.0f);
            autoLayout = true;
            autoLayoutDirection = LayoutDirection.Horizontal;
            autoLayoutStart = LayoutStart.BottomRight;

            relativePosition = SerializationService.Instance.GetToolBarPosition() ?? CalculateDefaultToolBarPosition();

            CreateToolBar();

            EnsureToolbarOnScreen();

            RefreshZOrder();
        }

        private void CreateToolBar() {
            toolBar = AddUIComponent<ToolBar>();
            toolBar.Setup(new Vector2(40.0f, 0.0f), 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            toolBar.EventButtonClicked += OnButtonClicked;
            space = AddUIComponent<UIPanel>();
            space.size = new Vector2(UIUtils.DEFAULT_SPACING, toolBar.height);
        }

        private PanelBase CreatePanel(UIPart uiPart) {
            switch (uiPart) {
                case UIPart.Terrain:
                    Parts.TerrainPanel terrainPanel = AddUIComponent<Parts.TerrainPanel>();
                    terrainPanel.Setup(new Vector2(400.0f, 0.0f), UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
                    return terrainPanel;
                default: return null;
            }
        }

        private void OnButtonClicked(UIPart uiPart, UIButton button, UIButton[] buttons) {
            if (currentPanel != null) {
                Destroy(currentPanel);
                currentPanel = null;
                SetButtonUnfocused(button);
                return;
            }
            currentPanel = CreatePanel(uiPart);
            RefreshButtons(button, buttons);
            RefreshZOrder();
        }

        private void RefreshButtons(UIButton focusedButton, UIButton[] buttons) {
            for (int i = 0; i < buttons.Length; i++) {
                SetButtonUnfocused(buttons[i]);
            }
            SetButtonFocused(focusedButton);
        }

        private void SetButtonFocused(UIButton button) {
            if (button != null) {
                button.normalBgSprite = button.focusedBgSprite = button.hoveredBgSprite = string.Concat(button.normalBgSprite.Replace("Focused", ""), "Focused");
            }
        }

        private void SetButtonUnfocused(UIButton button) {
            if (button != null) {
                button.normalBgSprite = button.focusedBgSprite = button.normalBgSprite.Replace("Focused", "");
                button.hoveredBgSprite = button.hoveredBgSprite.Replace("Focused", "Hovered");
            }
        }

        #region Position
        private static Vector2 CalculateDefaultToolBarPosition() {
            Vector2 screenRes = UIView.GetAView().GetScreenResolution();
            return screenRes - new Vector2(10.0f, 830.0f);
        }

        private void EnsureToolbarOnScreen() {
            Vector2 screenRes = UIView.GetAView().GetScreenResolution();
            if (relativePosition.x < 0f || relativePosition.x > screenRes.x || relativePosition.y < 0f || relativePosition.y > screenRes.y) {
                relativePosition = CalculateDefaultToolBarPosition();
            }
        }

        public override void Update() {
            base.Update();

            Vector2 screenRes = UIView.GetAView().GetScreenResolution();

            if ((autoLayoutStart == LayoutStart.TopLeft || autoLayoutStart == LayoutStart.BottomLeft) && relativePosition.x > screenRes.x / 2.0f) {
                autoLayoutStart = autoLayoutStart == LayoutStart.TopLeft ? LayoutStart.TopRight : LayoutStart.BottomRight;
                RefreshZOrder();
            } else if ((autoLayoutStart == LayoutStart.TopRight || autoLayoutStart == LayoutStart.BottomRight) && relativePosition.x + width < screenRes.x / 2.0f) {
                autoLayoutStart = autoLayoutStart == LayoutStart.TopRight ? LayoutStart.TopLeft : LayoutStart.BottomLeft;
                RefreshZOrder();
            }
            if ((autoLayoutStart == LayoutStart.TopLeft || autoLayoutStart == LayoutStart.TopRight) && relativePosition.y > screenRes.y / 2.0f) {
                autoLayoutStart = autoLayoutStart == LayoutStart.TopLeft ? LayoutStart.BottomLeft : LayoutStart.BottomRight;
                RefreshZOrder();
            } else if ((autoLayoutStart == LayoutStart.BottomLeft || autoLayoutStart == LayoutStart.BottomRight) && relativePosition.y + height < screenRes.y / 2.0f) {
                autoLayoutStart = autoLayoutStart == LayoutStart.BottomLeft ? LayoutStart.TopLeft : LayoutStart.TopRight;
                RefreshZOrder();
            }
        }

        private void RefreshZOrder() {
            if (autoLayoutStart == LayoutStart.TopLeft || autoLayoutStart == LayoutStart.BottomLeft) {
                toolBar.zOrder = 0;
                space.zOrder = 1;
                if (currentPanel != null) currentPanel.zOrder = 2;
            } else if (autoLayoutStart == LayoutStart.TopRight || autoLayoutStart == LayoutStart.BottomRight) {
                if (currentPanel != null) {
                    currentPanel.zOrder = 0;
                    space.zOrder = 1;
                    toolBar.zOrder = 2;
                } else {
                    space.zOrder = 0;
                    toolBar.zOrder = 1;
                }
            }
        }
        #endregion
    }
}

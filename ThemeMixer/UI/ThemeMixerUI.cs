﻿using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Serialization;
using ThemeMixer.Themes;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.FastList;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class ThemeMixerUI : PanelBase
    {
        public event ItemClickedEventHandler EventThemeClicked;
        private PanelBase currentPanel;
        private ToolBar toolBar;
        private UIPanel space;

        public override void Start() {
            base.Start();
            name = "Theme Mixer UI";
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
            toolBar.EventDragEnd += OnDragEnd;
            space = AddUIComponent<UIPanel>();
            space.size = new Vector2(5.0f, 0.0f);
        }

        private void OnDragEnd() {
            Data.SetToolbarPosition(relativePosition);
        }

        private PanelBase CreatePanel(ThemeCategory themePart) {
            switch (themePart) {
                case ThemeCategory.Themes:
                    ThemePanelWrapper themesPanel = AddUIComponent<ThemePanelWrapper>();
                    themesPanel.Setup(TranslationID.LOAD_THEME, themePart);
                    themesPanel.EventItemClicked += OnThemeClicked;
                    return themesPanel;
                case ThemeCategory.Terrain:
                    Parts.TerrainPanel terrainPanel = AddUIComponent<Parts.TerrainPanel>();
                    terrainPanel.Setup(new Vector2(466.0f, 0.0f), UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel", themePart);
                    return terrainPanel;
                default: return null;
            }
        }

        private void OnThemeClicked(ListItem item) {
            EventThemeClicked?.Invoke(item);
        }

        private void OnButtonClicked(Button button, Button[] buttons) {
            UnfocusButtons(buttons);
            if (currentPanel != null) {
                bool same = button.part == currentPanel.ThemePart;
                Destroy(currentPanel.gameObject);
                EventThemeClicked = null;
                currentPanel = null;
                if (same) return;
            }
            currentPanel = CreatePanel(button.part);
            SetButtonFocused(button);
            RefreshZOrder();
        }

        private void UnfocusButtons(Button[] buttons) {
            for (int i = 0; i < buttons.Length; i++) {
                SetButtonUnfocused(buttons[i]);
            }
        }

        private void SetButtonFocused(Button button) {
            if (button != null) {
                button.button.normalBgSprite = button.button.focusedBgSprite = button.button.hoveredBgSprite = string.Concat(button.button.normalBgSprite.Replace("Focused", ""), "Focused");
            }
        }

        private void SetButtonUnfocused(Button button) {
            if (button != null) {
                button.button.normalBgSprite = button.button.focusedBgSprite = button.button.normalBgSprite.Replace("Focused", "");
                button.button.hoveredBgSprite = button.button.hoveredBgSprite.Replace("Focused", "Hovered");
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

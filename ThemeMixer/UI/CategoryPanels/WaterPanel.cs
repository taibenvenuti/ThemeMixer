using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.Color;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    public class WaterPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        protected WaterFoamPanel waterFoamPanel;
        protected WaterNormalPanel waterNormalPanel;

        protected PanelBase colorsPanel;

        protected WaterCleanPanel waterCleanColorPanel;
        protected WaterUnderPanel waterUnderColorPanel;
        protected WaterDirtyPanel waterDirtyColorPanel;

        private UIPanel space1;
        private UIPanel space2;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Water;
            Setup("Water Panel", 360.0f, 0.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            CreateLabel();
            CreateTexturePanels();
            CreateColorsPanel();
        }

        private void CreateLabel() {
            labelPanel = AddUIComponent<UIPanel>();
            labelPanel.size = new Vector2(width, 22.0f);
            label = labelPanel.AddUIComponent<UILabel>();
            label.font = UIUtils.BoldFont;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_WATER);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadWaterFromTheme;
        }

        private void CreateTexturePanels() {
            waterNormalPanel = AddUIComponent<WaterNormalPanel>();
            waterFoamPanel = AddUIComponent<WaterFoamPanel>();
        }

        private void CreateColorsPanel() {
            colorsPanel = AddUIComponent<PanelBase>();
            colorsPanel.Setup("Colors Panel", 360.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            waterCleanColorPanel = colorsPanel.AddUIComponent<WaterCleanPanel>();
            waterCleanColorPanel.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            space1 = colorsPanel.CreateSpace(1.0f, 5.0f);
            waterUnderColorPanel = colorsPanel.AddUIComponent<WaterUnderPanel>();
            waterUnderColorPanel.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            space2 = colorsPanel.CreateSpace(1.0f, 5.0f);
            waterDirtyColorPanel = colorsPanel.AddUIComponent<WaterDirtyPanel>();
            waterDirtyColorPanel.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            colorsPanel.CreateSpace(1.0f, 5.0f);
        }

        private void OnColorPanelVisibilityChanged(UIComponent component, bool value) {
            waterFoamPanel.isVisible = !value;
            waterNormalPanel.isVisible = !value;
            space1.isVisible = space2.isVisible = !value;
            waterCleanColorPanel.isVisible = ReferenceEquals(component, waterCleanColorPanel) ? true : !value;
            waterUnderColorPanel.isVisible = ReferenceEquals(component, waterUnderColorPanel) ? true : !value;
            waterDirtyColorPanel.isVisible = ReferenceEquals(component, waterDirtyColorPanel) ? true : !value;
        }

        private void OnLoadWaterFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Water, ThemeCategory.Water);
        }
    }
}

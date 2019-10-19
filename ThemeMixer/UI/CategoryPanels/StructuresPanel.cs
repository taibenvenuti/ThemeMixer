using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    public class StructuresPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        protected PanelBase container;
        protected PanelBase panelLeft;
        protected PanelBase panelRight;

        protected UpwardRoadDiffusePanel upwardRoadDiffuse;
        protected DownwardRoadDiffusePanel downwardRoadDiffuse;
        protected BuildingFloorDiffusePanel buildingFloorDiffuse;
        protected BuildingBaseDiffusePanel buildingBaseDiffuse;
        protected BuildingBaseNormalPanel buildingBaseNormal;
        protected BuildingBurntDiffusePanel buildingBurntDiffuse;
        protected BuildingAbandonedDiffusePanel buildingAbandonedDiffuse;
        protected LightColorPalettePanel lightColorPalette;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Structures;
            Setup("Terrain Panel", 715.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            this.CreateSpace(1.0f, 5.0f);
            CreateLabel();
            CreateContainers();
            CreateLeftSideTexturePanels();
            CreateRightSideTexturePanels();
            this.CreateSpace(1.0f, 5.0f);
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
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_STRUCTURES);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadTerrainFromTheme;
        }

        private void OnLoadTerrainFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Structures, ThemeCategory.Structures);
        }

        private void CreateContainers() {
            container = AddUIComponent<PanelBase>();
            container.Setup("Structures Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelLeft.Setup("Structures Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelRight = container.AddUIComponent<PanelBase>();
            panelRight.Setup("Structures Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
        }

        private void CreateLeftSideTexturePanels() {
            upwardRoadDiffuse = panelLeft.AddUIComponent<UpwardRoadDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            downwardRoadDiffuse = panelLeft.AddUIComponent<DownwardRoadDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            buildingBaseDiffuse = panelLeft.AddUIComponent<BuildingBaseDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            buildingBaseNormal = panelLeft.AddUIComponent<BuildingBaseNormalPanel>();
        }

        private void CreateRightSideTexturePanels() {
            buildingFloorDiffuse = panelRight.AddUIComponent<BuildingFloorDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            buildingBurntDiffuse = panelRight.AddUIComponent<BuildingBurntDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            buildingAbandonedDiffuse = panelRight.AddUIComponent<BuildingAbandonedDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            lightColorPalette = panelRight.AddUIComponent<LightColorPalettePanel>();
        }
    }
}

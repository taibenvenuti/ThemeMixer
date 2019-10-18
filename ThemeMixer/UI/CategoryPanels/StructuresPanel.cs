using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Structures)]
    [UIProperties("Terrain Panel", 715.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class StructuresPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        [UIProperties("Structures Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase container;

        [UIProperties("Structures Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelLeft;

        [UIProperties("Structures Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelRight;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.UpwardRoadDiffuse)]
        protected TexturePanel upwardRoadDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.DownwardRoadDiffuse)]
        protected TexturePanel downwardRoadDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.BuildingFloorDiffuse)]
        protected TexturePanel buildingFloorDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.BuildingBaseDiffuse)]
        protected TexturePanel buildingBaseDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.BuildingBaseNormal)]
        protected TexturePanel buildingBaseNormal;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.BuildingBurntDiffuse)]
        protected TexturePanel buildingBurntDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.BuildingAbandonedDiffuse)]
        protected TexturePanel buildingAbandonedDiffuse;

        [UICategory(ThemeCategory.Structures)]
        [UITextureID(TextureID.LightColorPalette)]
        protected TexturePanel lightColorPalette;

        public override void Awake() {
            base.Awake();
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
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelRight = container.AddUIComponent<PanelBase>();
        }

        private void CreateLeftSideTexturePanels() {
            upwardRoadDiffuse = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            downwardRoadDiffuse = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            buildingBaseDiffuse = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            buildingBaseNormal = panelLeft.AddUIComponent<TexturePanel>();
        }

        private void CreateRightSideTexturePanels() {
            buildingFloorDiffuse = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            buildingBurntDiffuse = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            buildingAbandonedDiffuse = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            lightColorPalette = panelRight.AddUIComponent<TexturePanel>();
        }
    }
}

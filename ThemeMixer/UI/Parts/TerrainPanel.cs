using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{

    [UICategory(ThemeCategory.Terrain)]
    [UIProperties("Terrain Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class TerrainPanel : PanelBase {
        protected UILabel label;

        [UIProperties("Textures Panel Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelContainer;

        [UIProperties("Textures Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelLeft;

        [UIProperties("Textures Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelCenter;

        [UIProperties("Textures Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelRight;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.GrassDiffuseTexture)]
        protected TexturePanel grassDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.RuinedDiffuseTexture)]
        protected TexturePanel ruinedDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.GravelDiffuseTexture)]
        protected TexturePanel gravelDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.CliffDiffuseTexture)]
        protected TexturePanel cliffDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.SandDiffuseTexture)]
        protected TexturePanel sandDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.CliffSandNormalTexture)]
        protected TexturePanel cliffSandNormalTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.OilDiffuseTexture)]
        protected TexturePanel oilDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.OreDiffuseTexture)]
        protected TexturePanel oreDiffuseTexture;

        [UICategory(ThemeCategory.Terrain)]
        [UITextureID(TextureID.PavementDiffuseTexture)]
        protected TexturePanel pavementDiffuseTexture;

        public ColorOffsetPanel grassPollutionColorOffset;
        public ColorOffsetPanel grassFieldColorOffset;
        public ColorOffsetPanel grassFertilityColorOffset;
        public ColorOffsetPanel grassForestColorOffset;

        public UICheckBox grassDetailEnabled;
        public UICheckBox fertileDetailEnabled;
        public UICheckBox rocksDetailEnabled;

        public override void Awake() {
            base.Awake();

            CreateLabel();
            CreateTextureContainers();
            CreateLeftSideTexturePanels();
            CreateMiddleTexturePanels();
            CreateRightSideTexturePanels();
            this.CreateSpace(1.0f, 5.0f);

        }

        private void CreateRightSideTexturePanels() {
            pavementDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
            texturesPanelRight.CreateSpace(1.0f, 5.0f);
            oilDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
            texturesPanelRight.CreateSpace(1.0f, 5.0f);
            oreDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
        }

        private void CreateMiddleTexturePanels() {
            cliffDiffuseTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();
            texturesPanelCenter.CreateSpace(1.0f, 5.0f);
            sandDiffuseTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();
            texturesPanelCenter.CreateSpace(1.0f, 5.0f);
            cliffSandNormalTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();
        }

        private void CreateLeftSideTexturePanels() {
            grassDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();
            texturesPanelLeft.CreateSpace(1.0f, 5.0f);
            ruinedDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();
            texturesPanelLeft.CreateSpace(1.0f, 5.0f);
            gravelDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();
        }

        private void CreateTextureContainers() {
            texturesPanelContainer = AddUIComponent<PanelBase>();
            texturesPanelContainer.autoFitChildrenVertically = true;
            texturesPanelLeft = texturesPanelContainer.AddUIComponent<PanelBase>();
            texturesPanelCenter = texturesPanelContainer.AddUIComponent<PanelBase>();
            texturesPanelRight = texturesPanelContainer.AddUIComponent<PanelBase>();
        }

        private void CreateLabel() {
            label = AddUIComponent<UILabel>();
            label.autoSize = false;
            label.size = new Vector2(width, 32.0f);
            label.font = UIUtils.BoldFont;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 4, 0);
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_TERRAIN);
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs e) {
            base.OnRefreshUI(sender, e);
        }
    }
}

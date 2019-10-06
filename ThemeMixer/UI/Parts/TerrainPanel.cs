using System;
using ColossalFramework.UI;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI.Abstraction;

namespace ThemeMixer.UI.Parts
{

    [UICategory(ThemeCategory.Terrain)]
    [UIProperties("Terrain Panel", 920.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class TerrainPanel : PanelBase {
        [UIProperties("Textures Panel Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelContainer;

        [UIProperties("Textures Panel Left", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelLeft;

        [UIProperties("Textures Panel Center", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelCenter;

        [UIProperties("Textures Panel Right", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
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
            texturesPanelContainer = AddUIComponent<PanelBase>();
            texturesPanelContainer.autoFitChildrenVertically = true;
            texturesPanelLeft = texturesPanelContainer.AddUIComponent<PanelBase>();
            texturesPanelCenter = texturesPanelContainer.AddUIComponent<PanelBase>();
            texturesPanelRight = texturesPanelContainer.AddUIComponent<PanelBase>();

            grassDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();
            texturesPanelLeft.CreateSpace(1.0f, 5.0f);
            ruinedDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();
            texturesPanelLeft.CreateSpace(1.0f, 5.0f);
            gravelDiffuseTexture = texturesPanelLeft.AddUIComponent<TexturePanel>();

            cliffDiffuseTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();
            texturesPanelCenter.CreateSpace(1.0f, 5.0f);
            sandDiffuseTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();
            texturesPanelCenter.CreateSpace(1.0f, 5.0f);
            cliffSandNormalTexture = texturesPanelCenter.AddUIComponent<TexturePanel>();

            pavementDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
            texturesPanelRight.CreateSpace(1.0f, 5.0f);
            oilDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
            texturesPanelRight.CreateSpace(1.0f, 5.0f);
            oreDiffuseTexture = texturesPanelRight.AddUIComponent<TexturePanel>();
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs e) {
            base.OnRefreshUI(sender, e);
        }
    }
}

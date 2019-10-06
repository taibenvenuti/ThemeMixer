using System;
using ColossalFramework.UI;
using ThemeMixer.Themes;
using ThemeMixer.UI.Abstraction;

namespace ThemeMixer.UI.Parts
{

    public delegate void LoadTextureClickedEventHandler(string packageID, TextureID textureID);
    public delegate void TextureTilingchangedEventHandler(float tiling, TextureID textureID);

    [UICategory(ThemeCategory.Terrain)]
    [UIProperties("Terrain Panel", 920.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class TerrainPanel : PanelBase
    {
        public event LoadTextureClickedEventHandler EventLoadTextureClicked;
        public event TextureTilingchangedEventHandler EventTextureTilingChanged;

        [UIProperties("Textures Panel Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelContainer;

        [UIProperties("Textures Panel Left", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelLeft;

        [UIProperties("Textures Panel Center", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelCenter;

        [UIProperties("Textures Panel Right", 300.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase texturesPanelRight;

        [UITextureID(TextureID.GrassDiffuseTexture)]
        protected TexturePanel grassDiffuseTexture;

        [UITextureID(TextureID.RuinedDiffuseTexture)]
        protected TexturePanel ruinedDiffuseTexture;

        [UITextureID(TextureID.GravelDiffuseTexture)]
        protected TexturePanel gravelDiffuseTexture;

        [UITextureID(TextureID.CliffDiffuseTexture)]
        protected TexturePanel cliffDiffuseTexture;

        [UITextureID(TextureID.SandDiffuseTexture)]
        protected TexturePanel sandDiffuseTexture;

        [UITextureID(TextureID.CliffSandNormalTexture)]
        protected TexturePanel cliffSandNormalTexture;

        [UITextureID(TextureID.OilDiffuseTexture)]
        protected TexturePanel oilDiffuseTexture;

        [UITextureID(TextureID.OreDiffuseTexture)]
        protected TexturePanel oreDiffuseTexture;

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

        public override void Start() {
            base.Start();
            BindEvents();
        }

        public override void OnDestroy() {
            base.OnDestroy();
            UnbindEvents();
        }

        private void BindEvents() {
            grassDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            ruinedDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            gravelDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            cliffDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            sandDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            cliffSandNormalTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            pavementDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            oilDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;
            oreDiffuseTexture.EventLoadTextureClicked += OnLoadTextureClicked;

            grassDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            ruinedDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            gravelDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            cliffDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            sandDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            cliffSandNormalTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            pavementDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            oilDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
            oreDiffuseTexture.EventTextureTilingChanged += OnTextureTilingChanged;
        }

        private void UnbindEvents() {
            grassDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            ruinedDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            gravelDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            cliffDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            sandDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            cliffSandNormalTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            pavementDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            oilDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;
            oreDiffuseTexture.EventLoadTextureClicked -= OnLoadTextureClicked;

            grassDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            ruinedDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            gravelDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            cliffDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            sandDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            cliffSandNormalTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            pavementDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            oilDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
            oreDiffuseTexture.EventTextureTilingChanged -= OnTextureTilingChanged;
        }

        private void OnLoadTextureClicked(string packageID, TextureID textureID) {
            EventLoadTextureClicked?.Invoke(packageID, textureID);
        }

        private void OnTextureTilingChanged(float tiling, TextureID textureID) {
            EventTextureTilingChanged?.Invoke(tiling, textureID);
        }

        protected override void Refresh(ThemeMix mix) {

        }
    }
}

using System;
using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{

    [UICategory(ThemeCategory.Terrain)]
    [UIProperties("Terrain Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class TerrainPanel : PanelBase {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        [UIProperties("Terrain Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase container;

        [UIProperties("Terrain Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelLeft;

        [UIProperties("Terrain Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelCenter;

        [UIProperties("Terrain Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelRight;

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

        [UICategory(ThemeCategory.Terrain)]
        [UIOffsetID(OffsetID.GrassPollutionColorOffset)]
        public OffsetPanel grassPollutionColorOffset;

        [UICategory(ThemeCategory.Terrain)]
        [UIOffsetID(OffsetID.GrassFieldColorOffset)]
        public OffsetPanel grassFieldColorOffset;

        [UICategory(ThemeCategory.Terrain)]
        [UIOffsetID(OffsetID.GrassFertilityColorOffset)]
        public OffsetPanel grassFertilityColorOffset;

        [UICategory(ThemeCategory.Terrain)]
        [UIOffsetID(OffsetID.GrassForestColorOffset)]
        public OffsetPanel grassForestColorOffset;
        
        [UICategory(ThemeCategory.Terrain)]
        [UIProperties("Terrain Detail Panel", 350.0f, 231.0f, 5, backgroundSprite: "WhiteRect")]
        public PanelBase detailPanel;
        
        [UICategory(ThemeCategory.Terrain)]
        [UIProperties("Terrain Detail Panel Inside", 1.0f, 1.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        public PanelBase detailPanelInner;

        [UICategory(ThemeCategory.Terrain)]
        [UIProperties("Cliff Detail Checkbox", 0.0f, 22.0f)]
        public CheckboxPanel cliffDetail;

        [UICategory(ThemeCategory.Terrain)]
        [UIProperties("Fertile Detail Checkbox", 0.0f, 22.0f)]
        public CheckboxPanel fertileDetail;

        [UICategory(ThemeCategory.Terrain)]
        [UIProperties("Grass Detail Checkbox", 0.0f, 22.0f)]
        public CheckboxPanel grassDetail;

        public UICheckBox grassDetailEnabled;
        public UICheckBox fertileDetailEnabled;
        public UICheckBox rocksDetailEnabled;

        public override void Awake() {
            base.Awake();
            this.CreateSpace(1.0f, 5.0f);
            CreateLabel();
            CreateContainers();
            CreateLeftSideTexturePanels();
            CreateMiddleTexturePanels();
            CreateRightSideTexturePanels();
            CreateLeftSideOffsetPanels();
            CreateMiddleSideOffsetPanels();
            CreateDetailPanel();
            this.CreateSpace(1.0f, 5.0f);
        }

        public override void Start() {
            base.Start();
            detailPanel.color = UIColorGrey;
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
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_TERRAIN);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadTerrainFromTheme;
        }

        private void OnLoadTerrainFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Terrain, ThemeCategory.Terrain);
        }

        private void CreateContainers() {
            container = AddUIComponent<PanelBase>();
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelCenter = container.AddUIComponent<PanelBase>();
            panelRight = container.AddUIComponent<PanelBase>();
        }

        private void CreateLeftSideTexturePanels() {
            grassDiffuseTexture = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            ruinedDiffuseTexture = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            gravelDiffuseTexture = panelLeft.AddUIComponent<TexturePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
        }

        private void CreateMiddleTexturePanels() {
            cliffDiffuseTexture = panelCenter.AddUIComponent<TexturePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            sandDiffuseTexture = panelCenter.AddUIComponent<TexturePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            cliffSandNormalTexture = panelCenter.AddUIComponent<TexturePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
        }

        private void CreateRightSideTexturePanels() {
            pavementDiffuseTexture = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            oilDiffuseTexture = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            oreDiffuseTexture = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
        }

        private void CreateLeftSideOffsetPanels() {
            grassFertilityColorOffset = panelLeft.AddUIComponent<OffsetPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            grassForestColorOffset = panelLeft.AddUIComponent<OffsetPanel>();
        }

        private void CreateMiddleSideOffsetPanels() {
            grassPollutionColorOffset = panelCenter.AddUIComponent<OffsetPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            grassFieldColorOffset = panelCenter.AddUIComponent<OffsetPanel>();
        }

        private void CreateDetailPanel() {
            detailPanel = panelRight.AddUIComponent<PanelBase>();
            detailPanelInner = detailPanel.AddUIComponent<PanelBase>();
            detailPanelInner.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;

            UILabel label = detailPanelInner.AddUIComponent<UILabel>();
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.relativePosition = Vector2.zero;
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_TITLE_DETAIL);

            grassDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool grassState = ThemeUtils.GetValue<bool>(ValueID.GrassDetailEnabled);
            string grassLabelText = Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE_GRASSDETAIL);
            string grassTooltipText = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_VALUE_GRASSDETAIL);
            grassDetail.Initialize(grassState, grassLabelText, grassTooltipText);
            grassDetail.EventCheckboxStateChanged += OnDetailStateChanged;

            fertileDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool fertileState = ThemeUtils.GetValue<bool>(ValueID.FertileDetailEnabled);
            string fertileLabelText = Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE_FERTILEDETAIL);
            string fertileTooltipText = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_VALUE_FERTILEDETAIL);
            fertileDetail.Initialize(fertileState, fertileLabelText, fertileTooltipText);
            fertileDetail.EventCheckboxStateChanged += OnDetailStateChanged;

            cliffDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool cliffState = ThemeUtils.GetValue<bool>(ValueID.RocksDetailEnabled);
            string cliffLabelText = Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE_CLIFFDETAIL);
            string cliffTooltipText = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_VALUE_CLIFFDETAIL);
            cliffDetail.Initialize(cliffState, cliffLabelText, cliffTooltipText);
            cliffDetail.EventCheckboxStateChanged += OnDetailStateChanged;

            detailPanelInner.autoFitChildrenHorizontally = true;
        }

        private void OnDetailStateChanged(UIComponent comp, bool state) {
            CheckboxPanel cbp = comp as CheckboxPanel;
            ValueID id = ValueID.None;
            if (ReferenceEquals(cbp, cliffDetail)) id = ValueID.RocksDetailEnabled;
            if (ReferenceEquals(cbp, fertileDetail)) id = ValueID.FertileDetailEnabled;
            if (ReferenceEquals(cbp, grassDetail)) id = ValueID.GrassDetailEnabled;
            Controller.OnValueChanged(id, state);
        }

        protected override void OnRefreshUI(object sender, UIDirtyEventArgs e) {
            base.OnRefreshUI(sender, e);
            try {
                cliffDetail.SetState(ThemeUtils.GetValue<bool>(ValueID.RocksDetailEnabled));
                fertileDetail.SetState(ThemeUtils.GetValue<bool>(ValueID.FertileDetailEnabled));
                grassDetail.SetState(ThemeUtils.GetValue<bool>(ValueID.GrassDetailEnabled));
            } catch (Exception ex) {
                Debug.LogError(string.Concat("Error caught in TerrainPanel.RefreshUI: ", ex));
            }
        }
    }
}

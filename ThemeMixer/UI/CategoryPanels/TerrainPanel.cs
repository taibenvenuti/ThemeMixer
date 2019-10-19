using System;
using ColossalFramework.UI;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using UnityEngine;

namespace ThemeMixer.UI.Parts
{
    public class TerrainPanel : PanelBase {

        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        protected PanelBase container;
        protected PanelBase panelLeft;
        protected PanelBase panelCenter;
        protected PanelBase panelRight;

        protected GrassDiffusePanel grassDiffuseTexture;
        protected RuinedDiffusePanel ruinedDiffuseTexture;
        protected GravelDiffusePanel gravelDiffuseTexture;
        protected CliffDiffusePanel cliffDiffuseTexture;
        protected SandDiffusePanel sandDiffuseTexture;
        protected CliffSandNormalPanel cliffSandNormalTexture;
        protected OilDiffusePanel oilDiffuseTexture;
        protected OreDiffusePanel oreDiffuseTexture;
        protected PavementDiffusePanel pavementDiffuseTexture;
        public GrassPollutionPanel grassPollutionColorOffset;
        public GrassFieldPanel grassFieldColorOffset;
        public GrassFertilityPanel grassFertilityColorOffset;
        public GrassForestPanel grassForestColorOffset;
        
        public PanelBase detailPanel;
        public PanelBase detailPanelInner;

        public CheckboxPanel cliffDetail;
        public CheckboxPanel fertileDetail;
        public CheckboxPanel grassDetail;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Terrain;
            Setup("Terrain Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
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
            container.Setup("Terrain Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelLeft.Setup("Terrain Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelCenter = container.AddUIComponent<PanelBase>();
            panelCenter.Setup("Terrain Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelRight = container.AddUIComponent<PanelBase>();
            panelRight.Setup("Terrain Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
        }

        private void CreateLeftSideTexturePanels() {
            grassDiffuseTexture = panelLeft.AddUIComponent<GrassDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            ruinedDiffuseTexture = panelLeft.AddUIComponent<RuinedDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            gravelDiffuseTexture = panelLeft.AddUIComponent<GravelDiffusePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
        }

        private void CreateMiddleTexturePanels() {
            cliffDiffuseTexture = panelCenter.AddUIComponent<CliffDiffusePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            sandDiffuseTexture = panelCenter.AddUIComponent<SandDiffusePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            cliffSandNormalTexture = panelCenter.AddUIComponent<CliffSandNormalPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
        }

        private void CreateRightSideTexturePanels() {
            pavementDiffuseTexture = panelRight.AddUIComponent<PavementDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            oilDiffuseTexture = panelRight.AddUIComponent<OilDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            oreDiffuseTexture = panelRight.AddUIComponent<OreDiffusePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
        }

        private void CreateLeftSideOffsetPanels() {
            grassFertilityColorOffset = panelLeft.AddUIComponent<GrassFertilityPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            grassForestColorOffset = panelLeft.AddUIComponent<GrassForestPanel>();
        }

        private void CreateMiddleSideOffsetPanels() {
            grassPollutionColorOffset = panelCenter.AddUIComponent<GrassPollutionPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            grassFieldColorOffset = panelCenter.AddUIComponent<GrassFieldPanel>();
        }

        private void CreateDetailPanel() {
            detailPanel = panelRight.AddUIComponent<PanelBase>();
            detailPanel.Setup("Terrain Detail Panel", 350.0f, 231.0f, 5, backgroundSprite: "WhiteRect");
            detailPanelInner = detailPanel.AddUIComponent<PanelBase>();
            detailPanelInner.Setup("Terrain Detail Panel Inside", 1.0f, 1.0f, 5, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            detailPanelInner.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;

            UILabel label = detailPanelInner.AddUIComponent<UILabel>();
            label.font = UIUtils.Font;
            label.textScale = 1.0f;
            label.padding = new RectOffset(4, 0, 4, 0);
            label.relativePosition = Vector2.zero;
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_TITLE_DETAIL);

            grassDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool grassState = Controller.GetValue<bool>(ValueID.GrassDetailEnabled);
            string grassLabelText = Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE_GRASSDETAIL);
            string grassTooltipText = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_VALUE_GRASSDETAIL);
            grassDetail.Initialize(grassState, grassLabelText, grassTooltipText);
            grassDetail.EventCheckboxStateChanged += OnDetailStateChanged;

            fertileDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool fertileState = Controller.GetValue<bool>(ValueID.FertileDetailEnabled);
            string fertileLabelText = Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE_FERTILEDETAIL);
            string fertileTooltipText = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_VALUE_FERTILEDETAIL);
            fertileDetail.Initialize(fertileState, fertileLabelText, fertileTooltipText);
            fertileDetail.EventCheckboxStateChanged += OnDetailStateChanged;

            cliffDetail = detailPanelInner.AddUIComponent<CheckboxPanel>();
            bool cliffState = Controller.GetValue<bool>(ValueID.RocksDetailEnabled);
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
                cliffDetail.SetState(Controller.GetValue<bool>(ValueID.RocksDetailEnabled));
                fertileDetail.SetState(Controller.GetValue<bool>(ValueID.FertileDetailEnabled));
                grassDetail.SetState(Controller.GetValue<bool>(ValueID.GrassDetailEnabled));
            } catch (Exception ex) {
                Debug.LogError(string.Concat("Error caught in TerrainPanel.RefreshUI: ", ex));
            }
        }
    }
}

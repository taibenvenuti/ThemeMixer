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
    public class AtmospherePanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;
        protected PanelBase container;
        protected PanelBase panelLeft;
        protected PanelBase panelCenter;
        protected PanelBase panelRight; 

        protected LongitudePanel longitude;
        protected LatitudePanel latitude;
        protected SunSizePanel sunSize;
        protected SunAnisotropyPanel sunAnisotropy;
        protected RayleighPanel rayleigh;
        protected MiePanel mie;
        protected ExposurePanel exposure;
        protected MoonSizePanel moonSize;
        protected StarsIntensityPanel starsIntensity;
        protected OuterSpaceIntensityPanel outerSpaceIntensity;

        protected SkyTintPanel skyTint;
        protected MoonTexturePanel moonTexture;
        protected MoonInnerCoronaPanel moonInnerCorona;
        protected MoonOuterCoronaPanel moonOuterCorona;
        protected NightHorizonPanel nightHorizonColor;
        protected EarlyNightZenithPanel earlyNightZenithColor;
        protected LateNightZenithPanel lateNightZenithColor;
        protected UIPanel space;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Atmosphere;
            Setup("Atmosphere Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            this.CreateSpace(1.0f, 5.0f);
            CreateLabel();
            CreateContainers();
            CreatePanels();
            this.CreateSpace(0.0f, 0.1f);
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
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_ATMOSPHERE);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadWeatherFromTheme;
        }

        private void OnLoadWeatherFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Atmosphere, ThemeCategory.Atmosphere);
        }

        private void CreateContainers() {
            container = AddUIComponent<PanelBase>();
            container.Setup("Atmosphere Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelLeft.Setup("Atmosphere Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelCenter = container.AddUIComponent<PanelBase>();
            panelCenter.Setup("Atmosphere Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelRight = container.AddUIComponent<PanelBase>();
            panelRight.Setup("Atmosphere Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelRight.autoLayoutPadding = new RectOffset(0, 0, 0, 8);
        }

        private void CreatePanels() {
            longitude = panelLeft.AddUIComponent<LongitudePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            latitude = panelLeft.AddUIComponent<LatitudePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            sunSize = panelLeft.AddUIComponent<SunSizePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            sunAnisotropy = panelLeft.AddUIComponent<SunAnisotropyPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            exposure = panelLeft.AddUIComponent<ExposurePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);

            rayleigh = panelCenter.AddUIComponent<RayleighPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            mie = panelCenter.AddUIComponent<MiePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            starsIntensity = panelCenter.AddUIComponent<StarsIntensityPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            outerSpaceIntensity = panelCenter.AddUIComponent<OuterSpaceIntensityPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            moonSize = panelCenter.AddUIComponent<MoonSizePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);


            skyTint = panelRight.AddUIComponent<SkyTintPanel>();
            skyTint.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            earlyNightZenithColor = panelRight.AddUIComponent<EarlyNightZenithPanel>();
            earlyNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            lateNightZenithColor = panelRight.AddUIComponent<LateNightZenithPanel>();
            lateNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            nightHorizonColor = panelRight.AddUIComponent<NightHorizonPanel>();
            nightHorizonColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            moonInnerCorona = panelRight.AddUIComponent<MoonInnerCoronaPanel>();
            moonInnerCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            moonOuterCorona = panelRight.AddUIComponent<MoonOuterCoronaPanel>();
            moonOuterCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            moonTexture = panelRight.AddUIComponent<MoonTexturePanel>();
        }

        private void OnColorPanelVisibilityChanged(object sender, ColorPanelVisibilityChangedEventArgs eventArgs) {
            bool isSkyTint = ReferenceEquals(eventArgs.panel, skyTint);
            bool isInnerCorona = ReferenceEquals(eventArgs.panel, moonInnerCorona);
            bool isOuterCorona = ReferenceEquals(eventArgs.panel, moonOuterCorona);
            bool isNightHorizon = ReferenceEquals(eventArgs.panel, nightHorizonColor);
            bool isEarlyNightZenith = ReferenceEquals(eventArgs.panel, earlyNightZenithColor);
            bool isLateNightZenith = ReferenceEquals(eventArgs.panel, lateNightZenithColor);

            skyTint.isVisible = isSkyTint ? true : !eventArgs.visible;
            moonInnerCorona.isVisible = isInnerCorona ? true : !eventArgs.visible;
            moonOuterCorona.isVisible = isOuterCorona ? true : !eventArgs.visible;
            nightHorizonColor.isVisible = isNightHorizon ? true : !eventArgs.visible;
            earlyNightZenithColor.isVisible = isEarlyNightZenith ? true : !eventArgs.visible;
            lateNightZenithColor.isVisible = isLateNightZenith ? true : !eventArgs.visible;
            moonTexture.isVisible = !eventArgs.visible;

            eventArgs.panel.backgroundSprite = eventArgs.visible ? "" : "WhiteRect";
        }
    }
}

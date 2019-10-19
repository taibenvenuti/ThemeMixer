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


        bool ignoreEvents = false;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Atmosphere;
            Setup("Atmosphere Panel", 715.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
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
            panelRight = container.AddUIComponent<PanelBase>();
            panelRight.Setup("Atmosphere Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
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
            rayleigh = panelLeft.AddUIComponent<RayleighPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            mie = panelLeft.AddUIComponent<MiePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            exposure = panelLeft.AddUIComponent<ExposurePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            skyTint = panelLeft.AddUIComponent<SkyTintPanel>();
            skyTint.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelLeft.CreateSpace(1.0f, 5.0f);

            moonTexture = panelRight.AddUIComponent<MoonTexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            moonSize = panelRight.AddUIComponent<MoonSizePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            moonInnerCorona = panelRight.AddUIComponent<MoonInnerCoronaPanel>();
            moonInnerCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            moonOuterCorona = panelRight.AddUIComponent<MoonOuterCoronaPanel>();
            moonOuterCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            nightHorizonColor = panelRight.AddUIComponent<NightHorizonPanel>();
            nightHorizonColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            earlyNightZenithColor = panelRight.AddUIComponent<EarlyNightZenithPanel>();
            earlyNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            lateNightZenithColor = panelRight.AddUIComponent<LateNightZenithPanel>();
            lateNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            starsIntensity = panelRight.AddUIComponent<StarsIntensityPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            outerSpaceIntensity = panelRight.AddUIComponent<OuterSpaceIntensityPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
        }

        private void OnColorPanelVisibilityChanged(UIComponent component, bool value) {
            if (ignoreEvents) return;
            ignoreEvents = true;
            bool isSkyTint = ReferenceEquals(component, skyTint);
            if (isSkyTint) {
                panelRight.isVisible = !value;
                ToggleLeftSideElementsVisibility(value);
            } else {
                panelLeft.isVisible = !value;
                ToggleRightSideElementsVisibility(value, component);
            }
            ignoreEvents = false;
        }

        private void ToggleLeftSideElementsVisibility(bool value) {
            longitude.isVisible = !value;
            latitude.isVisible = !value;
            sunSize.isVisible = !value;
            sunAnisotropy.isVisible = !value;
            rayleigh.isVisible = !value;
            mie.isVisible = !value;
            exposure.isVisible = !value;
            skyTint.isVisible = true;
        }

        private void ToggleRightSideElementsVisibility(bool value, UIComponent component) {
            bool isInnerCorona = ReferenceEquals(component, moonInnerCorona);
            bool isOuterCorona = ReferenceEquals(component, moonOuterCorona);
            bool isNightHorizon = ReferenceEquals(component, nightHorizonColor);
            bool isEarlyNightZenith = ReferenceEquals(component, earlyNightZenithColor);
            bool isLateNightZenith = ReferenceEquals(component, lateNightZenithColor);

            moonTexture.isVisible = !value;
            moonSize.isVisible = !value;
            moonInnerCorona.isVisible = isInnerCorona ? true : !value;
            moonOuterCorona.isVisible = isOuterCorona ? true : !value;
            nightHorizonColor.isVisible = isNightHorizon ? true : !value;
            earlyNightZenithColor.isVisible = isEarlyNightZenith ? true : !value;
            lateNightZenithColor.isVisible = isLateNightZenith ? true : !value;
            starsIntensity.isVisible = !value;
            outerSpaceIntensity.isVisible = !value;
        }
    }
}

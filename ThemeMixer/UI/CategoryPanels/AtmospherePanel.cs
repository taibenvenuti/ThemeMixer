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
    [UICategory(ThemeCategory.Atmosphere)]
    [UIProperties("Atmosphere Panel", 715.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class AtmospherePanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        [UIProperties("Atmosphere Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase container;

        [UIProperties("Atmosphere Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelLeft;

        [UIProperties("Atmosphere Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelRight; 

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.Longitude)]
        protected ValuePanel longitude;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.Latitude)]
        protected ValuePanel latitude;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.SunSize)]
        protected ValuePanel sunSize;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.SunAnisotropy)]
        protected ValuePanel sunAnisotropy;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.Rayleigh)]
        protected ValuePanel rayleigh;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.Mie)]
        protected ValuePanel mie;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.Exposure)]
        protected ValuePanel exposure;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.SkyTint)]
        protected ColorPanel skyTint;

        [UICategory(ThemeCategory.Atmosphere)]
        [UITextureID(TextureID.MoonTexture)]
        protected TexturePanel moonTexture;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.MoonSize)]
        protected ValuePanel moonSize;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.MoonInnerCorona)]
        protected ColorPanel moonInnerCorona;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.MoonOuterCorona)]
        protected ColorPanel moonOuterCorona;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.NightHorizonColor)]
        protected ColorPanel nightHorizonColor;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.EarlyNightZenithColor)]
        protected ColorPanel earlyNightZenithColor;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIColorID(ColorID.LateNightZenithColor)]
        protected ColorPanel lateNightZenithColor;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.StarsIntensity)]
        protected ValuePanel starsIntensity;

        [UICategory(ThemeCategory.Atmosphere)]
        [UIValueID(ValueID.OuterSpaceIntensity)]
        protected ValuePanel outerSpaceIntensity;

        bool ignoreEvents = false;

        public override void Awake() {
            base.Awake();
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
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelRight = container.AddUIComponent<PanelBase>();
        }

        private void CreatePanels() {
            longitude = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            latitude = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            sunSize = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            sunAnisotropy = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            rayleigh = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            mie = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            exposure = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            skyTint = panelLeft.AddUIComponent<ColorPanel>();
            skyTint.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelLeft.CreateSpace(1.0f, 5.0f);

            moonTexture = panelRight.AddUIComponent<TexturePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            moonSize = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            moonInnerCorona = panelRight.AddUIComponent<ColorPanel>();
            moonInnerCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            moonOuterCorona = panelRight.AddUIComponent<ColorPanel>();
            moonOuterCorona.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            nightHorizonColor = panelRight.AddUIComponent<ColorPanel>();
            nightHorizonColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            earlyNightZenithColor = panelRight.AddUIComponent<ColorPanel>();
            earlyNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            lateNightZenithColor = panelRight.AddUIComponent<ColorPanel>();
            lateNightZenithColor.EventVisibilityChanged += OnColorPanelVisibilityChanged;
            panelRight.CreateSpace(1.0f, 5.0f);
            starsIntensity = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            outerSpaceIntensity = panelRight.AddUIComponent<ValuePanel>();
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

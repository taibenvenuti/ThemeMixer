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
    public class WeatherPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        protected PanelBase container;
        protected PanelBase panelLeft;
        protected PanelBase panelCenter;
        protected PanelBase panelRight;

        protected MinTemperatureDayPanel minTemperatureDay;
        protected MaxTemperatureDayPanel maxTemperatureDay;
        protected MinTemperatureNightPanel minTemperatureNight;
        protected MaxTemperatureNightPanel maxTemperatureNight;
        protected MinTemperatureRainPanel minTemperatureRain;
        protected MaxTemperatureRainPanel maxTemperatureRain;
        protected MinTemperatureFogPanel minTemperatureFog;
        protected MaxTemperatureFogPanel maxTemperatureFog;
        protected RainProbabilityDayPanel rainProbabilityDay;
        protected RainProbabilityNightPanel rainProbabilityNight;
        protected FogProbabilityDayPanel fogProbabilityDay;
        protected FogProbabilityNightPanel fogProbabilityNight;
        protected NorthernLightsProbabilityPanel northernLightsProbability;

        public override void Awake() {
            base.Awake();
            Category = ThemeCategory.Weather;
            Setup("Weather Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
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
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_WEATHER);
            label.anchor = UIAnchorStyle.CenterHorizontal | UIAnchorStyle.CenterVertical;
            string loadTooltip = Translation.Instance.GetTranslation(TranslationID.TOOLTIP_LOADFROMTHEME);
            loadButton = UIUtils.CreateButton(label, new Vector2(22.0f, 22.0f), tooltip: loadTooltip, backgroundSprite: "ThemesIcon", atlas: UISprites.Atlas);
            loadButton.relativePosition = new Vector2(label.width + 5.0f, 0.0f);
            loadButton.eventClicked += OnLoadWeatherFromTheme;
        }

        private void OnLoadWeatherFromTheme(UIComponent component, UIMouseEventParameter eventParam) {
            Controller.OnLoadFromTheme(ThemeCategory.Weather, ThemeCategory.Weather);
        }

        private void CreateContainers() {
            container = AddUIComponent<PanelBase>();
            container.Setup("Weather Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft);
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelLeft.Setup("Weather Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelCenter = container.AddUIComponent<PanelBase>();
            panelCenter.Setup("Weather Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
            panelRight = container.AddUIComponent<PanelBase>();
            panelRight.Setup("Weather Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft);
        }

        private void CreatePanels() {
            rainProbabilityDay = panelLeft.AddUIComponent<RainProbabilityDayPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            rainProbabilityNight = panelLeft.AddUIComponent<RainProbabilityNightPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            fogProbabilityDay = panelLeft.AddUIComponent<FogProbabilityDayPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            fogProbabilityNight = panelLeft.AddUIComponent<FogProbabilityNightPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            northernLightsProbability = panelLeft.AddUIComponent<NorthernLightsProbabilityPanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);

            minTemperatureDay = panelCenter.AddUIComponent<MinTemperatureDayPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            maxTemperatureDay = panelCenter.AddUIComponent<MaxTemperatureDayPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            minTemperatureNight = panelCenter.AddUIComponent<MinTemperatureNightPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            maxTemperatureNight = panelCenter.AddUIComponent<MaxTemperatureNightPanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);

            minTemperatureRain = panelRight.AddUIComponent<MinTemperatureRainPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            maxTemperatureRain = panelRight.AddUIComponent<MaxTemperatureRainPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            minTemperatureFog = panelRight.AddUIComponent<MinTemperatureFogPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            maxTemperatureFog = panelRight.AddUIComponent<MaxTemperatureFogPanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
        }
    }
}

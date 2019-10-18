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
    [UICategory(ThemeCategory.Weather)]
    [UIProperties("Weather Panel", 1070.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel")]
    public class WeatherPanel : PanelBase
    {
        protected UIPanel labelPanel;
        protected UILabel label;
        protected UIButton loadButton;

        [UIProperties("Weather Container", 0.0f, 460.0f, 5, true, LayoutDirection.Horizontal, LayoutStart.TopLeft)]
        protected PanelBase container;

        [UIProperties("Weather Panel Left", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelLeft;

        [UIProperties("Weather Panel Center", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelCenter;

        [UIProperties("Structures Panel Right", 350.0f, 0.0f, 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft)]
        protected PanelBase panelRight;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MinTemperatureDay)]
        protected ValuePanel minTemperatureDay;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MaxTemperatureDay)]
        protected ValuePanel maxTemperatureDay;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MinTemperatureNight)]
        protected ValuePanel minTemperatureNight;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MaxTemperatureNight)]
        protected ValuePanel maxTemperatureNight;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MinTemperatureRain)]
        protected ValuePanel minTemperatureRain;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MaxTemperatureRain)]
        protected ValuePanel maxTemperatureRain;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MinTemperatureFog)]
        protected ValuePanel minTemperatureFog;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.MaxTemperatureFog)]
        protected ValuePanel maxTemperatureFog;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.RainProbabilityDay)]
        protected ValuePanel rainProbabilityDay;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.RainProbabilityNight)]
        protected ValuePanel rainProbabilityNight;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.FogProbabilityDay)]
        protected ValuePanel fogProbabilityDay;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.FogProbabilityNight)]
        protected ValuePanel fogProbabilityNight;

        [UICategory(ThemeCategory.Weather)]
        [UIValueID(ValueID.NorthernLightsProbability)]
        protected ValuePanel northernLightsProbability;

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
            container.autoFitChildrenVertically = true;
            panelLeft = container.AddUIComponent<PanelBase>();
            panelCenter = container.AddUIComponent<PanelBase>();
            panelRight = container.AddUIComponent<PanelBase>();
        }

        private void CreatePanels() {
            rainProbabilityDay = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            rainProbabilityNight = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            fogProbabilityDay = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            fogProbabilityNight = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);
            northernLightsProbability = panelLeft.AddUIComponent<ValuePanel>();
            panelLeft.CreateSpace(1.0f, 5.0f);

            minTemperatureDay = panelCenter.AddUIComponent<ValuePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            maxTemperatureDay = panelCenter.AddUIComponent<ValuePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            minTemperatureNight = panelCenter.AddUIComponent<ValuePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);
            maxTemperatureNight = panelCenter.AddUIComponent<ValuePanel>();
            panelCenter.CreateSpace(1.0f, 5.0f);

            minTemperatureRain = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            maxTemperatureRain = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            minTemperatureFog = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
            maxTemperatureFog = panelRight.AddUIComponent<ValuePanel>();
            panelRight.CreateSpace(1.0f, 5.0f);
        }
    }
}

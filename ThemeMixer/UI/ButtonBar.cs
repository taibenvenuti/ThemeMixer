using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.Locale;
using ThemeMixer.TranslationFramework;
using UnityEngine;
using ThemeMixer.Resources;
using ThemeMixer.Themes;

namespace ThemeMixer.UI
{
    public class ButtonBar : PanelBase
    {
        public delegate void ButtonClickedEventHandler(ThemePart uiPart, UIButton button, UIButton[] buttons);
        public event ButtonClickedEventHandler EventButtonClicked;

        private UIButton themesButton;
        private UIButton terrainButton;
        private UIButton waterButton;
        private UIButton atmosphereButton;
        private UIButton structuresButton;
        private UIButton weatherButton;
        private UIButton settingsButton;

        private UIButton[] buttons;

        public override void Start() {
            base.Start();

            CreateButtons();
            UIPanel space = AddUIComponent<UIPanel>();
            space.size = new Vector2(width, 0.1f);
        }

        public override void OnDestroy() {
            themesButton.eventClicked -= OnButtonClicked;
            terrainButton.eventClicked -= OnButtonClicked;
            waterButton.eventClicked -= OnButtonClicked;
            atmosphereButton.eventClicked -= OnButtonClicked;
            structuresButton.eventClicked -= OnButtonClicked;
            weatherButton.eventClicked -= OnButtonClicked;
            settingsButton.eventClicked -= OnButtonClicked;
            base.OnDestroy();
        }

        private void CreateButtons() {
            Vector2 buttonSize = new Vector2(30.0f, 30.0f);

            themesButton = CreateButton(buttonSize, backgroundSprite: UISprites.ThemesIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_THEMES));
            themesButton.eventClicked += OnButtonClicked; ;


            terrainButton = CreateButton(buttonSize, backgroundSprite: UISprites.TerrainIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_TERRAIN));
            terrainButton.eventClicked += OnButtonClicked;;

            waterButton = CreateButton(buttonSize, backgroundSprite: UISprites.WaterIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_WATER));
            waterButton.eventClicked += OnButtonClicked;

            atmosphereButton = CreateButton(buttonSize, backgroundSprite: UISprites.AtmosphereIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_ATMOSPHERE));
            atmosphereButton.eventClicked += OnButtonClicked;

            structuresButton = CreateButton(buttonSize, backgroundSprite: UISprites.StructuresIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_STRUCTURES));
            structuresButton.eventClicked += OnButtonClicked;

            weatherButton = CreateButton(buttonSize, backgroundSprite: UISprites.WeatherIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_WEATHER));
            weatherButton.eventClicked += OnButtonClicked;

            UIPanel panel = AddUIComponent<UIPanel>();
            panel.size = new Vector2(30.0f, 2.0f);
            panel.atlas = UISprites.DefaultAtlas;
            panel.backgroundSprite = "WhiteRect";
            panel.color = new Color32(53, 54, 54, 255);

            settingsButton = CreateButton(buttonSize, backgroundSprite: UISprites.SettingsIcon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_SETTINGS));
            settingsButton.eventClicked += OnButtonClicked;

            CreateButtonArray();
        }

        private void CreateButtonArray() {
            buttons = new UIButton[] {
                themesButton,
                terrainButton,
                waterButton,
                atmosphereButton,
                structuresButton,
                weatherButton,
                settingsButton
            };
        }

        private void OnButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            ThemePart part = component == themesButton ? ThemePart.Themes 
                        : component == terrainButton ? ThemePart.Terrain
                        : component == waterButton ? ThemePart.Water 
                        : component == structuresButton ? ThemePart.Structures 
                        : component == atmosphereButton ? ThemePart.Atmosphere 
                        : ThemePart.Weather;
            EventButtonClicked?.Invoke(part, (UIButton)component, buttons);
            component.RefreshTooltip();
        }
    }
}
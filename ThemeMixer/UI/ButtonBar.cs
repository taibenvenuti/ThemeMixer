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
        public delegate void ButtonClickedEventHandler(Button button, Button[] buttons);
        public event ButtonClickedEventHandler EventButtonClicked;

        private Button themesButton;
        private Button terrainButton;
        private Button waterButton;
        private Button atmosphereButton;
        private Button structuresButton;
        private Button weatherButton;
        private Button settingsButton;

        private Button[] buttons;

        public override void Start() {
            base.Start();

            CreateButtons();
            UIPanel space = AddUIComponent<UIPanel>();
            space.size = new Vector2(width, 0.1f);
        }

        public override void OnDestroy() {
            themesButton.EventClicked -= OnButtonClicked;
            terrainButton.EventClicked -= OnButtonClicked;
            waterButton.EventClicked -= OnButtonClicked;
            atmosphereButton.EventClicked -= OnButtonClicked;
            structuresButton.EventClicked -= OnButtonClicked;
            weatherButton.EventClicked -= OnButtonClicked;
            settingsButton.EventClicked -= OnButtonClicked;
            base.OnDestroy();
        }

        private void CreateButtons() {

            themesButton = new Button(ThemeCategory.Themes, this);
            themesButton.EventClicked += OnButtonClicked; ;


            terrainButton = new Button(ThemeCategory.Terrain, this);
            terrainButton.EventClicked += OnButtonClicked;;

            waterButton = new Button(ThemeCategory.Water, this);
            waterButton.EventClicked += OnButtonClicked;

            atmosphereButton = new Button(ThemeCategory.Atmosphere, this);
            atmosphereButton.EventClicked += OnButtonClicked;

            structuresButton = new Button(ThemeCategory.Structures, this);
            structuresButton.EventClicked += OnButtonClicked;

            weatherButton = new Button(ThemeCategory.Weather, this);
            weatherButton.EventClicked += OnButtonClicked;

            UIPanel panel = AddUIComponent<UIPanel>();
            panel.size = new Vector2(30.0f, 2.0f);
            panel.atlas = UISprites.DefaultAtlas;
            panel.backgroundSprite = "WhiteRect";
            panel.color = new Color32(53, 54, 54, 255);

            settingsButton = new Button(ThemeCategory.None, this);
            settingsButton.EventClicked += OnButtonClicked; ;

            CreateButtonArray();
        }

        private void OnButtonClicked(Button button) {
            EventButtonClicked?.Invoke(button, buttons);
        }

        private void CreateButtonArray() {
            buttons = new Button[] {
                themesButton,
                terrainButton,
                waterButton,
                atmosphereButton,
                structuresButton,
                weatherButton,
                settingsButton
            };
        }
    }

    public class Button
    {
        public delegate void ButtonClickedEventHandler(Button button);
        public event ButtonClickedEventHandler EventClicked;

        public ThemeCategory part;
        public UIButton button;

        private static Vector2 buttonSize = new Vector2(30.0f, 30.0f);

        public Button(ThemeCategory part, PanelBase parent) {
            this.part = part;
            string icon = string.Empty;
            string locale = string.Empty;
            switch (part) {
                case ThemeCategory.Themes:
                    icon = UISprites.ThemesIcon;
                    locale = TranslationID.TOOLTIP_THEMES;
                    break;
                case ThemeCategory.Terrain:
                    icon = UISprites.TerrainIcon;
                    locale = TranslationID.TOOLTIP_TERRAIN;
                    break;
                case ThemeCategory.Water:
                    icon = UISprites.WaterIcon;
                    locale = TranslationID.TOOLTIP_WATER;
                    break;
                case ThemeCategory.Structures:
                    icon = UISprites.StructuresIcon;
                    locale = TranslationID.TOOLTIP_STRUCTURES;
                    break;
                case ThemeCategory.Atmosphere:
                    icon = UISprites.AtmosphereIcon;
                    locale = TranslationID.TOOLTIP_ATMOSPHERE;
                    break;
                case ThemeCategory.Weather:
                    icon = UISprites.WeatherIcon;
                    locale = TranslationID.TOOLTIP_WEATHER;
                    break;
                default:
                    icon = UISprites.SettingsIcon;
                    locale = TranslationID.TOOLTIP_SETTINGS;
                    break;
            }
            button = parent.CreateButton(buttonSize, backgroundSprite: icon, atlas: UISprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(locale));
            button.eventClicked += OnButtonClicked;
        }

        private void OnButtonClicked(UIComponent component, UIMouseEventParameter eventParam) {
            EventClicked?.Invoke(this);
        }
    }
}
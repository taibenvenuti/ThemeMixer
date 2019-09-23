using ColossalFramework.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.Locale;
using ThemeMixer.TranslationFramework;
using UnityEngine;
using ThemeMixer.Resources;

namespace ThemeMixer.UI.Parts
{
    public class ButtonBar : PanelBase
    {
        public delegate void ButtonClickedEventHandler(UIPart uiPart, UIButton button, UIButton[] buttons);
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

            themesButton = CreateButton(buttonSize, backgroundSprite: Sprites.ThemesIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_THEMES));
            themesButton.eventClicked += OnButtonClicked; ;


            terrainButton = CreateButton(buttonSize, backgroundSprite: Sprites.TerrainIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_TERRAIN));
            terrainButton.eventClicked += OnButtonClicked;;

            waterButton = CreateButton(buttonSize, backgroundSprite: Sprites.WaterIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_WATER));
            waterButton.eventClicked += OnButtonClicked;

            atmosphereButton = CreateButton(buttonSize, backgroundSprite: Sprites.AtmosphereIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_ATMOSPHERE));
            atmosphereButton.eventClicked += OnButtonClicked;

            structuresButton = CreateButton(buttonSize, backgroundSprite: Sprites.StructuresIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_STRUCTURES));
            structuresButton.eventClicked += OnButtonClicked;

            weatherButton = CreateButton(buttonSize, backgroundSprite: Sprites.WeatherIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_WEATHER));
            weatherButton.eventClicked += OnButtonClicked;

            UIPanel panel = AddUIComponent<UIPanel>();
            panel.size = new Vector2(30.0f, 3.0f);
            panel.atlas = Sprites.DefaultAtlas;
            panel.backgroundSprite = "WhiteRect";
            panel.color = new Color32(53, 54, 54, 255);

            settingsButton = CreateButton(buttonSize, backgroundSprite: Sprites.SettingsIcon, atlas: Sprites.Atlas, isFocusable: true, tooltip: Translation.Instance.GetTranslation(TranslationID.TOOLTIP_SETTINGS));
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
            UIPart part = component == themesButton ? UIPart.Themes 
                        : component == terrainButton ? UIPart.Terrain
                        : component == waterButton ? UIPart.Water 
                        : component == structuresButton ? UIPart.Structures 
                        : component == atmosphereButton ? UIPart.Atmosphere 
                        : UIPart.Weather;
            EventButtonClicked?.Invoke(part, (UIButton)component, buttons);
            component.RefreshTooltip();
        }
    }
}
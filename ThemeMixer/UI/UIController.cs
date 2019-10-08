using System;
using ColossalFramework.Packaging;
using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.Parts;
using UnityEngine;

namespace ThemeMixer
{
    public class UIController: MonoBehaviour
    {
        public event EventHandler<UIDirtyEventArgs> EventUIDirty;

        private static UIController _instance;
        public static UIController Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<UIController>();
                    if (_instance == null) {
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<UIController>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public ThemeMix Mix { get => ThemeManager.Instance.CurrentMix; set => ThemeManager.Instance.CurrentMix = value; }

        public UITextureAtlas ThemeAtlas => ThemeSprites.Atlas;


        public ThemePart Part { get; set; } = ThemePart.None;
        public TextureID TextureID { get; private set; }
        public ColorID ColorID { get; private set; }
        public OffsetID OffsetID { get; private set; }
        public ValueID ValueID { get; private set; }

        public string PackageID { get; private set; }

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        private ThemeMixerUI _ui;
        private ThemeMixerUI ThemeMixerUI {
            get {
                if (_ui == null) _ui = FindObjectOfType<ThemeMixerUI>();
                return _ui;
            }
            set { _ui = value; }
        }
        private SelectPanel ThemeSelector { get; set; }

        private UIToggle _toggle;
        private UIToggle UIToggle {
            get {
                if (_toggle == null) _toggle = FindObjectOfType<UIToggle>();
                return _toggle;
            }
            set { _toggle = value; }
        }

        public static UIController Ensure() => Instance;

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            if (UIToggle != null) {
                Destroy(UIToggle.gameObject);
                UIToggle = null;
            }
            UIToggle = UIView.GetAView().AddUIComponent(typeof(UIToggle)) as UIToggle;
            UIToggle.EventUIToggleClicked += OnUIToggleClicked;
        }

        public void OnLevelUnloaded() {
            DestroyUI();
        }

        public static void Release() {
            if (_instance != null) {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

        public bool IsSelected(Package.Asset asset) {
            return SimulationManager.instance.m_metaData.m_MapThemeMetaData?.assetRef == asset;
        }

        public void OnLoadFromTheme<T>(ThemeCategory category, T ID) {
            ThemePart part = ThemePart.None;
            if (ID is TextureID textureID) {
                part = ThemePart.Texture;
                TextureID = textureID;
            } else if (ID is ColorID colorID) {
                part = ThemePart.Color;
                ColorID = colorID;
            } else if (ID is OffsetID offsetID) {
                part = ThemePart.Offset;
                OffsetID = offsetID;
            } else if (ID is ValueID valueID) {
                part = ThemePart.Value;
                ValueID = valueID;
            }
            if (part != ThemePart.None) ShowThemeSelectorPanel(category, part);
        }

        private void Awake() {
            PanelBase.EventThemeDirty += OnThemeDirty;
            ThemeManager.Instance.EventUIDirty += OnUIDirty;
        }

        private void OnUIDirty(object sender, UIDirtyEventArgs e) {
            EventUIDirty?.Invoke(sender, e);
        }

        private void OnDestroy() {
            PanelBase.EventThemeDirty -= OnThemeDirty;
            ThemeManager.Instance.EventUIDirty -= OnUIDirty;
        }

        private void DestroyUI() {
            if (UIToggle != null) {
                Destroy(UIToggle.gameObject);
                UIToggle = null;
            }
            if (ThemeMixerUI != null) {
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
            }
        }

        private void OnUIToggleClicked() {
            if (ThemeMixerUI != null) {
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
                return;
            }
            ThemeMixerUI = UIView.GetAView().AddUIComponent(typeof(ThemeMixerUI)) as ThemeMixerUI;
        }

        public void OnTilingChanged(TextureID textureID, float value) {
            ThemeManager.Instance.OnTilingChanged(textureID, value);
        }

        private void ShowThemeSelectorPanel(ThemeCategory category, ThemePart part) {
            Part = part;
            ThemeMixerUI.isVisible = false;
            UIToggle.isInteractive = false;
            switch (category) {
                case ThemeCategory.Terrain:
                    ThemeSelector = UIView.GetAView().AddUIComponent(typeof(SelectTerrainPanel)) as SelectTerrainPanel;
                    break;
                case ThemeCategory.Water:
                    ThemeSelector = UIView.GetAView().AddUIComponent(typeof(SelectWaterPanel)) as SelectWaterPanel;
                    break;
                case ThemeCategory.Structures:
                    ThemeSelector = UIView.GetAView().AddUIComponent(typeof(SelectStructuresPanel)) as SelectStructuresPanel;
                    break;
                case ThemeCategory.Atmosphere:
                    ThemeSelector = UIView.GetAView().AddUIComponent(typeof(SelectAtmospherePanel)) as SelectAtmospherePanel;
                    break;
                case ThemeCategory.Weather:
                    ThemeSelector = UIView.GetAView().AddUIComponent(typeof(SelectWeatherPanel)) as SelectWeatherPanel;
                    break;
                default:
                    break;
            }
        }

        public void OnThemeSelectorPanelClosing(object sender, ThemesPanelClosingEventArgs e) {
            Part = ThemePart.None;
            if (ThemeSelector != null) Destroy(ThemeSelector.gameObject);
            ThemeMixerUI.isVisible = true;
            UIToggle.isInteractive = true;
        }

        public void OnThemeSelected(object sender, ThemeSelectedEventArgs e) {
            switch (e.part) {
                case ThemePart.Category:
                    ThemeManager.Instance.LoadCategory(e.category, e.packageID);
                    break;
                case ThemePart.Texture:
                    ThemeManager.Instance.LoadTexture(TextureID, e.packageID);
                    break;
                case ThemePart.Color:
                    ThemeManager.Instance.LoadColor(ColorID, e.packageID);
                    break;
                case ThemePart.Offset:
                    ThemeManager.Instance.LoadOffset(OffsetID, e.packageID);
                    break;
                case ThemePart.Value:
                    ThemeManager.Instance.LoadValue(ValueID, e.packageID);
                    break;
                default:
                    break;
            }
        }

        private void OnThemeDirty(object sender, ThemeDirtyEventArgs e) {
            ThemeManager.Instance.OnThemeDirty(e);
        }
    }
}

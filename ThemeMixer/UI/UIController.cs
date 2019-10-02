using ColossalFramework.Packaging;
using ColossalFramework.UI;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.UI.FastList;
using UnityEngine;

namespace ThemeMixer.UI
{
    public delegate void ItemClickedEventHandler(ListItem item);
    public delegate void UIDirtyEventHandler(ILoadable themePart);
    public class UIController: MonoBehaviour
    {
        public event UIDirtyEventHandler EventUIDirty;

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

        public UITextureAtlas ThemeAtlas => ThemeSprites.Atlas;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        private ThemeMixerUI ThemeMixerUI { get; set; }
        private UIToggle UIToggle { get; set; }

        public static UIController Ensure() => Instance;

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            UIToggle = UIView.GetAView().AddUIComponent(typeof(UIToggle)) as UIToggle;
            UIToggle.EventUIToggleClicked += OnUIToggleClicked;
        }

        internal void OnLevelUnloaded() {
            DestroyUI();
        }

        public static void Release() {
            if (_instance != null) {
                _instance.EventUIDirty = null;
                _instance.DestroyUI();
                Destroy(_instance.gameObject);
                _instance = null;
            }
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
                ThemeMixerUI.EventThemeClicked -= OnThemeClicked;
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
                return;
            }
            ThemeMixerUI = UIView.GetAView().AddUIComponent(typeof(ThemeMixerUI)) as ThemeMixerUI;
            ThemeMixerUI.EventThemeClicked += OnThemeClicked;
        }

        private void OnThemeClicked(ListItem item) {
            ThemeManager.Instance.ThemeClicked(item);
        }

        internal bool IsSelected(Package.Asset asset) {
            return SimulationManager.instance.m_metaData.m_MapThemeMetaData?.assetRef == asset;
        }
    }
}

using ColossalFramework.UI;
using ICities;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class UIController: MonoBehaviour
    {

        public delegate void UIDirtyEventHandler(IThemePart themePart);
        public event UIDirtyEventHandler EventUIDirty;

        private static UIController _instance;
        public static UIController Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<UIController>();
                    if (_instance == null) {
                        var gameObject = new GameObject(nameof(UIController));
                        _instance = gameObject.AddComponent<UIController>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        private bool InGame => (ToolManager.instance.m_properties.m_mode & ItemClass.Availability.GameAndMap) != 0;

        private ThemeMixerUI ThemeMixerUI { get; set; }
        private UIToggle UIToggle { get; set; }

        public static UIController Ensure() => Instance;

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            if (!InGame) return;
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
            if (ThemeMixerUI != null) {
                Destroy(ThemeMixerUI.gameObject);
                ThemeMixerUI = null;
            }
            if (UIToggle != null) {
                Destroy(UIToggle.gameObject);
                UIToggle = null;
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
    }
}

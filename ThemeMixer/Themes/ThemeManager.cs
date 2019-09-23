using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public class ThemeManager : MonoBehaviour
    {
        private static ThemeManager _instance;
        public static ThemeManager Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<ThemeManager>();
                    if (_instance == null) {
                        var gameObject = new GameObject(nameof(ThemeManager));
                        _instance = gameObject.AddComponent<ThemeManager>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public static ThemeManager Ensure() => Instance;

        private bool InGame => (ToolManager.instance.m_properties.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            if (!InGame) return;

        }

        internal void OnLevelUnloaded() {

        }

        public static void Release() {
            if (_instance != null) {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }
    }
}

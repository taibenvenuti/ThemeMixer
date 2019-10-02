using ColossalFramework.Packaging;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemeMixer.UI.FastList;
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
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<ThemeManager>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public static ThemeManager Ensure() => Instance;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public Package.Asset[] Themes { get; private set; } = ThemeUtils.GetThemes().ToArray();

        private ThemeMix CurrentMix { get; set; }

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            PackageManager.eventPackagesChanged += OnPackagesChanged;

        }

        private void OnPackagesChanged() {
            Themes = ThemeUtils.GetThemes().ToArray();
        }

        internal void OnLevelUnloaded() {

        }

        public static void Release() {
            if (_instance != null) {
                PackageManager.eventPackagesChanged -= Instance.OnPackagesChanged;
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }

        public void ThemeClicked(ListItem item) {

        }
    }
}

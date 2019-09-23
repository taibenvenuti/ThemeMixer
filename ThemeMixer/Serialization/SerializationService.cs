using ColossalFramework.IO;
using ColossalFramework.Plugins;
using ICities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    public class SerializationService : MonoBehaviour
    {
        private static SerializationService _instance;
        public static SerializationService Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<SerializationService>();
                    if (_instance == null) {
                        var gameObject = new GameObject(nameof(SerializationService));
                        _instance = gameObject.AddComponent<SerializationService>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        public static SerializationService Ensure() => Instance;

        private bool InGame => (ToolManager.instance.m_properties.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public static void Release() {
            if (_instance != null) {
                Destroy(_instance.gameObject);
                _instance = null;
            }
        }
        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            if (!InGame) return;

            LoadAvailableMixes();

            PluginManager.instance.eventPluginsChanged += OnPluginsChanged;
        }

        internal void OnLevelUnloaded() {
            PluginManager.instance.eventPluginsChanged -= OnPluginsChanged;
        }

        private const string FILE_NAME = "ThemeMixerSettings.xml";
        private string FilePath => Path.Combine(DataLocation.localApplicationData, FILE_NAME);

        private Settings _settings;
        private Settings Settings {
            get {
                if (_settings == null) {
                    _settings = LoadData() ?? new Settings();
                }
                return _settings;
            }
        }

        public Vector2? GetToolBarPosition() {
            return Settings.ToolbarPosition;
        }

        public void SetToolbarPosition(Vector3? position) {
            Settings.ToolbarPosition = position;
            SaveData();
        }

        public Vector2? GetUITogglePosition() {
            return Settings.UITogglePosition;
        }

        public void SetUITogglePosition(Vector3? position) {
            Settings.UITogglePosition = position;
        }

        public List<ThemeMix> Mixes { get; private set; } = new List<ThemeMix>();

        private void OnPluginsChanged() {
            LoadAvailableMixes();
        }

        private void LoadAvailableMixes() {
            if (Mixes.Count != 0) Mixes.Clear();
            foreach (PluginManager.PluginInfo plugin in PluginManager.instance.GetPluginsInfo()) {
                if (!plugin.isEnabled) continue;
                ThemeMix mix = LoadMix(Path.Combine(plugin.modPath, "ThemeMix.xml"));
                if (mix == null) continue;
                Mixes.Add(mix);
            }
        }

        //TODO Create directory, Source folder, .cs file, etc.
        public void SaveMix(string filePath, ThemeMix mix) {
            XmlSerializer serializer = new XmlSerializer(typeof(ThemeMix));
            using (StreamWriter writer = new StreamWriter(filePath)) {
                mix.OnPreSerialize();
                serializer.Serialize(writer, mix);
            }
            if (!Mixes.Contains(mix)) Mixes.Add(mix);
        }

        //TODO delete mix mod
        public void DeleteMix(string filePath) {
            throw new NotImplementedException();
        }

        public ThemeMix LoadMix(string filePath) {
            XmlSerializer serializer = new XmlSerializer(typeof(ThemeMix));
            try {
                using (StreamReader reader = new StreamReader(filePath)) {
                    var data = serializer.Deserialize(reader) as ThemeMix;
                    data.OnPostDeserialize();
                    return data;
                }
            } catch (Exception) {
                return null;
            }
        }

        public void SaveData() {
            string fileName = FilePath;
            Settings data = Settings;
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            using (StreamWriter writer = new StreamWriter(fileName)) {
                data.OnPreSerialize();
                serializer.Serialize(writer, data);
            }
        }

        public Settings LoadData() {
            string fileName = FilePath;
            XmlSerializer serializer = new XmlSerializer(typeof(Settings));
            try {
                using (StreamReader reader = new StreamReader(fileName)) {
                    var data = serializer.Deserialize(reader) as Settings;
                    data.OnPostDeserialize();
                    return data;
                }
            } catch (Exception) {
                return null;
            }
        }
    }
}

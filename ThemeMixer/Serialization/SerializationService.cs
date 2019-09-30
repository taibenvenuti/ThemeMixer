using ColossalFramework.IO;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using ThemeMixer.Themes;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    //TODO Implement Favs + Blacklisted
    public class SerializationService : MonoBehaviour
    {
        private static SerializationService _instance;
        public static SerializationService Instance {
            get {
                if (_instance == null) {
                    _instance = FindObjectOfType<SerializationService>();
                    if (_instance == null) {
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<SerializationService>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        private bool InGame => ToolManager.instance.m_properties != null && (ToolManager.instance.m_properties.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public bool HideBlacklisted => Data.HideBlacklisted;

        public List<string> GetFavourites(ThemePart themePart) {
            return Data.Favourites[(int)themePart];
        }

        public List<string> GetBlacklisted(ThemePart themePart) {
            return Data.Blacklisted[(int)themePart];
        }

        public void AddToFavourites(string packageName, ThemePart themePart) {
            Add(Data.Favourites, packageName, themePart);
        }

        public void RemoveFromFavourites(string packageName, ThemePart themePart) {
            Remove(Data.Favourites, packageName, themePart);
        }

        public void AddToBlacklist(string packageName, ThemePart themePart) {
            Add(Data.Blacklisted, packageName, themePart);
        }

        public void RemoveFromBlacklist(string packageName, ThemePart themePart) {
            Remove(Data.Blacklisted, packageName, themePart);
        }

        public bool IsBlacklisted(string packageName, ThemePart themePart) {
            return Data.Blacklisted[(int)themePart].Contains(packageName);
        }

        public bool IsFavourite(string packageName, ThemePart themePart) {
            return Data.Favourites[(int)themePart].Contains(packageName);
        }

        private void Add(List<string>[] listArray, string packageName, ThemePart themePart) {
            if (!listArray[(int)themePart].Contains(packageName)) {
                listArray[(int)themePart].Add(packageName);
                SaveData();
            }
        }

        private void Remove(List<string>[] listArray, string packageName, ThemePart themePart) {
            if (listArray[(int)themePart].Contains(packageName)) {
                listArray[(int)themePart].Remove(packageName);
                SaveData();
            }
        }

        public static SerializationService Ensure() => Instance;

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
            LoadAvailableMixes();

            PluginManager.instance.eventPluginsChanged += OnPluginsChanged;
        }

        internal void OnLevelUnloaded() {
            PluginManager.instance.eventPluginsChanged -= OnPluginsChanged;
        }

        private const string FILE_NAME = "ThemeMixerSettings.xml";
        private string FilePath => Path.Combine(DataLocation.localApplicationData, FILE_NAME);

        private Data _data;
        private Data Data {
            get {
                if (_data == null) {
                    _data = LoadData() ?? new Data();
                }
                return _data;
            }
        }

        public Vector2? GetToolBarPosition() {
            return Data.ToolbarPosition;
        }

        public void SetToolbarPosition(Vector3? position) {
            Data.ToolbarPosition = position;
            SaveData();
        }

        public Vector2? GetUITogglePosition() {
            return Data.UITogglePosition;
        }

        public void SetUITogglePosition(Vector3? position) {
            Data.UITogglePosition = position;
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
            Data data = Data;
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            using (StreamWriter writer = new StreamWriter(fileName)) {
                data.OnPreSerialize();
                serializer.Serialize(writer, data);
            }
        }

        public Data LoadData() {
            string fileName = FilePath;
            XmlSerializer serializer = new XmlSerializer(typeof(Data));
            try {
                using (StreamReader reader = new StreamReader(fileName)) {
                    var data = serializer.Deserialize(reader) as Data;
                    data.OnPostDeserialize();
                    return data;
                }
            } catch (Exception) {
                return null;
            }
        }
    }
}

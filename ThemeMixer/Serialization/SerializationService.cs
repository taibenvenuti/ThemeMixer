using ColossalFramework.IO;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
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
                        GameObject gameObject = GameObject.Find("ThemeMixer");
                        if (gameObject == null) gameObject = new GameObject("ThemeMixer");
                        _instance = gameObject.AddComponent<SerializationService>();
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }
                return _instance;
            }
        }

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public List<SavedSwatch> GetSavedSwatches(ColorID colorID) {
            return new List<SavedSwatch>(Data.SavedSwatches[(int)colorID]);
        }

        public void UpdateSavedSwatches(List<SavedSwatch> savedSwatches, ColorID colorID) {
            Data.SavedSwatches[(int)colorID] = new List<SavedSwatch>(savedSwatches);
            SaveData();
        }

        public void SaveLocalMix(ThemeMix mix) {
            Data.LocalMix = mix;
            SaveData();
        }

        public ThemeMix GetSavedLocalMix() {
            return Data.LocalMix;
        }

        public bool HideBlacklisted => Data.HideBlacklisted;

        public List<string> GetFavourites(ThemeCategory themePart) {
            return Data.Favourites[(int)themePart];
        }

        public List<string> GetBlacklisted(ThemeCategory themePart) {
            return Data.Blacklisted[(int)themePart];
        }

        public void AddToFavourites(string packageName, ThemeCategory themePart) {
            Add(Data.Favourites, packageName, themePart);
        }

        public void RemoveFromFavourites(string packageName, ThemeCategory themePart) {
            Remove(Data.Favourites, packageName, themePart);
        }

        public void AddToBlacklist(string packageName, ThemeCategory themePart) {
            Add(Data.Blacklisted, packageName, themePart);
        }

        public void RemoveFromBlacklist(string packageName, ThemeCategory themePart) {
            Remove(Data.Blacklisted, packageName, themePart);
        }

        public bool IsBlacklisted(string packageName, ThemeCategory themePart) {
            return Data.Blacklisted[(int)themePart].Contains(packageName);
        }

        public bool IsFavourite(string packageName, ThemeCategory themePart) {
            return Data.Favourites[(int)themePart].Contains(packageName);
        }

        internal ThemeMix GetMix(string mixID) {
            if (Mixes.TryGetValue(mixID, out ThemeMix mix)) return mix;
            return null;
        }

        private void Add(List<string>[] listArray, string packageName, ThemeCategory themePart) {
            if (!listArray[(int)themePart].Contains(packageName)) {
                listArray[(int)themePart].Add(packageName);
                SaveData();
            }
        }

        private void Remove(List<string>[] listArray, string packageName, ThemeCategory themePart) {
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

        public Dictionary<string, ThemeMix> Mixes { get; private set; } = new Dictionary<string, ThemeMix>();
        private List<string> mixIds = new List<string>();
        private List<string> mixNames = new List<string>();
        private string[] _mixNames;
        public string[] MixNames { 
            get {
                if (mixIds.Count != Mixes.Count || _mixNames == null) {
                    _mixNames = GetMixNames();
                }
                return _mixNames;
            } 
        }

        private string[] GetMixNames() {
            mixIds.Clear();
            foreach (var mix in Mixes) {
                mixIds.Add(mix.Key);
            }
            mixIds.Sort();
            mixNames.Clear();
            foreach (var mixID in mixIds) {
                mixNames.Add(Mixes[mixID].Name);
            }
            return mixNames.ToArray();
        }

        public ThemeMix GetMixByIndex(int index) {
            if (Mixes.TryGetValue(mixIds[index], out ThemeMix mix)) {
                return mix;
            }
            return null;
        }
        private void OnPluginsChanged() {
            LoadAvailableMixes();
        }

        private void LoadAvailableMixes() {
            foreach (PluginManager.PluginInfo plugin in PluginManager.instance.GetPluginsInfo()) {
                ThemeMix mix = LoadMix(plugin);
                if (mix == null) continue;
                Mixes[mix.ID] = mix;
            }
        }

        public void SaveMix(ThemeMix mix) {
            string newMixModPath = DataLocation.modsPath;
            string mixName = Regex.Replace(mix.Name, @"(\s+|@|&|'|\(|\)|<|>|#|"")", "");
            string mixDir = Path.Combine(newMixModPath, mixName);
            string mixModSourceDir = Path.Combine(mixDir, "Source");
            string code = string.Concat(
                "using ICities;",
                "\nnamespace ", mixName,
                "\n{",
                "\n\tpublic class ", mixName, "Mod : IUserMod",
                "\n\t{",
                "\n\t\tpublic string Name",
                "\n\t\t{",
                "\n\t\t\tget",
                "\n\t\t\t{",
                "\n\t\t\t\treturn \"", mixName, "\";",
                "\n\t\t\t}",
                "\n\t\t}",
                "\n\t\tpublic string Description",
                "\n\t\t{",
                "\n\t\t\tget",
                "\n\t\t\t{",
                "\n\t\t\t\treturn \"", "A theme mix for use with Theme Mixer 2.", "\";",
                "\n\t\t\t}",
                "\n\t\t}",
                "\n\t}",
                "\n}");

            if (!Directory.Exists(mixDir)) {
                try {
                    Directory.CreateDirectory(mixDir);
                } catch (Exception e) {
                    Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                    return;
                }
            }

            if (!Directory.Exists(mixModSourceDir)) {
                try {
                    Directory.CreateDirectory(mixModSourceDir);
                } catch (Exception e) {
                    Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                    return;
                }
            }

            try {
                File.WriteAllText(Path.Combine(mixModSourceDir, mixName + ".cs"), code);
            } catch (Exception e) {
                Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                return;
            }

            string xml = Path.Combine(mixDir, "ThemeMix.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ThemeMix));
            try {
                using (StreamWriter writer = new StreamWriter(xml)) {
                    mix.OnPreSerialize();
                    serializer.Serialize(writer, mix);
                }
            } catch (Exception e) {
                Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                return;
            }
            LoadAvailableMixes();
        }

        public ThemeMix LoadMix(PluginManager.PluginInfo plugin) {
            string filePath = Path.Combine(plugin.modPath, "ThemeMix.xml");
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

using ColossalFramework.IO;
using ColossalFramework.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Serialization;
using ColossalFramework.Threading;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using UnityEngine;

namespace ThemeMixer.Serialization
{
    public class SerializationService : MonoBehaviour
    {
        public delegate void ThemeMixSavedEventHandler();
        public event ThemeMixSavedEventHandler EventThemeMixSaved;

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

        private static bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

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

        private void Add(IList<List<string>> listArray, string packageName, ThemeCategory themePart) {
            if (listArray[(int)themePart].Contains(packageName)) return;
            listArray[(int)themePart].Add(packageName);
            SaveData();
        }

        private void Remove(IList<List<string>> listArray, string packageName, ThemeCategory themePart) {
            if (!listArray[(int)themePart].Contains(packageName)) return;
            listArray[(int)themePart].Remove(packageName);
            SaveData();
        }

        public static SerializationService Ensure() => Instance;

        public static void Release() {
            if (_instance == null) return;
            Destroy(_instance.gameObject);
            _instance = null;
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

        private const string FileName = "ThemeMixerSettings.xml";
        private static string FilePath => Path.Combine(DataLocation.localApplicationData, FileName);

        private Data _data;
        private Data Data => _data ?? (_data = LoadData() ?? new Data());

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

        public Dictionary<string, ThemeMix> Mixes { get; } = new Dictionary<string, ThemeMix>();
        private readonly List<string> _mixIds = new List<string>();
        private readonly List<string> _mixNamesList = new List<string>();
        private string[] _mixNames;
        public string[] MixNames {
            get {
                if (_mixIds.Count != Mixes.Count || _mixNames == null) {
                    _mixNames = GetMixNames();
                }
                return _mixNames;
            }
        }

        private string[] GetMixNames() {
            _mixIds.Clear();
            foreach (var mix in Mixes) {
                _mixIds.Add(mix.Key);
            }
            _mixIds.Sort();
            _mixNamesList.Clear();
            foreach (string mixID in _mixIds) {
                _mixNamesList.Add(Mixes[mixID].Name);
            }
            return _mixNamesList.ToArray();
        }

        public ThemeMix GetMixByIndex(int index) {
            if (_mixIds.Count == 0 || index < 0 || index >= _mixIds.Count) return null;
            return Mixes.TryGetValue(_mixIds[index], out ThemeMix mix) ? mix : null;
        }

        private void OnPluginsChanged() {
            LoadAvailableMixes();
        }

        private void LoadAvailableMixes() {
            Mixes.Clear();
            foreach (PluginManager.PluginInfo plugin in PluginManager.instance.GetPluginsInfo()) {
                ThemeMix mix = LoadMix(plugin);
                if (mix == null) continue;
                Mixes[mix.ID] = mix;
            }
            EventThemeMixSaved?.Invoke();
        }

        public void SaveMix(ThemeMix mix) {
            Task task = Task.Create(() => {
                string newMixModPath = DataLocation.modsPath;
                string mixName = Regex.Replace(mix.Name, @"(@|&|'|\(|\)|<|>|#|"")", "");
                string mixNameTypeSafe = Regex.Replace(mixName, @"(\s+|\d+)", "");
                string mixDir = Path.Combine(newMixModPath, mixName);
                string mixModSourceDir = Path.Combine(mixDir, "Source");
                var sb = new StringBuilder();
                sb.AppendLine("using ICities;");
                sb.AppendLine($"namespace {mixNameTypeSafe}");
                sb.AppendLine("{");
                sb.AppendLine($"    public class {mixNameTypeSafe}Mod : IUserMod");
                sb.AppendLine("    {");
                sb.AppendLine("        public string Name {");
                sb.AppendLine("            get {");
                sb.AppendLine($"                return \"{mixName}\";");
                sb.AppendLine("            }");
                sb.AppendLine("        }");
                sb.AppendLine("        public string Description {");
                sb.AppendLine("            get {");
                sb.AppendLine("                return \"A theme mix for use with Theme Mixer 2\";");
                sb.AppendLine("            }");
                sb.AppendLine("        }");
                sb.AppendLine("    }");
                sb.AppendLine("}");
                string code = sb.ToString();

                sb = new StringBuilder();
                foreach (string usedTheme in mix.GetUsedThemes()) {
                    sb.AppendLine(usedTheme);
                }

                string usedThemes = sb.ToString();

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
                    File.WriteAllText(Path.Combine(mixModSourceDir, mixNameTypeSafe + ".cs"), code);
                    File.WriteAllText(Path.Combine(mixDir, "UsedThemes.txt"), usedThemes);
                } catch (Exception e) {
                    Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                    return;
                }

                string xml = Path.Combine(mixDir, "ThemeMix.xml");
                var serializer = new XmlSerializer(typeof(ThemeMix));
                try {
                    using (var writer = new StreamWriter(xml)) {
                        mix.OnPreSerialize();
                        serializer.Serialize(writer, mix);
                    }
                } catch (Exception e) {
                    Debug.LogError(string.Concat("Failed Creating Theme Mix: ", e.Message));
                    return;
                }
                LoadAvailableMixes();
            });
            task.WhenEnded(task.Dispose);
            task.Run();
        }

        public ThemeMix LoadMix(PluginManager.PluginInfo plugin) {
            string filePath = Path.Combine(plugin.modPath, "ThemeMix.xml");
            if (!File.Exists(filePath)) return null;
            var serializer = new XmlSerializer(typeof(ThemeMix));
            try {
                using (var reader = new StreamReader(filePath)) {
                    var data = serializer.Deserialize(reader) as ThemeMix;
                    data?.OnPostDeserialize();
                    return data;
                }
            } catch (Exception e) {
                Debug.LogError(string.Concat("Failed Loading Theme Mix: ", e.Message));
            }
            return null;
        }

        public void SaveData() {
            string fileName = FilePath;
            Data data = Data;
            var serializer = new XmlSerializer(typeof(Data));
            using (var writer = new StreamWriter(fileName)) {
                data.OnPreSerialize();
                serializer.Serialize(writer, data);
            }
        }

        public Data LoadData() {
            string fileName = FilePath;
            var serializer = new XmlSerializer(typeof(Data));
            try {
                using (var reader = new StreamReader(fileName)) {
                    var data = serializer.Deserialize(reader) as Data;
                    data?.OnPostDeserialize();
                    return data;
                }
            } catch (Exception) {
                return null;
            }
        }

        public bool IsDefaultMix(string mixID) {
            return Data.DefaultMix == mixID;
        }

        public void SetDefaultMix(string mixID) {
            Data.DefaultMix = mixID;
            SaveData();
        }

        public ThemeMix GetDefaultMix() {
            if (string.IsNullOrEmpty(Data.DefaultMix)) return null;
            return Mixes.TryGetValue(Data.DefaultMix, out ThemeMix defaultMix) ? defaultMix : null;
        }
    }
}

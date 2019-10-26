using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using ColossalFramework.Packaging;
using ColossalFramework.PlatformServices;
using JetBrains.Annotations;
using ThemeMixer.Themes.Atmosphere;
using ThemeMixer.Themes.Structures;
using ThemeMixer.Themes.Terrain;
using ThemeMixer.Themes.Water;
using ThemeMixer.Themes.Weather;

namespace ThemeMixer.Themes
{
    public class ThemeMix
    {
        public string ID;
        public string Name;
        public ThemeTerrain Terrain;
        public ThemeWater Water;
        public ThemeAtmosphere Atmosphere;
        public ThemeStructures Structures;
        public ThemeWeather Weather;

        [UsedImplicitly]
        public ThemeMix() {
            InitializeMix();
        }

        public ThemeMix(string themeID) {
            InitializeMix();
            Load(themeID);
        }

        public void OnPreSerialize() { }

        public void OnPostDeserialize() { }

        public bool Load(string themeID = null) {
            bool success = Terrain.Load(themeID);
            if (!Water.Load(themeID)) success = false;
            if (!Atmosphere.Load(themeID)) success = false;
            if (!Structures.Load(themeID)) success = false;
            if (!Weather.Load(themeID)) success = false;
            return success;
        }

        public void SetName(string name) {
            Name = name;
            ID = string.Concat(name.Replace(" ", ""), ID.Substring(ID.IndexOf('_')));
        }

        public bool ThemesMissing() {
            RefreshSubscribedThemes();

            var missing = false;

            foreach (string packageID in Atmosphere.GetPackageIDs()) {
                if (!ThemePackageIDs.Contains(packageID)) missing = true;
            }
            foreach (string packageID in Structures.GetPackageIDs()) {
                if (!ThemePackageIDs.Contains(packageID)) missing = true;
            }
            foreach (string packageID in Terrain.GetPackageIDs()) {
                if (!ThemePackageIDs.Contains(packageID)) missing = true;
            }
            foreach (string packageID in Water.GetPackageIDs()) {
                if (!ThemePackageIDs.Contains(packageID)) missing = true;
            }
            foreach (string packageID in Weather.GetPackageIDs()) {
                if (!ThemePackageIDs.Contains(packageID)) missing = true;
            }

            return missing;
        }

        public void SubscribeMissingThemes() {
            RefreshSubscribedThemes();

            foreach (string packageID in Atmosphere.GetPackageIDs()) {
                MaybeSubscribe(packageID);
            }
            foreach (string packageID in Structures.GetPackageIDs()) {
                MaybeSubscribe(packageID);
            }
            foreach (string packageID in Terrain.GetPackageIDs()) {
                MaybeSubscribe(packageID);
            }
            foreach (string packageID in Water.GetPackageIDs()) {
                MaybeSubscribe(packageID);
            }
            foreach (string packageID in Weather.GetPackageIDs()) {
                MaybeSubscribe(packageID);
            }
        }

        private static void MaybeSubscribe(string packageID) {
            if (string.IsNullOrEmpty(packageID)) return;
            if (ThemePackageIDs.Contains(packageID)) return;
            if (ulong.TryParse(packageID, out ulong publishedFileID))
                PlatformService.workshop.Subscribe(new PublishedFileId(publishedFileID));
        }

        private static void RefreshSubscribedThemes() {
            ThemePackageIDs.Clear();
            foreach (Package.Asset mapThemeAsset in PackageManager.FilterAssets(UserAssetType.MapThemeMetaData)) {
                if (ThemePackageIDs.Contains(mapThemeAsset.fullName)) continue;
                ThemePackageIDs.Add(mapThemeAsset.fullName);
            }
        }

        private void InitializeMix() {
            ID = string.Concat(Name, "_", Guid.NewGuid().ToString("N"));
            Terrain = new ThemeTerrain();
            Water = new ThemeWater();
            Atmosphere = new ThemeAtmosphere();
            Structures = new ThemeStructures();
            Weather = new ThemeWeather();
        }

        [XmlIgnore] private static readonly List<string> ThemePackageIDs = new List<string>();

        [XmlIgnore] public bool AreThemesMissing => ThemesMissing();
    }
}

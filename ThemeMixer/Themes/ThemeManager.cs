using ColossalFramework.Packaging;
using System;
using System.Collections.Generic;
using ThemeMixer.Themes.Enums;
using ThemeMixer.Themes.Terrain;
using ThemeMixer.UI;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public class ThemeManager : MonoBehaviour
    {
        public event EventHandler<UIDirtyEventArgs> EventUIDirty;

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

        internal MapThemeMetaData GetTheme(string themeID) {
            if (Themes.TryGetValue(themeID, out MapThemeMetaData theme)) return theme;
            return null;
        }

        public static ThemeManager Ensure() => Instance;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;


        private Dictionary<string, MapThemeMetaData> _themes;
        public Dictionary<string, MapThemeMetaData> Themes => _themes ?? CacheThemes();

        private void RefreshThemes() {
            if (_themes == null) _themes = new Dictionary<string, MapThemeMetaData>();
            foreach (var asset in PackageManager.FilterAssets(UserAssetType.MapThemeMetaData)) {
                if (asset == null || asset.package == null) continue;
                if (!_themes.ContainsKey(asset.package.packageName)) {
                    _themes[asset.fullName] = asset.Instantiate<MapThemeMetaData>();
                    _themes[asset.fullName].assetRef = asset;
                }
            }
        }

        internal Color GetCurrentColor(ColorID colorID) {
            switch (colorID) {
                case ColorID.MoonInnerCorona: return (Color)(CurrentMix.Atmosphere.MoonInnerCorona.CustomValue ?? CurrentMix.Atmosphere.MoonInnerCorona.Value);
                case ColorID.MoonOuterCorona: return (Color)(CurrentMix.Atmosphere.MoonOuterCorona.CustomValue ?? CurrentMix.Atmosphere.MoonOuterCorona.Value);
                case ColorID.SkyTint: return (Color)(CurrentMix.Atmosphere.SkyTint.CustomValue ?? CurrentMix.Atmosphere.SkyTint.Value);
                case ColorID.NightHorizonColor: return (Color)(CurrentMix.Atmosphere.NightHorizonColor.CustomValue ?? CurrentMix.Atmosphere.NightHorizonColor.Value);
                case ColorID.EarlyNightZenithColor: return (Color)(CurrentMix.Atmosphere.EarlyNightZenithColor.CustomValue ?? CurrentMix.Atmosphere.EarlyNightZenithColor.Value);
                case ColorID.LateNightZenithColor: return (Color)(CurrentMix.Atmosphere.LateNightZenithColor.CustomValue ?? CurrentMix.Atmosphere.LateNightZenithColor.Value);
                case ColorID.WaterClean: return (Color)(CurrentMix.Water.WaterClean.CustomValue ?? CurrentMix.Water.WaterClean.Value);
                case ColorID.WaterDirty: return (Color)(CurrentMix.Water.WaterDirty.CustomValue ?? CurrentMix.Water.WaterDirty.Value);
                case ColorID.WaterUnder: return (Color  )(CurrentMix.Water.WaterUnder.CustomValue ?? CurrentMix.Water.WaterUnder.Value);
                default: return default;
            }
        }

        private Dictionary<string, MapThemeMetaData> CacheThemes() {
            if (_themes == null) _themes = new Dictionary<string, MapThemeMetaData>();
            _themes.Clear();
            foreach (var asset in PackageManager.FilterAssets(UserAssetType.MapThemeMetaData)) {
                if (asset == null || asset.package == null) continue;
                if (asset.fullName.Contains("CO-Winter-Theme") && !SteamHelper.IsDLCOwned(SteamHelper.DLC.SnowFallDLC)) continue;
                _themes[asset.fullName] = asset.Instantiate<MapThemeMetaData>();
                _themes[asset.fullName].assetRef = asset;

            }
            return _themes;
        }
        public ThemeMix CurrentMix { get; set; }

        public void OnEnabled() {
            if (!InGame) return;
            OnLevelLoaded();
        }

        public void OnLevelLoaded() {
            PackageManager.eventPackagesChanged += OnPackagesChanged;
            CacheThemes();
            CurrentMix = Serialization.SerializationService.Instance.GetSavedLocalMix();
            if (CurrentMix == null) {
                switch (SimulationManager.instance.m_metaData.m_environment) {
                    case "Sunny":
                        CurrentMix = new ThemeMix("1888413747.CO-Temperate-Theme");
                        break;
                    case "Europe":
                        CurrentMix = new ThemeMix("1888413747.CO-European-Theme");
                        break;
                    case "Winter":
                        CurrentMix = new ThemeMix("1888413747.CO-Winter-Theme");
                        break;
                    case "North":
                        CurrentMix = new ThemeMix("1888413747.CO-Boreal-Theme");
                        break;
                    case "Tropical":
                        CurrentMix = new ThemeMix("1888413747.CO-Tropical-Theme");
                        break;
                }
            }
        }

        private void OnPackagesChanged() {
            RefreshThemes();
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

        public void LoadCategory(ThemeCategory category, string themeID) {
            switch (category) {
                case ThemeCategory.Themes: CurrentMix = new ThemeMix(themeID); break;
                case ThemeCategory.Terrain: CurrentMix.Terrain.Load(themeID); break;
                case ThemeCategory.Water: CurrentMix.Water.Load(themeID); break;
                case ThemeCategory.Structures: CurrentMix.Structures.Load(themeID); break;
                case ThemeCategory.Atmosphere: CurrentMix.Atmosphere.Load(themeID); break;
                case ThemeCategory.Weather: CurrentMix.Weather.Load(themeID); break;
                default: break;
            }

            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        public void LoadTexture(TextureID textureID, string themeID) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture: CurrentMix.Terrain.GrassDiffuseTexture.Load(themeID); break;
                case TextureID.RuinedDiffuseTexture: CurrentMix.Terrain.RuinedDiffuseTexture.Load(themeID); break;
                case TextureID.PavementDiffuseTexture: CurrentMix.Terrain.PavementDiffuseTexture.Load(themeID); break;
                case TextureID.GravelDiffuseTexture: CurrentMix.Terrain.GravelDiffuseTexture.Load(themeID); break;
                case TextureID.CliffDiffuseTexture: CurrentMix.Terrain.CliffDiffuseTexture.Load(themeID); break;
                case TextureID.SandDiffuseTexture: CurrentMix.Terrain.SandDiffuseTexture.Load(themeID); break;
                case TextureID.OilDiffuseTexture: CurrentMix.Terrain.OilDiffuseTexture.Load(themeID); break;
                case TextureID.OreDiffuseTexture: CurrentMix.Terrain.OreDiffuseTexture.Load(themeID); break;
                case TextureID.CliffSandNormalTexture: CurrentMix.Terrain.CliffSandNormalTexture.Load(themeID); break;
                case TextureID.UpwardRoadDiffuse: CurrentMix.Structures.UpwardRoadDiffuse.Load(themeID); break;
                case TextureID.DownwardRoadDiffuse: CurrentMix.Structures.DownwardRoadDiffuse.Load(themeID); break;
                case TextureID.BuildingFloorDiffuse: CurrentMix.Structures.BuildingFloorDiffuse.Load(themeID); break;
                case TextureID.BuildingBaseDiffuse: CurrentMix.Structures.BuildingBaseDiffuse.Load(themeID); break;
                case TextureID.BuildingBaseNormal: CurrentMix.Structures.BuildingBaseNormal.Load(themeID); break;
                case TextureID.BuildingBurntDiffuse: CurrentMix.Structures.BuildingBurntDiffuse.Load(themeID); break;
                case TextureID.BuildingAbandonedDiffuse: CurrentMix.Structures.BuildingAbandonedDiffuse.Load(themeID); break;
                case TextureID.LightColorPalette: CurrentMix.Structures.LightColorPalette.Load(themeID); break;
                case TextureID.MoonTexture: CurrentMix.Atmosphere.MoonTexture.Load(themeID); break;
                case TextureID.WaterFoam: CurrentMix.Water.WaterFoam.Load(themeID); break;
                case TextureID.WaterNormal: CurrentMix.Water.WaterNormal.Load(themeID); break;
                default: break;
            }
            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        internal Color GetColor(ColorID colorID, string themeID) {
            switch (colorID) {
                case ColorID.MoonInnerCorona: return Themes[themeID].moonInnerCorona;
                case ColorID.MoonOuterCorona: return Themes[themeID].moonOuterCorona;
                case ColorID.SkyTint: return Themes[themeID].skyTint;
                case ColorID.NightHorizonColor: return Themes[themeID].nightHorizonColor;
                case ColorID.EarlyNightZenithColor: return Themes[themeID].earlyNightZenithColor;
                case ColorID.LateNightZenithColor: return Themes[themeID].lateNightZenithColor;
                case ColorID.WaterClean: return Themes[themeID].waterClean;
                case ColorID.WaterDirty: return Themes[themeID].waterDirty;
                case ColorID.WaterUnder: return Themes[themeID].waterUnder;
                default: return default;
            }
        }

        internal void OnColorChanged(ColorID colorID, Color value) {
            switch (colorID) {
                case ColorID.MoonInnerCorona: CurrentMix.Atmosphere.MoonInnerCorona.SetCustomValue(value); break;
                case ColorID.MoonOuterCorona: CurrentMix.Atmosphere.MoonOuterCorona.SetCustomValue(value); break;
                case ColorID.SkyTint: CurrentMix.Atmosphere.SkyTint.SetCustomValue(value); break;
                case ColorID.NightHorizonColor: CurrentMix.Atmosphere.NightHorizonColor.SetCustomValue(value); break;
                case ColorID.EarlyNightZenithColor: CurrentMix.Atmosphere.EarlyNightZenithColor.SetCustomValue(value); break;
                case ColorID.LateNightZenithColor: CurrentMix.Atmosphere.LateNightZenithColor.SetCustomValue(value); break;
                case ColorID.WaterClean: CurrentMix.Water.WaterClean.SetCustomValue(value); break;
                case ColorID.WaterDirty: CurrentMix.Water.WaterDirty.SetCustomValue(value); break;
                case ColorID.WaterUnder: CurrentMix.Water.WaterUnder.SetCustomValue(value); break;
            }
        }

        internal void OnTilingChanged(TextureID textureID, float value) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture: CurrentMix.Terrain.GrassDiffuseTexture.SetCustomValue(value); break;
                case TextureID.RuinedDiffuseTexture: CurrentMix.Terrain.RuinedDiffuseTexture.SetCustomValue(value); break;
                case TextureID.PavementDiffuseTexture: CurrentMix.Terrain.PavementDiffuseTexture.SetCustomValue(value); break;
                case TextureID.GravelDiffuseTexture: CurrentMix.Terrain.GravelDiffuseTexture.SetCustomValue(value); break;
                case TextureID.CliffDiffuseTexture: CurrentMix.Terrain.CliffDiffuseTexture.SetCustomValue(value); break;
                case TextureID.SandDiffuseTexture: CurrentMix.Terrain.SandDiffuseTexture.SetCustomValue(value); break;
                case TextureID.OilDiffuseTexture: CurrentMix.Terrain.OilDiffuseTexture.SetCustomValue(value); break;
                case TextureID.OreDiffuseTexture: CurrentMix.Terrain.OreDiffuseTexture.SetCustomValue(value); break;
                case TextureID.CliffSandNormalTexture: CurrentMix.Terrain.CliffSandNormalTexture.SetCustomValue(value); break;
                default: break;
            }
        }

        internal void OnValueChanged<T>(ValueID valueID, T value) {
            switch (valueID) {
                case ValueID.Longitude: CurrentMix.Atmosphere.Longitude.SetCustomValue(value); break;
                case ValueID.Latitude: CurrentMix.Atmosphere.Latitude.SetCustomValue(value); break;
                case ValueID.SunSize: CurrentMix.Atmosphere.SunSize.SetCustomValue(value); break;
                case ValueID.SunAnisotropy: CurrentMix.Atmosphere.SunAnisotropy.SetCustomValue(value); break;
                case ValueID.MoonSize: CurrentMix.Atmosphere.MoonSize.SetCustomValue(value); break;
                case ValueID.Rayleigh: CurrentMix.Atmosphere.Rayleight.SetCustomValue(value); break;
                case ValueID.Mie: CurrentMix.Atmosphere.Mie.SetCustomValue(value); break;
                case ValueID.Exposure: CurrentMix.Atmosphere.Exposure.SetCustomValue(value); break;
                case ValueID.StarsIntensity: CurrentMix.Atmosphere.StarsIntensity.SetCustomValue(value); break;
                case ValueID.OuterSpaceIntensity: CurrentMix.Atmosphere.OuterSpaceIntensity.SetCustomValue(value); break;
                case ValueID.GrassDetailEnabled: CurrentMix.Terrain.GrassDetailEnabled.SetCustomValue(value); break;
                case ValueID.FertileDetailEnabled: CurrentMix.Terrain.FertileDetailEnabled.SetCustomValue(value); break;
                case ValueID.RocksDetailEnabled: CurrentMix.Terrain.RocksDetailEnabled.SetCustomValue(value); break;
                case ValueID.MinTemperatureDay: CurrentMix.Weather.MinTemperatureDay.SetCustomValue(value); break;
                case ValueID.MaxTemperatureDay: CurrentMix.Weather.MaxTemperatureDay.SetCustomValue(value); break;
                case ValueID.MinTemperatureNight: CurrentMix.Weather.MinTemperatureNight.SetCustomValue(value); break;
                case ValueID.MaxTemperatureNight: CurrentMix.Weather.MaxTemperatureNight.SetCustomValue(value); break;
                case ValueID.MinTemperatureRain: CurrentMix.Weather.MinTemperatureRain.SetCustomValue(value); break;
                case ValueID.MaxTemperatureRain: CurrentMix.Weather.MaxTemperatureRain.SetCustomValue(value); break;
                case ValueID.MinTemperatureFog: CurrentMix.Weather.MinTemperatureFog.SetCustomValue(value); break;
                case ValueID.MaxTemperatureFog: CurrentMix.Weather.MaxTemperatureFog.SetCustomValue(value); break;
                case ValueID.RainProbabilityDay: CurrentMix.Weather.RainProbabilityDay.SetCustomValue(value); break;
                case ValueID.RainProbabilityNight: CurrentMix.Weather.RainProbabilityNight.SetCustomValue(value); break;
                case ValueID.FogProbabilityDay: CurrentMix.Weather.FogProbabilityDay.SetCustomValue(value); break;
                case ValueID.FogProbabilityNight: CurrentMix.Weather.FogProbabilityNight.SetCustomValue(value); break;
                case ValueID.NorthernLightsProbability: CurrentMix.Weather.NorthernLightsProbability.SetCustomValue(value); break;
                default: break;
            }
        }

        internal void OnOffsetChanged(OffsetID offsetID, Vector3 value) {
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset: CurrentMix.Terrain.GrassPollutionColorOffset.SetCustomValue(value); break;
                case OffsetID.GrassFieldColorOffset: CurrentMix.Terrain.GrassFieldColorOffset.SetCustomValue(value); break;
                case OffsetID.GrassFertilityColorOffset: CurrentMix.Terrain.GrassFertilityColorOffset.SetCustomValue(value); break;
                case OffsetID.GrassForestColorOffset: CurrentMix.Terrain.GrassForestColorOffset.SetCustomValue(value); break;
                default: break;
            }
        }

        public void LoadColor(ColorID colorID, string themeID) {
            switch (colorID) {
                case ColorID.MoonInnerCorona: CurrentMix.Atmosphere.MoonInnerCorona.Load(themeID); break;
                case ColorID.MoonOuterCorona: CurrentMix.Atmosphere.MoonOuterCorona.Load(themeID); break;
                case ColorID.SkyTint: CurrentMix.Atmosphere.SkyTint.Load(themeID); break;
                case ColorID.NightHorizonColor: CurrentMix.Atmosphere.NightHorizonColor.Load(themeID); break;
                case ColorID.EarlyNightZenithColor: CurrentMix.Atmosphere.EarlyNightZenithColor.Load(themeID); break;
                case ColorID.LateNightZenithColor: CurrentMix.Atmosphere.LateNightZenithColor.Load(themeID); break;
                case ColorID.WaterClean: CurrentMix.Water.WaterClean.Load(themeID); break;
                case ColorID.WaterDirty: CurrentMix.Water.WaterDirty.Load(themeID); break;
                case ColorID.WaterUnder: CurrentMix.Water.WaterUnder.Load(themeID); break;
                default: break;
            }
            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        public void OnThemeDirty(ThemeDirtyEventArgs e) {
            throw new NotImplementedException();
        }

        public void LoadOffset(OffsetID offsetID, string themeID) {
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset: CurrentMix.Terrain.GrassPollutionColorOffset.Load(themeID); break;
                case OffsetID.GrassFieldColorOffset: CurrentMix.Terrain.GrassFieldColorOffset.Load(themeID); break;
                case OffsetID.GrassFertilityColorOffset: CurrentMix.Terrain.GrassFertilityColorOffset.Load(themeID); break;
                case OffsetID.GrassForestColorOffset: CurrentMix.Terrain.GrassForestColorOffset.Load(themeID); break;
                default: break;
            }
            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        public void LoadValue(ValueID valueID, string themeID) {
            switch (valueID) {
                case ValueID.Longitude: CurrentMix.Atmosphere.Longitude.Load(themeID); break;
                case ValueID.Latitude: CurrentMix.Atmosphere.Latitude.Load(themeID); break;
                case ValueID.SunSize: CurrentMix.Atmosphere.SunSize.Load(themeID); break;
                case ValueID.SunAnisotropy: CurrentMix.Atmosphere.SunAnisotropy.Load(themeID); break;
                case ValueID.MoonSize: CurrentMix.Atmosphere.MoonSize.Load(themeID); break;
                case ValueID.Rayleigh: CurrentMix.Atmosphere.Rayleight.Load(themeID); break;
                case ValueID.Mie: CurrentMix.Atmosphere.Mie.Load(themeID); break;
                case ValueID.Exposure: CurrentMix.Atmosphere.Exposure.Load(themeID); break;
                case ValueID.StarsIntensity: CurrentMix.Atmosphere.StarsIntensity.Load(themeID); break;
                case ValueID.OuterSpaceIntensity: CurrentMix.Atmosphere.OuterSpaceIntensity.Load(themeID); break;
                case ValueID.GrassDetailEnabled: CurrentMix.Terrain.GrassDetailEnabled.Load(themeID); break;
                case ValueID.FertileDetailEnabled: CurrentMix.Terrain.FertileDetailEnabled.Load(themeID); break;
                case ValueID.RocksDetailEnabled: CurrentMix.Terrain.RocksDetailEnabled.Load(themeID); break;
                case ValueID.MinTemperatureDay: CurrentMix.Weather.MinTemperatureDay.Load(themeID); break;
                case ValueID.MaxTemperatureDay: CurrentMix.Weather.MaxTemperatureDay.Load(themeID); break;
                case ValueID.MinTemperatureNight: CurrentMix.Weather.MinTemperatureNight.Load(themeID); break;
                case ValueID.MaxTemperatureNight: CurrentMix.Weather.MaxTemperatureNight.Load(themeID); break;
                case ValueID.MinTemperatureRain: CurrentMix.Weather.MinTemperatureRain.Load(themeID); break;
                case ValueID.MaxTemperatureRain: CurrentMix.Weather.MaxTemperatureRain.Load(themeID); break;
                case ValueID.MinTemperatureFog: CurrentMix.Weather.MinTemperatureFog.Load(themeID); break;
                case ValueID.MaxTemperatureFog: CurrentMix.Weather.MaxTemperatureFog.Load(themeID); break;
                case ValueID.RainProbabilityDay: CurrentMix.Weather.RainProbabilityDay.Load(themeID); break;
                case ValueID.RainProbabilityNight: CurrentMix.Weather.RainProbabilityNight.Load(themeID); break;
                case ValueID.FogProbabilityDay: CurrentMix.Weather.FogProbabilityDay.Load(themeID); break;
                case ValueID.FogProbabilityNight: CurrentMix.Weather.FogProbabilityNight.Load(themeID); break;
                case ValueID.NorthernLightsProbability: CurrentMix.Weather.NorthernLightsProbability.Load(themeID); break;
                default: break;
            }
            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        public float GetTilingValue(TextureID textureID) {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (textureID) {
                case TextureID.GrassDiffuseTexture: return (float)(CurrentMix.Terrain.GrassDiffuseTexture.CustomValue ?? CurrentMix.Terrain.GrassDiffuseTexture.Value);
                case TextureID.RuinedDiffuseTexture: return (float)(CurrentMix.Terrain.RuinedDiffuseTexture.CustomValue ?? CurrentMix.Terrain.RuinedDiffuseTexture.Value);
                case TextureID.PavementDiffuseTexture: return (float)(CurrentMix.Terrain.PavementDiffuseTexture.CustomValue ?? CurrentMix.Terrain.PavementDiffuseTexture.Value);
                case TextureID.GravelDiffuseTexture: return (float)(CurrentMix.Terrain.GravelDiffuseTexture.CustomValue ?? CurrentMix.Terrain.GravelDiffuseTexture.Value);
                case TextureID.CliffDiffuseTexture: return (float)(CurrentMix.Terrain.CliffDiffuseTexture.CustomValue ?? CurrentMix.Terrain.CliffDiffuseTexture.Value);
                case TextureID.SandDiffuseTexture: return (float)(CurrentMix.Terrain.SandDiffuseTexture.CustomValue ?? CurrentMix.Terrain.SandDiffuseTexture.Value);
                case TextureID.OilDiffuseTexture: return (float)(CurrentMix.Terrain.OilDiffuseTexture.CustomValue ?? CurrentMix.Terrain.OilDiffuseTexture.Value);
                case TextureID.OreDiffuseTexture: return (float)(CurrentMix.Terrain.OreDiffuseTexture.CustomValue ?? CurrentMix.Terrain.OreDiffuseTexture.Value);
                case TextureID.CliffSandNormalTexture: return (float)(CurrentMix.Terrain.CliffSandNormalTexture.CustomValue ?? CurrentMix.Terrain.CliffSandNormalTexture.Value);
                default: return 0.5f;
            }
        }

        public Vector3 GetOffsetValue(OffsetID offsetID) {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset: return (Vector3)(CurrentMix.Terrain.GrassPollutionColorOffset.CustomValue ?? CurrentMix.Terrain.GrassPollutionColorOffset.Value);
                case OffsetID.GrassFieldColorOffset: return (Vector3)(CurrentMix.Terrain.GrassFieldColorOffset.CustomValue ?? CurrentMix.Terrain.GrassFieldColorOffset.Value);
                case OffsetID.GrassFertilityColorOffset: return (Vector3)(CurrentMix.Terrain.GrassFertilityColorOffset.CustomValue ?? CurrentMix.Terrain.GrassFertilityColorOffset.Value);
                case OffsetID.GrassForestColorOffset: return (Vector3)(CurrentMix.Terrain.GrassForestColorOffset.CustomValue ?? CurrentMix.Terrain.GrassForestColorOffset.Value);
                default: return Vector3.zero;
            }
        }

        public T GetValue<T>(ValueID valueID) {
            TerrainProperties terrain = TerrainManager.instance.m_properties;
            DayNightProperties atmosphere = DayNightProperties.instance;
            WeatherProperties weather = WeatherManager.instance.m_properties;
            switch (valueID) {
                case ValueID.Longitude: return (T)(CurrentMix.Atmosphere.Longitude.CustomValue ?? CurrentMix.Atmosphere.Longitude.Value);
                case ValueID.Latitude: return (T)(CurrentMix.Atmosphere.Latitude.CustomValue ?? CurrentMix.Atmosphere.Latitude.Value);
                case ValueID.SunSize: return (T)(CurrentMix.Atmosphere.SunSize.CustomValue ?? CurrentMix.Atmosphere.SunSize.Value);
                case ValueID.SunAnisotropy: return (T)(CurrentMix.Atmosphere.SunAnisotropy.CustomValue ?? CurrentMix.Atmosphere.SunAnisotropy.Value);
                case ValueID.MoonSize: return (T)(CurrentMix.Atmosphere.MoonSize.CustomValue ?? CurrentMix.Atmosphere.MoonSize.Value);
                case ValueID.Rayleigh: return (T)(CurrentMix.Atmosphere.Rayleight.CustomValue ?? CurrentMix.Atmosphere.Rayleight.Value);
                case ValueID.Mie: return (T)(CurrentMix.Atmosphere.Mie.CustomValue ?? CurrentMix.Atmosphere.Mie.Value);
                case ValueID.Exposure: return (T)(CurrentMix.Atmosphere.Exposure.CustomValue ?? CurrentMix.Atmosphere.Exposure.Value);
                case ValueID.StarsIntensity: return (T)(CurrentMix.Atmosphere.StarsIntensity.CustomValue ?? CurrentMix.Atmosphere.StarsIntensity.Value);
                case ValueID.OuterSpaceIntensity: return (T)(CurrentMix.Atmosphere.OuterSpaceIntensity.CustomValue ?? CurrentMix.Atmosphere.OuterSpaceIntensity.Value);
                case ValueID.GrassDetailEnabled: return (T)(CurrentMix.Terrain.GrassDetailEnabled.CustomValue ?? CurrentMix.Terrain.GrassDetailEnabled.Value);
                case ValueID.FertileDetailEnabled: return (T)(CurrentMix.Terrain.FertileDetailEnabled.CustomValue ?? CurrentMix.Terrain.FertileDetailEnabled.Value);
                case ValueID.RocksDetailEnabled: return (T)(CurrentMix.Terrain.RocksDetailEnabled.CustomValue ?? CurrentMix.Terrain.RocksDetailEnabled.Value);
                case ValueID.MinTemperatureDay: return (T)(CurrentMix.Weather.MinTemperatureDay.CustomValue ?? CurrentMix.Weather.MinTemperatureDay.Value);
                case ValueID.MaxTemperatureDay: return (T)(CurrentMix.Weather.MaxTemperatureDay.CustomValue ?? CurrentMix.Weather.MaxTemperatureDay.Value);
                case ValueID.MinTemperatureNight: return (T)(CurrentMix.Weather.MinTemperatureNight.CustomValue ?? CurrentMix.Weather.MinTemperatureNight.Value);
                case ValueID.MaxTemperatureNight: return (T)(CurrentMix.Weather.MaxTemperatureNight.CustomValue ?? CurrentMix.Weather.MaxTemperatureNight.Value);
                case ValueID.MinTemperatureRain: return (T)(CurrentMix.Weather.MinTemperatureRain.CustomValue ?? CurrentMix.Weather.MinTemperatureRain.Value);
                case ValueID.MaxTemperatureRain: return (T)(CurrentMix.Weather.MaxTemperatureRain.CustomValue ?? CurrentMix.Weather.MaxTemperatureRain.Value);
                case ValueID.MinTemperatureFog: return (T)(CurrentMix.Weather.MinTemperatureFog.CustomValue ?? CurrentMix.Weather.MinTemperatureFog.Value);
                case ValueID.MaxTemperatureFog: return (T)(CurrentMix.Weather.MaxTemperatureFog.CustomValue ?? CurrentMix.Weather.MaxTemperatureFog.Value);
                case ValueID.RainProbabilityDay: return (T)(CurrentMix.Weather.RainProbabilityDay.CustomValue ?? CurrentMix.Weather.RainProbabilityDay.Value);
                case ValueID.RainProbabilityNight: return (T)(CurrentMix.Weather.RainProbabilityNight.CustomValue ?? CurrentMix.Weather.RainProbabilityNight.Value);
                case ValueID.FogProbabilityDay: return (T)(CurrentMix.Weather.FogProbabilityDay.CustomValue ?? CurrentMix.Weather.FogProbabilityDay.Value);
                case ValueID.FogProbabilityNight: return (T)(CurrentMix.Weather.FogProbabilityNight.CustomValue ?? CurrentMix.Weather.FogProbabilityNight.Value);
                case ValueID.NorthernLightsProbability: return (T)(CurrentMix.Weather.NorthernLightsProbability.CustomValue ?? CurrentMix.Weather.NorthernLightsProbability.Value);
                default: return default;
            }
        }

        public string GetTextureThemeID(TextureID textureID) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture: return CurrentMix.Terrain.GrassDiffuseTexture.ThemeID;
                case TextureID.RuinedDiffuseTexture: return CurrentMix.Terrain.RuinedDiffuseTexture.ThemeID;
                case TextureID.PavementDiffuseTexture: return CurrentMix.Terrain.PavementDiffuseTexture.ThemeID;
                case TextureID.GravelDiffuseTexture: return CurrentMix.Terrain.GravelDiffuseTexture.ThemeID;
                case TextureID.CliffDiffuseTexture: return CurrentMix.Terrain.CliffDiffuseTexture.ThemeID;
                case TextureID.SandDiffuseTexture: return CurrentMix.Terrain.SandDiffuseTexture.ThemeID;
                case TextureID.OilDiffuseTexture: return CurrentMix.Terrain.OilDiffuseTexture.ThemeID;
                case TextureID.OreDiffuseTexture: return CurrentMix.Terrain.OreDiffuseTexture.ThemeID;
                case TextureID.CliffSandNormalTexture: return CurrentMix.Terrain.CliffSandNormalTexture.ThemeID;
                case TextureID.UpwardRoadDiffuse: return CurrentMix.Structures.UpwardRoadDiffuse.ThemeID;
                case TextureID.DownwardRoadDiffuse: return CurrentMix.Structures.DownwardRoadDiffuse.ThemeID;
                case TextureID.BuildingFloorDiffuse: return CurrentMix.Structures.BuildingFloorDiffuse.ThemeID;
                case TextureID.BuildingBaseDiffuse: return CurrentMix.Structures.BuildingBaseDiffuse.ThemeID;
                case TextureID.BuildingBaseNormal: return CurrentMix.Structures.BuildingBaseNormal.ThemeID;
                case TextureID.BuildingBurntDiffuse: return CurrentMix.Structures.BuildingBurntDiffuse.ThemeID;
                case TextureID.BuildingAbandonedDiffuse: return CurrentMix.Structures.BuildingAbandonedDiffuse.ThemeID;
                case TextureID.LightColorPalette: return CurrentMix.Structures.LightColorPalette.ThemeID;
                case TextureID.MoonTexture: return CurrentMix.Atmosphere.MoonTexture.ThemeID;
                case TextureID.WaterFoam: return CurrentMix.Water.WaterFoam.ThemeID;
                case TextureID.WaterNormal: return CurrentMix.Water.WaterNormal.ThemeID;
                default: return null;
            }
        }
    }
}

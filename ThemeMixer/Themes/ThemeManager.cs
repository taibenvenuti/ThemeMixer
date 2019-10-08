using ColossalFramework.Packaging;
using System;
using System.Linq;
using ThemeMixer.Themes.Enums;
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

        public static ThemeManager Ensure() => Instance;

        private bool InGame => ToolManager.instance?.m_properties != null && (ToolManager.instance.m_properties?.m_mode & ItemClass.Availability.GameAndMap) != 0;

        public Package.Asset[] Themes { get; private set; } = ThemeUtils.GetThemes().ToArray();

        private ThemeMix _currentMix;
        public ThemeMix CurrentMix {
            get {
                if (_currentMix == null) _currentMix = new ThemeMix();
                return _currentMix;
            }
            set {
                _currentMix = value;
            }
        }

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

        public void LoadCategory(ThemeCategory category, string packageID) {
            switch (category) {
                case ThemeCategory.Themes:
                    CurrentMix = new ThemeMix(packageID);
                    break;
                case ThemeCategory.Terrain:
                    CurrentMix.Terrain.Load(packageID);
                    break;
                case ThemeCategory.Water:
                    CurrentMix.Water.Load(packageID);
                    break;
                case ThemeCategory.Structures:
                    CurrentMix.Structures.Load(packageID);
                    break;
                case ThemeCategory.Atmosphere:
                    CurrentMix.Atmosphere.Load(packageID);
                    break;
                case ThemeCategory.Weather:
                    CurrentMix.Weather.Load(packageID);
                    break;
                default:
                    break;
            }

            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        public void LoadTexture(TextureID textureID, string packageID) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    CurrentMix.Terrain.GrassDiffuseTexture.Load(packageID);
                    break;
                case TextureID.RuinedDiffuseTexture:
                    CurrentMix.Terrain.RuinedDiffuseTexture.Load(packageID);
                    break;
                case TextureID.PavementDiffuseTexture:
                    CurrentMix.Terrain.PavementDiffuseTexture.Load(packageID);
                    break;
                case TextureID.GravelDiffuseTexture:
                    CurrentMix.Terrain.GravelDiffuseTexture.Load(packageID);
                    break;
                case TextureID.CliffDiffuseTexture:
                    CurrentMix.Terrain.CliffDiffuseTexture.Load(packageID);
                    break;
                case TextureID.SandDiffuseTexture:
                    CurrentMix.Terrain.SandDiffuseTexture.Load(packageID);
                    break;
                case TextureID.OilDiffuseTexture:
                    CurrentMix.Terrain.OilDiffuseTexture.Load(packageID);
                    break;
                case TextureID.OreDiffuseTexture:
                    CurrentMix.Terrain.OreDiffuseTexture.Load(packageID);
                    break;
                case TextureID.CliffSandNormalTexture:
                    CurrentMix.Terrain.CliffSandNormalTexture.Load(packageID);
                    break;
                case TextureID.UpwardRoadDiffuse:
                    CurrentMix.Structures.UpwardRoadDiffuse.Load(packageID);
                    break;
                case TextureID.DownwardRoadDiffuse:
                    CurrentMix.Structures.DownwardRoadDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingFloorDiffuse:
                    CurrentMix.Structures.BuildingFloorDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingBaseDiffuse:
                    CurrentMix.Structures.BuildingBaseDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingBaseNormal:
                    CurrentMix.Structures.BuildingBaseNormal.Load(packageID);
                    break;
                case TextureID.BuildingBurntDiffuse:
                    CurrentMix.Structures.BuildingBurntDiffuse.Load(packageID);
                    break;
                case TextureID.BuildingAbandonedDiffuse:
                    CurrentMix.Structures.BuildingAbandonedDiffuse.Load(packageID);
                    break;
                case TextureID.LightColorPalette:
                    CurrentMix.Structures.LightColorPalette.Load(packageID);
                    break;
                case TextureID.MoonTexture:
                    CurrentMix.Atmosphere.MoonTexture.Load(packageID);
                    break;
                case TextureID.WaterFoam:
                    CurrentMix.Water.WaterFoam.Load(packageID);
                    break;
                case TextureID.WaterNormal:
                    CurrentMix.Water.WaterNormal.Load(packageID);
                    break;
                default:
                    break;
            }
            EventUIDirty?.Invoke(this, new UIDirtyEventArgs(CurrentMix));
        }

        internal void OnTilingChanged(TextureID textureID, float value) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    CurrentMix.Terrain.GrassDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.RuinedDiffuseTexture:
                    CurrentMix.Terrain.RuinedDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.PavementDiffuseTexture:
                    CurrentMix.Terrain.PavementDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.GravelDiffuseTexture:
                    CurrentMix.Terrain.GravelDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.CliffDiffuseTexture:
                    CurrentMix.Terrain.CliffDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.SandDiffuseTexture:
                    CurrentMix.Terrain.SandDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.OilDiffuseTexture:
                    CurrentMix.Terrain.OilDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.OreDiffuseTexture:
                    CurrentMix.Terrain.OreDiffuseTexture.SetCustomValue(value);
                    break;
                case TextureID.CliffSandNormalTexture:
                    CurrentMix.Terrain.CliffSandNormalTexture.SetCustomValue(value);
                    break;
                default:
                    break;
            }
        }

        public void LoadColor(ColorID colorID, string packageID) {
            switch (colorID) {
                case ColorID.MoonInnerCorona:
                    CurrentMix.Atmosphere.MoonInnerCorona.Load(packageID);
                    break;
                case ColorID.MoonOuterCorona:
                    CurrentMix.Atmosphere.MoonOuterCorona.Load(packageID);
                    break;
                case ColorID.SkyTint:
                    CurrentMix.Atmosphere.SkyTint.Load(packageID);
                    break;
                case ColorID.NightHorizonColor:
                    CurrentMix.Atmosphere.NightHorizonColor.Load(packageID);
                    break;
                case ColorID.EarlyNightZenithColor:
                    CurrentMix.Atmosphere.EarlyNightZenithColor.Load(packageID);
                    break;
                case ColorID.LateNightZenithColor:
                    CurrentMix.Atmosphere.LateNightZenithColor.Load(packageID);
                    break;
                case ColorID.WaterClean:
                    CurrentMix.Water.WaterClean.Load(packageID);
                    break;
                case ColorID.WaterDirty:
                    CurrentMix.Water.WaterDirty.Load(packageID);
                    break;
                case ColorID.WaterUnder:
                    CurrentMix.Water.WaterUnder.Load(packageID);
                    break;
                default:
                    break;
            }
        }

        public void OnThemeDirty(ThemeDirtyEventArgs e) {
            throw new NotImplementedException();
        }

        public void LoadOffset(OffsetID offsetID, string packageID) {
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset:
                    CurrentMix.Terrain.GrassPollutionColorOffset.Load(packageID);
                    break;
                case OffsetID.GrassFieldColorOffset:
                    CurrentMix.Terrain.GrassFieldColorOffset.Load(packageID);
                    break;
                case OffsetID.GrassFertilityColorOffset:
                    CurrentMix.Terrain.GrassFertilityColorOffset.Load(packageID);
                    break;
                case OffsetID.GrassForestColorOffset:
                    CurrentMix.Terrain.GrassForestColorOffset.Load(packageID);
                    break;
                default:
                    break;
            }
        }

        public void LoadValue(ValueID valueID, string packageID) {
            switch (valueID) {
                case ValueID.Longitude:
                    CurrentMix.Atmosphere.Longitude.Load(packageID);
                    break;
                case ValueID.Latitude:
                    CurrentMix.Atmosphere.Latitude.Load(packageID);
                    break;
                case ValueID.SunSize:
                    CurrentMix.Atmosphere.SunSize.Load(packageID);
                    break;
                case ValueID.SunAnisotropy:
                    CurrentMix.Atmosphere.SunAnisotropy.Load(packageID);
                    break;
                case ValueID.MoonSize:
                    CurrentMix.Atmosphere.MoonSize.Load(packageID);
                    break;
                case ValueID.Rayleight:
                    CurrentMix.Atmosphere.Rayleight.Load(packageID);
                    break;
                case ValueID.Mie:
                    CurrentMix.Atmosphere.Mie.Load(packageID);
                    break;
                case ValueID.Exposure:
                    CurrentMix.Atmosphere.Exposure.Load(packageID);
                    break;
                case ValueID.StarsIntensity:
                    CurrentMix.Atmosphere.StarsIntensity.Load(packageID);
                    break;
                case ValueID.OuterSpaceIntensity:
                    CurrentMix.Atmosphere.OuterSpaceIntensity.Load(packageID);
                    break;
                case ValueID.GrassDetailEnabled:
                    CurrentMix.Terrain.GrassDetailEnabled.Load(packageID);
                    break;
                case ValueID.FertileDetailEnabled:
                    CurrentMix.Terrain.FertileDetailEnabled.Load(packageID);
                    break;
                case ValueID.RocksDetailEnabled:
                    CurrentMix.Terrain.RocksDetailEnabled.Load(packageID);
                    break;
                case ValueID.MinTemperatureDay:
                    CurrentMix.Weather.MinTemperatureDay.Load(packageID);
                    break;
                case ValueID.MaxTemperatureDay:
                    CurrentMix.Weather.MaxTemperatureDay.Load(packageID);
                    break;
                case ValueID.MinTemperatureNight:
                    CurrentMix.Weather.MinTemperatureNight.Load(packageID);
                    break;
                case ValueID.MaxTemperatureNight:
                    CurrentMix.Weather.MaxTemperatureNight.Load(packageID);
                    break;
                case ValueID.MinTemperatureRain:
                    CurrentMix.Weather.MinTemperatureRain.Load(packageID);
                    break;
                case ValueID.MaxTemperatureRain:
                    CurrentMix.Weather.MaxTemperatureRain.Load(packageID);
                    break;
                case ValueID.MinTemperatureFog:
                    CurrentMix.Weather.MinTemperatureFog.Load(packageID);
                    break;
                case ValueID.MaxTemperatureFog:
                    CurrentMix.Weather.MaxTemperatureFog.Load(packageID);
                    break;
                case ValueID.RainProbabilityDay:
                    CurrentMix.Weather.RainProbabilityDay.Load(packageID);
                    break;
                case ValueID.RainProbabilityNight:
                    CurrentMix.Weather.RainProbabilityNight.Load(packageID);
                    break;
                case ValueID.FogProbabilityDay:
                    CurrentMix.Weather.FogProbabilityDay.Load(packageID);
                    break;
                case ValueID.FogProbabilityNight:
                    CurrentMix.Weather.FogProbabilityNight.Load(packageID);
                    break;
                case ValueID.NorthernLightsProbability:
                    CurrentMix.Weather.NorthernLightsProbability.Load(packageID);
                    break;
                default:
                    break;
            }
        }
    }
}

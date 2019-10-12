using ColossalFramework.Packaging;
using System.Collections.Generic;
using System.Linq;
using ThemeMixer.Locale;
using ThemeMixer.Themes.Enums;
using UnityEngine;

namespace ThemeMixer.Themes
{
    public static class ThemeUtils
    {
        internal static MapThemeMetaData GetThemeFromPackage(string packageID) {
            Package package = PackageManager.GetPackage(packageID);
            if (package == null) {
                UI.UIUtils.ShowExceptionPanel(TranslationID.ERROR_MISSING_PACKAGE_TITLE, TranslationID.ERROR_MISSING_PACKAGE_MESSAGE, true);
                Debug.LogError($"No Package named {packageID} was found.");
                return null;
            }

            MapThemeMetaData metaData = package.FilterAssets(new Package.AssetType[] { UserAssetType.MapThemeMetaData })?
                                                 .FirstOrDefault()?.Instantiate<MapThemeMetaData>();
            if (metaData == null) {
                UI.UIUtils.ShowExceptionPanel(TranslationID.ERROR_BROKEN_PACKAGE_TITLE, TranslationID.ERROR_BROKEN_PACKAGE_MESSAGE, true);
                Debug.LogError($"The {packageID} package is not a Map Theme.");
                return null;
            }
            return metaData;
        }

        public static IEnumerable<Package.Asset> GetThemes() {
            return PackageManager.FilterAssets(UserAssetType.MapThemeMetaData);
        }

        public static float GetTilingValue(TextureID textureID) {
            ThemeMix mix = ThemeManager.Instance.CurrentMix;
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    return (float)(mix.Terrain.GrassDiffuseTexture.CustomValue ?? mix.Terrain.GrassDiffuseTexture.Value);
                case TextureID.RuinedDiffuseTexture:
                    return (float)(mix.Terrain.RuinedDiffuseTexture.CustomValue ?? mix.Terrain.RuinedDiffuseTexture.Value);
                case TextureID.PavementDiffuseTexture:
                    return (float)(mix.Terrain.PavementDiffuseTexture.CustomValue ?? mix.Terrain.PavementDiffuseTexture.Value);
                case TextureID.GravelDiffuseTexture:
                    return (float)(mix.Terrain.GravelDiffuseTexture.CustomValue ?? mix.Terrain.GravelDiffuseTexture.Value);
                case TextureID.CliffDiffuseTexture:
                    return (float)(mix.Terrain.CliffDiffuseTexture.CustomValue ?? mix.Terrain.CliffDiffuseTexture.Value);
                case TextureID.SandDiffuseTexture:
                    return (float)(mix.Terrain.SandDiffuseTexture.CustomValue ?? mix.Terrain.SandDiffuseTexture.Value);
                case TextureID.OilDiffuseTexture:
                    return (float)(mix.Terrain.OilDiffuseTexture.CustomValue ?? mix.Terrain.OilDiffuseTexture.Value);
                case TextureID.OreDiffuseTexture:
                    return (float)(mix.Terrain.OreDiffuseTexture.CustomValue ?? mix.Terrain.OreDiffuseTexture.Value);
                case TextureID.CliffSandNormalTexture:
                    return (float)(mix.Terrain.CliffSandNormalTexture.CustomValue ?? mix.Terrain.CliffSandNormalTexture.Value);
                default: return 0.5f;
            }
        }

        internal static Vector3 GetOffsetValue(OffsetID offsetID) {
            ThemeMix mix = ThemeManager.Instance.CurrentMix;
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset: return (Vector3)(mix.Terrain.GrassPollutionColorOffset.CustomValue ?? mix.Terrain.GrassPollutionColorOffset.Value);
                case OffsetID.GrassFieldColorOffset: return (Vector3)(mix.Terrain.GrassFieldColorOffset.CustomValue ?? mix.Terrain.GrassFieldColorOffset.Value);
                case OffsetID.GrassFertilityColorOffset: return (Vector3)(mix.Terrain.GrassFertilityColorOffset.CustomValue ?? mix.Terrain.GrassFertilityColorOffset.Value);
                case OffsetID.GrassForestColorOffset: return (Vector3)(mix.Terrain.GrassForestColorOffset.CustomValue ?? mix.Terrain.GrassForestColorOffset.Value);
                default: return Vector3.zero;
            }
        }

        public static T GetValue<T>(ValueID valueID) {
            ThemeMix mix = ThemeManager.Instance.CurrentMix;
            switch (valueID) {
                case ValueID.Longitude: return (T)(mix.Atmosphere.Longitude.CustomValue ?? mix.Atmosphere.Longitude.Value);
                case ValueID.Latitude: return (T)(mix.Atmosphere.Latitude.CustomValue ?? mix.Atmosphere.Latitude.Value);
                case ValueID.SunSize: return (T)(mix.Atmosphere.SunSize.CustomValue ?? mix.Atmosphere.SunSize.Value);
                case ValueID.SunAnisotropy: return (T)(mix.Atmosphere.SunAnisotropy.CustomValue ?? mix.Atmosphere.SunAnisotropy.Value);
                case ValueID.MoonSize: return (T)(mix.Atmosphere.MoonSize.CustomValue ?? mix.Atmosphere.MoonSize.Value);
                case ValueID.Rayleight: return (T)(mix.Atmosphere.Rayleight.CustomValue ?? mix.Atmosphere.Rayleight.Value);
                case ValueID.Mie: return (T)(mix.Atmosphere.Mie.CustomValue ?? mix.Atmosphere.Mie.Value);
                case ValueID.Exposure: return (T)(mix.Atmosphere.Exposure.CustomValue ?? mix.Atmosphere.Exposure.Value);
                case ValueID.StarsIntensity: return (T)(mix.Atmosphere.StarsIntensity.CustomValue ?? mix.Atmosphere.StarsIntensity.Value);
                case ValueID.OuterSpaceIntensity: return (T)(mix.Atmosphere.OuterSpaceIntensity.CustomValue ?? mix.Atmosphere.OuterSpaceIntensity.Value);
                case ValueID.GrassDetailEnabled: return (T)(mix.Terrain.GrassDetailEnabled.CustomValue ?? mix.Terrain.GrassDetailEnabled.Value);
                case ValueID.FertileDetailEnabled: return (T)(mix.Terrain.FertileDetailEnabled.CustomValue ?? mix.Terrain.FertileDetailEnabled.Value);
                case ValueID.RocksDetailEnabled: return (T)(mix.Terrain.RocksDetailEnabled.CustomValue ?? mix.Terrain.RocksDetailEnabled.Value);
                case ValueID.MinTemperatureDay: return (T)(mix.Weather.MinTemperatureDay.CustomValue ?? mix.Weather.MinTemperatureDay.Value);
                case ValueID.MaxTemperatureDay: return (T)(mix.Weather.MaxTemperatureDay.CustomValue ?? mix.Weather.MaxTemperatureDay.Value);
                case ValueID.MinTemperatureNight: return (T)(mix.Weather.MinTemperatureNight.CustomValue ?? mix.Weather.MinTemperatureNight.Value);
                case ValueID.MaxTemperatureNight: return (T)(mix.Weather.MaxTemperatureNight.CustomValue ?? mix.Weather.MaxTemperatureNight.Value);
                case ValueID.MinTemperatureRain: return (T)(mix.Weather.MinTemperatureRain.CustomValue ?? mix.Weather.MinTemperatureRain.Value);
                case ValueID.MaxTemperatureRain: return (T)(mix.Weather.MaxTemperatureRain.CustomValue ?? mix.Weather.MaxTemperatureRain.Value);
                case ValueID.MinTemperatureFog: return (T)(mix.Weather.MinTemperatureFog.CustomValue ?? mix.Weather.MinTemperatureFog.Value);
                case ValueID.MaxTemperatureFog: return (T)(mix.Weather.MaxTemperatureFog.CustomValue ?? mix.Weather.MaxTemperatureFog.Value);
                case ValueID.RainProbabilityDay: return (T)(mix.Weather.RainProbabilityDay.CustomValue ?? mix.Weather.RainProbabilityDay.Value);
                case ValueID.RainProbabilityNight: return (T)(mix.Weather.RainProbabilityNight.CustomValue ?? mix.Weather.RainProbabilityNight.Value);
                case ValueID.FogProbabilityDay: return (T)(mix.Weather.FogProbabilityDay.CustomValue ?? mix.Weather.FogProbabilityDay.Value);
                case ValueID.FogProbabilityNight: return (T)(mix.Weather.FogProbabilityNight.CustomValue ?? mix.Weather.FogProbabilityNight.Value);
                case ValueID.NorthernLightsProbability: return (T)(mix.Weather.NorthernLightsProbability.CustomValue ?? mix.Weather.NorthernLightsProbability.Value);
                default: return default;
            }
        }
    }
}

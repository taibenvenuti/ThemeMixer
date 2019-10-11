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
    }
}

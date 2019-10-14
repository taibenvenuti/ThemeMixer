using ColossalFramework.Importers;
using ColossalFramework.Packaging;
using ColossalFramework.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ThemeMixer.Resources
{
    public class ThemeSprites
    {
        private static UITextureAtlas _atlas;
        public static UITextureAtlas Atlas {
            get {
                if (_atlas == null) _atlas = CreateAtlas();
                return _atlas;
            }
        }

        private static List<string> _spriteNames { get; } = new List<string>();
        private static List<Texture2D> _spriteTextures { get; } = new List<Texture2D>();

        public const string SteamPreview = "SteamPreview";
        public const string SnapShot = "Snapshot";

        public const string GrassDiffuseTexture = "GrassDiffuseTexture";
        public const string RuinedDiffuseTexture = "RuinedDiffuseTexture";
        public const string PavementDiffuseTexture = "PavementDiffuseTexture";
        public const string GravelDiffuseTexture = "GravelDiffuseTexture";
        public const string CliffDiffuseTexture = "CliffDiffuseTexture";
        public const string OilDiffuseTexture = "OilDiffuseTexture";
        public const string OreDiffuseTexture = "OreDiffuseTexture";
        public const string SandDiffuseTexture = "SandDiffuseTexture";
        public const string CliffSandNormalTexture = "CliffSandNormalTexture";

        public const string WaterFoam = "WaterFoam";
        public const string WaterNormal = "WaterNormal";

        public const string UpwardRoadDiffuse = "UpwardRoadDiffuse";
        public const string DownwardRoadDiffuse = "DownwardRoadDiffuse";
        public const string FloorDiffuse = "FloorDiffuse";
        public const string BaseDiffuse = "BaseDiffuse";
        public const string BaseNormal = "BaseNormal";
        public const string BurntDiffuse = "BurntDiffuse";
        public const string AbandonedDiffuse = "AbandonedDiffuse";
        public const string LightColorPalette = "LightColorPalette";

        public const string MoonTexture = "MoonTexture";

        private static string[] _assetNames { get; } = new string[] {
            SteamPreview,
            SnapShot,
            GrassDiffuseTexture,
            RuinedDiffuseTexture,
            PavementDiffuseTexture,
            GravelDiffuseTexture,
            CliffDiffuseTexture,
            OilDiffuseTexture,
            OreDiffuseTexture,
            SandDiffuseTexture,
            CliffSandNormalTexture,
            WaterFoam,
            WaterNormal,
            UpwardRoadDiffuse,
            DownwardRoadDiffuse,
            FloorDiffuse,
            BaseDiffuse,
            BaseNormal,
            BurntDiffuse,
            AbandonedDiffuse,
            LightColorPalette,
            MoonTexture
        };

        private static UITextureAtlas CreateAtlas() {
            _spriteNames.Clear();
            _spriteTextures.Clear();
            var themeAssets = PackageManager.FilterAssets(UserAssetType.MapThemeMetaData);
            foreach (Package.Asset themeAsset in themeAssets) {
                if (themeAsset == null || themeAsset.package == null) continue;
                MapThemeMetaData meta = themeAsset.Instantiate<MapThemeMetaData>();
                if (meta == null) continue;
                for (int i = 0; i < _assetNames.Length; i++) {
                    string assetName = i < 2 ? string.Concat(meta.name, "_", _assetNames[i]) : _assetNames[i];
                    string spriteName = string.Concat(themeAsset.fullName, assetName);
                    spriteName = Regex.Replace(spriteName, @"(\s+|@|&|'|\(|\)|<|>|#|"")", "");

                    Texture2D tex = themeAsset.package.Find(assetName)?.Instantiate<Texture2D>();
                    if (tex != null) {
                        Texture2D spriteTex = tex.ScaledCopy(64.0f / tex.height);
                        Object.Destroy(tex);
                        spriteTex.Apply();
                        _spriteNames.Add(spriteName);
                        _spriteTextures.Add(spriteTex);
                    }
                }
            }
            return ResourceUtils.CreateAtlas("ThemesAtlas", _spriteNames.ToArray(), _spriteTextures.ToArray());
        }
    }
}
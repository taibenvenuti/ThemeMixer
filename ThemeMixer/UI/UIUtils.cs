using ColossalFramework.UI;
using System;
using System.Linq;
using ThemeMixer.Locale;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;

namespace ThemeMixer.UI
{
    public static class UIUtils
    {
        public const int DEFAULT_SPACING = 5;
        public static UIFont Font {
            get {
                if (_font == null) {
                    UIFont[] fonts = UnityEngine.Resources.FindObjectsOfTypeAll<UIFont>();
                    _font = fonts.FirstOrDefault(f => f.name == "OpenSans-Regular");
                }
                return _font;
            }
        }
        private static UIFont _font;

        public static UIFont BoldFont {
            get {
                if (_boldFont == null) {
                    UIFont[] fonts = UnityEngine.Resources.FindObjectsOfTypeAll<UIFont>();
                    _boldFont = fonts.FirstOrDefault(f => f.name == "OpenSans-Bold");
                }
                return _boldFont;
            }
        }
        private static UIFont _boldFont;

        public static void ShowExceptionPanel(string title, string message, bool error) {
            UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                 Translation.Instance.GetTranslation(title),
                 Translation.Instance.GetTranslation(message),
                 error);
        }


        public static string GetTextureSpriteName(TextureID textureID) {
            string packageName = string.Empty;
            ThemeMix mix = ThemeManager.Instance.CurrentMix;
            switch (textureID) {
                case TextureID.GrassDiffuseTexture:
                    packageName = mix.Terrain.GrassDiffuseTexture.PackageID;
                    break;
                case TextureID.RuinedDiffuseTexture:
                    packageName = mix.Terrain.RuinedDiffuseTexture.PackageID;
                    break;
                case TextureID.PavementDiffuseTexture:
                    packageName = mix.Terrain.PavementDiffuseTexture.PackageID;
                    break;
                case TextureID.GravelDiffuseTexture:
                    packageName = mix.Terrain.GravelDiffuseTexture.PackageID;
                    break;
                case TextureID.CliffDiffuseTexture:
                    packageName = mix.Terrain.CliffDiffuseTexture.PackageID;
                    break;
                case TextureID.SandDiffuseTexture:
                    packageName = mix.Terrain.SandDiffuseTexture.PackageID;
                    break;
                case TextureID.OilDiffuseTexture:
                    packageName = mix.Terrain.OilDiffuseTexture.PackageID;
                    break;
                case TextureID.OreDiffuseTexture:
                    packageName = mix.Terrain.OreDiffuseTexture.PackageID;
                    break;
                case TextureID.CliffSandNormalTexture:
                    packageName = mix.Terrain.CliffSandNormalTexture.PackageID;
                    break;
                case TextureID.UpwardRoadDiffuse:
                    packageName = mix.Structures.UpwardRoadDiffuse.PackageID;
                    break;
                case TextureID.DownwardRoadDiffuse:
                    packageName = mix.Structures.DownwardRoadDiffuse.PackageID;
                    break;
                case TextureID.BuildingFloorDiffuse:
                    packageName = mix.Structures.BuildingFloorDiffuse.PackageID;
                    break;
                case TextureID.BuildingBaseDiffuse:
                    packageName = mix.Structures.BuildingBaseDiffuse.PackageID;
                    break;
                case TextureID.BuildingBaseNormal:
                    packageName = mix.Structures.BuildingBaseNormal.PackageID;
                    break;
                case TextureID.BuildingBurntDiffuse:
                    packageName = mix.Structures.BuildingBurntDiffuse.PackageID;
                    break;
                case TextureID.BuildingAbandonedDiffuse:
                    packageName = mix.Structures.BuildingAbandonedDiffuse.PackageID;
                    break;
                case TextureID.LightColorPalette:
                    packageName = mix.Structures.LightColorPalette.PackageID;
                    break;
                case TextureID.MoonTexture:
                    packageName = mix.Atmosphere.MoonTexture.PackageID;
                    break;
                case TextureID.WaterFoam:
                    packageName = mix.Water.WaterFoam.PackageID;
                    break;
                case TextureID.WaterNormal:
                    packageName = mix.Water.WaterNormal.PackageID;
                    break;
                default:
                    break;
            }
            return string.Concat(packageName, Enum.GetName(typeof(TextureID), textureID));
        }

        public static string GetCategoryAndPartLabel(ThemeCategory category, ThemePart part) {
            string prefix = string.Concat(Translation.Instance.GetTranslation(TranslationID.LABEL_SELECT), " ");
            string text = string.Empty;
            switch (category) {
                case ThemeCategory.Themes:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_THEME);
                    break;
                case ThemeCategory.Terrain:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_TERRAIN);
                    break;
                case ThemeCategory.Water:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_WATER);
                    break;
                case ThemeCategory.Structures:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_STRUCTURES);
                    break;
                case ThemeCategory.Atmosphere:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_ATMOSPHERE);
                    break;
                case ThemeCategory.Weather:
                    text = Translation.Instance.GetTranslation(TranslationID.LABEL_WEATHER);
                    break;
                default:
                    break;
            }
            string postFix = string.Empty;
            switch (part) {
                case ThemePart.Texture:
                    postFix = string.Concat(" ", Translation.Instance.GetTranslation(TranslationID.LABEL_TEXTURE));
                    break;
                case ThemePart.Color:
                    postFix = string.Concat(" ", Translation.Instance.GetTranslation(TranslationID.LABEL_COLOR));
                    break;
                case ThemePart.Offset:
                    postFix = string.Concat(" ", Translation.Instance.GetTranslation(TranslationID.LABEL_OFFSET));
                    break;
                case ThemePart.Value:
                    postFix = string.Concat(" ", Translation.Instance.GetTranslation(TranslationID.LABEL_VALUE));
                    break;
                default:
                    break;
            }
            return string.Concat(prefix, text, postFix);
        }
    }
}

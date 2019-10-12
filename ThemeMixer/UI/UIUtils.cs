using ColossalFramework.UI;
using System;
using System.Linq;
using ThemeMixer.Locale;
using ThemeMixer.Resources;
using ThemeMixer.Themes;
using ThemeMixer.Themes.Enums;
using ThemeMixer.TranslationFramework;
using UnityEngine;

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

        public static UIButton CreateButton(UIComponent parent, Vector2 size, 
                string text = "", string tooltip = "", string foregroundSprite = "", 
                string backgroundSprite = "ButtonSmall", bool isFocusable = false, 
                UITextureAtlas atlas = null, RectOffset padding = null, float textScale = 1.0f) {
            UIButton button = parent.AddUIComponent<UIButton>();
            button.size = size;
            button.text = text;
            button.tooltip = tooltip;
            button.textPadding = padding ?? new RectOffset(8, 8, 8, 5);
            button.disabledTextColor = new Color32(128, 128, 128, 255);
            button.normalBgSprite = backgroundSprite;
            button.hoveredBgSprite = string.Concat(backgroundSprite, "Hovered");
            button.pressedBgSprite = string.Concat(backgroundSprite, "Pressed");
            button.disabledBgSprite = string.Concat(backgroundSprite, "Disabled");
            button.focusedBgSprite = string.Concat(backgroundSprite, isFocusable ? "Focused" : "");
            button.normalFgSprite = foregroundSprite;
            button.hoveredFgSprite = string.Concat(foregroundSprite, "Hovered");
            button.pressedFgSprite = string.Concat(foregroundSprite, "Pressed");
            button.disabledFgSprite = string.Concat(foregroundSprite, "Disabled");
            button.focusedFgSprite = string.Concat(foregroundSprite, isFocusable ? "Focused" : "");
            button.atlas = atlas ?? UISprites.DefaultAtlas;
            button.textScale = textScale;
            button.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            return button;
        }

        public static UISlider CreateSlider(UIComponent parent, float width, float minValue, float maxValue, float step) {
            UISlider slider = parent.AddUIComponent<UISlider>();
            slider.size = new Vector2(width, 10.0f);
            slider.backgroundSprite = "WhiteRect";
            slider.color = new Color32(40, 40, 40, 255);
            slider.scrollWheelAmount = step;
            slider.minValue = minValue;
            slider.maxValue = maxValue;
            slider.stepSize = step;
            slider.atlas = UISprites.DefaultAtlas;

            UISprite thumb = slider.AddUIComponent<UISprite>();
            thumb.size = new Vector2(8.0f, 14.0f);
            thumb.spriteName = "WhiteRect";
            thumb.atlas = UISprites.DefaultAtlas;

            slider.thumbObject = thumb;

            return slider;
        }

        public static string GetTextureSpriteName(TextureID textureID, string packageID = "") {
            if(packageID == string.Empty) {
                ThemeMix mix = ThemeManager.Instance.CurrentMix;
                switch (textureID) {
                    case TextureID.GrassDiffuseTexture:
                        packageID = mix.Terrain.GrassDiffuseTexture.PackageID;
                        break;
                    case TextureID.RuinedDiffuseTexture:
                        packageID = mix.Terrain.RuinedDiffuseTexture.PackageID;
                        break;
                    case TextureID.PavementDiffuseTexture:
                        packageID = mix.Terrain.PavementDiffuseTexture.PackageID;
                        break;
                    case TextureID.GravelDiffuseTexture:
                        packageID = mix.Terrain.GravelDiffuseTexture.PackageID;
                        break;
                    case TextureID.CliffDiffuseTexture:
                        packageID = mix.Terrain.CliffDiffuseTexture.PackageID;
                        break;
                    case TextureID.SandDiffuseTexture:
                        packageID = mix.Terrain.SandDiffuseTexture.PackageID;
                        break;
                    case TextureID.OilDiffuseTexture:
                        packageID = mix.Terrain.OilDiffuseTexture.PackageID;
                        break;
                    case TextureID.OreDiffuseTexture:
                        packageID = mix.Terrain.OreDiffuseTexture.PackageID;
                        break;
                    case TextureID.CliffSandNormalTexture:
                        packageID = mix.Terrain.CliffSandNormalTexture.PackageID;
                        break;
                    case TextureID.UpwardRoadDiffuse:
                        packageID = mix.Structures.UpwardRoadDiffuse.PackageID;
                        break;
                    case TextureID.DownwardRoadDiffuse:
                        packageID = mix.Structures.DownwardRoadDiffuse.PackageID;
                        break;
                    case TextureID.BuildingFloorDiffuse:
                        packageID = mix.Structures.BuildingFloorDiffuse.PackageID;
                        break;
                    case TextureID.BuildingBaseDiffuse:
                        packageID = mix.Structures.BuildingBaseDiffuse.PackageID;
                        break;
                    case TextureID.BuildingBaseNormal:
                        packageID = mix.Structures.BuildingBaseNormal.PackageID;
                        break;
                    case TextureID.BuildingBurntDiffuse:
                        packageID = mix.Structures.BuildingBurntDiffuse.PackageID;
                        break;
                    case TextureID.BuildingAbandonedDiffuse:
                        packageID = mix.Structures.BuildingAbandonedDiffuse.PackageID;
                        break;
                    case TextureID.LightColorPalette:
                        packageID = mix.Structures.LightColorPalette.PackageID;
                        break;
                    case TextureID.MoonTexture:
                        packageID = mix.Atmosphere.MoonTexture.PackageID;
                        break;
                    case TextureID.WaterFoam:
                        packageID = mix.Water.WaterFoam.PackageID;
                        break;
                    case TextureID.WaterNormal:
                        packageID = mix.Water.WaterNormal.PackageID;
                        break;
                    default:
                        break;
                }
            }
            return string.Concat(packageID, Enum.GetName(typeof(TextureID), textureID));
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

        public static string GetPartAndIDLabel<T>(T ID) {
            string labelID = string.Empty;
            if (ID is TextureID textureID) {
                labelID = TranslationID.TextureToTranslationID(textureID);
            } else if (ID is ColorID colorID) {
                labelID = TranslationID.ColorToTranslationID(colorID);
            } else if (ID is OffsetID offsetID) {
                labelID = TranslationID.OffsetToTranslationID(offsetID);
            } else if (ID is ValueID valueID) {
                labelID = TranslationID.ValueToTranslationID(valueID);
            }
            return string.Concat(Translation.Instance.GetTranslation(TranslationID.LABEL_SELECT), " ", Translation.Instance.GetTranslation(labelID));
        }
    }
}

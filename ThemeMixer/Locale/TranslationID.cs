using System;
using ThemeMixer.Themes.Enums;
using ThemeMixer.UI.Parts;

namespace ThemeMixer.Locale
{
    public static class TranslationID
    {
        public const string MOD_DESCRIPTION = "MOD_DESCRIPTION";

        public const string ERROR_MISSING_PACKAGE_TITLE = "ERROR_MISSING_PACKAGE_TITLE";
        public const string ERROR_MISSING_PACKAGE_MESSAGE = "ERROR_MISSING_PACKAGE_MESSAGE";
        public const string ERROR_BROKEN_PACKAGE_TITLE = "ERROR_BROKEN_PACKAGE_TITLE";
        public const string ERROR_BROKEN_PACKAGE_MESSAGE = "ERROR_BROKEN_PACKAGE_MESSAGE";

        public const string BUTTON_OK = "BUTTON_OK";
        public const string BUTTON_CLOSE = "BUTTON_CLOSE";
        public const string BUTTON_SAVE = "BUTTON_SAVE";

        public const string TOOLTIP_THEMES = "TOOLTIP_THEMES";
        public const string TOOLTIP_TERRAIN = "TOOLTIP_TERRAIN";
        public const string TOOLTIP_WATER = "TOOLTIP_WATER";
        public const string TOOLTIP_ATMOSPHERE = "TOOLTIP_ATMOSPHERE";
        public const string TOOLTIP_STRUCTURES = "TOOLTIP_STRUCTURES";
        public const string TOOLTIP_WEATHER = "TOOLTIP_WEATHER";
        public const string TOOLTIP_SETTINGS = "TOOLTIP_SETTINGS";
        public const string TOOLTIP_ADDFAVOURITE_ADDBLACKLIST = "TOOLTIP_ADDFAVOURITE_ADDBLACKLIST";
        public const string TOOLTIP_REMOVEFAVOURITE = "TOOLTIP_REMOVEFAVOURITE";
        public const string TOOLTIP_REMOVEBLACKLIST = "TOOLTIP_REMOVEBLACKLIST";
        public const string TOOLTIP_TILING = "TOOLTIP_TILING";
        public const string TOOLTIP_OFFSET = "TOOLTIP_OFFSET";
        public const string TOOLTIP_LOADFROMTHEME = "TOOLTIP_LOADFROMTHEME";
        public const string TOOLTIP_RESET = "TOOLTIP_RESET";

        public const string TOOLTIP_VALUE_GRASSDETAIL = "TOOLTIP_VALUE_GRASSDETAIL";
        public const string TOOLTIP_VALUE_CLIFFDETAIL = "TOOLTIP_VALUE_CLIFFDETAIL";
        public const string TOOLTIP_VALUE_FERTILEDETAIL = "TOOLTIP_VALUE_FERTILEDETAIL";

        public const string TOOLTIP_BUTTON_SAVE_MAXREACHED = "TOOLTIP_BUTTON_SAVE_MAXREACHED";
        public const string TOOLTIP_BUTTON_SAVE_COLOREXISTS = "TOOLTIP_BUTTON_SAVE_COLOREXISTS";
        public const string TOOLTIP_BUTTON_SAVE = "TOOLTIP_BUTTON_SAVE";
        public const string TOOLTIP_OPENCOLORPICKER = "TOOLTIP_OPENCOLORPICKER";

        public const string LABEL_RED = "LABEL_RED";
        public const string LABEL_GREEN = "LABEL_GREEN";
        public const string LABEL_BLUE = "LABEL_BLUE";
        public const string LABEL_NEW_SWATCH = "LABEL_NEW_SWATCH";

        public const string LABEL_MOONINNERCORONA = "LABEL_MOONINNERCORONA";
        public const string LABEL_MOONOUTERCORONA = "LABEL_MOONOUTERCORONA";
        public const string LABEL_SKYTINT = "LABEL_SKYTINT";
        public const string LABEL_NIGHTHORIZONCOLOR = "LABEL_NIGHTHORIZONCOLOR";
        public const string LABEL_EARLYNIGHTZENITHCOLOR = "LABEL_EARLYNIGHTZENITHCOLOR";
        public const string LABEL_LATENIGHTZENITHCOLOR = "LABEL_LATENIGHTZENITHCOLOR";
        public const string LABEL_WATERCLEAN = "LABEL_WATERCLEAN";
        public const string LABEL_WATERDIRTY = "LABEL_WATERDIRTY";
        public const string LABEL_WATERUNDER = "LABEL_WATERUNDER";

        public const string LABEL_BY = "LABEL_BY"; 
        public const string LABEL_THEME = "LABEL_THEME";
        public const string LABEL_TERRAIN = "LABEL_TERRAIN";
        public const string LABEL_ATMOSPHERE = "LABEL_ATMOSPHERE";
        public const string LABEL_WATER = "LABEL_WATER";
        public const string LABEL_WEATHER = "LABEL_WEATHER";
        public const string LABEL_STRUCTURES = "LABEL_STRUCTURES";
        public const string LABEL_COLOR = "LABEL_COLOR";
        public const string LABEL_OFFSET = "LABEL_OFFSET";
        public const string LABEL_VALUE = "LABEL_VALUE";
        public const string LABEL_TEXTURE = "LABEL_TEXTURE";
        public const string LABEL_SELECT = "LABEL_SELECT";

        public const string LABEL_GRASS_DIFFUSE = "LABEL_GRASS_DIFFUSE";
        public const string LABEL_RUINED_DIFFUSE = "LABEL_RUINED_DIFFUSE";
        public const string LABEL_PAVEMENT_DIFFUSE = "LABEL_PAVEMENT_DIFFUSE";
        public const string LABEL_GRAVEL_DIFFUSE = "LABEL_GRAVEL_DIFFUSE";
        public const string LABEL_CLIFF_DIFFUSE = "LABEL_CLIFF_DIFFUSE";
        public const string LABEL_SAND_DIFFUSE = "LABEL_SAND_DIFFUSE";
        public const string LABEL_OIL_DIFFUSE = "LABEL_OIL_DIFFUSE";
        public const string LABEL_ORE_DIFFUSE = "LABEL_ORE_DIFFUSE";
        public const string LABEL_CLIFFSAND_NORMAL = "LABEL_CLIFFSAND_NORMAL";
        public const string LABEL_UPWARD_ROAD_DIFFUSE = "LABEL_UPWARD_ROAD_DIFFUSE";
        public const string LABEL_DOWNWARD_ROAD_DIFFUSE = "LABEL_DOWNWARD_ROAD_DIFFUSE";
        public const string LABEL_BUILDING_FLOOR_DIFFUSE = "LABEL_BUILDING_FLOOR_DIFFUSE";
        public const string LABEL_BUILDING_BASE_DIFFUSE = "LABEL_BUILDING_BASE_DIFFUSE";
        public const string LABEL_BUILDING_BASE_NORMAL = "LABEL_BUILDING_BASE_NORMAL";
        public const string LABEL_BUILDING_BURNT_DIFFUSE = "LABEL_BUILDING_BURNT_DIFFUSE";
        public const string LABEL_BUILDING_ABANDONED_DIFFUSE = "LABEL_BUILDING_ABANDONED_DIFFUSE";
        public const string LABEL_LIGHT_COLOR_PALETTE = "LABEL_LIGHT_COLOR_PALETTE";
        public const string LABEL_MOON_DIFFUSE = "LABEL_MOON_DIFFUSE";
        public const string LABEL_WATER_FOAM = "LABEL_WATER_FOAM";
        public const string LABEL_WATER_NORMAL = "LABEL_WATER_NORMAL";
        public const string LABEL_OFFSET_POLLUTION = "LABEL_OFFSET_POLLUTION";
        public const string LABEL_OFFSET_FIELD = "LABEL_OFFSET_FIELD";
        public const string LABEL_OFFSET_FERTILITY = "LABEL_OFFSET_FERTILITY";
        public const string LABEL_OFFSET_FOREST = "LABEL_OFFSET_FOREST";

        public const string LABEL_TITLE_DETAIL = "LABEL_TITLE_DETAIL";
        public const string LABEL_VALUE_GRASSDETAIL = "LABEL_VALUE_GRASSDETAIL";
        public const string LABEL_VALUE_CLIFFDETAIL = "LABEL_VALUE_CLIFFDETAIL";
        public const string LABEL_VALUE_FERTILEDETAIL = "LABEL_VALUE_FERTILEDETAIL";

        public static string TextureToTranslationID(TextureID textureID) {
            switch (textureID) {
                case TextureID.GrassDiffuseTexture: return LABEL_GRASS_DIFFUSE;
                case TextureID.RuinedDiffuseTexture: return LABEL_RUINED_DIFFUSE;
                case TextureID.PavementDiffuseTexture: return LABEL_PAVEMENT_DIFFUSE;
                case TextureID.GravelDiffuseTexture: return LABEL_GRAVEL_DIFFUSE;
                case TextureID.CliffDiffuseTexture: return LABEL_CLIFF_DIFFUSE;
                case TextureID.SandDiffuseTexture: return LABEL_SAND_DIFFUSE;
                case TextureID.OilDiffuseTexture: return LABEL_OIL_DIFFUSE;
                case TextureID.OreDiffuseTexture: return LABEL_ORE_DIFFUSE;
                case TextureID.CliffSandNormalTexture: return LABEL_CLIFFSAND_NORMAL;
                case TextureID.UpwardRoadDiffuse: return LABEL_UPWARD_ROAD_DIFFUSE;
                case TextureID.DownwardRoadDiffuse: return LABEL_DOWNWARD_ROAD_DIFFUSE;
                case TextureID.BuildingFloorDiffuse: return LABEL_BUILDING_FLOOR_DIFFUSE;
                case TextureID.BuildingBaseDiffuse: return LABEL_BUILDING_BASE_DIFFUSE;
                case TextureID.BuildingBaseNormal: return LABEL_BUILDING_BASE_NORMAL;
                case TextureID.BuildingBurntDiffuse: return LABEL_BUILDING_BURNT_DIFFUSE;
                case TextureID.BuildingAbandonedDiffuse: return LABEL_BUILDING_ABANDONED_DIFFUSE;
                case TextureID.LightColorPalette: return LABEL_LIGHT_COLOR_PALETTE;
                case TextureID.MoonTexture: return LABEL_MOON_DIFFUSE;
                case TextureID.WaterFoam: return LABEL_WATER_FOAM;
                case TextureID.WaterNormal: return LABEL_WATER_NORMAL;
                default: return string.Empty;
            }
        }

        internal static string ColorToTranslationID(ColorID colorID) {
            throw new NotImplementedException();
        }

        internal static string OffsetToTranslationID(OffsetID offsetID) {
            switch (offsetID) {
                case OffsetID.GrassPollutionColorOffset: return LABEL_OFFSET_POLLUTION;
                case OffsetID.GrassFieldColorOffset: return LABEL_OFFSET_FIELD;
                case OffsetID.GrassFertilityColorOffset: return LABEL_OFFSET_FERTILITY;
                case OffsetID.GrassForestColorOffset: return LABEL_OFFSET_FOREST;
                default: return string.Empty;
            }
        }

        internal static string ValueToTranslationID(ValueID valueID) {
            throw new NotImplementedException();
        }
    }
}

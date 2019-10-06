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

        public const string LABEL_BY = "LABEL_BY"; 
        public const string LABEL_LOAD_THEME = "LABEL_LOAD_THEME";
        public const string LABEL_LOAD_TEXTURE = "LABEL_LOAD_TEXTURE";
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
    }
}

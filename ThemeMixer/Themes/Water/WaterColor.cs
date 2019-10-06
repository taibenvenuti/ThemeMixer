using UnityEngine;

namespace ThemeMixer.Themes.Water
{
    public class WaterColor : ThemePartBase
    {
        public ColorName Name;

        public WaterColor() { }

        public WaterColor(ColorName colorName) {
            Name = colorName;
        }

        public WaterColor(string packageID, ColorName colorName) : base(packageID) {
            Name = colorName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case ColorName.WaterClean:
                    SetValue(metaData.waterClean);
                    break;
                case ColorName.WaterDirty:
                    SetValue(metaData.waterDirty);
                    break;
                case ColorName.WaterUnder:
                    SetValue(metaData.waterUnder);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (Name) {
                case ColorName.WaterClean:
                    properties.m_waterColorClean = (Color)(CustomValue ?? Value);
                    Shader.SetGlobalColor("_WaterColorClean", new Color(properties.m_waterColorClean.r, properties.m_waterColorClean.g, properties.m_waterColorClean.b, properties.m_waterRainFoam));
                    break;
                case ColorName.WaterDirty:
                    properties.m_waterColorDirty = (Color)(CustomValue ?? Value);
                    Shader.SetGlobalColor("_WaterColorDirty", properties.m_waterColorDirty);
                    break;
                case ColorName.WaterUnder:
                    properties.m_waterColorUnder = (Color)(CustomValue ?? Value);
                    Shader.SetGlobalColor("_WaterColorUnder", properties.m_waterColorUnder);
                    break;
                default:
                    break;
            }
        }

        public enum ColorName
        {
            WaterClean,
            WaterDirty,
            WaterUnder,
            Count
        }
    }
}

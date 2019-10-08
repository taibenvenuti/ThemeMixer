using UnityEngine;

namespace ThemeMixer.Themes.Water
{
    public class WaterTexture : TexturePartBase
    {
        public TextureName Name;

        public WaterTexture() { }

        public WaterTexture(TextureName textureName) {
            Name = textureName;
        }

        public WaterTexture(TextureName textureName, string packageID) : base(packageID) {
            Name = textureName;
            Load(packageID);
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case TextureName.WaterFoam:
                    return SetTexture(metaData.waterFoamAsset);
                case TextureName.WaterNormal:
                    return SetTexture(metaData.waterNormalAsset);
                default: return false;
            }
        }

        protected override void LoadValue() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            Texture oldTexture = null;
            switch (Name) {
                case TextureName.WaterFoam:
                    oldTexture = properties.m_waterFoam;
                    properties.m_waterFoam = Texture;
                    Shader.SetGlobalTexture("_WaterFoam", properties.m_waterFoam);
                    break;
                case TextureName.WaterNormal:
                    oldTexture = properties.m_waterNormal;
                    properties.m_waterNormal = Texture;
                    Shader.SetGlobalTexture("_WaterNormal", properties.m_waterNormal);
                    break;

                default:
                    break;
            }
            if (oldTexture != null && !ReferenceEquals(oldTexture, Texture)) Object.Destroy(oldTexture);
        }

        public enum TextureName
        {
            WaterNormal,
            WaterFoam,
            Count
        }
    }
}

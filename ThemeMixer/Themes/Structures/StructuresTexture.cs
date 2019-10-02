using UnityEngine;

namespace ThemeMixer.Themes.Structures
{
    public class StructureTexture : TexturePartBase
    {
        public TextureName Name;

        public StructureTexture(string packageID, TextureName textureName) : base(packageID) {
            Name = textureName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case TextureName.UpwardRoadDiffuse:
                    return SetValue(metaData.upwardRoadDiffuse);
                case TextureName.DownwardRoadDiffuse:
                    return SetValue(metaData.downwardRoadDiffuse);
                case TextureName.BuildingFloorDiffuse:
                    return SetValue(metaData.buildingFloorDiffuse);
                case TextureName.BuildingBaseDiffuse:
                    return SetValue(metaData.buildingBaseDiffuse);
                case TextureName.BuildingBaseNormal:
                    return SetValue(metaData.buildingBaseNormal);
                case TextureName.BuildingBurntDiffuse:
                    return SetValue(metaData.buildingBurntDiffuse);
                case TextureName.BuildingAbandonedDiffuse:
                    return SetValue(metaData.buildingAbandonedDiffuse);
                case TextureName.LightColorPalette:
                    return SetValue(metaData.lightColorPalette);
                default: return false;
            }
        }

        protected override void LoadValue() {
            BuildingProperties buildingProperties = BuildingManager.instance.m_properties;
            NetProperties netProperties = NetManager.instance.m_properties;
            Texture oldTexture = null;
            switch (Name) {
                case TextureName.UpwardRoadDiffuse:
                    oldTexture = netProperties.m_upwardDiffuse;
                    netProperties.m_upwardDiffuse = Texture;
                    Shader.SetGlobalTexture("_RoadUpwardDiffuse", netProperties.m_upwardDiffuse);
                    break;
                case TextureName.DownwardRoadDiffuse:
                    oldTexture = netProperties.m_downwardDiffuse;
                    netProperties.m_downwardDiffuse = Texture;
                    Shader.SetGlobalTexture("_RoadDownwardDiffuse", netProperties.m_downwardDiffuse);
                    break;
                case TextureName.BuildingFloorDiffuse:
                    oldTexture = buildingProperties.m_floorDiffuse;
                    buildingProperties.m_floorDiffuse = Texture;
                    Shader.SetGlobalTexture("_BuildingFloorDiffuse", buildingProperties.m_floorDiffuse);
                    break;
                case TextureName.BuildingBaseDiffuse:
                    oldTexture = buildingProperties.m_baseDiffuse;
                    buildingProperties.m_baseDiffuse = Texture;
                    Shader.SetGlobalTexture("_BuildingBaseDiffuse", buildingProperties.m_baseDiffuse);
                    break;
                case TextureName.BuildingBaseNormal:
                    oldTexture = buildingProperties.m_baseNormal;
                    buildingProperties.m_baseNormal = Texture;
                    Shader.SetGlobalTexture("_BuildingBaseNormal", buildingProperties.m_baseNormal);
                    break;
                case TextureName.BuildingBurntDiffuse:
                    oldTexture = buildingProperties.m_burnedDiffuse;
                    buildingProperties.m_burnedDiffuse = Texture;
                    Shader.SetGlobalTexture("_BuildingBurnedDiffuse", buildingProperties.m_burnedDiffuse);
                    break;
                case TextureName.BuildingAbandonedDiffuse:
                    oldTexture = buildingProperties.m_abandonedDiffuse;
                    buildingProperties.m_abandonedDiffuse = Texture;
                    Shader.SetGlobalTexture("_BuildingAbandonedDiffuse", buildingProperties.m_abandonedDiffuse);
                    break;
                case TextureName.LightColorPalette:
                    oldTexture = buildingProperties.m_lightColorPalette;
                    buildingProperties.m_lightColorPalette = Texture;
                    Shader.SetGlobalTexture("_BuildingLightColorPalette", buildingProperties.m_lightColorPalette);
                    break;
                default:
                    break;
            }
            if (oldTexture != null) Object.Destroy(oldTexture);
        }

        public enum TextureName
        {
            UpwardRoadDiffuse,
            DownwardRoadDiffuse,
            BuildingFloorDiffuse,
            BuildingBaseDiffuse,
            BuildingBaseNormal,
            BuildingBurntDiffuse,
            BuildingAbandonedDiffuse,
            LightColorPalette,
            Count
        }
    }
}

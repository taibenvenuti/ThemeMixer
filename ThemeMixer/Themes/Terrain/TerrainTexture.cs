using UnityEngine;

namespace ThemeMixer.Themes.Terrain
{
    public class TerrainTexture : TexturePartBase
    {
        public TextureName Name;

        public TerrainTexture(string packageID, TextureName textureName) : base(packageID) {
            Name = textureName;
        }

        public override bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (Texture == null && !SetFromTheme()) return false;
            LoadValue();
            LoadTiling();
            return true;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            bool success = false;
            switch (Name) {
                case TextureName.GrassDiffuseTexture:
                    success = SetValue(metaData.grassDiffuseAsset);
                    if (success) SetValue(metaData.grassTiling);
                    break;
                case TextureName.RuinedDiffuseTexture:
                    success = SetValue(metaData.ruinedDiffuseAsset);
                    if (success) SetValue(metaData.ruinedTiling);
                    break;
                case TextureName.PavementDiffuseTexture:
                    success = SetValue(metaData.pavementDiffuseAsset);
                    if (success) SetValue(metaData.pavementTiling);
                    break;
                case TextureName.GravelDiffuseTexture:
                    success = SetValue(metaData.gravelDiffuseAsset);
                    if (success) SetValue(metaData.gravelTiling);
                    break;
                case TextureName.CliffDiffuseTexture:
                    success = SetValue(metaData.cliffDiffuseAsset);
                    if (success) SetValue(metaData.cliffDiffuseTiling);
                    break;
                case TextureName.OilDiffuseTexture:
                    success = SetValue(metaData.oilDiffuseAsset);
                    if (success) SetValue(metaData.oilTiling);
                    break;
                case TextureName.OreDiffuseTexture:
                    success = SetValue(metaData.oreDiffuseAsset);
                    if (success) SetValue(metaData.oreTiling);
                    break;
                case TextureName.SandDiffuseTexture:
                    success = SetValue(metaData.sandDiffuseAsset);
                    if (success) SetValue(metaData.sandDiffuseTiling);
                    break;
                case TextureName.CliffSandNormalTexture:
                    success = SetValue(metaData.cliffSandNormalAsset);
                    if (success) SetValue(metaData.cliffSandNormalTiling);
                    break;
                default: return false;
            }
            return success;
        }

        protected override void LoadValue() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            Texture2D oldTexture = null;
            switch (Name) {
                case TextureName.GrassDiffuseTexture:
                    oldTexture = properties.m_grassDiffuse;
                    properties.m_grassDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainGrassDiffuse", properties.m_grassDiffuse);
                    break;
                case TextureName.RuinedDiffuseTexture:
                    oldTexture = properties.m_ruinedDiffuse;
                    properties.m_ruinedDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainRuinedDiffuse", properties.m_ruinedDiffuse);
                    break;
                case TextureName.PavementDiffuseTexture:
                    oldTexture = properties.m_pavementDiffuse;
                    properties.m_pavementDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainPavementDiffuse", properties.m_pavementDiffuse);
                    break;
                case TextureName.GravelDiffuseTexture:
                    oldTexture = properties.m_gravelDiffuse;
                    properties.m_gravelDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainGravelDiffuse", properties.m_gravelDiffuse);
                    break;
                case TextureName.CliffDiffuseTexture:
                    oldTexture = properties.m_cliffDiffuse;
                    properties.m_cliffDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainCliffDiffuse", properties.m_cliffDiffuse);
                    break;
                case TextureName.OreDiffuseTexture:
                    oldTexture = properties.m_oreDiffuse;
                    properties.m_oreDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainOreDiffuse", properties.m_oreDiffuse);
                    break;
                case TextureName.OilDiffuseTexture:
                    oldTexture = properties.m_oilDiffuse;
                    properties.m_oilDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainOilDiffuse", properties.m_oilDiffuse);
                    break;
                case TextureName.SandDiffuseTexture:
                    oldTexture = properties.m_sandDiffuse;
                    properties.m_sandDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainSandDiffuse", properties.m_sandDiffuse);
                    break;
                case TextureName.CliffSandNormalTexture:
                    oldTexture = properties.m_cliffSandNormal;
                    properties.m_cliffSandNormal = Texture;
                    Shader.SetGlobalTexture("_TerrainCliffSandNormal", properties.m_cliffSandNormal);
                    break;
                default:
                    break;
            }
            if (oldTexture != null) Object.Destroy(oldTexture);
        }

        public void LoadTiling() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (Name) {
                case TextureName.GrassDiffuseTexture:
                    properties.m_grassTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.RuinedDiffuseTexture:
                    properties.m_ruinedTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.PavementDiffuseTexture:
                    properties.m_pavementTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.GravelDiffuseTexture:
                    properties.m_gravelTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.CliffDiffuseTexture:
                    properties.m_cliffTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.OilDiffuseTexture:
                    properties.m_oilTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.OreDiffuseTexture:
                    properties.m_oreTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.SandDiffuseTexture:
                    properties.m_sandTiling = (float)(CustomValue ?? Value);
                    break;
                case TextureName.CliffSandNormalTexture:
                    properties.m_cliffSandNormalTiling = (float)(CustomValue ?? Value);
                    break;
                default:
                    break;
            }

            Shader.SetGlobalVector("_GrassPollutionColorOffset", // Needed here to set CliffSandNormalTiling
                                   new Vector4(properties.m_grassPollutionColorOffset.x,
                                   properties.m_grassPollutionColorOffset.y,
                                   properties.m_grassPollutionColorOffset.z,
                                   properties.m_cliffSandNormalTiling));

            Shader.SetGlobalVector("_TerrainTextureTiling1",
                                   new Vector4(properties.m_pavementTiling,
                                   properties.m_ruinedTiling,
                                   properties.m_sandTiling,
                                   properties.m_cliffTiling));

            Shader.SetGlobalVector("_TerrainTextureTiling2",
                                   new Vector4(properties.m_grassTiling,
                                   properties.m_gravelTiling,
                                   properties.m_oreTiling,
                                   properties.m_oilTiling));
        }

        public enum TextureName
        {
            GrassDiffuseTexture,
            RuinedDiffuseTexture,
            PavementDiffuseTexture,
            GravelDiffuseTexture,
            CliffDiffuseTexture,
            OilDiffuseTexture,
            OreDiffuseTexture,
            SandDiffuseTexture,
            CliffSandNormalTexture,
            Count
        }
    }
}

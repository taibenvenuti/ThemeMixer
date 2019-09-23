using ColossalFramework.Packaging;
using System;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes.Terrain
{

    [Serializable]
    public class TerrainTexture : IThemePart
    {
        public string PackageID;
        public Name TextureName;
        public float? CustomTiling;

        [XmlIgnore]
        public Texture2D Texture;
        [XmlIgnore]
        public float? Tiling;

        public TerrainTexture() { }

        public TerrainTexture(string packageID, Name textureName) {
            PackageID = packageID;
            TextureName = textureName;
        }

        public bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (Texture == null && CustomTiling == null && Tiling == null && !SetFromTheme()) return false;
            LoadTexture();
            LoadTiling();
            return true;
        }

        private bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetMapThemeMetaData(PackageID);
            if (metaData == null) return false;
            switch (TextureName) {
                case Name.GrassDiffuseTexture:
                    return SetTextureAndTiling(metaData.grassDiffuseAsset, metaData.grassTiling);
                case Name.RuinedDiffuseTexture:
                    return SetTextureAndTiling(metaData.ruinedDiffuseAsset, metaData.ruinedTiling);
                case Name.PavementDiffuseTexture:
                    return SetTextureAndTiling(metaData.pavementDiffuseAsset, metaData.pavementTiling);
                case Name.GravelDiffuseTexture:
                    return SetTextureAndTiling(metaData.gravelDiffuseAsset, metaData.gravelTiling);
                case Name.CliffDiffuseTexture:
                    return SetTextureAndTiling(metaData.cliffDiffuseAsset, metaData.cliffDiffuseTiling);
                case Name.OilDiffuseTexture:
                    return SetTextureAndTiling(metaData.oilDiffuseAsset, metaData.oilTiling);
                case Name.OreDiffuseTexture:
                    return SetTextureAndTiling(metaData.oreDiffuseAsset, metaData.oreTiling);
                case Name.SandDiffuseTexture:
                    return SetTextureAndTiling(metaData.sandDiffuseAsset, metaData.sandDiffuseTiling);
                case Name.CliffSandNormalTexture:
                    return SetTextureAndTiling(metaData.cliffSandNormalAsset, metaData.cliffSandNormalTiling);
                default: return false;
            }
        }

        private bool SetTextureAndTiling(Package.Asset asset, float tiling) {
            if (asset == null) return false;
            Texture = asset.Instantiate<Texture2D>();
            if (Texture == null) return false;
            Texture.anisoLevel = 8;
            Texture.filterMode = FilterMode.Trilinear;
            Texture.Apply();
            Tiling = CustomTiling = tiling;
            return true;
        }

        public void LoadTexture() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            Texture2D oldTexture = null;
            switch (TextureName) {
                case Name.GrassDiffuseTexture:
                    oldTexture = properties.m_grassDiffuse;
                    properties.m_grassDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainGrassDiffuse", properties.m_grassDiffuse);
                    break;
                case Name.RuinedDiffuseTexture:
                    oldTexture = properties.m_ruinedDiffuse;
                    properties.m_ruinedDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainRuinedDiffuse", properties.m_ruinedDiffuse);
                    break;
                case Name.PavementDiffuseTexture:
                    oldTexture = properties.m_pavementDiffuse;
                    properties.m_pavementDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainPavementDiffuse", properties.m_pavementDiffuse);
                    break;
                case Name.GravelDiffuseTexture:
                    oldTexture = properties.m_gravelDiffuse;
                    properties.m_gravelDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainGravelDiffuse", properties.m_gravelDiffuse);
                    break;
                case Name.CliffDiffuseTexture:
                    oldTexture = properties.m_cliffDiffuse;
                    properties.m_cliffDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainCliffDiffuse", properties.m_cliffDiffuse);
                    break;
                case Name.OreDiffuseTexture:
                    oldTexture = properties.m_oreDiffuse;
                    properties.m_oreDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainOreDiffuse", properties.m_oreDiffuse);
                    break;
                case Name.OilDiffuseTexture:
                    oldTexture = properties.m_oilDiffuse;
                    properties.m_oilDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainOilDiffuse", properties.m_oilDiffuse);
                    break;
                case Name.SandDiffuseTexture:
                    oldTexture = properties.m_sandDiffuse;
                    properties.m_sandDiffuse = Texture;
                    Shader.SetGlobalTexture("_TerrainSandDiffuse", properties.m_sandDiffuse);
                    break;
                case Name.CliffSandNormalTexture:
                    oldTexture = properties.m_cliffSandNormal;
                    properties.m_cliffSandNormal = Texture;
                    Shader.SetGlobalTexture("_TerrainCliffSandNormal", properties.m_cliffSandNormal);
                    break;
                default:
                    break;
            }
            UnityEngine.Object.Destroy(oldTexture);
        }

        public void LoadTiling() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (TextureName) {
                case Name.GrassDiffuseTexture:
                    properties.m_grassTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.RuinedDiffuseTexture:
                    properties.m_ruinedTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.PavementDiffuseTexture:
                    properties.m_pavementTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.GravelDiffuseTexture:
                    properties.m_gravelTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.CliffDiffuseTexture:
                    properties.m_cliffTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.OilDiffuseTexture:
                    properties.m_oilTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.OreDiffuseTexture:
                    properties.m_oreTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.SandDiffuseTexture:
                    properties.m_sandTiling = (float)(CustomTiling ?? Tiling);
                    break;
                case Name.CliffSandNormalTexture:
                    properties.m_cliffSandNormalTiling = (float)(CustomTiling ?? Tiling);
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

        public enum Name
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

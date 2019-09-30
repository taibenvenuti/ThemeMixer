using ColossalFramework.Packaging;
using System.Xml.Serialization;
using UnityEngine;

namespace ThemeMixer.Themes.Terrain
{
    public class TerrainColorOffset : ILoadable
    {
        public string PackageID;
        public Name OffsetName;
        public Vector3? CustomOffsetValue = null;

        [XmlIgnore]
        public Vector3? OffsetValue = null;

        public TerrainColorOffset() { }

        public TerrainColorOffset(string packageID, Name offsetName) {
            PackageID = packageID;
            OffsetName = offsetName;
        }

        public bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (CustomOffsetValue == null && OffsetValue == null && !SetFromTheme()) return false;
            LoadOffset();
            return true;
        }

        private bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (OffsetName) {
                case Name.GrassFertilityColorOffset:
                    SetColorOffset(metaData.grassFertilityColorOffset);
                    break;
                case Name.GrassFieldColorOffset:
                    SetColorOffset(metaData.grassFieldColorOffset);
                    break;
                case Name.GrassForestColorOffset:
                    SetColorOffset(metaData.grassForestColorOffset);
                    break;
                case Name.GrassPollutionColorOffset:
                    SetColorOffset(metaData.grassPollutionColorOffset);
                    break;
                default: return false;
            }
            return true;
        }

        private void SetColorOffset(Vector3 offset) {
            OffsetValue = CustomOffsetValue = offset;
        }

        private void LoadOffset() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (OffsetName) {
                case Name.GrassFertilityColorOffset:
                    properties.m_grassFertilityColorOffset = (Vector3)(CustomOffsetValue ?? OffsetValue);
                    break;
                case Name.GrassFieldColorOffset:
                    properties.m_grassFieldColorOffset = (Vector3)(CustomOffsetValue ?? OffsetValue);
                    break;
                case Name.GrassForestColorOffset:
                    properties.m_grassForestColorOffset = (Vector3)(CustomOffsetValue ?? OffsetValue);
                    break;
                case Name.GrassPollutionColorOffset:
                    properties.m_grassPollutionColorOffset = (Vector3)(CustomOffsetValue ?? OffsetValue);
                    break;
                default: break;
            }
            SetShaderVectors();
        }

        private static void SetShaderVectors() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            Shader.SetGlobalVector("_GrassFieldColorOffset", properties.m_grassFieldColorOffset);
            Shader.SetGlobalVector("_GrassFertilityColorOffset", properties.m_grassFertilityColorOffset);
            Shader.SetGlobalVector("_GrassForestColorOffset", properties.m_grassForestColorOffset);
            Shader.SetGlobalVector("_GrassPollutionColorOffset",
                                   new Vector4(properties.m_grassPollutionColorOffset.x,
                                   properties.m_grassPollutionColorOffset.y,
                                   properties.m_grassPollutionColorOffset.z,
                                   properties.m_cliffSandNormalTiling));
        }

        public enum Name
        {
            GrassPollutionColorOffset,
            GrassFieldColorOffset,
            GrassFertilityColorOffset,
            GrassForestColorOffset,
            Count
        }
    }
}

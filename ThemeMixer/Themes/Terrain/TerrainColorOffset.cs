using UnityEngine;

namespace ThemeMixer.Themes.Terrain
{
    public class TerrainColorOffset : ThemePartBase
    {
        public OffsetName Name;

        public TerrainColorOffset() { }

        public TerrainColorOffset(string packageID, OffsetName offsetName) : base(packageID) {
            Name = offsetName;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetThemeFromPackage(PackageID);
            if (metaData == null) return false;
            switch (Name) {
                case OffsetName.GrassFertilityColorOffset:
                    SetValue(metaData.grassFertilityColorOffset);
                    break;
                case OffsetName.GrassFieldColorOffset:
                    SetValue(metaData.grassFieldColorOffset);
                    break;
                case OffsetName.GrassForestColorOffset:
                    SetValue(metaData.grassForestColorOffset);
                    break;
                case OffsetName.GrassPollutionColorOffset:
                    SetValue(metaData.grassPollutionColorOffset);
                    break;
                default: return false;
            }
            return true;
        }

        protected override void LoadValue() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (Name) {
                case OffsetName.GrassFertilityColorOffset:
                    properties.m_grassFertilityColorOffset = (Vector3)(CustomValue ?? Value);
                    break;
                case OffsetName.GrassFieldColorOffset:
                    properties.m_grassFieldColorOffset = (Vector3)(CustomValue ?? Value);
                    break;
                case OffsetName.GrassForestColorOffset:
                    properties.m_grassForestColorOffset = (Vector3)(CustomValue ?? Value);
                    break;
                case OffsetName.GrassPollutionColorOffset:
                    properties.m_grassPollutionColorOffset = (Vector3)(CustomValue ?? Value);
                    break;
                default: break;
            }
            SetShaderVectors();
        }

        private static void SetShaderVectors() {
            TerrainProperties properties = TerrainManager.instance.m_properties;
            Shader.SetGlobalVector("_GrassFieldColorOffset", properties.m_grassFieldColorOffset);
            Shader.SetGlobalVector("_GrassFertilityColorOffset", properties.m_grassFertilityColorOffset);
            Shader.SetGlobalVector("_GrassForestColorOffset", properties.m_grassForestColorOffset);
            Shader.SetGlobalVector("_GrassPollutionColorOffset",
                                   new Vector4(properties.m_grassPollutionColorOffset.x,
                                   properties.m_grassPollutionColorOffset.y,
                                   properties.m_grassPollutionColorOffset.z,
                                   properties.m_cliffSandNormalTiling));
        }

        public enum OffsetName
        {
            GrassPollutionColorOffset,
            GrassFieldColorOffset,
            GrassFertilityColorOffset,
            GrassForestColorOffset,
            Count
        }
    }
}

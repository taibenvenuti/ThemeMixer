using System;

namespace ThemeMixer.Themes.Terrain
{
    [Serializable]
    public class TerrainPart : ILoadable
    {
        public TerrainTexture GrassDiffuseTexture;
        public TerrainTexture RuinedDiffuseTexture;
        public TerrainTexture PavementDiffuseTexture;
        public TerrainTexture GravelDiffuseTexture;
        public TerrainTexture CliffDiffuseTexture;
        public TerrainTexture SandDiffuseTexture;
        public TerrainTexture OilDiffuseTexture;
        public TerrainTexture OreDiffuseTexture;
        public TerrainTexture CliffSandNormalTexture;

        public TerrainColorOffset GrassPollutionColorOffset;
        public TerrainColorOffset GrassFieldColorOffset;
        public TerrainColorOffset GrassFertilityColorOffset;
        public TerrainColorOffset GrassForestColorOffset;

        public TerrainDetail GrassDetailEnabled;
        public TerrainDetail FertileDetailEnabled;
        public TerrainDetail RocksDetailEnabled;

        public bool Load(string packageID = null) {
            if (packageID != null) {
                SetAll(packageID);
            }
            return LoadAll();
        }

        private void SetAll(string packageID) {
            for (int i = 0; i < (int)TerrainTexture.Name.Count; i++) {
                SetTexture(packageID, (TerrainTexture.Name)i);
            }
            for (int j = 0; j < (int)TerrainColorOffset.Name.Count; j++) {
                SetColorOffset(packageID, (TerrainColorOffset.Name)j);
            }
            for (int k = 0; k < (int)TerrainDetail.Name.Count; k++) {
                SetDetail(packageID, (TerrainDetail.Name)k);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)TerrainTexture.Name.Count; i++) {
                if (!LoadTexture((TerrainTexture.Name)i)) success = false;
            }
            for (int j = 0; j < (int)TerrainColorOffset.Name.Count; j++) {
                if (!LoadColorOffset((TerrainColorOffset.Name)j)) success = false;
            }
            for (int k = 0; k < (int)TerrainDetail.Name.Count; k++) {
                if (!LoadDetail((TerrainDetail.Name)k)) success = false;
            }
            return success;
        }

        private void SetTexture(string packageID, TerrainTexture.Name textureName) {
            switch (textureName) {
                case TerrainTexture.Name.GrassDiffuseTexture:
                    GrassDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.RuinedDiffuseTexture:
                    RuinedDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.PavementDiffuseTexture:
                    PavementDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.GravelDiffuseTexture:
                    GravelDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.CliffDiffuseTexture:
                    CliffDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.OilDiffuseTexture:
                    OilDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.OreDiffuseTexture:
                    OreDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.SandDiffuseTexture:
                    SandDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.Name.CliffSandNormalTexture:
                    CliffSandNormalTexture = new TerrainTexture(packageID, textureName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadTexture(TerrainTexture.Name textureName) {
            switch (textureName) {
                case TerrainTexture.Name.GrassDiffuseTexture:
                    return GrassDiffuseTexture.Load();
                case TerrainTexture.Name.RuinedDiffuseTexture:
                    return RuinedDiffuseTexture.Load();
                case TerrainTexture.Name.PavementDiffuseTexture:
                    return PavementDiffuseTexture.Load();
                case TerrainTexture.Name.GravelDiffuseTexture:
                    return GravelDiffuseTexture.Load();
                case TerrainTexture.Name.CliffDiffuseTexture:
                    return CliffDiffuseTexture.Load();
                case TerrainTexture.Name.OilDiffuseTexture:
                    return OilDiffuseTexture.Load();
                case TerrainTexture.Name.OreDiffuseTexture:
                    return OreDiffuseTexture.Load();
                case TerrainTexture.Name.SandDiffuseTexture:
                    return SandDiffuseTexture.Load();
                case TerrainTexture.Name.CliffSandNormalTexture:
                    return CliffSandNormalTexture.Load();
                default: return false;
            }
        }

        private void SetColorOffset(string packageID, TerrainColorOffset.Name offsetName) {
            switch (offsetName) {
                case TerrainColorOffset.Name.GrassPollutionColorOffset:
                    GrassPollutionColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.Name.GrassFieldColorOffset:
                    GrassFieldColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.Name.GrassFertilityColorOffset:
                    GrassFertilityColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.Name.GrassForestColorOffset:
                    GrassForestColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadColorOffset(TerrainColorOffset.Name offsetName) {
            switch (offsetName) {
                case TerrainColorOffset.Name.GrassPollutionColorOffset:
                    return GrassPollutionColorOffset.Load();
                case TerrainColorOffset.Name.GrassFieldColorOffset:
                    return GrassFieldColorOffset.Load();
                case TerrainColorOffset.Name.GrassFertilityColorOffset:
                    return GrassFertilityColorOffset.Load();
                case TerrainColorOffset.Name.GrassForestColorOffset:
                    return GrassForestColorOffset.Load();
                default: return false;
            }
        }

        private void SetDetail(string packageID, TerrainDetail.Name detailName) {
            switch (detailName) {
                case TerrainDetail.Name.GrassDetailEnabled:
                    GrassDetailEnabled = new TerrainDetail(packageID, detailName);
                    break;
                case TerrainDetail.Name.FertileDetailEnabled:
                    FertileDetailEnabled = new TerrainDetail(packageID, detailName);
                    break;
                case TerrainDetail.Name.RocksDetailEnabled:
                    RocksDetailEnabled = new TerrainDetail(packageID, detailName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadDetail(TerrainDetail.Name detailName) {
            switch (detailName) {
                case TerrainDetail.Name.GrassDetailEnabled:
                    return GrassDetailEnabled.Load();
                case TerrainDetail.Name.FertileDetailEnabled:
                    return FertileDetailEnabled.Load();
                case TerrainDetail.Name.RocksDetailEnabled:
                    return RocksDetailEnabled.Load();
                default: return false;
            }
        }
    }
}

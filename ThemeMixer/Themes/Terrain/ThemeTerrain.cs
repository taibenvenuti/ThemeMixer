using System;

namespace ThemeMixer.Themes.Terrain
{
    [Serializable]
    public class ThemeTerrain : ILoadable
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

        public ThemeTerrain() {
            Initialize();
        }

        private void Initialize() {
            GrassDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.GrassDiffuseTexture);
            RuinedDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.RuinedDiffuseTexture);
            PavementDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.PavementDiffuseTexture);
            GravelDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.GravelDiffuseTexture);
            CliffDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.CliffDiffuseTexture);
            SandDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.SandDiffuseTexture);
            OilDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.OilDiffuseTexture);
            OreDiffuseTexture = new TerrainTexture(TerrainTexture.TextureName.OreDiffuseTexture);
            CliffSandNormalTexture = new TerrainTexture(TerrainTexture.TextureName.CliffSandNormalTexture);

            GrassPollutionColorOffset = new TerrainColorOffset(TerrainColorOffset.OffsetName.GrassPollutionColorOffset);
            GrassFieldColorOffset = new TerrainColorOffset(TerrainColorOffset.OffsetName.GrassFieldColorOffset);
            GrassFertilityColorOffset = new TerrainColorOffset(TerrainColorOffset.OffsetName.GrassFertilityColorOffset);
            GrassForestColorOffset = new TerrainColorOffset(TerrainColorOffset.OffsetName.GrassForestColorOffset);

            GrassDetailEnabled = new TerrainDetail(TerrainDetail.Name.GrassDetailEnabled);
            FertileDetailEnabled = new TerrainDetail(TerrainDetail.Name.FertileDetailEnabled);
            RocksDetailEnabled = new TerrainDetail(TerrainDetail.Name.RocksDetailEnabled);
        }

        public ThemeTerrain(string packageID) {
            Load(packageID);
        }

        public void Set(string packageID) {
            SetAll(packageID);
        }

        public bool Load(string packageID = null) {
            if (packageID != null) {
                Set(packageID);
            }
            return LoadAll();
        }

        private void SetAll(string packageID) {
            for (int i = 0; i < (int)TerrainTexture.TextureName.Count; i++) {
                SetTexture(packageID, (TerrainTexture.TextureName)i);
            }
            for (int j = 0; j < (int)TerrainColorOffset.OffsetName.Count; j++) {
                SetColorOffset(packageID, (TerrainColorOffset.OffsetName)j);
            }
            for (int k = 0; k < (int)TerrainDetail.Name.Count; k++) {
                SetDetail(packageID, (TerrainDetail.Name)k);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)TerrainTexture.TextureName.Count; i++) {
                if (!LoadTexture((TerrainTexture.TextureName)i)) success = false;
            }
            for (int j = 0; j < (int)TerrainColorOffset.OffsetName.Count; j++) {
                if (!LoadColorOffset((TerrainColorOffset.OffsetName)j)) success = false;
            }
            for (int k = 0; k < (int)TerrainDetail.Name.Count; k++) {
                if (!LoadDetail((TerrainDetail.Name)k)) success = false;
            }
            return success;
        }

        private void SetTexture(string packageID, TerrainTexture.TextureName textureName) {
            switch (textureName) {
                case TerrainTexture.TextureName.GrassDiffuseTexture:
                    GrassDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.RuinedDiffuseTexture:
                    RuinedDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.PavementDiffuseTexture:
                    PavementDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.GravelDiffuseTexture:
                    GravelDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.CliffDiffuseTexture:
                    CliffDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.OilDiffuseTexture:
                    OilDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.OreDiffuseTexture:
                    OreDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.SandDiffuseTexture:
                    SandDiffuseTexture = new TerrainTexture(packageID, textureName);
                    break;
                case TerrainTexture.TextureName.CliffSandNormalTexture:
                    CliffSandNormalTexture = new TerrainTexture(packageID, textureName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadTexture(TerrainTexture.TextureName textureName) {
            switch (textureName) {
                case TerrainTexture.TextureName.GrassDiffuseTexture:
                    return GrassDiffuseTexture.Load();
                case TerrainTexture.TextureName.RuinedDiffuseTexture:
                    return RuinedDiffuseTexture.Load();
                case TerrainTexture.TextureName.PavementDiffuseTexture:
                    return PavementDiffuseTexture.Load();
                case TerrainTexture.TextureName.GravelDiffuseTexture:
                    return GravelDiffuseTexture.Load();
                case TerrainTexture.TextureName.CliffDiffuseTexture:
                    return CliffDiffuseTexture.Load();
                case TerrainTexture.TextureName.OilDiffuseTexture:
                    return OilDiffuseTexture.Load();
                case TerrainTexture.TextureName.OreDiffuseTexture:
                    return OreDiffuseTexture.Load();
                case TerrainTexture.TextureName.SandDiffuseTexture:
                    return SandDiffuseTexture.Load();
                case TerrainTexture.TextureName.CliffSandNormalTexture:
                    return CliffSandNormalTexture.Load();
                default: return false;
            }
        }

        private void SetColorOffset(string packageID, TerrainColorOffset.OffsetName offsetName) {
            switch (offsetName) {
                case TerrainColorOffset.OffsetName.GrassPollutionColorOffset:
                    GrassPollutionColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.OffsetName.GrassFieldColorOffset:
                    GrassFieldColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.OffsetName.GrassFertilityColorOffset:
                    GrassFertilityColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                case TerrainColorOffset.OffsetName.GrassForestColorOffset:
                    GrassForestColorOffset = new TerrainColorOffset(packageID, offsetName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadColorOffset(TerrainColorOffset.OffsetName offsetName) {
            switch (offsetName) {
                case TerrainColorOffset.OffsetName.GrassPollutionColorOffset:
                    return GrassPollutionColorOffset.Load();
                case TerrainColorOffset.OffsetName.GrassFieldColorOffset:
                    return GrassFieldColorOffset.Load();
                case TerrainColorOffset.OffsetName.GrassFertilityColorOffset:
                    return GrassFertilityColorOffset.Load();
                case TerrainColorOffset.OffsetName.GrassForestColorOffset:
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

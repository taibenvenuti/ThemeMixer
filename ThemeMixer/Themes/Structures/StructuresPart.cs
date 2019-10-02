using System;

namespace ThemeMixer.Themes.Structures
{
    [Serializable]
    public class StructuresPart : ILoadable, ISettable
    {
        public StructureTexture UpwardRoadDiffuse;
        public StructureTexture DownwardRoadDiffuse;
        public StructureTexture BuildingFloorDiffuse;
        public StructureTexture BuildingBaseDiffuse;
        public StructureTexture BuildingBaseNormal;
        public StructureTexture BuildingBurntDiffuse;
        public StructureTexture BuildingAbandonedDiffuse;
        public StructureTexture LightColorPalette;

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
            for (int i = 0; i < (int)StructureTexture.TextureName.Count; i++) {
                SetTexture(packageID, (StructureTexture.TextureName)i);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)StructureTexture.TextureName.Count; i++) {
                if (!LoadTexture((StructureTexture.TextureName)i)) success = false;
            }
            return success;
        }

        private void SetTexture(string packageID, StructureTexture.TextureName textureName) {
            switch (textureName) {
                case StructureTexture.TextureName.UpwardRoadDiffuse:
                    UpwardRoadDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.DownwardRoadDiffuse:
                    DownwardRoadDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.BuildingFloorDiffuse:
                    BuildingFloorDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.BuildingBaseDiffuse:
                    BuildingBaseDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.BuildingBaseNormal:
                    BuildingBaseNormal = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.BuildingBurntDiffuse:
                    BuildingBurntDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.BuildingAbandonedDiffuse:
                    BuildingAbandonedDiffuse = new StructureTexture(packageID, textureName);
                    break;
                case StructureTexture.TextureName.LightColorPalette:
                    LightColorPalette = new StructureTexture(packageID, textureName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadTexture(StructureTexture.TextureName textureName) {
            switch (textureName) {
                case StructureTexture.TextureName.UpwardRoadDiffuse:
                    return UpwardRoadDiffuse.Load();
                case StructureTexture.TextureName.DownwardRoadDiffuse:
                    return DownwardRoadDiffuse.Load();
                case StructureTexture.TextureName.BuildingFloorDiffuse:
                    return BuildingFloorDiffuse.Load();
                case StructureTexture.TextureName.BuildingBaseDiffuse:
                    return BuildingBaseDiffuse.Load();
                case StructureTexture.TextureName.BuildingBaseNormal:
                    return BuildingBaseNormal.Load();
                case StructureTexture.TextureName.BuildingBurntDiffuse:
                    return BuildingBurntDiffuse.Load();
                case StructureTexture.TextureName.BuildingAbandonedDiffuse:
                    return BuildingAbandonedDiffuse.Load();
                case StructureTexture.TextureName.LightColorPalette:
                    return LightColorPalette.Load();
                default: return false;
            }
        }
    }
}

using System;

namespace ThemeMixer.Themes.Water
{
    [Serializable]
    public class WaterPart : ILoadable, ISettable
    {
        public WaterTexture WaterFoam;
        public WaterTexture WaterNormal;

        public WaterColor WaterClean;
        public WaterColor WaterDirty;
        public WaterColor WaterUnder;

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
            for (int i = 0; i < (int)WaterTexture.TextureName.Count; i++) {
                SetTexture(packageID, (WaterTexture.TextureName)i);
            }
            for (int j = 0; j < (int)WaterColor.ColorName.Count; j++) {
                SetColor(packageID, (WaterColor.ColorName)j);
            }
        }

        private bool LoadAll() {
            bool success = true;
            for (int i = 0; i < (int)WaterTexture.TextureName.Count; i++) {
                if (!LoadTexture((WaterTexture.TextureName)i)) success = false;
            }
            for (int j = 0; j < (int)WaterColor.ColorName.Count; j++) {
                if (!LoadColor((WaterColor.ColorName)j)) success = false;
            }
            return success;
        }

        private void SetTexture(string packageID, WaterTexture.TextureName textureName) {
            switch (textureName) {
                case WaterTexture.TextureName.WaterFoam:
                    WaterFoam = new WaterTexture(packageID, textureName);
                    break;
                case WaterTexture.TextureName.WaterNormal:
                    WaterNormal = new WaterTexture(packageID, textureName);
                    break;
                default:
                    break;
            }
        }

        private bool LoadTexture(WaterTexture.TextureName textureName) {
            switch (textureName) {
                case WaterTexture.TextureName.WaterFoam:
                    return WaterFoam.Load();
                case WaterTexture.TextureName.WaterNormal:
                    return WaterNormal.Load();
                default: return false;
            }
        }

        private void SetColor(string packageID, WaterColor.ColorName name) {
            switch (name) {
                case WaterColor.ColorName.WaterClean:
                    WaterClean = new WaterColor(packageID, name);
                    break;
                case WaterColor.ColorName.WaterDirty:
                    WaterDirty = new WaterColor(packageID, name);
                    break;
                case WaterColor.ColorName.WaterUnder:
                    WaterUnder = new WaterColor(packageID, name);
                    break;
                default:
                    break;
            }
        }

        private bool LoadColor(WaterColor.ColorName name) {
            switch (name) {
                case WaterColor.ColorName.WaterClean:
                    return WaterClean.Load();
                case WaterColor.ColorName.WaterDirty:
                    return WaterDirty.Load();
                case WaterColor.ColorName.WaterUnder:
                    return WaterUnder.Load();
                default: return false;
            }
        }
    }
}

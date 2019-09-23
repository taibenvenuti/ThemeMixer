using System.Xml.Serialization;

namespace ThemeMixer.Themes.Terrain
{
    public class TerrainDetail : IThemePart
    {
        public string PackageID;
        public Name DetailName;
        public bool? CustomDetailValue;

        [XmlIgnore]
        public bool? DetailValue;

        public TerrainDetail() { }

        public TerrainDetail(string packageID, Name name) {
            PackageID = packageID;
            DetailName = name;
        }

        public bool Load(string packageID = null) {
            if (packageID != null) PackageID = packageID;
            if (CustomDetailValue == null && DetailValue == null && !SetFromTheme()) return false;
            LoadDetail();
            return true;
        }

        private bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeUtils.GetMapThemeMetaData(PackageID);
            if (metaData == null) return false;
            switch (DetailName) {
                case Name.GrassDetailEnabled:
                    SetDetail(metaData.grassDetailEnabled);
                    break;
                case Name.FertileDetailEnabled:
                    SetDetail(metaData.fertileDetailEnabled);
                    break;
                case Name.RocksDetailEnabled:
                    SetDetail(metaData.rocksDetailEnabled);
                    break;
                default:
                    break;
            }
            return true;
        }

        private void SetDetail(bool value) {
            DetailValue = CustomDetailValue = value;
        }

        private void LoadDetail() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (DetailName) {
                case Name.GrassDetailEnabled:
                    properties.m_useGrassDecorations = (bool)(CustomDetailValue ?? DetailValue);
                    break;
                case Name.FertileDetailEnabled:
                    properties.m_useFertileDecorations = (bool)(CustomDetailValue ?? DetailValue);
                    break;
                case Name.RocksDetailEnabled:
                    properties.m_useCliffDecorations = (bool)(CustomDetailValue ?? DetailValue);
                    break;
                default: break;
            }
        }

        public enum Name
        {
            GrassDetailEnabled,
            FertileDetailEnabled,
            RocksDetailEnabled,
            Count
        }
    }
}

namespace ThemeMixer.Themes.Terrain
{
    public class TerrainDetail : ThemePartBase
    {
        public Name DetailName;

        public TerrainDetail() { }

        public TerrainDetail(Name detailName) {
            DetailName = detailName;
        }

        public TerrainDetail(string themeID, Name name) : base(themeID) {
            DetailName = name;
        }

        protected override bool SetFromTheme() {
            MapThemeMetaData metaData = ThemeManager.GetTheme(ThemeID);
            if (metaData == null) return false;
            switch (DetailName) {
                case Name.GrassDetailEnabled:
                    SetValue(metaData.grassDetailEnabled);
                    break;
                case Name.FertileDetailEnabled:
                    SetValue(metaData.fertileDetailEnabled);
                    break;
                case Name.RocksDetailEnabled:
                    SetValue(metaData.rocksDetailEnabled);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected override void LoadValue() {
            global::TerrainProperties properties = TerrainManager.instance.m_properties;
            switch (DetailName) {
                case Name.GrassDetailEnabled:
                    properties.m_useGrassDecorations = (bool)(CustomValue ?? Value);
                    break;
                case Name.FertileDetailEnabled:
                    properties.m_useFertileDecorations = (bool)(CustomValue ?? Value);
                    break;
                case Name.RocksDetailEnabled:
                    properties.m_useCliffDecorations = (bool)(CustomValue ?? Value);
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

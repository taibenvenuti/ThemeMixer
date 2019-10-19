using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GrassForestPanel : OffsetPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            OffsetID = OffsetID.GrassForestColorOffset;
            base.Awake();
        }
    }
}

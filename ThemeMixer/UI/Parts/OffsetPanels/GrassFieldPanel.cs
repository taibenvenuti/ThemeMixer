using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GrassFieldPanel : OffsetPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            OffsetID = OffsetID.GrassFieldColorOffset;
            base.Awake();
        }
    }
}

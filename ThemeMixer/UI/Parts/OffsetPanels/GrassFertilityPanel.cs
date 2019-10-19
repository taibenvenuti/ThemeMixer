using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GrassFertilityPanel : OffsetPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            OffsetID = OffsetID.GrassFertilityColorOffset;
            base.Awake();
        }
    }
}

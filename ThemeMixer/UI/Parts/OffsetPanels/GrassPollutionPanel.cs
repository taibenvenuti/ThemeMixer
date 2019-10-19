using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GrassPollutionPanel : OffsetPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            OffsetID = OffsetID.GrassPollutionColorOffset;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class WaterDirtyPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            ColorID = ColorID.WaterDirty;
            base.Awake();
        }
    }
}

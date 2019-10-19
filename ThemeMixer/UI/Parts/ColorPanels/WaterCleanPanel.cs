using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class WaterCleanPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            ColorID = ColorID.WaterClean;
            base.Awake();
        }
    }
}

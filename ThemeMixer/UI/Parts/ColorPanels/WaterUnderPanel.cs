using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class WaterUnderPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            ColorID = ColorID.WaterUnder;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class EarlyNightZenithPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.EarlyNightZenithColor;
            base.Awake();
        }
    }
}

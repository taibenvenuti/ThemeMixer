using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class LateNightZenithPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.LateNightZenithColor;
            base.Awake();
        }
    }
}

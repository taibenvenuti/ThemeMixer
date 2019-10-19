using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class MoonOuterCoronaPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.MoonOuterCorona;
            base.Awake();
        }
    }
}

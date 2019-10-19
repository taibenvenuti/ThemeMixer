using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class MoonInnerCoronaPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.MoonInnerCorona;
            base.Awake();
        }
    }
}

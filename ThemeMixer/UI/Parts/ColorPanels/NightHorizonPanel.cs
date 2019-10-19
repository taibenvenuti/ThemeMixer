using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class NightHorizonPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.NightHorizonColor;
            base.Awake();
        }
    }
}

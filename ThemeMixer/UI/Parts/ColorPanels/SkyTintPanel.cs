using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Color
{
    public class SkyTintPanel : ColorPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ColorID = ColorID.SkyTint;
            base.Awake();
        }
    }
}

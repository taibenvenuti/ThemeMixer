using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectAtmospherePanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            base.Awake();
            button.isVisible = false;
            CenterToParent();
        }
    }
}

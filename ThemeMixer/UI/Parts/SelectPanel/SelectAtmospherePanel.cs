using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Atmosphere)]
    public class SelectAtmospherePanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = false;
        }
    }
}

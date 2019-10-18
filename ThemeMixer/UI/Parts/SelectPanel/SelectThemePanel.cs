using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Themes)]
    public class SelectThemePanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = false;
        }
    }
}

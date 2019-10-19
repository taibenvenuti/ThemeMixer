using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectThemePanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Themes;
            base.Awake();
            buttonPanel.isVisible = false;
        }
    }
}

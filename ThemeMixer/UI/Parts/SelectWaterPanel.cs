using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Water)]
    public class SelectWaterPanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = false;
        }
    }
}

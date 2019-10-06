using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Weather)]
    public class SelectWeatherPanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = true;
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectWeatherPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            base.Awake();
            button.isVisible = true;
            CenterToParent();
        }
    }
}

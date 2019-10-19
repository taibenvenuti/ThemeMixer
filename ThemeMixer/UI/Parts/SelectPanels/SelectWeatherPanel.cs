using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectWeatherPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            base.Awake();
            buttonPanel.isVisible = true;
        }

        public override void Start() {
            base.Start();
            CenterToParent();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MinTemperatureRainPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MinTemperatureRain;
            base.Awake();
        }
    }
}

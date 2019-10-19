using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MaxTemperatureRainPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MaxTemperatureRain;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MaxTemperatureDayPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MaxTemperatureDay;
            base.Awake();
        }
    }
}

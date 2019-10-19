using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MinTemperatureDayPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MinTemperatureDay;
            base.Awake();
        }
    }
}

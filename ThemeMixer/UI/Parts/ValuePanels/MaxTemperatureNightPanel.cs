using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MaxTemperatureNightPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MaxTemperatureNight;
            base.Awake();
        }
    }
}

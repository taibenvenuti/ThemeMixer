using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MinTemperatureNightPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MinTemperatureNight;
            base.Awake();
        }
    }
}

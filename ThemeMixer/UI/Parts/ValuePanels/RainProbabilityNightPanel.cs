using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class RainProbabilityNightPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.RainProbabilityNight;
            base.Awake();
        }
    }
}

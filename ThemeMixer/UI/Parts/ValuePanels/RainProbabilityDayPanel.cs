using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class RainProbabilityDayPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.RainProbabilityDay;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class FogProbabilityDayPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.FogProbabilityDay;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class FogProbabilityNightPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.FogProbabilityNight;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class NorthernLightsProbabilityPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.NorthernLightsProbability;
            base.Awake();
        }
    }
}

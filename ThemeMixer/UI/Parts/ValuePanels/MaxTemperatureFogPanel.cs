using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MaxTemperatureFogPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MaxTemperatureFog;
            base.Awake();
        }
    }
}

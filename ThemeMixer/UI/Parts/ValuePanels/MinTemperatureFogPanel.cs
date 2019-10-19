using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MinTemperatureFogPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Weather;
            ValueID = ValueID.MinTemperatureFog;
            base.Awake();
        }
    }
}

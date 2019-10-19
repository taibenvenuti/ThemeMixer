using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class StarsIntensityPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.StarsIntensity;
            base.Awake();
        }
    }
}

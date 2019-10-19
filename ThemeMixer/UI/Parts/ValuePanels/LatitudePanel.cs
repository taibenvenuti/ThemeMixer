using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class LatitudePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.Latitude;
            base.Awake();
        }
    }
}

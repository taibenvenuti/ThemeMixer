using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class LongitudePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.Longitude;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class ExposurePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.Exposure;
            base.Awake();
        }
    }
}

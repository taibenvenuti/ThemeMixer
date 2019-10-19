using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SunSizePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.SunSize;
            base.Awake();
        }
    }
}

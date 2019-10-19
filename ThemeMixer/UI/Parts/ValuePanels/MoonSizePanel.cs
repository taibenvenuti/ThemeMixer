using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MoonSizePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.MoonSize;
            base.Awake();
        }
    }
}

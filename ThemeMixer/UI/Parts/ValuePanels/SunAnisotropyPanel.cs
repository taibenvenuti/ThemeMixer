using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SunAnisotropyPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.SunAnisotropy;
            base.Awake();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MiePanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.Mie;
            base.Awake();
        }
    }
}

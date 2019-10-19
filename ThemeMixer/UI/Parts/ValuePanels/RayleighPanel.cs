using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class RayleighPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.Rayleigh;
            base.Awake();
        }
    }
}

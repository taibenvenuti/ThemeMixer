using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class OuterSpaceIntensityPanel : ValuePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            ValueID = ValueID.OuterSpaceIntensity;
            base.Awake();
        }
    }
}

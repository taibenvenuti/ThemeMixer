using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectAtmospherePanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            base.Awake();
            buttonPanel.isVisible = true;
        }
            
        public override void Start() {
            base.Start();
            CenterToParent();
        }
    }
}

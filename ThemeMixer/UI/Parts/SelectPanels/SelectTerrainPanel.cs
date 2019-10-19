using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectTerrainPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            base.Awake();
            buttonPanel.isVisible = true;
        }

        public override void Start() {
            base.Start();
            CenterToParent();
        }
    }
}

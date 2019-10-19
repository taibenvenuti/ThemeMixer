using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectTerrainPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            base.Awake();
            button.isVisible = true;
            CenterToParent();
        }
    }
}

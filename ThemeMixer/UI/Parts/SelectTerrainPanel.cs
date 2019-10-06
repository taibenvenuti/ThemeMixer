using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Terrain)]
    public class SelectTerrainPanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = true;
            CenterToParent();
        }
    }
}

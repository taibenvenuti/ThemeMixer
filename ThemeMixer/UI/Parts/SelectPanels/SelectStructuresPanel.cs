using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectStructuresPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            base.Awake();
            button.isVisible = true;
            CenterToParent();
        }
    }
}

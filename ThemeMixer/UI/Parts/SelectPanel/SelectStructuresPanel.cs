using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Structures)]
    public class SelectStructuresPanel : SelectPanel
    {
        public override void Start() {
            base.Start();
            button.isVisible = true;
            CenterToParent();
        }
    }
}

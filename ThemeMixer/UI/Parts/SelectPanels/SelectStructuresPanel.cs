using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectStructuresPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            base.Awake();
            buttonPanel.isVisible = true;
        }

        public override void Start() {
            base.Start();
            CenterToParent();
        }
    }
}

using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SelectWaterPanel : SelectPanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            base.Awake();
            buttonPanel.isVisible = true;
        }
        public override void Start() {
            base.Start();
            CenterToParent();
        }
    }
}

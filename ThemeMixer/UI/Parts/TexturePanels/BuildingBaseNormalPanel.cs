using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class BuildingBaseNormalPanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.BuildingBaseNormal;
            base.Awake();
        }
    }
}

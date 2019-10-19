using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class BuildingBurntDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.BuildingBurntDiffuse;
            base.Awake();
        }
    }
}

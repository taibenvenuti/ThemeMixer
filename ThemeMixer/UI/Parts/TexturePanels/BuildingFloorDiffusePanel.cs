using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class BuildingFloorDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.BuildingFloorDiffuse;
            base.Awake();
        }
    }
}

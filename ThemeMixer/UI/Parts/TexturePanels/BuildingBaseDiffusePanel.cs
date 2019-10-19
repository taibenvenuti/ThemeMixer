using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class BuildingBaseDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.BuildingBaseDiffuse;
            base.Awake();
        }
    }
}

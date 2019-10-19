using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class BuildingAbandonedDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.BuildingAbandonedDiffuse;
            base.Awake();
        }
    }
}

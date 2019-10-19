using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class PavementDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.PavementDiffuseTexture;
            base.Awake();
        }
    }
}

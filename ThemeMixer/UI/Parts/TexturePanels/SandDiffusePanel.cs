using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class SandDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.SandDiffuseTexture;
            base.Awake();
        }
    }
}

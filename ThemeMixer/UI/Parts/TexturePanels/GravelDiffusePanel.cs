using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GravelDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.GravelDiffuseTexture;
            base.Awake();
        }
    }
}

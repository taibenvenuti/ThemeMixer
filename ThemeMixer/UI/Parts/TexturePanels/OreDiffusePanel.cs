using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class OreDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.OreDiffuseTexture;
            base.Awake();
        }
    }
}

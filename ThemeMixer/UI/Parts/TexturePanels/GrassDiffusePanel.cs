using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class GrassDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.GrassDiffuseTexture;
            base.Awake();
        }
    }
}

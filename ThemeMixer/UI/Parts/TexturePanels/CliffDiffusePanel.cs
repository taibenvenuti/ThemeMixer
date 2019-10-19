using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class CliffDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.CliffDiffuseTexture;
            base.Awake();
        }
    }
}

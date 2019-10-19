using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class RuinedDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.RuinedDiffuseTexture;
            base.Awake();
        }
    }
}

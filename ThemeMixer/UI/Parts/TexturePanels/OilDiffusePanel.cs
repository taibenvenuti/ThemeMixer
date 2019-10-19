using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class OilDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Terrain;
            TextureID = TextureID.OilDiffuseTexture;
            base.Awake();
        }
    }
}

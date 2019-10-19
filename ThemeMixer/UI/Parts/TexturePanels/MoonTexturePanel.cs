using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class MoonTexturePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Atmosphere;
            TextureID = TextureID.MoonTexture;
            base.Awake();
        }
    }
}

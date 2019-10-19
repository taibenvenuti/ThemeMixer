using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class LightColorPalettePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.LightColorPalette;
            base.Awake();
        }
    }
}

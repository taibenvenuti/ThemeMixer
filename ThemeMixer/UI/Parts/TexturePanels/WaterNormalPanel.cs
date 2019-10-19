using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class WaterNormalPanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            TextureID = TextureID.WaterNormal;
            base.Awake();
        }
    }
}

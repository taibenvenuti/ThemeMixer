using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class WaterFoamPanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Water;
            TextureID = TextureID.WaterFoam;
            base.Awake();
        }
    }
}

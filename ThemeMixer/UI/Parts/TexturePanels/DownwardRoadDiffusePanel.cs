using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class DownwardRoadDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.DownwardRoadDiffuse;
            base.Awake();
        }
    }
}

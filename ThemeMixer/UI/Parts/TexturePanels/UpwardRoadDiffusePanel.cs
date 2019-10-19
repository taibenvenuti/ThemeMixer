using ThemeMixer.Themes.Enums;

namespace ThemeMixer.UI.Parts
{
    public class UpwardRoadDiffusePanel : TexturePanel
    {
        public override void Awake() {
            Category = ThemeCategory.Structures;
            TextureID = TextureID.UpwardRoadDiffuse;
            base.Awake();
        }
    }
}

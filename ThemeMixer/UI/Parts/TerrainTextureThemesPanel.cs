using ThemeMixer.Locale;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;

namespace ThemeMixer.UI.Parts
{
    [UICategory(ThemeCategory.Terrain)]
    public class TerrainTextureThemesPanel : FastListPanel
    {
        public override void Start() {
            base.Start();
            label.text = Translation.Instance.GetTranslation(TranslationID.LABEL_LOAD_TEXTURE);
        }
    }
}

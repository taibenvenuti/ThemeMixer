using ColossalFramework.UI;
using ThemeMixer.Themes;
using ThemeMixer.TranslationFramework;
using ThemeMixer.UI.Abstraction;
using ThemeMixer.UI.Parts;
using UnityEngine;

namespace ThemeMixer.UI
{
    public class ThemePanelWrapper : PanelBase
    {
        private UILabel label;
        private ThemesPanel themePanel;

        public void Setup(string title, ThemePart themePart) {
            color = UIColor;
            Setup(new Vector2(466.0f, 0.0f), 0, true, LayoutDirection.Vertical, LayoutStart.TopLeft, "GenericPanel");
            label = AddUIComponent<UILabel>();
            label.text = "";
            label.autoSize = false;
            label.size = new Vector2(width, 32.0f);
            label.font = UIUtils.BoldFont;
            label.textScale = 1.0f;
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.padding = new RectOffset(0, 0, 8, 0);
            label.text = Translation.Instance.GetTranslation(title);
            themePanel = AddUIComponent<ThemesPanel>();
            themePanel.Setup(new Vector2(466.0f, 0.0f), UIUtils.DEFAULT_SPACING, true, LayoutDirection.Vertical, LayoutStart.TopLeft, themePart: themePart);
        }
    }
}

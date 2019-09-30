using ColossalFramework.UI;
using System.Linq;
using ThemeMixer.TranslationFramework;

namespace ThemeMixer.UI
{
    public static class UIUtils
    {
        public const int DEFAULT_SPACING = 5;
        public static UIFont Font {
            get {
                if (_font == null) {
                    UIFont[] fonts = UnityEngine.Resources.FindObjectsOfTypeAll<UIFont>();
                    _font = fonts.FirstOrDefault(f => f.name == "OpenSans-Regular");
                }
                return _font;
            }
        }
        private static UIFont _font;

        public static UIFont BoldFont {
            get {
                if (_boldFont == null) {
                    UIFont[] fonts = UnityEngine.Resources.FindObjectsOfTypeAll<UIFont>();
                    _boldFont = fonts.FirstOrDefault(f => f.name == "OpenSans-Bold");
                }
                return _boldFont;
            }
        }
        private static UIFont _boldFont;

        public static void ShowExceptionPanel(string title, string message, bool error) {
            UIView.library.ShowModal<ExceptionPanel>("ExceptionPanel").SetMessage(
                 Translation.Instance.GetTranslation(title),
                 Translation.Instance.GetTranslation(message),
                 error);
        }
    }
}
